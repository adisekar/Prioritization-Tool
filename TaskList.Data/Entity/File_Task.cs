using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.Data.Entity
{
    public class File_Task
    {
        [Key]
        public Guid FileGuid { get; set; }

        [StringLength(255), Required]
        public string FileName { get; set; }

        [StringLength(100)]
        public string ContentType { get; set; }

        [StringLength(10)]
        public string FileExtension { get; set; }

        [StringLength(255), Required]
        public string FilePath { get; set; }

        [Required]
        public int TaskId { get; set; }
        [ForeignKey("TaskId")]
        public virtual TaskSet Task { get; set; }
    }
}
