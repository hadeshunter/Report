<template>
  <div class="k main-form">
    <div class="k wf main-container">
      <div class="k wf card">
        <!----------------------------- sms list --------------------------->
        <div class="k wf card-titleb">
          <span>{{ $t("LIST_USER_REGISTER").toUpperCase() }}</span>
        </div>
        <div class="k wf card-container">
          <table class="table">
            <tr class="table-title">
              <th class="table-title">{{ this.$i18n.t(title.SMSID) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.NAME) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.PHONE) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.ACTIVI) }}</th>
              <th class="table-title"><i class="el-icon-circle-plus-outline" @click="modalCreateSMS=true"></i></th>
            </tr>
            <tr v-for="(sms,index)  in smsList" :key="index">
              <th>{{ sms.smsid }}</th>
              <th>{{ sms.name }}</th>
              <th>{{ sms.phone }}</th>
              <th>{{ sms.active==true?"Kích hoạt" : "Chưa kích hoạt"}}</th>
              <th>
                <el-button type="text" @click="getById(sms.smsid)">
                  <span class="tooltipedit">
                    <i class="far fa-edit"></i>
                    <span class="tooltipedit1">Chỉnh sửa</span>
                  </span>
                </el-button>
                <el-button type="text" @click="deleteSms(sms.smsid)">
                  <span class="tooltipdelete">
                    <i class="far fa-trash-alt"></i>
                    <span class="tooltipdelete1">Xóa</span>
                  </span>
                </el-button>
              </th>
            </tr>
          </table>
          <!------------------- add user sms ---------------------------------------->
          <b-modal v-model="modalCreateSMS" ref="my-modal" hide-footer title="Tạo mới User">
            <div class="form-group row">
              <label for="name" class="col-sm-3 col-form-label">Nhập họ tên</label>
              <div class="col-sm-9">
                <input
                  type="name"
                  class="form-control"
                  v-model="sms.name"
                  placeholder="Nhập họ tên"
                  maxlength="50"
                />
              </div>
            </div>
            <div class="form-group row">
              <label for="name" class="col-sm-3 col-form-label">Điện thoại</label>
              <div class="col-sm-9">
                <input
                  type="number"
                  class="form-control"
                  v-model="sms.phone"
                  placeholder="Nhập số điện thoại"
                  maxlength="10"
                />
              </div>
            </div>
            <div class="form-group row">
              <label for="name" class="col-sm-3 col-form-label">Trạng thái</label>
              <div class="col-sm-9" aria-placeholder="csdmsl">
                <select
                  class="custom-select my-1 mr-sm-2"
                  v-model="sms.active"
                  id="inlineFormCustomSelectPref"
                >
                  <option select="selected">chọn...</option>
                  <option value="true">Kích hoạt</option>
                  <option value="false">Chưa kích hoạt</option>
                </select>
              </div>
            </div>
            <button
              type="button"
              style="width:100%"
              @click="insertSms()"
              class="btn btn-success"
            >Tạo mới</button>
          </b-modal>
          <!-- ------------------------update -------------------------------->
          <b-modal v-model="modalUpdateSMS" ref="my-modal" hide-footer title="Cập nhật User">
            <div class="form-group row">
              <label for="name" class="col-sm-3 col-form-label">Nhập họ tên</label>
              <div class="col-sm-9">
                <input
                  type="name"
                  class="form-control"
                  v-model="sms.name"
                  placeholder="Nhập họ tên"
                  maxlength="50"
                />
              </div>
            </div>
            <div class="form-group row">
              <label for="name" class="col-sm-3 col-form-label">Điện thoại</label>
              <div class="col-sm-9">
                <input
                  type="number"
                  class="form-control"
                  v-model="sms.phone"
                  placeholder="Nhập số điện thoại"
                  maxlength="10"
                />
              </div>
            </div>
            <div class="form-group row">
              <label for="name" class="col-sm-3 col-form-label">Trạng thái</label>
              <div class="col-sm-9">
                <select
                  class="custom-select my-1 mr-sm-2"
                  v-model="sms.active"
                  id="inlineFormCustomSelectPref"
                >
                  <option select="selected">chọn...</option>
                  <option value="true">Kích hoạt</option>
                  <option value="false">Chưa kích hoạt</option>
                </select>
              </div>
            </div>
            <button
              type="button"
              style="width:100%"
              @click="updateSms()"
              class="btn btn-success"
            >Cập nhật</button>
          </b-modal>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import Vue from "vue";
import $ from "jquery";
import bootbox from "bootbox";
Vue.use(bootbox);
import moment from "moment";
import { removeSpace } from "../../utils/common";
import smsreponsitory from "../../service/Impl/smsreponsitory";
import responsitories from "../../service/factory/reponsitoryfactory";
const reportResponsitories = responsitories.get("getListSms");

export default {
  name: "userlistsmsComponent",
  data() {
    return {
      title: {
        SMSID: "SMSID",
        NAME: "NAME",
        PHONE: "PHONE",
        ACTIVI: "ACTIVI",
        SETTING: "SETTING",
      },
      sms: {
        smsid: "",
        name: "",
        phone: "",
        activi: true,
      },
      smsList: [],
      modalUpdateSMS: false,
      modalCreateSMS: false,
    };
  },
  created() {
    this.getListSms();
  },
  methods: {
    getListSms() {
      var selt = this;
      smsreponsitory
        .getListSms()
        .then(function (response) {
          if (response.data.success) {
            selt.smsList = response.data.data;
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
    insertSms() {
      var self = this;
      smsreponsitory
        .insertSms(this.sms)
        .then(function (response) {
          if (response.data.success) {
            self.getListSms();
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
    getById(sms) {
      this.modalUpdateSMS = true;
      var self = this;
      smsreponsitory.getById(sms).then(function (response) {
        if (response.data.success) {
          self.sms = response.data.data;
          console.log(self.sms);
        }
      });
    },
    updateSms() {
      var self = this;
      console.log(this.sms);
      smsreponsitory
        .updateSms(this.sms)
        .then(function (response) {
          if (response.data.success) {
            self.modalUpdateSMS = false;
            self.getListSms();

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
    deleteSms(smsid) {
      var self = this;
      bootbox.confirm({
        message: "Bạn có muốn xóa User này không?",
        buttons: {
          confirm: {
            label: "Có",
            className: "btn-success",
          },
          cancel: {
            label: "Không",
            className: "btn-danger",
          },
        },
        callback: function (result) {
          if (result) {
            smsreponsitory
              .deleteItemSms(smsid)
              .then(function (response) {
                console.log(result);
                if (response.data.success) {
                  console.log(response);
                  self.getListSms();
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
          }
        },
      });
    },
  },
};
</script>
<style scoped>
@import url(./userlistsms.css);
</style>