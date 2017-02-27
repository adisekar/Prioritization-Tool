using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.Data.Entity
{
    public class Task_Category
    {
        [Key]
        public int Task_CategoryId { get; set; }

        [Required]
        public int TaskId { get; set; }
        [ForeignKey("TaskId")]
        public virtual TaskSet TaskSet { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual CategoryLookup Category { get; set; }

        public virtual List<Category_Question_Answer> Category_Question_Answer { get; set; }
    }
}
