using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.Business.Objects
{
    public class CategoryQuestionsLookup
    {
        public CategoryLookupObject Category { get; set; }
        public List<QuestionLookupObject> Questions { get; set; }
        public List<QuestionAnswersLookup> QuestionAnswers { get; set; }
    }
}
