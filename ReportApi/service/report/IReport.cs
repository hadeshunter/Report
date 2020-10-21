using ModelClass.model.report.request;

namespace REPORT.service.report
{
    public interface IReport
    {
        dynamic execureQuery();
        dynamic execureStoreProcedure(int test);
        dynamic getReportDasboard(ReportRequest rq);
        dynamic getReportTongYeuCau(ReportRequest rq);
        dynamic insertSLADetail1st(ReportRequest rq); //gd 6 - KSYC
        dynamic insertSLADetail2nd(ReportRequest rq); // gd 7 - TKHD
        dynamic insertSLADetail3rd(ReportRequest rq); // gd 8 - DWST
        dynamic getListReportSLA(ReportRequest rq);
        dynamic filterByGD_DG(ReportRequest rq);
        dynamic getThongTinYeuCau(int v_ID_YEU_CAU);
        dynamic insertFilterByTKH1(ReportRequest rq); // Insert data by tkh and id stage = 6
        dynamic insertFilterByTKH2(ReportRequest rq); // Insert data by tkh and id stage = 7
        dynamic insertFilterByTKH3(ReportRequest rq); // Insert data by tkh and id stage = 8
        dynamic getFilterByTKH(ReportRequest rq);
        dynamic insertFilterByID1(ReportRequest rq); // Insert data by id_yeu_cau and id stage = 6
        dynamic insertFilterByID2(ReportRequest rq); // Insert data by id_yeu_cau and id stage = 7
        dynamic insertFilterByID3(ReportRequest rq); // Insert data by id_yeu_cau and id stage = 8
        dynamic getFilterByID(ReportRequest rq);
        dynamic getWarningList(ReportRequest rq);
        dynamic reportSLA (ReportRequest rq);
        dynamic reportSum(ReportRequest rq);
        dynamic TestSql();
    }
}
