using ModelClassLibrary.area.auth.roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClass.auth
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int usid { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public int role { get; set; }
        public Boolean active { get; set; }

    }
    public class RequestUser
    {
        public int v_ID_DON_VI { get; set; }
        public string name { get; set; }
    }
    public class OracleUsers
    {
        public int ID_NGUOI_DUNG { get; set; }
        public string VNPT_EMAIL { get; set; }
        public int ID_DON_VI { get; set; }
        public int ID_CHUC_VU { get; set; }
        public string DIEN_THOAI { get; set; }
        public int DAU_MOI { get; set; }
        public string HO_TEN { get; set; }
        public string ID_PHONG_BAN { get; set; }
        public string MA_DOI_TUONG { get; set; }
    }
}
