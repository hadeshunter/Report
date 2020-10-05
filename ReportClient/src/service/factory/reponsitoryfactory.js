import userReponsitory from "../Impl/userReponsitory";
import authReponsitory from "../Impl/authReponsitory";
import reportResponsitory from "../Impl/reportResponsitory";
import smsreponsitory from "../Impl/smsreponsitory";
import holidayReponsitory from "../Impl/holidayReponsitory";
import slatimereponsitory from "../Impl/slatimerepository";
import timerepository from "../Impl/timerepository";
import processreponsitory from "../Impl/processreponsitory";
import phonesendedreponsitory from "../Impl/phonesended";
import permissionResponsitory from "../Impl/permissionReponsitory";

const responsitories = {
  user: userReponsitory,
  auth: authReponsitory,
  report: reportResponsitory,
  sms: smsreponsitory,
  sla: slatimereponsitory,
  timer: timerepository,
  process: processreponsitory,
  phonesended: phonesendedreponsitory,
  holiday: holidayReponsitory,
  permission: permissionResponsitory
};
export default {
  get: name => responsitories[name]
};
