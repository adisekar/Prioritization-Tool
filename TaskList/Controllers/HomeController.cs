using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskList.Models;
using TaskList.Business;
using PagedList;
using TaskList.Business.Objects;

namespace TaskList.Controllers
{
    [Authorize(Roles = "RegisteredUser")]
    public class HomeController : Controller
    {
        public ActionResult Index(string status = "Pending", int page = 1)
        {
            // Default for All Tasks
            int statusId = 0;
            if (status != "All")
            {
                statusId = TaskManager.GetStatusIdByName(status);
            }

            List<TaskLookup> tasks;
            if (statusId > 0)
            {
                tasks = TaskManager.GetTasksByStatusId(statusId);
            }
            else
            {
                tasks = TaskManager.GetTasks();
            }

            var taskList = new List<TaskViewModel>();

            int priority = 1;
            foreach (var item in tasks)
            {
                TaskViewModel taskVM = new TaskViewModel();
                taskVM.TaskId = item.TaskId;
                taskVM.TaskName = item.TaskName;
                taskVM.TaskDescription = item.TaskDescription;

                taskVM.DateCreated = item.DateCreated.ToShortDateString();
                taskVM.Score = item.Score;

                if (item.Status.StatusName == "Pending")
                {
                    taskVM.Priority = priority;
                    priority++;
                }

                taskVM.StatusName = item.Status.StatusName;

                taskList.Add(taskVM);
            }

            IPagedList pagedTaskList = taskList.ToPagedList(page, 6);

            return View(pagedTaskList);
        }
    }
}