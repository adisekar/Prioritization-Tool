using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.Data.Entity
{
    public class MultiplierLookup
    {
        [Key]
        public int MultiplierId { get; set; }
        public int MultiplierValue { get; set; }
    }
}
