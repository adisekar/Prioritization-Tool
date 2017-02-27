using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.Data.Entity
{
    public class TaskSet
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(250)]
        public string TaskName { get; set; }

        public int Score { get; set; }

        public string TaskDescription { get; set; }

        public DateTime DateCreated { get; set; }

        [ForeignKey("Status")]
        public int StatusId { get; set; }
        public Status Status { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string CreatedBy { get; set; }


        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string ModifiedBy { get; set; }

        public virtual List<Task_Category> Categories { get; set; }
        public virtual List<File_Task> Files { get; set; }
    }
}
