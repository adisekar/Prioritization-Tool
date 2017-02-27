using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskList.Business.Objects;
using TaskList.Business;
using TaskList.Models;
using System.IO;

namespace TaskList.Controllers
{
    [Authorize(Roles = "RegisteredUser")]
    public class TaskController : Controller
    {
        // GET: Task
        public ActionResult Index()
        {
            return View();
        }

        // GET: Task/Details/5
        public ActionResult Details(int id)
        {
            var task = TaskManager.GetTaskById(id);

            TaskViewModel taskVM = new TaskViewModel();
            taskVM.TaskId = task.TaskId;
            taskVM.TaskName = task.TaskName;
            taskVM.TaskDescription = task.TaskDescription;
            taskVM.Score = task.Score;
            taskVM.CreatedBy = task.CreatedBy;
            taskVM.ModifiedBy = task.ModifiedBy;
            taskVM.DateCreated = task.DateCreated.ToString(); ;
            taskVM.StatusName = task.Status.StatusName;

            taskVM.Attachments = new List<FileObject>();
            foreach (var file in task.Attachments)
            {
                FileObject fileObject = new FileObject();
                fileObject.FileGuid = file.FileGuid;
                fileObject.FileName = file.FileName;
                fileObject.FilePath = file.FilePath;
                fileObject.ContentType = file.ContentType;
                fileObject.FileExtension = file.FileExtension;
                taskVM.Attachments.Add(fileObject);
            }

            return PartialView("~/Views/Task/Partial/TaskDetails.cshtml", taskVM);
        }

        // GET: Task/Create
        public ActionResult Create()
        {
            TaskViewModel taskVM = new TaskViewModel();
            taskVM.CreatedBy = User.Identity.Name;
            taskVM.ModifiedBy = User.Identity.Name;
            return View(taskVM);
        }

        // POST: Task/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "TaskName, TaskDescription")] TaskViewModel taskVM, IEnumerable<HttpPostedFileBase> files)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TaskLookup task = new TaskLookup();
                    task.TaskName = taskVM.TaskName;
                    task.TaskDescription = taskVM.TaskDescription;
                    task.CreatedBy = User.Identity.Name;
                    task.ModifiedBy = User.Identity.Name;

                    task.Attachments = new List<FileObject>();
                    AddFilesToTask(files, task);

                    TaskManager.AddTask(task);
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View("Error");
            }
        }

        private static FileObject CreateFileObject(Guid fileGuid, string fileName, string contentType, string fileExtension, string filePath)
        {
            FileObject fileObj = new FileObject();
            fileObj.FileGuid = fileGuid;
            fileObj.FileName = fileName;
            fileObj.ContentType = contentType;
            fileObj.FileExtension = fileExtension;
            fileObj.FilePath = filePath;
            return fileObj;
        }

        // GET: Task/Edit/5
        public ActionResult Edit(int id)
        {
            var task = TaskManager.GetTaskById(id);

            TaskViewModel taskVM = new TaskViewModel();
            taskVM.TaskId = task.TaskId;
            taskVM.TaskName = task.TaskName;
            taskVM.TaskDescription = task.TaskDescription;
            taskVM.Score = task.Score;
            taskVM.CreatedBy = task.CreatedBy;
            taskVM.ModifiedBy = task.ModifiedBy;
            taskVM.DateCreated = task.DateCreated.ToString(); ;
            taskVM.StatusName = task.Status.StatusName;

            taskVM.Attachments = new List<FileObject>();
            foreach (var file in task.Attachments)
            {
                FileObject fileObject = new FileObject();
                fileObject.FileGuid = file.FileGuid;
                fileObject.FileName = file.FileName;
                fileObject.FilePath = file.FilePath;
                fileObject.ContentType = file.ContentType;
                fileObject.FileExtension = file.FileExtension;
                taskVM.Attachments.Add(fileObject);
            }
            return PartialView("~/Views/Task/Partial/EditTask.cshtml", taskVM);
        }

        //POST: Task/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "TaskId, TaskName, TaskDescription")] TaskViewModel taskVM, IEnumerable<HttpPostedFileBase> files)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TaskLookup task = new TaskLookup();
                    task.TaskId = taskVM.TaskId;
                    task.TaskName = taskVM.TaskName;
                    task.TaskDescription = taskVM.TaskDescription;
                    task.ModifiedBy = User.Identity.Name;

                    task.Attachments = new List<FileObject>();
                    AddFilesToTask(files, task);

                    TaskManager.UpdateTask(task);
                }

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View("Error");
            }
        }

        private void AddFilesToTask(IEnumerable<HttpPostedFileBase> files, TaskLookup task)
        {
            foreach (var file in files)
            {
                if (file != null && file.ContentLength > 0)
                {
                    Guid fileGuid = Guid.NewGuid();
                    var fileName = Path.GetFileName(file.FileName);
                    var fileExtension = Path.GetExtension(file.FileName);
                    var contentType = file.ContentType;

                    var fileNameFormat = string.Format("{0}_{1}_{2}", fileGuid, task.TaskName, fileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Documents/"), fileNameFormat);
                    file.SaveAs(path);

                    FileObject fileObj = CreateFileObject(fileGuid, fileName, contentType, fileExtension, "~/Content/Documents/" + fileNameFormat);
                    task.Attachments.Add(fileObj);
                }
            }
        }

        [HttpPost]
        public ActionResult DeleteFileByGuid(string fileGuidStr)
        {
            try
            {
                var fileGuid = new Guid(fileGuidStr);
                bool result = TaskManager.DeleteFileByGuid(fileGuid);
                if (result)
                {
                    return Json(new { message = "Success" });
                }
                else
                {
                    return Json(new { message = "Error" });
                }
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: Task/Delete/5
        public ActionResult Delete(int id)
        {
            var task = TaskManager.GetTaskById(id);

            TaskViewModel taskVM = new TaskViewModel();
            taskVM.TaskId = task.TaskId;
            taskVM.TaskName = task.TaskName;
            taskVM.TaskDescription = task.TaskDescription;
            taskVM.Score = task.Score;
            taskVM.CreatedBy = task.CreatedBy;
            taskVM.ModifiedBy = task.ModifiedBy;
            taskVM.DateCreated = task.DateCreated.ToString(); ;
            taskVM.StatusName = task.Status.StatusName;

            taskVM.Attachments = new List<FileObject>();
            foreach (var file in task.Attachments)
            {
                FileObject fileObject = new FileObject();
                fileObject.FileGuid = file.FileGuid;
                fileObject.FileName = file.FileName;
                fileObject.FilePath = file.FilePath;
                fileObject.ContentType = file.ContentType;
                fileObject.FileExtension = file.FileExtension;
                taskVM.Attachments.Add(fileObject);
            }

            return PartialView("~/Views/Task/Partial/DeleteTask.cshtml", taskVM);
        }

        // POST: Task/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // Delete Documents, if present for that task
                var task = TaskManager.GetTaskById(id);
                if (task.Attachments.Count > 0)
                {
                    foreach (var attachment in task.Attachments)
                    {
                        string fullPath = Request.MapPath(attachment.FilePath);
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                    }
                }

                // TODO: Add delete logic here
                TaskManager.DeleteTask(id);

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
