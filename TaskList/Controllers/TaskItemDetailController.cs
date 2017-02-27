using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskList.Business;
using TaskList.Business.Objects;
using TaskList.Models;

namespace TaskList.Controllers
{
    [Authorize(Roles = "RegisteredUser")]
    public class TaskItemDetailController : Controller
    {
        // GET: TaskItemDetail/Details/5
        public ActionResult Details(int id)
        {
            TaskItemViewModel taskItemDetail = new TaskItemViewModel();

            taskItemDetail.Task = TaskManager.GetTaskById(id);

            var taskCategories = TaskManager.GetCategoriesByTaskId(id);

            List<CategoryDetail> categoryDetailList = new List<CategoryDetail>();

            foreach (var taskCategory in taskCategories)
            {
                CategoryLookupObject categoryLookup = new CategoryLookupObject { CategoryId = taskCategory.CategoryId, CategoryName = taskCategory.CategoryName };

                var taskCategoryId = taskCategory.TaskCategoryId;
                var questionsAnswers = TaskManager.GetQuestionsAnswersByTaskCategoryId(taskCategoryId);

                List<QuestionAnswerDetail> questionsAnswersList = new List<QuestionAnswerDetail>();

                foreach (var innerItem in questionsAnswers)
                {
                    QuestionAnswerDetail questionAnswerDetail = new QuestionAnswerDetail();

                    questionAnswerDetail.Question = innerItem.Question;
                    questionAnswerDetail.Answer = innerItem.Answer;
                    questionAnswerDetail.Multiplier = innerItem.Multiplier;
                    questionAnswerDetail.Score = innerItem.Score;

                    questionsAnswersList.Add(questionAnswerDetail);
                }

                CategoryDetail categoryDetail = new CategoryDetail { Category = categoryLookup, QuestionsAnswers = questionsAnswersList };
                categoryDetailList.Add(categoryDetail);
            }

            taskItemDetail.Categories = categoryDetailList;

            return View(taskItemDetail);
        }

        // GET: TaskItemDetail/Create
        public ActionResult Create(int id)
        {
            var task = TaskManager.GetTaskById(id);

            var categories = TaskManager.GetCategories();

            var multiplierValues = TaskManager.GetMultiplierValues();

            int totalScore = 0;

            List<CategoryQuestionsLookup> CategoryQuestionsList = new List<CategoryQuestionsLookup>();
            List<QuestionAnswersLookup> QuestionAnswersList = new List<QuestionAnswersLookup>();


            foreach (var category in categories)
            {
                List<QuestionLookupObject> questionsByCategory = TaskManager.GetQuestionsByCategoryId(category.CategoryId);

                CategoryQuestionsLookup CategoryQuestions = new CategoryQuestionsLookup();
                CategoryQuestions.Category = category;
                CategoryQuestions.Questions = questionsByCategory;

                CategoryQuestionsList.Add(CategoryQuestions);


                foreach (var question in questionsByCategory)
                {
                    var answers = TaskManager.GetAnswersByQuestionId(question.QuestionId);

                    QuestionAnswersLookup questionAnswers = new QuestionAnswersLookup();
                    questionAnswers.Question = question;
                    questionAnswers.Answers = answers;

                    if (answers.Count > 0 && multiplierValues.Count > 0)
                    {
                        // Initial Score = First Weight Value * First Multiplier Value
                        questionAnswers.Score = TaskManager.CalculateScore(answers[0].Weight.WeightValue, multiplierValues[0].MultiplierValue);
                    }
                    else
                    {
                        questionAnswers.Score = 0;
                    }

                    totalScore += questionAnswers.Score;
                    QuestionAnswersList.Add(questionAnswers);
                }

            }

            TaskItemLookupViewModel taskItem = new TaskItemLookupViewModel();
            taskItem.Task = task;
            taskItem.Categories = categories;
            taskItem.CategoryQuestions = CategoryQuestionsList;
            taskItem.QuestionAnswers = QuestionAnswersList;
            taskItem.MultiplierValues = multiplierValues;
            taskItem.TotalScore = totalScore;

            return View(taskItem);
        }

        // POST: TaskItemDetail/Create
        [HttpPost]
        public ActionResult Create(CreateTaskItemDetailJson taskItemDetail)
        {
            try
            {
                int taskId = taskItemDetail.taskId;
                int totalScore = taskItemDetail.totalScore;

                var categories = taskItemDetail.categories;

                TaskManager.UpdateTotalScore(taskId, totalScore);

                foreach (var category in categories)
                {
                    var categoryId = category.categoryId;
                    // Insert to TaskCategory Table. Get TaskCategoryId PK. Return 0, if record exists
                    int taskCategoryId = TaskManager.AddTaskCategory(taskId, categoryId);

                    var questionAnswerDetails = category.questionAnswerDetails;
                    if (questionAnswerDetails != null)
                    {
                        for (int i = 0; i < questionAnswerDetails.Length; i++)
                        {
                            var questionAnswerDetail = questionAnswerDetails[i];
                            if (questionAnswerDetail != null)
                            {
                                int questionId = questionAnswerDetail.questionId;
                                int answerId = questionAnswerDetail.answerId;
                                //int weightId = TaskManager.GetWeightIdByValue(questionAnswerDetail.weightValue);
                                int multiplierId = questionAnswerDetail.multiplierId;
                                int score = questionAnswerDetail.score;

                                // Insert to Category_Question_Answer Table
                                TaskManager.AddCategoryQuestionAnswer(taskCategoryId, questionId, answerId, multiplierId, score);
                            }
                        }
                    }
                }
                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Details", "TaskItemDetail", new { id = taskId, created = true });
                return Json(new { message = "Success", Url = redirectUrl });
            }
            catch
            {
                return View();
            }
        }

        // GET: TaskItemDetail/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TaskItemDetail/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
