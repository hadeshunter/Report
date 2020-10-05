<template>
  <div class="k main-form">
    <div class="k wf main-container">
      <div class="k wf card">
        <div class="k wf card-title">
          <span>{{ $t("REQUEST_LIST").toUpperCase() }}</span>
        </div>

        <div class="k wf flex-row">
          <div class="k part">
            <div class="demonstration">{{ $t("DATE_TO_DATE") }}</div>
            <el-date-picker
              class="date-time-picker"
              v-model="rangeDate"
              @change="getDasboard(rangeDate)"
              type="datetimerange"
              :picker-options="pickerOptions"
              range-separator="-"
              v-bind:start-placeholder="this.$i18n.t(startDate)"
              v-bind:end-placeholder="this.$i18n.t(endDate)"
              align="right"
            ></el-date-picker>
          </div>
        </div>

        <div class="k wf card-container">
          <table class="table">
            <tr class="table-title">
              <th class="table-title">{{ title.STT }}</th>
              <th class="table-title">{{ this.$i18n.t(title.TEN_KHACH_HANG) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.TEN_DO_UU_TIEN) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.THUC_HIEN) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.GIAI_DOAN_HIEN_TAI) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.DON_VI_THUC_HIEN) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.NGAY_BAT_DAU) }}</th>
            </tr>
            <tr v-for="(item, index) in pageOfItems" :key="index">
              <th
                v-if="item.TEN_DO_UU_TIEN === 'cao'"
                v-bind:class="{ high: isHigh }"
              >{{ item.STT }}</th>
              <th v-else-if="item.TEN_DO_UU_TIEN !== 'cao'">{{ item.STT }}</th>
              <th v-if="item.TEN_DO_UU_TIEN === 'cao'" v-bind:class="{ high: isHigh }">
                <span class="w-tkh">{{ item.TEN_KHACH_HANG }}</span>
              </th>
              <th v-else-if="item.TEN_DO_UU_TIEN !== 'cao'">
                <span class="w-tkh">{{ item.TEN_KHACH_HANG }}</span>
              </th>
              <th
                v-if="item.TEN_DO_UU_TIEN === 'cao'"
                v-bind:class="{ high: isHigh }"
              >{{ item.TEN_DO_UU_TIEN }}</th>
              <th v-else-if="item.TEN_DO_UU_TIEN !== 'cao'">{{ item.TEN_DO_UU_TIEN }}</th>
              <th
                v-if="item.TEN_DO_UU_TIEN === 'cao'"
                v-bind:class="{ high: isHigh }"
              >{{ item.THUC_HIEN }}</th>
              <th v-else-if="item.TEN_DO_UU_TIEN !== 'cao'">{{ item.THUC_HIEN }}</th>
              <th
                v-if="item.TEN_DO_UU_TIEN === 'cao'"
                v-bind:class="{ high: isHigh }"
              >{{ item.GIAI_DOAN_HIEN_TAI }}</th>
              <th v-else-if="item.TEN_DO_UU_TIEN !== 'cao'">{{ item.GIAI_DOAN_HIEN_TAI }}</th>
              <th
                v-if="item.TEN_DO_UU_TIEN === 'cao'"
                v-bind:class="{ high: isHigh }"
              >{{ item.DON_VI_THUC_HIEN }}</th>
              <th v-else-if="item.TEN_DO_UU_TIEN !== 'cao'">{{ item.DON_VI_THUC_HIEN }}</th>
              <th
                v-if="item.TEN_DO_UU_TIEN === 'cao'"
                v-bind:class="{ high: isHigh }"
              >{{ item.NGAY_BAT_DAU }}</th>
              <th v-else-if="item.TEN_DO_UU_TIEN !== 'cao'">{{ item.NGAY_BAT_DAU }}</th>
            </tr>
          </table>
        </div>
        <el-pagination
          @current-change="handleCurrentChange"
          :current-page.sync="currentPage"
          :page-size="perPage"
          :total="totalReport"
          layout="total, prev, pager, next"
        ></el-pagination>
      </div>
    </div>
    <div class="loading"></div>
  </div>
</template>
<script>
import Vue from "vue";
import $ from "jquery";
import moment from "moment";
import { removeSpace } from "../../utils/common";
import responsitories from "../../service/factory/reponsitoryfactory";
const reportResponsitories = responsitories.get("report");
import pagination from "../pagination/pagination";

export default {
  name: "ReportDasboard",
  data() {
    return {
      sum: [
        {
          name: "Tổng yêu cầu",
          value: 100,
          children: [],
        },
        {
          name: "Tổng hoàn tất",
          value: 90,
          children: [
            {
              name: "Đúng hạn",
              value: 85,
            },
            {
              name: "Trễ hạn",
              value: 5,
            },
          ],
        },
        {
          name: "Tổng tồn",
          value: 10,
          children: [
            {
              name: "Đúng hạn",
              value: 8,
            },
            {
              name: "Trễ hạn",
              value: 2,
            },
          ],
        },
      ],
      title: {
        STT: "STT",
        NGAY_BAT_DAU: "DATE_CREATED",
        TEN_KHACH_HANG: "CUSTOMER",
        TEN_DO_UU_TIEN: "PRIORITY",
        THUC_HIEN: "STATUS",
        GIAI_DOAN_HIEN_TAI: "CURRENT_STAGE",
        DON_VI_THUC_HIEN: "EXECUTION_UNIT",
      },
      isHigh: true,
      pickerOptions: {
        shortcuts: [
          {
            text: this.$i18n.t("LAST_WEEK"),
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
              picker.$emit("pick", [start, end]);
            },
          },
          {
            text: this.$i18n.t("LAST_MONTH"),
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 30);
              picker.$emit("pick", [start, end]);
            },
          },
          {
            text: this.$i18n.t("LAST_3_MONTHS"),
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 90);
              picker.$emit("pick", [start, end]);
            },
          },
        ],
      },
      rangeDate: "",
      startDate: "START_DATE",
      endDate: "END_DATE",
      inputDate: "",
      inputCustomer: "",
      select_stage: "SELECT_STAGE",
      stages: [
        {
          value: 6,
          label: this.$i18n.t("REQUEST"),
        },
        {
          value: 7,
          label: this.$i18n.t("DESIGN"),
        },
        {
          value: 8,
          label: this.$i18n.t("BUILD_WEBSITE_DEMO"),
        },
        {
          value: 9,
          label: this.$i18n.t("BUILD_WEBSITE_OFFICIAL"),
        },
      ],
      selectedStage: "",
      reportList: [],
      pageOfItems: [],
      perPage: 15,
      currentPage: 1,
      totalReport: 0,
    };
  },
  methods: {
    hideLoading() {
      $(".loading").hide(100);
    },
    async getDasboard() {
      $(".loading").show(100);
      setTimeout(() => {
        $(".loading").hide(100);
      }, 500);
      var self = this;
      this.currentPage = 1;
      this.reportList = [];
      var rangeDate = this.rangeDate;
      var perPage = this.perPage;
      var currentPage = this.currentPage;
      var endDate = new Date();
      var startDate = new Date();
      startDate.setTime(startDate.getTime() - 3600 * 1000 * 24 * 7);
      if (rangeDate) {
        startDate = moment(rangeDate[0]).format("YYYY-MM-DD");
        endDate = moment(rangeDate[1]).format("YYYY-MM-DD");
      } else {
        startDate = moment(startDate).format("YYYY-MM-DD");
        endDate = moment(endDate).format("YYYY-MM-DD");
      }
      var data = {
        v_FROM_DATE: startDate,
        v_TO_DATE: endDate,
      };
      await reportResponsitories
        .getReportDasboard(data)
        .then(function (response) {
          if (response && response.data.success) {
            self.pageOfItems = [];
            response.data.data.forEach((report, index) => {
              self.reportList.push({
                STT: index + 1,
                NGAY_BAT_DAU: report.NGAY_BAT_DAU,
                TEN_KHACH_HANG: report.TEN_KHACH_HANG,
                TEN_DO_UU_TIEN: report.TEN_DO_UU_TIEN,
                THUC_HIEN: report.THUC_HIEN,
                GIAI_DOAN_HIEN_TAI: report.GIAI_DOAN_HIEN_TAI,
                DON_VI_THUC_HIEN: report.DON_VI_THUC_HIEN,
              });
              if (
                index + 1 > perPage * (currentPage - 1) &&
                index + 1 < perPage * currentPage + 1
              ) {
                self.pageOfItems.push(report);
              }
            });
            self.totalReport = response.data.data.length;
            $(".loading").hide(100);
          } else {
            Vue.$toast.error(response.data.message);
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
    handleCurrentChange(val) {
      this.pageOfItems = [];
      this.reportList.forEach((report) => {
        if (
          report.STT > this.perPage * (val - 1) &&
          report.STT < this.perPage * val + 1
        ) {
          this.pageOfItems.push(report);
        }
      });
    },
  },
  mounted() {
    this.hideLoading();
  },
  created() {
    this.getDasboard();
  },
};
</script>
<style scoped>
@import url(./reportDasboard.css);
</style>
