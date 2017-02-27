using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.Data.Entity
{
    public class Category_Question_Answer
    {
        [Key]
        public int Category_Question_AnswerId { get; set; }

        [Required]
        public int Task_CategoryId { get; set; }
        [ForeignKey("Task_CategoryId")]
        public virtual Task_Category Task_Category { get; set; }

        [Required]
        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual QuestionLookup Question { get; set; }

        [Required]
        public int AnswerId { get; set; }
        [ForeignKey("AnswerId")]
        public virtual AnswerLookup Answer { get; set; }

        [Required]
        public int MultiplierId { get; set; }
        [ForeignKey("MultiplierId")]
        public virtual MultiplierLookup Multiplier { get; set; }

        public int Score { get; set; }
    }
}
