using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskList.Models;
using TaskList.Business.Objects;
using TaskList.Business;

namespace TaskList.Models
{
    public class TaskItemLookupViewModel
    {
        public TaskLookup Task { get; set; }
        public List<CategoryLookupObject> Categories { get; set; }
        public List<CategoryQuestionsLookup> CategoryQuestions { get; set; }
        public List<QuestionAnswersLookup> QuestionAnswers { get; set; }
        public List<MultiplierLookupObject> MultiplierValues { get; set; }
        public int TotalScore { get; set; }
    }
}