using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.Business.Objects
{
    public class AnswerLookupObject
    {
        public int AnswerId { get; set; }
        public string AnswerName { get; set; }
        public WeightLookup Weight { get; set; }
        public QuestionLookupObject Question { get; set; }
    }
}
