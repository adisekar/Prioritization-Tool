using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.Business.Objects
{
    public class QuestionAnswerDetail
    {
        public QuestionLookupObject Question { get; set; }
        public AnswerLookupObject Answer { get; set; }
        public MultiplierLookupObject Multiplier { get; set; }
        public int Score { get; set; }
    }
}
