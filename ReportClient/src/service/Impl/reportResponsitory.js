import responsitory from "../responsitory/responsitory";

const resource = "/report";
export default {
  getReportDasboard(data) {
    return responsitory.post(`${resource}/getReportDasboard`, data);
  },
  getReportTongYeuCau(data) {
    return responsitory.post(`${resource}/getReportTongYeuCau`, data);
  },
  insertSLADetail1st(data) {
    return responsitory.post(`${resource}/insertSLADetail1st`, data);
  },
  insertSLADetail2nd(data) {
    return responsitory.post(`${resource}/insertSLADetail2nd`, data);
  },
  insertSLADetail3rd(data) {
    return responsitory.post(`${resource}/insertSLADetail3rd`, data);
  },
  getListReportSLA(data) {
    return responsitory.post(`${resource}/getListReportSLA`, data);
  },
  filterByGD_DG(data) {
    return responsitory.post(`${resource}/filterByGD_DG`, data);
  },
  getFilterByTKH(data) {
    return responsitory.post(`${resource}/getFilterByTKH`, data);
  },
  getFilterByID(data) {
    return responsitory.post(`${resource}/getFilterByID`, data);
  },
  getThongTinYeuCau(id_yeu_cau) {
    return responsitory.get(`${resource}/getThongTinYeuCau/${id_yeu_cau}`);
  },
  getWarningList(data) {
    return responsitory.post(`${resource}/getWarningList`, data);
  },
  ReportSLA(data) {
    return responsitory.post(`${resource}/ReportSLA`, data);
  },
  ReportSum(data) {
    return responsitory.post(`${resource}/ReportSum`, data);
  },
};
