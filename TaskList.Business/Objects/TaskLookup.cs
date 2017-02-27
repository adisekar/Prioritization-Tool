using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.Business.Objects
{
    public class TaskLookup
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public int Score { get; set; }
        public string TaskDescription { get; set; }
        public DateTime DateCreated { get; set; }
        public StatusLookup Status { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public List<FileObject> Attachments { get; set; }
    }
}
