using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.Business.Objects
{
    public class TaskCategories
    {
        public TaskLookup Task { get; set; }
        public List<CategoryQuestionAnswer> Categories { get; set; }
    }
}
