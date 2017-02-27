using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.Business.Objects
{
    public class QuestionAnswersLookup
    {
        public QuestionLookupObject Question { get; set; }
        public List<AnswerLookupObject> Answers { get; set; }
        public WeightLookup Weight { get; set; }
        public int Multiplier { get; set; }
        public int Score { get; set; }
    }
}
