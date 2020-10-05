import responsitory from "../responsitory/authreponsitory";
//import axios from 'axios';
const resource = "/auth";
export default {
  login(auth) {
    return responsitory.post(`${resource}/login`, auth);
  },
  getUserByUserName(username) {
    return responsitory.get(
      `${resource}/getUserByUserName?username=${username}`
    );
  },
  sso_login() {
    return responsitory.get(`/VNPTLogin/Login`);
  },
  sso_logout() {
    return responsitory.get(`/VNPTLogin/Logout`);
  }
};
