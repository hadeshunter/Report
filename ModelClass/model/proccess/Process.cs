using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClass.model.proccess
{
    public class Process
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int processid { get; set; }
        public int id_yeu_cau { get; set; }
        public int status { get; set; }
        public DateTime createday { get; set; }
        public DateTime updateday { get; set; }
        public int sendth { get; set; }
        public Boolean active { get; set; }

    }
}
