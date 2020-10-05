import responsitory from "../responsitory/responsitory";
//import axios from 'axios';
const resource = "/user";
export default {
  getAllUser() {
    return responsitory.get(`${resource}/getAllUser`);
  },
  getById(usid) {
    return responsitory.get(`${resource}/getById?usid=${usid}`);
  },
  createFirstUser(user) {
    return responsitory.post(`${resource}/createFirstUser`, user);
  },
  update(user) {
    return responsitory.put(`${resource}/update`, user);
  },
  delete(userid) {
    return responsitory.delete(`${resource}/delete?usid=${userid}`);
  },
  search(dataFilter) {
    return responsitory.post(`${resource}/search`, dataFilter);
  },
  changePass(data) {
    return responsitory.post(`${resource}/changePass`, data);
  },
  deleteListUser(data) {
    return responsitory.post(`${resource}/deleteListUser`, data);
  },
  getUserByUserName(username) {
    return responsitory.post(
      `${resource}/getUserByUserName?username=${username}`
    );
  },
  getUserCNTT(data) {
    return responsitory.get(`${resource}/getUserCNTT`, data);
  },
  testLogin() {
    return responsitory.get(`/values`);
  }
};
