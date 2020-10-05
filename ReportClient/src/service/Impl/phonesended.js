import responsitory from "../responsitory/responsitory";

const resource = "/PhoneSended";
export default {
  getAllPhoneSended() {
     return responsitory.get(`${resource}/getAllPhoneSended`);
   },
    
}