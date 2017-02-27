using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.Business.Objects
{
    public class QuestionLookupObject
    {
        public int QuestionId { get; set; }
        public string QuestionName { get; set; }
        public CategoryLookupObject Category { get; set; }
    }
}
