using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskList.Models
{
    public class CategoryDetailJson
    {
        public int categoryId { get; set; }
        public QuestionAnswerDetailsJson[] questionAnswerDetails { get; set; }
    }
}