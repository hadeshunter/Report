<template>
  <div class="k main-form">
    <div class="k wf main-container">
      <div class="k wf card">
        <div class="k wf card-title">
          <span>{{ $t("SETTING_SLA").toUpperCase() }}</span>
        </div>
        <!----------------------------HEADER--------------------------------->
        <div class="k wf flex-row">
          <div class="k part">
            <div class="demonstration">{{ $t("PICK_MONTH") }}</div>
            <el-date-picker
              class="picker"
              v-model="month"
              type="month"
              v-bind:placeholder="this.$i18n.t(pickMonth)"
              align="right"
            ></el-date-picker>
          </div>
          <div class="k part">
            <div class="demonstration">{{ $t(select_stage) }}</div>
            <b-form-select
              v-model="time.timeid"
              @change="getByIdTime(time.timeid)"
              :options="stages"
            ></b-form-select>
            <div class="mt-3">
              Thời gian gửi SMS:
              <strong>{{ time.totaltime + " (phút)"}}</strong>
            </div>
          </div>
          <div class="k part">
            <br class="mobile" />
            <el-button type="primary" @click="modalUpdateSLAGai=true">
              <i class="el-icon-alarm-clock" style="font-size: 20px;"></i>
              {{ $t("SETTING_TIME") }}
            </el-button>
          </div>
        </div>
        <!--------------------------------modalUpdateSLA Gai----------------------------------->
        <b-modal
          v-model="modalUpdateSLAGai"
          ref="my-modal"
          hide-footer
          title="Cập nhật SLA giai đoạn"
        >
          <div class="form-group row">
            <label for="name" class="col-sm-5 col-form-label">Giai đoạn</label>
            <div class="col-sm-7">
              <select
                class="custom-select my-1 mr-sm-2"
                @change="getByIdTime(time.timeid)"
                v-model="time.timeid"
                id="inlineFormCustomSelectPref"
              >
                <option select="selected">chọn...</option>
                <option value="1">Khảo sát yêu cầu</option>
                <option value="2">Thiết kế hóa đơn</option>
                <option value="3">Dựng web site demo</option>
              </select>
            </div>
          </div>
          <div class="form-group row">
            <label for="name" class="col-sm-5 col-form-label">Thời gian gửi SMS (phút)</label>
            <div class="col-sm-7">
              <input
                type="name"
                class="form-control"
                v-model="time.totaltime"
                placeholder="Nhập họ tên"
                maxlength="50"
              />
            </div>
          </div>

          <button
            type="button"
            style="width:100%"
            @click="updateTimer()"
            class="btn btn-success"
          >Cập nhật</button>
        </b-modal>
        <!----------------------------------pHONESENT--------------------------------->
        <div class="k wf tab-setting">
          <el-tabs v-model="selectComponent" @tab-click="handleClick">
            <el-tab-pane
              v-bind:label="this.$i18n.t('LIST_SMS_ERRO')"
              name="processComponent"
              @click="selectComponent = 'processComponent'"
            ></el-tab-pane>
            <el-tab-pane
              label="Danh sách tin nhắn "
              name="phonesendedComponent"
              @click="selectComponent = 'phonesendedComponent'"
            ></el-tab-pane>
            <el-tab-pane
              v-bind:label="this.$i18n.t('LIST_USER_REGISTER')"
              name="userlistsmsComponent"
              @click="selectComponent = 'userlistsmsComponent'"
            ></el-tab-pane>
            <el-tab-pane
              v-bind:label="this.$i18n.t('LIST_SLA_TRUE')"
              name="slatimeComponent"
              @click="selectComponent = 'slatimeComponent'"
            ></el-tab-pane>
            <el-tab-pane
              v-bind:label="this.$i18n.t(header.HOLIDAY)"
              name="holidayComponent"
              @click="selectComponent = 'holidayComponent'"
            ></el-tab-pane>
          </el-tabs>
        </div>
        <div class="h wf body">
          <component :is="selectComponent"></component>
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
import responsitories from "../../service/factory/reponsitoryfactory";
import timereponsitory from "../../service/Impl/timerepository";
import holidayResponsitories from "../../service/Impl/holidayReponsitory";
// componet
import userlistsmsComponent from "../userlistsms/userlistsmsComponent";
import phonesended from "../phonesended/phonesendedComponent";
import process from "../process/processComponent";
import slatime from "../slatime/slatimeComponent";
import holiday from "../holiday/holiday";
import slatimeComponentVue from "../slatime/slatimeComponent.vue";
const reportResponsitories = responsitories.get("getListSms");
export default {
  name: "setting",
  data() {
    return {
      header: {
        HOLIDAY: "HOLIDAYS",
      },
      title: {
        ID_GIAI_DOAN: "ID_GIAI_DOAN",
        TOTALTIME: "TOTALTIME",
        TIMEID: "TIMEID",
      },
      time: {
        TIMEID: "",
        TOTALTIME: "",
        ID_GIAI_DOAN: "",
      },
      timelist: [],
      processComponent: "first",
      modalUpdateSLAGai: false,
      modaladdTime: false,
      selectComponent: "processComponent",
      yearSelected: new Date(),
      month: new Date(),
      pickMonth: "PICK_MONTH",
      inputCustomer: "",
      select_stage: "SELECT_STAGE",
      stages: [
        {
          value: 1,
          text: this.$i18n.t("REQUEST"),
        },
        {
          value: 2,
          text: this.$i18n.t("DESIGN"),
        },
        {
          value: 3,
          text: this.$i18n.t("BUILD_WEBSITE_DEMO"),
        },
      ],
    };
  },
  created() {
    this.getAllTimer();
  },
  components: {
    userlistsmsComponent: userlistsmsComponent,
    phonesendedComponent: phonesended,
    processComponent: process,
    slatimeComponent: slatime,
    holidayComponent: holiday,
  },
  methods: {
    handleClick(tab, event) {},
    clickFunction() {
      var x = document.getElementById("myTopnav-setting");
      if (x.className === "h wf topnav") {
        x.className += " responsive";
      } else {
        x.className = "h wf topnav";
      }
    },
    getAllTimer() {
      var selt = this;
      timereponsitory
        .getAllTimer()
        .then(function (response) {
          if (response.data.success) {
            selt.time = response.data.data[0];
            selt.time.totaltime = selt.time.totaltime / 60;
            console.log(selt.time);
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
    updateTimer() {
      var self = this;
      this.time.totaltime = this.time.totaltime * 60;
      timereponsitory
        .updateTimer(this.time)
        .then(function (response) {
          if (response.data.success) {
            self.modalUpdateSLAGai = false;
            self.getAllTimer();
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
    insertTimer() {
      var selt = this;
      timereponsitory
        .insertTimer(this.time)
        .then(function (response) {
          if (response.data.success) {
            selt.getAllTimer();
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
    getByIdTime(timeid) {
      var self = this;
      timereponsitory.getById(timeid).then(function (response) {
        if (response.data.success) {
          self.time = response.data.data;
          self.time.totaltime = self.time.totaltime / 60;
        }
      });
    },
  },
};
</script>

<style scoped>
@import url(./setting.css);
</style>
