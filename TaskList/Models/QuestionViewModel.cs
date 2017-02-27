
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using TaskList.Business.Objects;

namespace TaskList.Models
{
    public class QuestionViewModel
    {
        [Key]
        public int QuestionId { get; set; }

        [Required]
        [Display(Name = "Question")]
        public string QuestionName { get; set; }
        public List<CategoryLookupObject> Categories { get; set; }
        public CategoryLookupObject Category { get; set; }
    }
}