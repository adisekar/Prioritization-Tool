using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Data.Entity;

namespace TaskList.Data
{
    public class ContextManager
    {
        #region Weight
        public static void AddWeight(int weightValue)
        {
            Weight weight = new Weight { WeightValue = weightValue };
            using (var db = new Context())
            {
                db.Weight.Add(weight);
                db.SaveChanges();
            }
        }

        public static void UpdateWeight(Weight weight)
        {
            using (var db = new Context())
            {
                db.Entry(weight).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void DeleteWeight(int weightId)
        {
            using (var db = new Context())
            {
                Weight weightRef = db.Weight.Where(w => w.WeightId == weightId).FirstOrDefault();
                if (weightRef != null)
                {
                    db.Weight.Remove(weightRef);
                    db.SaveChanges();
                }
            }
        }

        public static Weight GetWeightById(int weightId)
        {
            using (var db = new Context())
            {
                Weight weightRef = db.Weight.Where(w => w.WeightId == weightId).FirstOrDefault();
                return weightRef;
            }
        }

        public static int GetWeightIdByValue(int weightValue)
        {
            using (var db = new Context())
            {
                Weight weightRef = db.Weight.Where(w => w.WeightValue == weightValue).FirstOrDefault();
                return weightRef.WeightId;
            }
        }

        public static List<Weight> GetWeights()
        {
            List<Weight> weights = new List<Weight>();
            using (var db = new Context())
            {
                var query = db.Weight.OrderBy(w => w.WeightValue);
                foreach (var item in query)
                {
                    weights.Add(item);
                }
            }
            return weights;
        }
        #endregion

        #region Status
        public static void AddStatus(string statusName)
        {
            Status status = new Status { StatusName = statusName };
            using (var db = new Context())
            {
                db.Status.Add(status);
                db.SaveChanges();
            }
        }

        public static void UpdateStatus(Status status)
        {
            using (var db = new Context())
            {
                db.Entry(status).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void DeleteStatus(int statusId)
        {
            using (var db = new Context())
            {
                Status statusRef = db.Status.Where(s => s.StatusId == statusId).FirstOrDefault();
                if (statusRef != null)
                {
                    db.Status.Remove(statusRef);
                    db.SaveChanges();
                }
            }
        }

        public static Status GetStatusById(int statusId)
        {
            using (var db = new Context())
            {
                Status statusRef = db.Status.Where(s => s.StatusId == statusId).FirstOrDefault();
                return statusRef;
            }
        }

        public static List<Status> GetStatuses()
        {
            List<Status> statuses = new List<Status>();
            using (var db = new Context())
            {
                var query = db.Status.OrderBy(s => s.StatusName);
                foreach (var item in query)
                {
                    statuses.Add(item);
                }
            }
            return statuses;
        }

        #region Status

        public static int GetStatusIdByName(string statusName)
        {
            using (var db = new Context())
            {
                var query = db.Status.Where(s => s.StatusName == statusName);
                int statusId = query.FirstOrDefault().StatusId;
                if (statusId > 0 && statusId != null)
                {
                    return statusId;
                }
            }
            return -1;
        }
        #endregion

        #endregion

        #region Category
        public static void AddCategory(string categoryName)
        {
            CategoryLookup category = new CategoryLookup { CategoryName = categoryName };
            using (var db = new Context())
            {
                db.Categories.Add(category);
                db.SaveChanges();
            }
        }

        public static void UpdateCategory(CategoryLookup category)
        {
            using (var db = new Context())
            {
                CategoryLookup categoryRef = db.Categories.Find(category.CategoryId);
                if (categoryRef != null)
                {
                    categoryRef.CategoryName = category.CategoryName;
                    db.SaveChanges();
                }
            }
        }

        public static void DeleteCategory(CategoryLookup category)
        {
            using (var db = new Context())
            {
                CategoryLookup categoryRef = db.Categories.Find(category.CategoryId);
                if (categoryRef != null)
                {
                    db.Categories.Remove(categoryRef);
                    db.SaveChanges();
                }
            }
        }

        public static CategoryLookup GetCategoryById(int categoryId)
        {
            using (var db = new Context())
            {
                CategoryLookup category = db.Categories.Find(categoryId);
                return category;
            }
        }

        public static List<CategoryLookup> GetCategories()
        {
            List<CategoryLookup> categories = new List<CategoryLookup>();
            using (var db = new Context())
            {
                var query = db.Categories;
                foreach (var item in query)
                {
                    categories.Add(item);
                }
            }
            return categories;
        }
        #endregion

        #region Question
        public static void AddQuestion(string questionName, int categoryId)
        {
            QuestionLookup question = new QuestionLookup { QuestionName = questionName, CategoryId = categoryId };
            using (var db = new Context())
            {
                db.Questions.Add(question);
                db.SaveChanges();
            }
        }

        public static void UpdateQuestion(QuestionLookup question)
        {
            using (var db = new Context())
            {
                QuestionLookup questionRef = db.Questions.Find(question.QuestionId);
                if (questionRef != null)
                {
                    questionRef.QuestionName = question.QuestionName;
                    db.SaveChanges();
                }
            }
        }

        public static void DeleteQuestion(QuestionLookup question)
        {
            using (var db = new Context())
            {
                QuestionLookup questionRef = db.Questions.Find(question.QuestionId);
                if (questionRef != null)
                {
                    db.Questions.Remove(questionRef);
                    db.SaveChanges();
                }
            }
        }

        public static List<QuestionLookup> GetQuestions()
        {
            List<QuestionLookup> questions = new List<QuestionLookup>();
            using (var db = new Context())
            {
                var query = db.Questions.Include("Category");
                foreach (var item in query)
                {
                    questions.Add(item);
                }
            }
            return questions;
        }

        public static QuestionLookup GetQuestionById(int id)
        {
            QuestionLookup question = new QuestionLookup();
            using (var db = new Context())
            {
                question = db.Questions.Include(q => q.Category).SingleOrDefault(x => x.QuestionId == id);
            }
            return question;
        }

        public static List<QuestionLookup> GetQuestionsByCategoryId(int categoryId)
        {
            List<QuestionLookup> questions = new List<QuestionLookup>();
            using (var db = new Context())
            {
                var query = db.Questions.Where(q => q.Category.CategoryId == categoryId).Include("Category");
                foreach (var item in query)
                {
                    questions.Add(item);
                }
            }
            return questions;
        }
        #endregion

        #region Answers

        public static List<AnswerLookup> GetAnswers()
        {
            List<AnswerLookup> answers = new List<AnswerLookup>();
            using (var db = new Context())
            {
                var query = db.Answers.Include("Question").Include("Weight");
                foreach (var item in query)
                {
                    answers.Add(item);
                }
            }
            return answers;
        }

        public static List<AnswerLookup> GetAnswersByQuestionId(int questionId)
        {
            List<AnswerLookup> answers = new List<AnswerLookup>();
            using (var db = new Context())
            {
                var query = db.Answers.Where(a => a.QuestionId == questionId).Include("Weight").Include("Question");
                foreach (var item in query)
                {
                    answers.Add(item);
                }
            }
            return answers;
        }

        public static void AddAnswer(string answerName, int questionId, int weightId)
        {
            AnswerLookup answer = new AnswerLookup { AnswerName = answerName, QuestionId = questionId, WeightId = weightId };
            using (var db = new Context())
            {
                db.Answers.Add(answer);
                db.SaveChanges();
            }
        }

        #endregion

        #region Tasks

        public static void AddTask(TaskSet task)
        {
            using (var db = new Context())
            {
                db.Tasks.Add(task);

                // Add Multiple file paths to table
                foreach (var file in task.Files)
                {
                    db.Files.Add(file);
                }

                db.SaveChanges();
            }
        }

        public static TaskSet GetTaskById(int taskId)
        {
            using (var db = new Context())
            {
                TaskSet taskRef = db.Tasks.Where(t => t.TaskId == taskId).Include("Status").Include("Files").FirstOrDefault();
                return taskRef;
            }
        }

        public static bool UpdateTaskStatusById(int taskId, int statusId)
        {
            using (var db = new Context())
            {
                TaskSet taskRef = db.Tasks.Find(taskId);
                if (taskRef != null)
                {
                    //var status = ContextManager.GetStatusById(statusId);

                    taskRef.StatusId = statusId;
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public static void UpdateTask(TaskSet task)
        {
            using (var db = new Context())
            {
                TaskSet taskRef = db.Tasks.Find(task.TaskId);
                if (taskRef != null)
                {
                    taskRef.TaskName = task.TaskName;
                    taskRef.TaskDescription = task.TaskDescription;
                    taskRef.ModifiedBy = task.ModifiedBy;

                    // Add Multiple file paths to table
                    foreach (var file in task.Files)
                    {
                        file.TaskId = task.TaskId;
                        db.Files.Add(file);
                    }
                    db.SaveChanges();
                }
            }
        }

        public static void DeleteTask(TaskSet task)
        {
            using (var db = new Context())
            {
                TaskSet taskRef = db.Tasks.Find(task.TaskId);
                if (taskRef != null)
                {
                    List<Task_Category> taskCategoriesToDelete = db.Task_Categories.Where(t => t.TaskId == task.TaskId).ToList();

                    var taskCategoryIds = taskCategoriesToDelete.Where(t => t.TaskId == task.TaskId).Select(s => s.Task_CategoryId).ToList();
                    var categoryQuestionAnswersToDelete = db.Category_Questions.Where(q => taskCategoryIds.Contains(q.Task_CategoryId)).ToList();

                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            db.Category_Questions.RemoveRange(categoryQuestionAnswersToDelete);
                            db.SaveChanges();

                            db.Task_Categories.RemoveRange(taskCategoriesToDelete);
                            db.SaveChanges();

                            List<File_Task> files = db.Files.Where(f => f.TaskId == taskRef.TaskId).ToList();
                            db.Files.RemoveRange(files);

                            db.Entry(taskRef).State = EntityState.Deleted;
                            db.SaveChanges();

                            dbContextTransaction.Commit();
                        }
                        catch (Exception)
                        {
                            dbContextTransaction.Rollback();
                        }
                    }
                }
            }
        }

        public static List<TaskSet> GetTasks()
        {
            List<TaskSet> tasks = new List<TaskSet>();
            using (var db = new Context())
            {
                var query = db.Tasks.Include("Status");
                foreach (var item in query)
                {
                    tasks.Add(item);
                }
            }
            return tasks;
        }

        public static List<TaskSet> GetTasksByStatusId(int statusId)
        {
            List<TaskSet> tasks = new List<TaskSet>();
            using (var db = new Context())
            {
                var query = db.Tasks.Include("Status").Where(t => t.StatusId == statusId);
                foreach (var item in query)
                {
                    tasks.Add(item);
                }
            }
            return tasks;
        }

        #endregion

        #region Task Item Detail

        public static List<Task_Category> GetCategoriesByTaskId(int taskId)
        {
            List<Task_Category> taskCategories = new List<Task_Category>();
            using (var db = new Context())
            {
                var query = db.Task_Categories.Where(t => t.TaskId == taskId).Include("Category");
                foreach (var item in query)
                {
                    taskCategories.Add(item);
                }
            }
            return taskCategories;
        }

        public static List<Category_Question_Answer> GetQuestionsAnswersByTaskCategoryId(int categoryId)
        {
            List<Category_Question_Answer> categoryQuestionsAnswers = new List<Category_Question_Answer>();
            using (var db = new Context())
            {
                var query = db.Category_Questions.Include("Task_Category").Where(c => c.Task_CategoryId == categoryId).Include("Question").Include("Answer").Include("Answer.Weight").Include("Multiplier");
                foreach (var item in query)
                {
                    categoryQuestionsAnswers.Add(item);
                }
            }
            return categoryQuestionsAnswers;
        }

        public static int AddTaskCategory(int taskId, int categoryId)
        {
            Task_Category taskCategory = new Task_Category();
            taskCategory.TaskId = taskId;
            taskCategory.CategoryId = categoryId;

            using (var db = new Context())
            {
                if (db.Task_Categories.Any(t => t.TaskId == taskId && t.CategoryId == categoryId))
                {
                    string id = db.Task_Categories.Where(t => t.TaskId == taskId && t.CategoryId == categoryId).Select(tc => tc.Task_CategoryId).ToString();
                    return Int32.Parse(id);
                }
                var taskCategories = db.Task_Categories.Add(taskCategory);
                db.SaveChanges();
            }
            return taskCategory.Task_CategoryId;
        }

        public static void AddCategoryQuestionAnswer(int taskCategoryId, int questionId, int answerId, int multiplierId, int score)
        {
            Category_Question_Answer categoryQuestionAnswer = new Category_Question_Answer();
            categoryQuestionAnswer.Task_CategoryId = taskCategoryId;
            categoryQuestionAnswer.QuestionId = questionId;
            categoryQuestionAnswer.AnswerId = answerId;
            categoryQuestionAnswer.MultiplierId = multiplierId;
            categoryQuestionAnswer.Score = score;

            using (var db = new Context())
            {
                if (db.Category_Questions.Any(c => c.Task_CategoryId == taskCategoryId && c.QuestionId == questionId && c.AnswerId == answerId))
                {
                    return;
                }
                var taskCategories = db.Category_Questions.Add(categoryQuestionAnswer);
                db.SaveChanges();
            }
        }

        #endregion

        #region Multiplier
        public static List<MultiplierLookup> GetMultiplierValues()
        {
            List<MultiplierLookup> multiplierValues = new List<MultiplierLookup>();
            using (var db = new Context())
            {
                var query = db.Multiplier;
                foreach (var item in query)
                {
                    multiplierValues.Add(item);
                }
            }
            return multiplierValues;
        }

        #endregion

        #region Score

        public static void UpdateTotalScore(int taskId, int totalScore)
        {
            using (var db = new Context())
            {
                db.Tasks.Find(taskId).Score = totalScore;
                db.SaveChanges();
            }
        }
        #endregion

        public static bool DeleteFileByGuid(Guid fileGuid)
        {
            try
            {
                using (var db = new Context())
                {
                    var fileToDelete = db.Files.Single(f => f.FileGuid == fileGuid);
                    db.Files.Remove(fileToDelete);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                // Handle Exception
                return false;
            }
        }
    }
}
