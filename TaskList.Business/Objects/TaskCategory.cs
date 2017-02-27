using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.Business.Objects
{
    public class TaskCategory
    {
        public int TaskCategoryId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
