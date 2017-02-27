using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.Data.Entity
{
    public class Status
    {
        public int StatusId { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string StatusName { get; set; }
    }
}
