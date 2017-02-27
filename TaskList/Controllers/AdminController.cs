using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskList.Business;
using TaskList.Business.Objects;
using TaskList.Models;
using PagedList;

namespace TaskList.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public ActionResult Index(int page = 1)
        {
            // 1 is the status for Pending tasks
            var tasks = TaskManager.GetTasksByStatusId(1);

            List<TaskViewModel> taskList = new List<TaskViewModel>();

            int priority = 1;
            foreach (var item in tasks)
            {
                TaskViewModel taskVM = new TaskViewModel();
                taskVM.TaskId = item.TaskId;
                taskVM.TaskName = item.TaskName;
                taskVM.TaskDescription = item.TaskDescription;

                taskVM.DateCreated = item.DateCreated.ToShortDateString();
                taskVM.Score = item.Score;
                taskVM.Priority = priority;
                taskVM.StatusName = item.Status.StatusName;

                taskList.Add(taskVM);
                priority++;
            }
            IPagedList pagedTaskList = taskList.ToPagedList(page, 4);
            return View(pagedTaskList);
        }

        #region Questions
        // Questions
        public ActionResult QuestionsIndex()
        {
            var questions = TaskManager.GetQuestions();
            List<QuestionViewModel> questionViewModelList = new List<QuestionViewModel>();

            foreach (var item in questions)
            {
                QuestionViewModel questionModel = new QuestionViewModel();
                questionModel.QuestionId = item.QuestionId;
                questionModel.QuestionName = item.QuestionName;
                questionModel.Category = item.Category;
                questionViewModelList.Add(questionModel);
            }
            return View(questionViewModelList);
        }

        // GET: Question/Create
        public ActionResult CreateQuestion()
        {
            QuestionViewModel questionModel = new QuestionViewModel();
            questionModel.Categories = TaskManager.GetCategories();
            return View(questionModel);
        }

        // POST: Question/Create
        [HttpPost]
        public ActionResult CreateQuestion(FormCollection collection)
        {
            try
            {
                string questionName = collection["QuestionName"];
                int categoryId = Int32.Parse(collection["DDlCategories"]);

                TaskManager.AddQuestion(questionName, categoryId);
                // TODO: Add insert logic here

                return RedirectToAction("QuestionsIndex");
            }
            catch
            {
                return View();
            }
        }

        #endregion

        #region Categories
        public ActionResult CategoriesIndex()
        {
            var categories = TaskManager.GetCategories();
            List<CategoryViewModel> categoryViewModelList = new List<CategoryViewModel>();

            foreach (var item in categories)
            {
                CategoryViewModel categoryModel = new CategoryViewModel();
                categoryModel.CategoryId = item.CategoryId;
                categoryModel.CategoryName = item.CategoryName;
                categoryViewModelList.Add(categoryModel);
            }
            return View(categoryViewModelList);
        }

        // GET: Category/Create
        public ActionResult CreateCategory()
        {
            CategoryViewModel categoryModel = new CategoryViewModel();
            return View(categoryModel);
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult CreateCategory(FormCollection collection)
        {
            try
            {
                string categoryName = collection["CategoryName"];
                TaskManager.AddCategory(categoryName);

                return RedirectToAction("CategoriesIndex");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region Answers
        // Answers
        public ActionResult AnswersIndex()
        {
            var answers = TaskManager.GetAnswers();
            List<AnswerViewModel> answerViewModelList = new List<AnswerViewModel>();

            foreach (var item in answers)
            {
                AnswerViewModel answerModel = new AnswerViewModel();
                answerModel.AnswerId = item.AnswerId;
                answerModel.AnswerName = item.AnswerName;
                answerModel.Question = item.Question;
                answerModel.Weight = item.Weight;

                answerViewModelList.Add(answerModel);
            }
            return View(answerViewModelList);
        }

        // GET: Answer/Create
        public ActionResult CreateAnswer()
        {
            AnswerViewModel answerModel = new AnswerViewModel();
            answerModel.Questions = TaskManager.GetQuestions();
            answerModel.WeightValues = TaskManager.GetWeights();

            answerModel.Question = answerModel.Questions.FirstOrDefault();
            answerModel.Weight = answerModel.WeightValues.FirstOrDefault();

            return View(answerModel);
        }

        // POST: Answer/Create
        [HttpPost]
        public ActionResult CreateAnswer(FormCollection collection)
        {
            try
            {
                string answerName = collection["AnswerName"];
                int questionId = Int32.Parse(collection["Question"]);
                int weightId = Int32.Parse(collection["Weight"]);

                TaskManager.AddAnswer(answerName, questionId, weightId);
                // TODO: Add insert logic here

                return RedirectToAction("AnswersIndex");
            }
            catch
            {
                return View();
            }
        }
        #endregion


        [HttpPost]
        public ActionResult Approve(int id)
        {
            bool success = TaskManager.UpdateTaskStatusById(id, 2);

            if (success)
            {
                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Admin");
                return Json(new { message = "Success", Url = redirectUrl });
            }
            return Json(new { message = "Error" });
        }

        [HttpPost]
        public ActionResult Reject(int id)
        {
            bool success = TaskManager.UpdateTaskStatusById(id, 3);

            if (success)
            {
                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Admin");
                return Json(new { message = "Success", Url = redirectUrl });
            }
            return Json(new { message = "Error" });
        }
        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
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

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
