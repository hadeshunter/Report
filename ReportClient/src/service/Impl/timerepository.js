import responsitory from "../responsitory/responsitory";

const resource = "/timer";
export default {
   getAllTimer(data) {
      return responsitory.get(`${resource}/getAllTimer`, data);
    },
    getById(timerid) {
      return responsitory.get(`${resource}/getById?timerid=${timerid}`)
    },
    updateTimer(timerid) {
      return responsitory.post(`${resource}/updateTimer`, timerid);
    },
    
}