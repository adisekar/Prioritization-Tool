using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.Business.Objects
{
    public class FileObject
    {
        public Guid FileGuid { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string FileExtension { get; set; }
        public string FilePath { get; set; }
    }

    // Not Used so bfar
    public enum FileType
    {
        Image,
        Document
    }
}
