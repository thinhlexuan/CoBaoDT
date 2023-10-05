using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBClient.Common
{
    public class ComboBoxItem
    {
        public int ValueMember { get; set; }
        public string DisplayMember { get; set; }
    }
    public class ComboBoxItemString
    {
        public string ValueMember { get; set; }
        public string DisplayMember { get; set; }
    }
    public class ResultIntOK
    {
        public int? IsOK { get; set; }
        public string msg { get; set; }
    }
    public class ResultLongOK
    {
        public int? IsOK { get; set; }
        public string msg { get; set; }
    }   

}
