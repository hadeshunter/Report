<template>
  <div class="k wf main-container" id="app">
    <!-- <button @click="sso_login()">login</button> -->
    <!-- <button @click="sso_logout()">logout</button> -->

    <div class="k wf header">
      <a href>
        <div class="k header-logo"></div>
      </a>
      <span class="k header-title">{{ $t("CNTT_CENTER") }}</span>
      <div class="btn-login">
        <div class="btn-group">
          <span
            type="button"
            class="btn dropdown-toggle"
            data-toggle="dropdown"
          >
            <i class="fas fa-globe-europe"></i>
            <span class="pc">{{ $t("select_lang") }}</span>
          </span>
          <div class="dropdown-menu">
            <a class="dropdown-item" @click="setLangNew('vi')">
              {{ $t("VI") }}
            </a>
            <a class="dropdown-item" @click="setLangNew('en')">
              {{ $t("EN") }}
            </a>
          </div>
        </div>
        <!-- <button
          v-if="!islogin"
          type="button"
          class="btn btn-primary"
          @click="modalShow = true"
          data-toggle="modal"
          data-target="#exampleModal"
        >
          {{ $t("LOGIN") }}
        </button> -->
        <button
          v-if="!islogin"
          type="button"
          class="btn btn-primary"
          @click="sso_logout(), sso_login()"
          data-toggle="modal"
          data-target="#exampleModal"
        >
          {{ $t("LOGIN") }}
        </button>

        <div class="border-tab-user" v-if="islogin">
          <span class="k name-user">
            <i class="fa fa-user-secret"></i>
            {{ username }}
          </span>
          <div class="form-text-logout">
            <span class="k text_logout" v-on:click="sso_logout()">
              {{ $t("LOGOUT") }}
            </span>
            <span
              class="k text_changelanguage text-logout"
              @click="modalChangePass = true"
              >{{ $t("CHANGE_PASS") }}</span
            >
          </div>
        </div>
      </div>
      <div class="k wf">
        <el-button @click="drawer = true" class="mobile btn-menu" size="medium">
          <i class="fas fa-bars white"></i>
        </el-button>
      </div>
    </div>
    <div class="k wf topnav" v-if="isActive">
      <el-menu
        :default-active="activeIndex"
        class="k pc"
        mode="horizontal"
        background-color="#387cbc"
        text-color="#fff"
        active-text-color="#ffd04b"
        v-for="per in usPer"
        v-bind:key="per.perid"
        @select="handleSelect"
      >
        <el-menu-item
          v-bind:index="per.id.toString()"
          v-bind:href="'#' + per.link"
          @click="selectComponent = per.policy"
          >{{ per.pername }}</el-menu-item
        >
      </el-menu>
    </div>

    <div class="k wf body" v-if="isActive">
      <component :is="selectComponent"></component>
    </div>

    <div class="k wf topnav" v-if="!isActive">
      <el-card shadow="never"> {{ $t("NOT_ACTIVE") }} </el-card>
    </div>

    <div class="k wf footer">
      <div class="k wf footer-title">
        <div class="footer-copyright text-center py-3 text-white">
          © 2020 copyright: {{ $t("CNTT_CENTER") }}
        </div>
      </div>
    </div>
    <notifications group="foo-success" position="bottom right" :speed="500" />
    <!-- login -->
    <b-modal v-model="modalShow" ref="my-modal" hide-footer title="Đăng Nhập">
      <div class="modal-dialog">
        <loginComponent></loginComponent>
      </div>
    </b-modal>

    <!-- Drawer -->
    <el-drawer :visible.sync="drawer" size="60%" direction="ltr">
      <div class="drawer-logo" slot="title"></div>
      <div>
        <el-menu
          :default-active="activeIndex"
          mode="vertical"
          background-color="#387cbc"
          text-color="#fff"
          active-text-color="#ffd04b"
          v-for="per in usPer"
          v-bind:key="per.perid"
          @select="handleSelect"
        >
          <el-menu-item
            v-bind:index="per.id.toString()"
            v-bind:href="'#' + per.link"
            @click="selectComponent = per.policy"
            >{{ per.pername }}</el-menu-item
          >
        </el-menu>
      </div>
    </el-drawer>
  </div>
</template>

<script>
import Vue from "vue";
Vue.use(BootstrapVue);

import { BootstrapVue, IconsPlugin } from "bootstrap-vue";
import $ from "jquery";
import VueHead from 'vue-head';
import VueRouter from 'vue-router'
Vue.use(VueHead);
Vue.use(VueRouter);
import { homeUrl, ssoLogin, ssoLogout, ssoValidate } from "../../utils/sso";
import user from "../user/user";
import login from "../login/login";
import setting from "../setting/settingComponent";
import report from "../report/reportComponent";
import reportDasboard from "../reportDasboard/reportDasboard";
import reportListSLA from "../reportListSLA/reportListSLA";
import observation from "../observation/observation";
import permission from "../permission/permission";
import reponsitoryfactory from "../../service/factory/reponsitoryfactory";
const authReponsitory = reponsitoryfactory.get("auth");
const userReponsitory = reponsitoryfactory.get("user");
const permissionReponsitory = reponsitoryfactory.get("permission");

export default {
  name: "app",
  head:{
    link: [
    { rel: 'icon', href: require('../../assets/img/VNPT_Logo.png'), sizes: '16x16', type: 'image/png' }, 
  ],
  },
  data() {
    return {
      drawer: false,
      modalShow: false,
      modalShow1: true,
      activeIndex: "1",
      selectComponent: "",
      isActive: false,
      lang: "",
      user: {},
      islogin: false,
      username: "",
      usPer: [],
      validate: "",
      //show name username on header
      datalogin: {
        username: "",
        password: "",
        Grant_type: "password",
      },
      token: "", //save token
    };
  },

  created() {
    this.getTokenByLocal();
  },
  // dung de chuyen data
  methods: {
    sso_login() {
      var ssoIn = ssoLogin();
      window.location.href = ssoIn;
    },
    sso_logout() {
      var ssoOut = ssoLogout();
      this.signout();
      window.location.href = ssoOut;
    },
    async getTokenByLocal() {
      var section_local = this.getCookie("token_session");
      if (section_local) {
        var tok = this.strToObj(this.getCookie("token_session"));
        this.token = tok.token;
        await this.getUserById(parseInt(tok.usid));
        await this.getPerOfUser(parseInt(tok.usid), this.lang);
        this.modalShow = false; //hide form login
      } else {
        this.modalShow = false; //show form login
        if (window.location.href.includes("ticket")) {
          this.validate = await ssoValidate(window.location.href);
          this.login(this.validate);
        }
      }
    },
    setLangNew: function (lang) {
      this.$store.dispatch("setLangNew", lang);
      this.lang = lang;
      this.getTokenByLocal();
    },
    getCookie(cname) {
      var name = cname + "=";
      var decodedCookie = decodeURIComponent(document.cookie);
      for (var i = 0; i < decodedCookie.length; i++) {
        var c = decodedCookie;
        while (c.charAt(0) === " ") {
          c = c.substring(1);
        }
        if (c.indexOf(name) === 0) {
          return c.substring(name.length, c.length);
        }
      }
      return "";
    },
    strToObj(str) {
      //convert string to object
      var obj = {};
      if (str && typeof str === "string") {
        var objStr = str.match(/\{(.)+\}/g);
        eval("obj =" + objStr);
      }
      return obj;
    },
    async getUserById(id) {
      var self = this;
      await userReponsitory.getById(id).then(function (response) {
        if (response.data.success) {
          self.username = response.data.data.username;
          self.islogin = true;
          self.isActive = response.data.data.active;
        } else {
          self.islogin = false;
        }
      });
    },
    async getPerOfUser(id, lang) {
      var self = this;
      await permissionReponsitory
        .getAllPermissionByUser(id, lang)
        .then(function (response) {
          self.usPer = [];
          if (response.data.success) {
            if (response.data.data.length > 0) {
              response.data.data.forEach((per, index) => {
                self.usPer.push(Object.assign({ id: index + 1 }, per));
              });
              self.selectComponent = self.usPer[0].policy;
            }
          }
        });
    },
    async login(validate) {
      var self = this;
      self.datalogin = {
        username: validate.user,
        password: "123456",
        Grant_type: "password",
      };
      await userReponsitory.getUserByUserName(validate.user);
      await authReponsitory
        .login(
          Object.keys(self.datalogin)
            .map(function (key) {
              return (
                encodeURIComponent(key) +
                "=" +
                encodeURIComponent(self.datalogin[key])
              );
            })
            .join("&")
        )
        .then(function (response) {
          if (response.data) {
            self.show_login = false; //hide bnt login
            self.token = response.data.access_token;
            self.getUserByUserName(); //lay thong tin user sau do moi ghi token
          } else {
          }
        });
    },
    getUserByUserName() {
      var self = this;
      userReponsitory
        .getUserByUserName(this.datalogin.username)
        .then(function (response) {
          if (response.data.success) {
            self.name_user = response.data.data.username; //show name user
            if (typeof Storage !== "undefined") {
              var value = {
                token: self.token,
                usid: response.data.data.usid,
              };
              var today = new Date();
              var nexthour = new Date();
              nexthour.setDate(today.getDate() + 1);
              document.cookie =
                "token_session=" +
                JSON.stringify(value) +
                ";expires =" +
                nexthour +
                "";
            } else {
              alert("Trình duyệt của bạn không được hỗ trợ!");
            }
            window.location.href = "/";
          }
        });
    },
    signout() {
      //logout user
      document.cookie = "token_session=; expires=Thu, 01 Jan 1970 00:00:00 UTC";
      this.modalShow = false;
      this.islogin = false;
      //this.show_login =true;
    },
    handleSelect(key, keyPath) {
      this.activeIndex = keyPath[0];
    },
  },
  //format date
  filters: {
    moment: function (date) {
      return moment(date).format("MMMM Do YYYY, h:mm:ss a");
    },
  },
  components: {
    loginComponent: login,
    userComponent: user,
    settingComponent: setting,
    reportListSLAComponent: reportListSLA,
    observationComponent: observation,
    permissionComponent: permission,
    reportComponent : report,
  },
  
};

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
@import url(./main.css);
</style>
