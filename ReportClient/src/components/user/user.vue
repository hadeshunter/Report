<template>
  <div class="k main-form">
    <div class="k wf main-container">
      <div class="k wf card">
        <div class="k wf card-title">
          <span>{{ $t("USER").toUpperCase() }}</span>
        </div>

        <div class="k wf flex-row">
          <div class="k part">
            <el-dropdown trigger="click">
              <el-button type="primary">
                <i class="fas fa-cog"></i>
                <span>{{ $t("OPTIONS") }}</span>
                <i class="el-icon-arrow-down el-icon--right"></i>
              </el-button>
              <el-dropdown-menu slot="dropdown">
                <!-- <el-dropdown-item>
                  <a @click="getCreateModal()" href="#">
                    <i class="far fa-plus-square"></i>
                    {{ $t("ADD") }}
                  </a>
                </el-dropdown-item> -->
                <el-dropdown-item>
                  <a @click="getUpdateUser()" href="#">
                    <i class="fas fa-edit"></i>
                    {{ $t("EDIT") }}
                  </a>
                </el-dropdown-item>
                <!-- <el-dropdown-item>
                  <a @click="deleteUser()" href="#">
                    <i class="far fa-trash-alt"></i>
                    {{ $t("DELETE") }}
                  </a>
                </el-dropdown-item> -->
              </el-dropdown-menu>
            </el-dropdown>
          </div>
          <div class="k part">
            <el-input
              v-bind:placeholder="this.$i18n.t(searchTxt)"
              v-model="input"
              @keyup.native.enter="getSearch(input)"
              @keyup.native="checkNull()"
            >
              <el-button
                v-model="input"
                type="text"
                icon="el-icon-search"
                slot="suffix"
                :loading="btnSearchLoading"
                @click="getSearch(input)"
              ></el-button>
            </el-input>
          </div>
        </div>

        <!-- Table -->
        <div class="k wf card-container">
          <table class="table">
            <tr class="table-title">
              <th class="table-title">
                <input
                  type="checkbox"
                  class="select-item"
                  name="select-item"
                  @change="getSelectAll(checkAll)"
                  v-model="checkAll"
                />
              </th>
              <th class="table-title">{{ $t(title.username) }}</th>
              <th class="table-title">{{ $t(title.password) }}</th>
              <th class="table-title">{{ $t(title.phone) }}</th>
              <th class="table-title">{{ $t(title.active) }}</th>
            </tr>
            <tr v-for="user in pageOfItems" :key="user.id">
              <th class="check">
                <input
                  type="checkbox"
                  v-model="user.check"
                  class="select-item"
                  name="select-item"
                />
              </th>
              <th>{{ user.username }}</th>
              <th>*******</th>
              <th>{{ user.phone }}</th>
              <th>{{ user.active }}</th>
            </tr>
          </table>
        </div>
        <el-pagination
          @current-change="handleCurrentChange"
          :current-page.sync="currentPage"
          :page-size="perPage"
          :total="totalUser"
          layout="total, prev, pager, next"
        ></el-pagination>
      </div>
    </div>

    <!-- create user modal -->
    <b-modal
      v-model="modalCreateUser"
      ref="my-modal"
      hide-footer
      v-bind:title="this.$i18n.t(titleCreate)"
    >
      <div class="modal-dialog">
        <div class="modal-container">
          <div class="margin-b10">
            <label class="label" for="uname">
              <span>{{ $t(title.username) }} <span class="red">(*)</span></span>
            </label>
            <input
              v-model="user.username"
              class="text-create"
              type="text"
              v-bind:placeholder="this.$i18n.t(title.username)"
              name="uname"
              required
            />
          </div>
          <div class="margin-b10">
            <label class="label" for="uname">
              <span>{{ $t(title.password) }}</span>
            </label>
            <input
              v-model="user.password"
              class="text-create"
              type="text"
              v-bind:placeholder="this.$i18n.t(title.password)"
              name="uname"
              disabled
            />
          </div>
          <div class="margin-b10">
            <label class="label" for="uname">
              <span>{{ $t(title.phone) }}</span>
            </label>
            <input
              v-model="user.phone"
              class="text-create"
              type="text"
              v-bind:placeholder="this.$i18n.t(title.phone)"
              name="uname"
              required
            />
          </div>
          <div class="margin-b10">
            <label class="label" for="uname">
              <input
                v-model="user.active"
                class="select-item"
                type="checkbox"
                name="uname"
                required
              />
              <span>{{ $t(title.active) }}</span>
            </label>
          </div>
          <button
            @click="createUser()"
            type="button"
            class="btn btn-primary margin-t15"
          >
            {{ $t("SUBMIT") }}
          </button>
        </div>
      </div>
    </b-modal>
    <!-- update user modal -->
    <b-modal
      v-model="modalUpdateUser"
      ref="my-modal"
      hide-footer
      v-bind:title="this.$i18n.t(titleUpdate)"
    >
      <div class="modal-dialog">
        <div class="modal-container">
          <div class="margin-b10">
            <label class="label" for="uname">
              <span>{{ $t(title.username) }}</span>
            </label>
            <input
              v-model="user.username"
              class="text-create"
              type="text"
              v-bind:placeholder="this.$i18n.t(title.username)"
              name="uname"
              disabled
            />
          </div>
          <!-- <div class="margin-b10">
            <label class="label" for="uname">
              <span>{{ $t(title.password) }} ({{ $t("BLANK") }})</span>
            </label>
            <input
              v-model="user.password"
              class="text-create"
              type="text"
              v-bind:placeholder="this.$i18n.t(title.password)"
              name="uname"
              required
            />
          </div> -->
          <div class="margin-b10">
            <label class="label" for="uname">
              <span>{{ $t(title.phone) }}</span>
            </label>
            <input
              v-model="user.phone"
              class="text-create"
              type="text"
              v-bind:placeholder="this.$i18n.t(title.phone)"
              name="uname"
              required
            />
          </div>
          <div class="margin-b10">
            <label class="label" for="uname">
              <input
                v-model="user.active"
                class="select-item"
                type="checkbox"
                name="uname"
                required
              />
              <span>{{ $t(title.active) }}</span>
            </label>
          </div>
          <button
            @click="updateUser()"
            type="button"
            class="btn btn-primary margin-t15"
          >
            {{ $t("SUBMIT") }}
          </button>
        </div>
      </div>
    </b-modal>
    <div class="loading"></div>
  </div>
</template>
<script>
import Vue from "vue";
import { BootstrapVue, IconsPlugin } from "bootstrap-vue";
import "vue-toast-notification/dist/theme-sugar.css";
import { removeSpace } from "../../utils/common";
import pagination from "../pagination/pagination";
import responsitories from "../../service/factory/reponsitoryfactory";
import reportResponsitory from "../../service/Impl/reportResponsitory";
const userResponsitories = responsitories.get("user");

export default {
  name: "users",
  data() {
    return {
      title: {
        usid: "STT",
        username: "USERNAME",
        password: "PASSWORD",
        phone: "PHONE",
        // email: "Email",
        // address: "Địa chỉ",
        role: "ROLE",
        active: "ACTIVE",
      },
      userList: [],
      pageOfItems: [],
      user: {
        username: "",
        password: "",
        phone: "",
        // email: "",
        // address: "",
        role: 2,
        active: true,
      },
      titleCreate: "CREATE_NEW",
      titleUpdate: "UPDATE_USER",
      searchTxt: "SEARCH_TXT",
      btnSearchLoading: false,
      input: "",
      modalCreateUser: false,
      modalUpdateUser: false,
      perPage: 10,
      currentPage: 1,
      totalUser: 0,
      checkAll: false,
    };
  },
  methods: {
    async getUserList() {
      $(".loading").show(100);
      setTimeout(() => {
        $(".loading").hide(100);
      }, 500);
      var self = this;
      this.currentPage = 1;
      this.userList = [];
      this.pageOfItems = [];
      var perPage = this.perPage;
      await userResponsitories
        .getAllUser()
        .then(function (response) {
          if (response.data.success) {
            self.userList = [];
            self.pageOfItems = [];
            response.data.data.forEach((user, index) => {
              self.userList.push({
                id: index + 1,
                usid: user.usid,
                username: user.username,
                password: "",
                phone: user.phone,
                // email: user.email,
                // address: user.address,
                role: user.role,
                active: user.active,
                check: false,
              });

              if (index < perPage) {
                self.pageOfItems.push({
                  id: index + 1,
                  usid: user.usid,
                  username: user.username,
                  password: "",
                  phone: user.phone,
                  // email: user.email,
                  // address: user.address,
                  role: user.role,
                  active: user.active,
                  check: false,
                });
              }
            });
            self.totalUser = response.data.data.length;
            $(".loading").hide(100);
          }
        })
        .catch((error) => {
          var err = removeSpace(error.message.toUpperCase());
          Vue.$toast.error(
            this.$i18n.te(err) ? this.$i18n.t(err) : error.message
          );
          $(".loading").hide(100);
        });
    },
    getCreateModal() {
      this.user = {
        username: "",
        password: "123456",
        phone: "",
        // email: "",
        // address: "",
        role: 2,
        active: true,
      };
      this.modalCreateUser = true;
    },
    createUser() {
      var self = this;
      var userList = this.userList;
      var username = this.user.username;
      var password = this.user.password;

      if (!username && username.length <= 0) {
        Vue.$toast.error(this.$i18n.t("USERNAME_IS_NOT_BLANKED"));
        return;
      }

      if (!password && password.length <= 0) {
        Vue.$toast.error(this.$i18n.t("PASSWORD_IS_NOT_BLANKED"));
        return;
      }

      var valObj = userList.filter(function (user) {
        if (user.username == username) return user.username;
      });

      if (valObj.length > 0) {
        Vue.$toast.error(
          this.$i18n.t("USERNAME_IS_VALID") + ": " + valObj[0].username
        );
        return;
      }

      userResponsitories
        .createFirstUser(this.user)
        .then(function (response) {
          if (response.data.success) {
            self.modalCreateUser = false;
            self.getUserList();
            Vue.$toast.success(response.data.message);
          } else {
            Vue.$toast.error(response.data.message);
          }
        })
        .catch((error) => {
          var err = removeSpace(error.message.toUpperCase());
          Vue.$toast.error(
            this.$i18n.te(err) ? this.$i18n.t(err) : error.message
          );
        });
    },
    getUpdateUser() {
      this.user = {};
      var count = 0;
      this.pageOfItems.forEach((user) => {
        if (user.check) {
          this.user = user;
          count = count + 1;
        }
      });
      if (count > 1) {
        Vue.$toast.error(this.$i18n.t("CHECK_ONE_TO_UPDATE"));
      } else if (count == 0) {
        Vue.$toast.error(this.$i18n.t("EMPTY_CHECK"));
      } else if (count == 1) {
        this.modalUpdateUser = true;
      }
    },
    updateUser() {
      var self = this;
      var data = {
        usid: this.user.usid,
        username: this.user.username,
        password: this.user.password,
        phone: this.user.phone,
        role: Number(this.user.role),
        active: this.user.active,
      };
      userResponsitories
        .update(data)
        .then(function (response) {
          if (response.data.success) {
            self.modalUpdateUser = false;
            self.getUserList();
            Vue.$toast.success(response.data.message);
          } else {
            Vue.$toast.error(response.data.message);
          }
        })
        .catch((error) => {
          var err = removeSpace(error.message.toUpperCase());
          Vue.$toast.error(
            this.$i18n.te(err) ? this.$i18n.t(err) : error.message
          );
        });
    },
    deleteUser() {
      var self = this;
      var checkAdmin = false;
      var deleteList = [];
      self.pageOfItems.forEach((user) => {
        if (user.check) {
          if (user.username.toLowerCase() !== "admin") {
            deleteList.push({
              usid: user.usid,
            });
          } else {
            checkAdmin = true;
            Vue.$toast.error(this.$i18n.t("DO_NOT_CHOOSE_ADMIN"));
          }
        }
      });
      if (checkAdmin == false) {
        if (deleteList.length > 0)
          userResponsitories
            .deleteListUser(deleteList)
            .then(function (response) {
              if (response.data.success) {
                Vue.$toast.success(response.data.message);
                self.getUserList();
              } else {
                Vue.$toast.error(response.data.message);
              }
            })
            .catch((error) => {
              var err = removeSpace(error.message.toUpperCase());
              Vue.$toast.error(
                this.$i18n.te(err) ? this.$i18n.t(err) : error.message
              );
            });
        else {
          Vue.$toast.error(this.$i18n.t("EMPTY_CHECK"));
        }
      }
    },
    getSearch(input) {
      var self = this;
      this.btnSearchLoading = true;
      this.pageOfItems = [];
      var perPage = this.perPage;
      var data = {
        filter: input,
      };
      if (input) {
        userResponsitories
          .search(data)
          .then(function (response) {
            if (response.data.success) {
              response.data.data.forEach((user, index) => {
                if (index < perPage) {
                  self.pageOfItems.push({
                    id: index + 1,
                    usid: user.usid,
                    username: user.username,
                    password: "",
                    phone: user.phone,
                    // email: user.email,
                    // address: user.address,
                    role: user.role,
                    active: user.active,
                    check: false,
                  });
                }
              });

              self.totalUser = response.data.data.length;
              self.btnSearchLoading = false;
            } else {
              Vue.$toast.error(response.data.message);
              self.btnSearchLoading = false;
            }
          })
          .catch((error) => {
            var err = removeSpace(error.message.toUpperCase());
            Vue.$toast.error(
              this.$i18n.te(err) ? this.$i18n.t(err) : error.message
            );
            self.btnSearchLoading = false;
          });
      } else {
        setTimeout(() => {
          this.btnSearchLoading = false;
        }, 500);
      }
    },
    checkNull() {
      if (this.input == null || this.input == "") {
        this.getUserList();
      }
    },
    handleCurrentChange(val) {
      this.pageOfItems = [];
      this.userList.forEach((user) => {
        if (
          user.id > this.perPage * (val - 1) &&
          user.id < this.perPage * val + 1
        ) {
          this.pageOfItems.push(user);
        }
      });
    },
    getSelectAll: function (checkAll) {
      if (checkAll == true) {
        this.userList.forEach((user) => (user.check = true));
        this.pageOfItems.forEach((user) => (user.check = true));
      } else {
        this.userList.forEach((user) => (user.check = false));
        this.pageOfItems.forEach((user) => (user.check = false));
      }
    },
  },
  created() {
    this.getUserList();
  },
};
</script>

<style scoped>
@import url(./user.css);
</style>
