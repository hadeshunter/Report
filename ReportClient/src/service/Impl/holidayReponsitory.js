import responsitory from "../responsitory/responsitory";

const resource = "/holiday";
export default {
  getAllHoliday(year) {
    return responsitory.get(`${resource}/getAllHoliday?year=${year}`, year);
  },
  insertHoliday(year) {
    return responsitory.post(`${resource}/insertHoliday?year=${year}`, year);
  },
  insertByDate(date) {
    return responsitory.post(`${resource}/insertByDate?date=${date}`, date);
  },
  deleteDate(date) {
    return responsitory.delete(`${resource}/deleteDate?date=${date}`, date);
  }
};
