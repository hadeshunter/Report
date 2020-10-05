<template>
  <div class="h main-form">
    <div class="container">
      <label for="uname">
        <b>{{ $t("USERNAME") }}</b>
      </label>

      <input
        v-on:keyup.enter="login()"
        class="text"
        type="text"
        placeholder="Nhập tên đăng nhập"
        v-model="datalogin.username"
        name="uname"
        required
      />

      <label for="psw">
        <b>{{ $t("PASSWORD") }}</b>
      </label>
      <input
        v-on:keyup.enter="login()"
        type="password"
        placeholder="Nhập mật khẩu"
        v-model="datalogin.password"
        name="psw"
        required
      />

      <button type="submit" v-on:click="login()">{{ $t("LOGIN") }}</button>
      <label>
        <input type="checkbox" checked="checked" name="remember" />
        {{ $t("REMEMBER_PASS") }}
      </label>
    </div>
  </div>
</template>

<script>
import reponsitoryfactory from "../../service/factory/reponsitoryfactory";
import axios from "axios";
import $ from "jquery";
const authreponsitory = reponsitoryfactory.get("auth");
const userreponsitory = reponsitoryfactory.get("user");
export default {
  name: "app",
  data() {
    return {
      datalogin: {
        username: "",
        password: "",
        Grant_type: "password",
      },
      //show name username on header
      token: "", //save token
    };
  },

  methods: {
    login() {
      var self = this;
      authreponsitory
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
      userreponsitory
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
  },
};
</script>

@<style scoped>
@import url(./login.css);
</style>
