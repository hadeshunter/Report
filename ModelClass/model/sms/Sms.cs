using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClass.model.sms
{
    public class Sms
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int smsid { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public Boolean active { get; set; }
    }
}
