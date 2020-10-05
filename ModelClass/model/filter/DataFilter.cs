using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClass.model.filter
{
    public class DataFilter
    {
        public string filter { get; set; }
        public string oldpass { get; set; }
        public string newpass { get; set; }
        public string confirmpass { get; set; }
        public int usid { get; set; }
        public int username { get; set; }
    }
}
