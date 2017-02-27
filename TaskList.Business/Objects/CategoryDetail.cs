using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.Business.Objects
{
    public class CategoryDetail
    {
        public CategoryLookupObject Category { get; set; }
        public List<QuestionAnswerDetail> QuestionsAnswers { get; set; }
    }
}
