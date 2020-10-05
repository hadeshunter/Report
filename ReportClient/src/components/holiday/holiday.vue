<template>
  <div class="k wf main-form">
    <!-- ---------------------holiday-------------------------- -->
    <div class="k wf card-titleb">
      <span>{{ $t("HOLIDAYS").toUpperCase() }}</span>
    </div>
    <div class="calendar">
      <vc-calendar
        v-bind:locale="this.$i18n.locale"
        ref="calendar"
        :attributes="attrs"
        :columns="$screens({ default: 1, laptop: 2 })"
      ></vc-calendar>
      <el-card class="calendar-card">
        <div slot="header" class="clearfix">
          <span>{{this.$t("OPTIONS")}}</span>
        </div>
        <el-collapse v-model="activeCollapse" class="wf" accordion>
          <el-collapse-item v-bind:title="this.$i18n.t(chooseYear)" name="1">
            <div class="year-picker">
              <el-date-picker
                @change="getListHoliday(yearSelected)"
                v-model="yearSelected"
                type="year"
                placeholder="Pick a year"
              ></el-date-picker>
              <el-button type="primary" class="btn-calendar" @click="insertByYear(yearSelected)">
                <i class="fas fa-plus"></i>
              </el-button>
            </div>
            <div class="calendar-description">{{this.$t("YEAR_DES")}}</div>
          </el-collapse-item>
          <el-collapse-item v-bind:title="this.$i18n.t(chooseDate)" name="2">
            <div class="year-picker">
              <el-date-picker
                @change="getListHoliday(yearSelected)"
                v-model="yearSelected"
                type="date"
                placeholder="Pick a date"
              ></el-date-picker>
              <el-button
                slot="reference"
                type="primary"
                class="btn-calendar"
                @click="insertDate(yearSelected)"
              >
                <i class="fas fa-plus"></i>
              </el-button>
              <el-button
                slot="reference"
                type="danger"
                class="btn-calendar"
                @click="deleteSelectedDate(yearSelected)"
              >
                <i class="el-icon-delete"></i>
              </el-button>
            </div>
            <div class="calendar-description">{{this.$t("DATE_DES")}}</div>
          </el-collapse-item>
        </el-collapse>
      </el-card>
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
import slatimeComponentVue from "../slatime/slatimeComponent.vue";
const reportResponsitories = responsitories.get("getListSms");
export default {
  data() {
    return {
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
      modalUpdateSLAGai: false,
      modaladdTime: false,
      selectComponent: "processComponent",
      yearSelected: new Date(),
      listHoliday: [],
      attrs: [
        {
          key: "today",
          dot: true,
          popover: {
            label: "You just hovered over today's date!",
          },
          dates: new Date(),
        },
      ],
      chooseYear: "CHOOSE_YEAR",
      chooseDate: "CHOOSE_DATE",
      desAdd: "ADD",
      desDelete: "DELETE",
      activeCollapse: ["1"],
      month: new Date(),
      pickMonth: "PICK_MONTH",
      inputCustomer: "",
      select_stage: "SELECT_STAGE",
      stages: [
        {
          value: 1,
          label: this.$i18n.t("REQUEST"),
        },
        {
          value: 2,
          label: this.$i18n.t("DESIGN"),
        },
        {
          value: 3,
          label: this.$i18n.t("BUILD_WEBSITE_DEMO"),
        },
      ],
    };
  },
  mounted() {
    this.getListHoliday();
  },
  methods: {
    async getListHoliday() {
      var self = this;
      var yearSelected = this.yearSelected;
      var year;
      if (yearSelected) {
        year = Number(moment(yearSelected).format("YYYY"));
      } else {
        yearSelected = new Date();
        year = Number(moment(yearSelected).format("YYYY"));
      }
      holidayResponsitories.getAllHoliday(year).then(function (response) {
        if (response.data.success) {
          self.attrs = [];
          self.listHoliday = response.data.data;
          response.data.data.forEach((item) => {
            self.attrs.push({
              highlight: { color: "green", contentClass: "italic" },
              dates: item.NGAY,
              popover: {
                label: moment(item.NGAY).format("DD/MM/YYYY") + " - Holliday",
                visibility: "hover",
                hideIndicator: true,
              },
            });
          });
        }
      });

      // Get the calendar ref
      const calendar = this.$refs.calendar;
      // Moves to my birthday
      await calendar.move(yearSelected);
    },
    insertByYear(yearSelected) {
      var self = this;
      var year = Number(moment(this.yearSelected).format("YYYY"));
      holidayResponsitories.insertHoliday(year).then(function (response) {
        if (response.data.success) {
          self.attrs = [];
          self.listHoliday = response.data.data;
          Vue.$toast.success(response.data.message);
          self.getListHoliday();
        } else {
          Vue.$toast.error(response.data.message);
        }
      });
    },
    insertDate(yearSelected) {
      var self = this;
      var date = Number(moment(yearSelected).format("YYYYMMDD"));
      holidayResponsitories.insertByDate(date).then(function (response) {
        if (response.data.success) {
          self.attrs = [];
          self.listHoliday = response.data.data;
          Vue.$toast.success(response.data.message);
          self.getListHoliday();
        } else {
          Vue.$toast.error(response.data.message);
        }
      });
    },
    deleteSelectedDate(yearSelected) {
      var self = this;
      var date = Number(moment(yearSelected).format("YYYYMMDD"));
      holidayResponsitories.deleteDate(date).then(function (response) {
        if (response.data.success) {
          self.attrs = [];
          self.listHoliday = response.data.data;
          Vue.$toast.success(response.data.message);
          self.getListHoliday();
        } else {
          Vue.$toast.error(response.data.message);
        }
      });
    },
  },
};
</script>
<style scoped>
@import url(./holiday.css);
</style>