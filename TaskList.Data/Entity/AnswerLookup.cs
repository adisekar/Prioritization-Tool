using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace TaskList.Data.Entity
{
    public class AnswerLookup
    {
        [Key]
        public int AnswerId { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(500)]
        public string AnswerName { get; set; }

        [Required]
        public int WeightId { get; set; }
        [ForeignKey("WeightId")]
        public virtual Weight Weight { get; set; }

        [Required]
        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual QuestionLookup Question { get; set; }
    }
}
