using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class PostResultatExigenceModel
    {
        public ResultatCheckList ResultatCheckList { get; set; }
        public Stream StreamFile { get; set; }
        public byte[] ByteFile { get; set; }
        public string NameFile { get; set; }
    }
}
