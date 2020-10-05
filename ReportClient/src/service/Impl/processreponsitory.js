import responsitory from "../responsitory/responsitory";

const resource = "/Process";
export default {
  getAllProcess(data) {
    return responsitory.post(`${resource}/getAllProcess`,data);
  },
}