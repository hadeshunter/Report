import responsitory from "../responsitory/responsitory";

const resource = "/sms";
export default {
    getListSms(data) {
      return responsitory.get(`${resource}/getListSms`, data);
    },
    insertSms (sms) {
        return responsitory.post(`${resource}/insertSms`, sms)
      },
      updateSms (sms) {
      return responsitory.post(`${resource}/updateSms`, sms)
    },
    getById (smsid) {
      return responsitory.get(`${resource}/getById?smsid=${smsid}`)
    },
    deleteItemSms(smsid){
        return responsitory.delete(`${resource}/deleteItemSms?smsid=${smsid}`)
      },
      getAllSla(data) {
        return responsitory.get(`${resource}/getAllSla`, data);
      },
    
}