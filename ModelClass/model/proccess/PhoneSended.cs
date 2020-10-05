using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClass.model.proccess
{
    public class PhoneSended
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int phoneid { get; set; }
        public string phone { get; set; }
        public int processid { get; set; }
        public DateTime createday { get; set; }
        public DateTime updateday { get; set; }
        public int status { get; set; }
        public int timer { get; set; }

        public string content { get; set; }
        public string result { get; set; }

    }
}
