using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TaskList.Business.Objects;

namespace TaskList.Models
{
    public class AnswerViewModel
    {
        [Key]
        public int AnswerId { get; set; }

        [Required]
        [Display(Name = "Answer")]
        public string AnswerName { get; set; }

        [Required]
        [Display(Name = "Question")]
        public QuestionLookupObject Question { get; set; }

        [Required]
        [Display(Name = "Weight")]
        public WeightLookup Weight { get; set; }

        public List<QuestionLookupObject> Questions { get; set; }
        public List<WeightLookup> WeightValues { get; set; }       
    }
}