using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClass.model.proccess
{
    public class SLAData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
     
        public int ID { get; set; }
        public int ID_NHOM_GIAI_DOAN { get; set; }
        public string THEO_VB { get; set; }
        public int ID_GIAI_DOAN { get; set; }
        public int ID_DO_UU_TIEN { get; set; }
        public int THOI_GIAN { get; set; }
        public string NGAY_TAO { get; set; }
        public int NGUOI_TAO { get; set; }
        public string NGAY_CAP_NHAT { get; set; }
        public int NGUOI_CAP_NHAT { get; set; }
        public int TTHD { get; set; }
        public string NGAY_KT { get; set; }
    }
}
