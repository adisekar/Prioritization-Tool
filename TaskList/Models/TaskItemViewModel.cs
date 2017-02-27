using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskList.Business.Objects;

namespace TaskList.Models
{
    public class TaskItemViewModel
    {
        public TaskLookup Task { get; set; }
        public List<CategoryDetail> Categories { get; set; }
    }
}