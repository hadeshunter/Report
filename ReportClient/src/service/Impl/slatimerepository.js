import responsitory from "../responsitory/responsitory";

const resource = "/sla";
export default {
  getAllSla(data) {
     return responsitory.get(`${resource}/getAllSla`, data);
   },
   GetById(id) {
    return responsitory.get(`${resource}/GetById?id=${id}`)
  },
   updateSla (sla) {
     return responsitory.post(`${resource}/updateSla`, sla)
   },
  createSLA(sla){
    return responsitory.post(`${resource}/createSLA`, sla)
  },
  deleteSlatime(id) {
   return responsitory.delete(`${resource}/deleteSla?id=${id}`)
 },
}