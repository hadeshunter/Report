import axios from "axios";
import { json2xml } from "./json2xml";
import { xml2json } from "./xml2json";
import { parseXml } from "./parseXml";

// export var homeUrl = "http://localhost:8080";
export var homeUrl = "http://10.70.51.11:2002";
//export var authUrl = "http://localhost:8080/api/auth/login";
export var cashostURL = "https://id.vnpt.com.vn/cas/";
export var cashostURLLogout = "https://id.vnpt.com.vn/cas/logout?service=";
// export var exam =
//   "https://api.nytimes.com/svc/mostpopular/v2/emailed/7.json?api-key=8bqJ6WsPsOKyE5qgAEwpbHPwcqEoZlac";

// Single Sign On Login
export function ssoLogin() {
  var serviceUrl = encodeURIComponent(homeUrl);
  var sso_login = cashostURL.concat("login?service=", serviceUrl);
  return sso_login;
}

// Single Sign On Logout
export function ssoLogout() {
  var sso_logout = cashostURLLogout.concat(homeUrl);
  return sso_logout;
}

// Validation
export function ssoValidate(url) {
  var ticket = getTicket(url);
  var serviceUrl = encodeURIComponent(homeUrl);
  var validateURL = cashostURL.concat(
    `serviceValidate?ticket=${ticket}&service=${serviceUrl}`
  );
  var sso = createCORSRequest(validateURL);
  return sso;
}

// Get ticket after redirection from sso login
export function getTicket(url) {
  var ticket;
  var queryString = url.split("?");
  var getVars = {};
  if (queryString.length == 2) {
    queryString.forEach(str => {
      if (str.includes("ticket")) {
        ticket = queryString[queryString.indexOf(str)];
      }
    });
    queryString = ticket.split("&");
    if (queryString.length == 2) {
      queryString.forEach(str => {
        if (str.includes("ticket")) {
          ticket = queryString[queryString.indexOf(str)];
        }
      });
      queryString = ticket.split("#");
      let tmp = "";
      queryString.forEach(function(v) {
        tmp = v.split("=");
        if (tmp.length == 2) getVars[tmp[0]] = tmp[1];
      });
    } else {
      queryString.forEach(str => {
        if (str.includes("ticket")) {
          ticket = queryString[queryString.indexOf(str)];
        }
      });
      let vars = ticket.split("#");
      let tmp = "";
      vars.forEach(function(v) {
        tmp = v.split("=");
        if (tmp.length == 2) getVars[tmp[0]] = tmp[1];
      });
    }
    return getVars.ticket;
  }
}

// fetch api to get validate result
export async function createCORSRequest(validate) {
  var xml = { user: "" };
  const proxyurl = "https://cors-anywhere.herokuapp.com/";
  await fetch(proxyurl + validate, {
    // mode: "no-cors", // 'cors' by default
  })
    .then(response => response.text())
    .then(contents => {
      xml = catchUser(contents);
    })
    .catch(err => {});
  return xml;
}

// Get user sso
export function catchUser(str) {
  var xml = str,
    dom = parseXml(xml),
    json = xml2json(dom);
  var usObject = { user: "" };
  if (json.includes("user")) {
    var query = json.split("cas:");
    query.forEach(e => {
      if (e.includes("user")) {
        json = query[query.indexOf(e)];
      }
    });
    query = json.split(`}`);
    query.forEach(e => {
      if (e.includes("user")) {
        json = query[query.indexOf(e)];
      }
    });
    usObject = JSON.parse("{" + `"` + json + "}");
  }
  return usObject;
}
