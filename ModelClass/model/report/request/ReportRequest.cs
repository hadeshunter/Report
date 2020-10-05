using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClass.model.report.request
{
    public class ReportRequest
    {
        public DateTime v_FROM_DATE { get; set; } //YYYY-MM-DD
        public DateTime v_TO_DATE { get; set; }   //YYYY-MM-DD

        public Int32 v_ID_NHOM_GIAI_DOAN { get; set; } //HDDT: 18
        public Int32 v_YEAR { get; set; }
        public Int32 v_MONTH { get; set; }
        public Int32 v_ID_GIAI_DOAN { get; set; }
        public Int32 v_DANH_GIA { get; set; } //1: dung han, 2: tre han, 3: chua bat dau, 4: chua ket thuc
        public Int32 v_ID_YEU_CAU { get; set; }
        public string v_TEN_KHACH_HANG { get; set; }
        public long v_FIX_DATE { get; set; }
        public Int32 v_TIME_WARNING { get; set; }
        public string v_TEN_NV { get; set; }
    }

    public class ReportResult
    {
        public int STT { get; set; }
        public int ID_YEU_CAU { get; set; }
        public string TEN_KHACH_HANG { get; set; }
        public string NGAY_TAO { get; set; }
        public string ID_DO_UU_TIEN { get; set; }
        public string TEN_DO_UU_TIEN { get; set; }
        public string NGUOI_TIEP_NHAN_KSYC { get; set; }
        public string THOI_GIAN_BAT_DAU_KSYC { get; set; }
        public string THOI_GIAN_KET_THUC_KSYC { get; set; }
        public string THOI_GIAN_SLA_KSYC_CHITIET { get; set; }
        public string NGUOI_TIEP_NHAN_TKHD { get; set; }
        public string THOI_GIAN_BAT_DAU_TKHD { get; set; }
        public string THOI_GIAN_KET_THUC_TKHD { get; set; }
        public string THOI_GIAN_SLA_TKHD_CHITIET { get; set; }
        public string NGUOI_TIEP_NHAN_DWST { get; set; }
        public string THOI_GIAN_BAT_DAU_DWST { get; set; }
        public string THOI_GIAN_KET_THUC_DWST { get; set; }
        public string THOI_GIAN_SLA_DWST_CHITIET { get; set; }
        public int SLA_KSYC { get; set; }
        public int SLA_TKHD { get; set; }
        public int SLA_DWST { get; set; }
        public int DANH_GIA_SLA_KSYC { get; set; }
        public int DANH_GIA_SLA_TKHD { get; set; }
        public int DANH_GIA_SLA_DWST { get; set; }
    }
    public class WarningList
    {
        public int STT { get; set; }
        public int ID_YEU_CAU { get; set; }
        public string TEN_KHACH_HANG { get; set; }
        public string NGAY_TAO { get; set; }
        public string ID_DO_UU_TIEN { get; set; }
        public string TEN_DO_UU_TIEN { get; set; }
        public string NGUOI_TIEP_NHAN { get; set; }
        public string THOI_GIAN_BAT_DAU { get; set; }
        public string THOI_GIAN_KET_THUC { get; set; }
        public string THOI_GIAN_THUC_HIEN { get; set; }
        public string SLA { get; set; }
        public string TONG_TG { get; set; }
        public string LAST_MESSAGE { get; set; }
        public int DONVI_BL_CUOI { get; set; }
    }
}
