import axios from "axios";
import $ from "jquery";
// const baseDomain = "http://10.70.31.18:2001";
const baseDomain = "http://localhost:56424";
const baseUrl = `${baseDomain}/api`;
//const baseUrl =  'https://jsonplaceholder.typicode.com'
//var token = getTokenByLocal();
const instance = axios.create({
  baseURL: baseUrl,
  headers: {
    "Content-Type": "application/x-www-form-urlencoded",
    "Cache-Control": "no-cache",
    "Postman-Token": "42e6c291-9a09-c29f-f28f-11872e2490a5"
  }
});
export default instance;
