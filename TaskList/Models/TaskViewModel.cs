using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TaskList.Business.Objects;

namespace TaskList.Models
{
    public class TaskViewModel
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        [Display(Name = "Task Name")]
        public string TaskName { get; set; }

        [Display(Name = "Task Description")]
        public string TaskDescription { get; set; }

        [Display(Name = "Date Created")]
        public string DateCreated { get; set; }

        public int Score { get; set; }

        public int Priority { get; set; }

        [Display(Name = "Status")]
        public string StatusName { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public List<FileObject> Attachments { get; set; }
    }
}