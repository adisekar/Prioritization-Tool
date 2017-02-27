using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Business.Objects;
using TaskList.Data;
using TaskList.Data.Entity;

namespace TaskList.Business
{
    public class TaskManager
    {
        #region Questions
        public static List<QuestionLookupObject> GetQuestions()
        {
            List<QuestionLookupObject> questionsList = new List<QuestionLookupObject>();
            var questions = ContextManager.GetQuestions();

            foreach (var question in questions)
            {
                QuestionLookupObject questionObject = new QuestionLookupObject();
                questionObject.QuestionId = question.QuestionId;
                questionObject.QuestionName = question.QuestionName;
                questionObject.Category = new CategoryLookupObject { CategoryId = question.CategoryId, CategoryName = question.Category.CategoryName };

                questionsList.Add(questionObject);
            }
            return questionsList;
        }

        public static void AddQuestion(string questionName, int categoryId)
        {
            ContextManager.AddQuestion(questionName, categoryId);
        }
        #endregion

        #region Category
        public static List<CategoryLookupObject> GetCategories()
        {
            var categories = ContextManager.GetCategories();

            List<CategoryLookupObject> categoryLookupList = new List<CategoryLookupObject>();

            foreach (var category in categories)
            {
                CategoryLookupObject categoryLookup = new CategoryLookupObject();
                categoryLookup.CategoryId = category.CategoryId;
                categoryLookup.CategoryName = category.CategoryName;

                categoryLookupList.Add(categoryLookup);
            }
            return categoryLookupList;
        }

        public static void AddCategory(string categoryName)
        {
            ContextManager.AddCategory(categoryName);
        }

        public static CategoryLookup GetCategoryById(int categoryId)
        {
            var category = ContextManager.GetCategoryById(categoryId);

            CategoryLookup categoryLookup = new CategoryLookup { CategoryId = category.CategoryId, CategoryName = category.CategoryName };
            return categoryLookup;
        }
        #endregion

        #region Answers
        public static List<AnswerLookupObject> GetAnswers()
        {
            List<AnswerLookupObject> answersList = new List<AnswerLookupObject>();
            var answers = ContextManager.GetAnswers();

            foreach (var answer in answers)
            {
                AnswerLookupObject answerObject = new AnswerLookupObject();
                answerObject.AnswerId = answer.AnswerId;
                answerObject.AnswerName = answer.AnswerName;
                answerObject.Weight = new WeightLookup { WeightId = answer.Weight.WeightId, WeightValue = answer.Weight.WeightValue };
                answerObject.Question = new QuestionLookupObject { QuestionId = answer.Question.QuestionId, QuestionName = answer.Question.QuestionName };

                answersList.Add(answerObject);
            }
            return answersList;
        }

        public static void AddAnswer(string answerName, int questionId, int weightId)
        {
            ContextManager.AddAnswer(answerName, questionId, weightId);
        }
        #endregion

        #region Weights
        public static List<WeightLookup> GetWeights()
        {
            List<WeightLookup> weightsList = new List<WeightLookup>();
            var weights = ContextManager.GetWeights();

            foreach (var weight in weights)
            {
                WeightLookup weightObject = new WeightLookup();
                weightObject.WeightId = weight.WeightId;
                weightObject.WeightValue = weight.WeightValue;

                weightsList.Add(weightObject);
            }
            return weightsList;
        }

        public static int GetWeightIdByValue(int weightValue)
        {
            int weightId = ContextManager.GetWeightIdByValue(weightValue);
            return weightId;
        }

        #endregion

        #region Tasks
        public static TaskLookup GetTaskById(int id)
        {
            var task = ContextManager.GetTaskById(id);

            TaskLookup taskLookup = new TaskLookup();

            taskLookup.TaskId = task.TaskId;
            taskLookup.TaskName = task.TaskName;
            taskLookup.TaskDescription = task.TaskDescription;
            taskLookup.Score = task.Score;
            taskLookup.DateCreated = task.DateCreated;
            taskLookup.CreatedBy = task.CreatedBy;
            taskLookup.ModifiedBy = task.ModifiedBy;

            taskLookup.Status = new StatusLookup { StatusId = task.StatusId, StatusName = task.Status.StatusName };

            taskLookup.Attachments = new List<FileObject>();
            foreach (var file in task.Files)
            {
                FileObject fileObject = new FileObject();
                fileObject.FileGuid = file.FileGuid;
                fileObject.FileName = file.FileName;
                fileObject.ContentType = file.ContentType;
                fileObject.FileExtension = file.FileExtension;
                fileObject.FilePath = file.FilePath;
                taskLookup.Attachments.Add(fileObject);
            }

            return taskLookup;
        }

        public static List<TaskLookup> GetTasks()
        {
            var taskList = ContextManager.GetTasks();

            List<TaskLookup> taskLookupList = new List<TaskLookup>();

            foreach (var task in taskList)
            {
                TaskLookup taskLookup = new TaskLookup();
                taskLookup.TaskId = task.TaskId;
                taskLookup.TaskName = task.TaskName;
                taskLookup.TaskDescription = task.TaskDescription;
                taskLookup.Score = task.Score;
                taskLookup.Status = new StatusLookup { StatusId = task.StatusId, StatusName = task.Status.StatusName };
                taskLookup.DateCreated = task.DateCreated;

                taskLookupList.Add(taskLookup);

            }

            taskLookupList = taskLookupList.OrderByDescending(s => s.Score).ToList();

            return taskLookupList;
        }

        public static List<TaskLookup> GetTasksByStatusId(int statusId)
        {
            var tasks = ContextManager.GetTasksByStatusId(statusId);

            List<TaskLookup> taskLookupList = new List<TaskLookup>();

            foreach (var task in tasks)
            {
                TaskLookup taskLookup = new TaskLookup();
                taskLookup.TaskId = task.TaskId;
                taskLookup.TaskName = task.TaskName;
                taskLookup.TaskDescription = task.TaskDescription;
                taskLookup.Score = task.Score;
                taskLookup.Status = new StatusLookup { StatusId = task.StatusId, StatusName = task.Status.StatusName };
                taskLookup.DateCreated = task.DateCreated;

                taskLookupList.Add(taskLookup);

            }

            taskLookupList = taskLookupList.OrderByDescending(s => s.Score).ToList();

            return taskLookupList;
        }

        public static void AddTask(TaskLookup task)
        {
            TaskSet taskSet = new TaskSet();
            taskSet.TaskName = task.TaskName;
            taskSet.TaskDescription = task.TaskDescription;
            taskSet.CreatedBy = task.CreatedBy;
            taskSet.ModifiedBy = task.ModifiedBy;
            taskSet.DateCreated = DateTime.Now;
            // Set Status Id of 1 for Pending
            taskSet.StatusId = 1;

            taskSet.Files = new List<File_Task>();
            foreach (var file in task.Attachments)
            {
                File_Task fileEntity = new File_Task();
                fileEntity.FileGuid = file.FileGuid;
                fileEntity.FileName = file.FileName;
                fileEntity.ContentType = file.ContentType;
                fileEntity.FileExtension = file.FileExtension;
                fileEntity.FilePath = file.FilePath;
                taskSet.Files.Add(fileEntity);
            }

            ContextManager.AddTask(taskSet);
        }

        public static void UpdateTask(TaskLookup task)
        {
            TaskSet taskSet = new TaskSet();
            taskSet.TaskId = task.TaskId;
            taskSet.TaskName = task.TaskName;
            taskSet.TaskDescription = task.TaskDescription;
            taskSet.ModifiedBy = task.ModifiedBy;

            taskSet.Files = new List<File_Task>();
            foreach (var file in task.Attachments)
            {
                File_Task fileEntity = new File_Task();
                fileEntity.FileGuid = file.FileGuid;
                fileEntity.FileName = file.FileName;
                fileEntity.ContentType = file.ContentType;
                fileEntity.FileExtension = file.FileExtension;
                fileEntity.FilePath = file.FilePath;
                taskSet.Files.Add(fileEntity);
            }
            ContextManager.UpdateTask(taskSet);
        }

        public static bool UpdateTaskStatusById(int taskId, int statusId)
        {
            return ContextManager.UpdateTaskStatusById(taskId, statusId);
        }

        public static void DeleteTask(int taskId)
        {
            TaskSet task = ContextManager.GetTaskById(taskId);

            ContextManager.DeleteTask(task);
        }

        #endregion

        #region Status

        public static int GetStatusIdByName(string statusName)
        {
            return ContextManager.GetStatusIdByName(statusName);
        }
        #endregion


        public static List<MultiplierLookupObject> GetMultiplierValues()
        {
            var multiplierValues = ContextManager.GetMultiplierValues();

            List<MultiplierLookupObject> multiplierLookupList = new List<MultiplierLookupObject>();

            foreach (var multiplierValue in multiplierValues)
            {
                MultiplierLookupObject multiplierLookup = new MultiplierLookupObject();

                multiplierLookup.MultiplierId = multiplierValue.MultiplierId;
                multiplierLookup.MultiplierValue = multiplierValue.MultiplierValue;

                multiplierLookupList.Add(multiplierLookup);
            }
            return multiplierLookupList;
        }

        public static List<QuestionLookupObject> GetQuestionsByCategoryId(int categoryId)
        {
            var questions = ContextManager.GetQuestionsByCategoryId(categoryId);

            List<QuestionLookupObject> questionLookupList = new List<QuestionLookupObject>();

            foreach (var question in questions)
            {
                QuestionLookupObject questionLookup = new QuestionLookupObject();

                questionLookup.QuestionId = question.QuestionId;
                questionLookup.QuestionName = question.QuestionName;
                questionLookup.Category = new CategoryLookupObject { CategoryId = question.CategoryId, CategoryName = question.Category.CategoryName };

                questionLookupList.Add(questionLookup);
            }
            return questionLookupList;
        }

        public static List<AnswerLookupObject> GetAnswersByQuestionId(int questionId)
        {
            var answers = ContextManager.GetAnswersByQuestionId(questionId);

            List<AnswerLookupObject> answerLookupList = new List<AnswerLookupObject>();

            foreach (var answer in answers)
            {
                AnswerLookupObject answerLookup = new AnswerLookupObject();

                answerLookup.AnswerId = answer.AnswerId;
                answerLookup.AnswerName = answer.AnswerName;
                answerLookup.Question = new QuestionLookupObject { QuestionId = answer.Question.QuestionId, QuestionName = answer.Question.QuestionName };
                answerLookup.Weight = new WeightLookup { WeightId = answer.WeightId, WeightValue = answer.Weight.WeightValue };

                answerLookupList.Add(answerLookup);
            }
            return answerLookupList;
        }

        #region Task Item Detail
        public static List<TaskCategory> GetCategoriesByTaskId(int taskId)
        {
            var taskcategories = ContextManager.GetCategoriesByTaskId(taskId);

            List<TaskCategory> taskCategoriesList = new List<TaskCategory>();

            foreach (var category in taskcategories)
            {
                TaskCategory taskCategory = new TaskCategory();
                taskCategory.TaskCategoryId = category.Task_CategoryId;
                taskCategory.CategoryId = category.CategoryId;
                taskCategory.CategoryName = category.Category.CategoryName;

                taskCategoriesList.Add(taskCategory);
            }
            return taskCategoriesList;
        }

        public static List<CategoryQuestionAnswer> GetQuestionsAnswersByTaskCategoryId(int taskCategoryId)
        {
            List<CategoryQuestionAnswer> categoryQuestionsAnswerList = new List<CategoryQuestionAnswer>();
            var questionAnswers = ContextManager.GetQuestionsAnswersByTaskCategoryId(taskCategoryId);

            foreach (var questionAnswer in questionAnswers)
            {
                CategoryQuestionAnswer categoryQuestionAnswer = new CategoryQuestionAnswer();
                categoryQuestionAnswer.Category_Question_AnswerId = questionAnswer.Category_Question_AnswerId;
                categoryQuestionAnswer.Task_CategoryId = questionAnswer.Task_CategoryId;
                categoryQuestionAnswer.Question = new QuestionLookupObject { QuestionId = questionAnswer.QuestionId, QuestionName = questionAnswer.Question.QuestionName };
                categoryQuestionAnswer.Answer = new AnswerLookupObject { AnswerId = questionAnswer.AnswerId, AnswerName = questionAnswer.Answer.AnswerName, Weight = new WeightLookup { WeightId = questionAnswer.Answer.Weight.WeightId, WeightValue = questionAnswer.Answer.Weight.WeightValue } };
                categoryQuestionAnswer.Multiplier = new MultiplierLookupObject { MultiplierId = questionAnswer.MultiplierId, MultiplierValue = questionAnswer.Multiplier.MultiplierValue };
                categoryQuestionAnswer.Score = questionAnswer.Score;

                categoryQuestionsAnswerList.Add(categoryQuestionAnswer);
            }
            return categoryQuestionsAnswerList;
        }

        public static int AddTaskCategory(int taskId, int categoryId)
        {
            return ContextManager.AddTaskCategory(taskId, categoryId);
        }

        public static void AddCategoryQuestionAnswer(int taskCategoryId, int questionId, int answerId, int multiplierId, int score)
        {
            ContextManager.AddCategoryQuestionAnswer(taskCategoryId, questionId, answerId, multiplierId, score);
        }

        #endregion

        #region Score
        public static int CalculateScore(int weightValue, int multiplierValue)
        {
            return weightValue * multiplierValue;
        }

        public static void UpdateTotalScore(int taskId, int totalScore)
        {
            ContextManager.UpdateTotalScore(taskId, totalScore);
        }
        #endregion


        public static bool DeleteFileByGuid(Guid fileGuid)
        {
            return ContextManager.DeleteFileByGuid(fileGuid);
        }
    }
}
