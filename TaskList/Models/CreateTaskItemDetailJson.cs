using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskList.Models
{
    public class CreateTaskItemDetailJson
    {
        public int taskId { get; set; }
        public int totalScore { get; set; }
        public CategoryDetailJson[] categories { get; set; }
    }
}