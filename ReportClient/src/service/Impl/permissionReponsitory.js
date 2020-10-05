import responsitory from "../responsitory/responsitory";

const resource = "/permission";
export default {
  getAllGroup() {
    return responsitory.get(`${resource}/getAllGroup`);
  },
  addGroup(data) {
    return responsitory.post(`${resource}/insertGroup`, data);
  },
  editGroup(data) {
    return responsitory.post(`${resource}/updateGroup`, data);
  },
  deleteGroup(grid) {
    return responsitory.delete(`${resource}/deleteGroup?grid=${grid}`);
  },
  getUserInGroup(groupid) {
    return responsitory.get(`${resource}/getUserInGroup?groupid=${groupid}`);
  },
  getUserNotInGroup(groupid) {
    return responsitory.get(`${resource}/getUserNotInGroup?groupid=${groupid}`);
  },
  getPerMissionByGroupId(groupid) {
    return responsitory.get(
      `${resource}/getPerMissionByGroupId?groupid=${groupid}`
    );
  },
  getPerMissionNotInGroup(groupid) {
    return responsitory.get(
      `${resource}/getPerMissionNotInGroup?groupid=${groupid}`
    );
  },
  getAllPermissionByUser(usid, lang) {
    return responsitory.get(
      `${resource}/getAllPermissionByUser?usid=${usid}&&lang=${lang}`
    );
  },
  insertGroupPermission(data) {
    return responsitory.post(`${resource}/insertGroupPermission`, data);
  },
  removeGroupPermission(data) {
    return responsitory.post(`${resource}/removeGroupPermission`, data);
  },
  addUserToGroup(data) {
    return responsitory.post(`${resource}/addUserToGroup`, data);
  },
  removeUserToGroup(data) {
    return responsitory.post(`${resource}/removeUserToGroup`, data);
  }
};
