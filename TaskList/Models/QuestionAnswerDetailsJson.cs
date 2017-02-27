using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskList.Models
{
    public class QuestionAnswerDetailsJson
    {
        public int questionId { get; set; }
        public int answerId { get; set; }
        public int multiplierId { get; set; }
        public int weightValue { get; set; }
        public int score { get; set; }
    }
}