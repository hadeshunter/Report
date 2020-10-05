<template>
  <div class="k main-form">
    <div class="k wf main-container">
      <div class="k wf card">
        <div class="k wf card-title">
          <span>{{ $t("OBSERVATION").toUpperCase() }}</span>
        </div>

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
            <el-select
              class="picker"
              v-model="selectedStage"
              filterable
              v-bind:placeholder="this.$i18n.t(select_stage)"
              @change="getByIdTime()"
            >
              <el-option
                v-for="item in stages"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              ></el-option>
            </el-select>
          </div>
          <div class="k part">
            <div class="demonstration">{{ $t(select_Executor) }}</div>
            <el-input
              class="input"
              v-model="selectedExecutor"
              v-bind:placeholder="this.$i18n.t(select_Executor)"
            ></el-input>
          </div>
          <div class="k part">
            <div class="demonstration">{{ $t(SLA_review) }}</div>
            <el-select
              class="picker"
              v-model="selectedReview"
              filterable
              v-bind:placeholder="this.$i18n.t(SLA_review)"
            >
              <el-option
                v-for="item in SLA_reviews"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              ></el-option>
            </el-select>
          </div>
          <div class="k part">
            <br />
            <el-button type="primary" @click="getReportSLA()">
              <i class="fas fa-database"></i>
              {{ $t("GET_DATA") }}
            </el-button>

            <vue-excel-xlsx
              class="primary"
              :data="reportList"
              :columns="columns"
              :filename="file_name"
              :sheetname="file_sheet_name"
            >
              <i class="fas fa-file-export"></i>
              <span>{{ $t("EXPORT") }}</span>
            </vue-excel-xlsx>
          </div>
        </div>

        <div class="k wf card-container">
          <table class="table">
            <tr class="table-title">
              <th class="table-title">{{ title.STT }}</th>
              <th class="table-title">{{ this.$i18n.t(title.ID_YEU_CAU) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.TEN_KHACH_HANG) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.TEN_DO_UU_TIEN) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.NGUOI_TIEP_NHAN) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.THOI_GIAN_BAT_DAU) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.THOI_GIAN_KET_THUC) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.SLA_CHUAN) }} (ph)</th>
              <th class="table-title">{{ this.$i18n.t(title.ACTUAL_TIME) }} (ph)</th>
              <th class="table-title">{{ this.$i18n.t(title.DELAY_TIME) }} (ph)</th>
              <th
                v-if="selectedReview == 0"
                class="table-title"
              >{{ this.$i18n.t(title.TINH_TRANG_PHIEU) }}</th>
            </tr>
            <tr class="table-title">
              <th class="table-title"></th>
              <th class="table-title">
                <el-input
                  class="wf"
                  autosize
                  v-bind:placeholder="this.$i18n.t(title.ID_YEU_CAU)"
                  v-model="inputID"
                  clearable
                  @keyup.native.enter="filterByID(inputID)"
                  @keyup.native="checkNullID()"
                  @clear="handleClear"
                >
                  <el-button
                    v-model="inputID"
                    type="text"
                    icon="el-icon-search"
                    slot="suffix"
                    :loading="btnIDLoading"
                    @click="filterByID(inputID)"
                  ></el-button>
                </el-input>
              </th>
              <th class="table-title">
                <el-autocomplete
                  class="wf"
                  autosize
                  v-bind:placeholder="this.$i18n.t(title.TEN_KHACH_HANG)"
                  v-model="inputCustomer"
                  :fetch-suggestions="querySearch"
                  :trigger-on-focus="false"
                  clearable
                  @keyup.native.enter="filterByTKH(inputCustomer)"
                  @keyup.native="checkNullTKH()"
                  @select="handleSelect"
                  @clear="handleClear"
                >
                  <el-button
                    v-model="inputCustomer"
                    slot="suffix"
                    type="text"
                    icon="el-icon-search"
                    :loading="btnTKHLoading"
                    @click="filterByTKH(inputCustomer)"
                  ></el-button>
                </el-autocomplete>
              </th>
              <th class="table-title"></th>
              <th class="table-title"></th>
              <th class="table-title"></th>
              <th class="table-title"></th>
              <th class="table-title"></th>
              <th class="table-title"></th>
              <th class="table-title"></th>
              <th v-if="selectedReview == 0" class="table-title"></th>
            </tr>
            <tr v-for="(item, index) in pageOfItems" :key="index" @click="getDetail(item)">
              <th class="fs-14">{{ item.STT }}</th>
              <th class="fs-14">{{ item.ID_YEU_CAU }}</th>
              <th class="fs-14">
                <span class="w-tkh">{{ item.TEN_KHACH_HANG }}</span>
              </th>
              <th class="fs-14">{{ item.TEN_DO_UU_TIEN }}</th>
              <th class="fs-14">{{ item.NGUOI_TIEP_NHAN }}</th>
              <th class="fs-14">{{ item.THOI_GIAN_BAT_DAU }}</th>
              <th class="fs-14">{{ item.THOI_GIAN_KET_THUC }}</th>
              <th class="fs-14">{{ item.SLA_CHUAN }}</th>
              <th class="fs-14">{{ item.ACTUAL_TIME }}</th>
              <th class="fs-14">{{ item.DELAY_TIME }}</th>
              <th v-if="selectedReview == 0" class="fs-14">{{ item.TINH_TRANG_PHIEU }}</th>
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
      <el-dialog :visible.sync="dialogDetailVisible" fullscreen :show-close="false">
        <div slot="title" class="wf">
          <span class="dialog-title">{{dialogTitle}}</span>
          <button
            class="button-close primary"
            @click="dialogDetailVisible=false"
          >{{this.$i18n.t("CLOSE")}}</button>
        </div>

        <div class="k wf flex-row">
          <div class="div-card">
            <el-card
              v-for="item in list_detail_KSYC"
              :key="item.ID_CHI_TIET_GIAI_DOAN"
              class="el-card"
            >
              <div class="fs-16">{{ item.MO_TA }}</div>
              <div class="fs-18">{{ item.HO_TEN }}</div>
              <div class="fs-16">{{ item.TEN_DON_VI }}</div>
              <div class="fs-16 blue1">{{ item.NGAY_BINH_LUAN }}</div>
              <el-divider></el-divider>
              <div class="fs-16">{{ item.MAU_NOI_DUNG }}</div>
              <div v-html="item.BINH_LUAN" class="fs-16">></div>
            </el-card>
          </div>
          <div class="div-card">
            <el-card
              v-for="item in list_detail_TKHD"
              :key="item.ID_CHI_TIET_GIAI_DOAN"
              class="el-card"
            >
              <div class="fs-16">{{ item.MO_TA }}</div>
              <div class="fs-18">{{ item.HO_TEN }}</div>
              <div class="fs-16">{{ item.TEN_DON_VI }}</div>
              <div class="fs-16 blue1">{{ item.NGAY_BINH_LUAN }}</div>
              <el-divider></el-divider>
              <div class="fs-16">{{ item.MAU_NOI_DUNG }}</div>
              <div v-html="item.BINH_LUAN" class="fs-16">></div>
            </el-card>
          </div>
          <div class="div-card">
            <el-card
              v-for="item in list_detail_DWST"
              :key="item.ID_CHI_TIET_GIAI_DOAN"
              class="el-card"
            >
              <div class="fs-16">{{ item.MO_TA }}</div>
              <div class="fs-18">{{ item.HO_TEN }}</div>
              <div class="fs-16">{{ item.TEN_DON_VI }}</div>
              <div class="fs-16 blue1">{{ item.NGAY_BINH_LUAN }}</div>
              <el-divider></el-divider>
              <div class="fs-16">{{ item.MAU_NOI_DUNG }}</div>
              <div v-html="item.BINH_LUAN" class="fs-16">></div>
            </el-card>
          </div>
          <div class="div-card">
            <el-card
              v-for="item in list_detail_DWSCT"
              :key="item.ID_CHI_TIET_GIAI_DOAN"
              class="el-card"
            >
              <div class="fs-16">{{ item.MO_TA }}</div>
              <div class="fs-18">{{ item.HO_TEN }}</div>
              <div class="fs-16">{{ item.TEN_DON_VI }}</div>
              <div class="fs-16 blue1">{{ item.NGAY_BINH_LUAN }}</div>
              <el-divider></el-divider>
              <div class="fs-16">{{ item.MAU_NOI_DUNG }}</div>
              <div v-html="item.BINH_LUAN" class="fs-16">></div>
            </el-card>
          </div>
        </div>
      </el-dialog>
      <div class="loading"></div>
    </div>
  </div>
</template>
<script>
import Vue from "vue";
import $ from "jquery";
import moment from "moment";
import { removeSpace } from "../../utils/common";
import responsitories from "../../service/factory/reponsitoryfactory";
const reportResponsitories = responsitories.get("report");
const timerReponsitories = responsitories.get("timer");

export default {
  name: "Observation",
  data() {
    return {
      title: {
        STT: "STT",
        ID_YEU_CAU: "ID_YEU_CAU",
        TEN_KHACH_HANG: "CUSTOMER",
        TEN_DO_UU_TIEN: "PRIORITY",
        GIAI_DOAN_HIEN_TAI: "CURRENT_STAGE",
        THOI_GIAN_BAT_DAU: "START_DATE",
        THOI_GIAN_KET_THUC: "END_DATE",
        SLA_CHUAN: "SLA_CHUAN",
        ACTUAL_TIME: "ACTUAL_TIME",
        DELAY_TIME: "DELAY_TIME",
        NGUOI_TIEP_NHAN: "NGUOI_THUC_HIEN",
        TINH_TRANG_PHIEU: "TINH_TRANG_PHIEU",
      },
      columns: [
        {
          label: "STT",
          field: "STT",
        },
        {
          label: "ID_YEU_CAU",
          field: "ID_YEU_CAU",
        },
        {
          label: "TEN_KHACH_HANG",
          field: "TEN_KHACH_HANG",
        },
        {
          label: "TEN_DO_UU_TIEN",
          field: "TEN_DO_UU_TIEN",
        },
        {
          label: "NGUOI_TIEP_NHAN",
          field: "NGUOI_TIEP_NHAN",
        },
        {
          label: "THOI_GIAN_BAT_DAU",
          field: "THOI_GIAN_BAT_DAU",
        },
        {
          label: "THOI_GIAN_KET_THUC",
          field: "THOI_GIAN_KET_THUC",
        },
        {
          label: "SLA_CHUAN",
          field: "SLA_CHUAN",
        },
        {
          label: "THOI_GIAN_THUC_HIEN",
          field: "ACTUAL_TIME",
        },
        {
          label: "THOI_GIAN_TRE",
          field: "DELAY_TIME",
        },
        {
          label: "GIAI_DOAN",
          field: "SELECTED_STAGE",
        },
        {
          label: "DANH_GIA",
          field: "REVIEW",
        },
      ],
      file_name: "THONG_KE_SLA",
      file_sheet_name: "total_SLA",
      isHigh: true,
      month: new Date(),
      pickMonth: "PICK_MONTH",
      btnIDLoading: false,
      btnTKHLoading: false,
      inputID: "",
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
      ],
      SLA_review: "SLA_REVIEW",
      SLA_reviews: [
        {
          value: 0,
          label: "Sắp trễ",
        },
        {
          value: 1,
          label: "Đúng hạn",
        },
        {
          value: 2,
          label: "Trễ hạn",
        },
        {
          value: 3,
          label: "Chưa bắt đầu",
        },
        {
          value: 4,
          label: "Chưa kết thúc",
        },
      ],
      select_Executor: "NGUOI_THUC_HIEN",
      selectedStage: 6,
      selectedExecutor: "",
      selectedReview: 0,
      reportList: [],
      pageOfItems: [],
      totalReport: 0,
      perPage: 15,
      currentPage: 1,
      dialogDetailVisible: false,
      dialogTitle: "",
      detailList: [],
      list_detail_KSYC: [],
      list_detail_TKHD: [],
      list_detail_DWST: [],
      list_detail_DWSCT: [],
      time: {},
    };
  },
  methods: {
    // showLoading() {
    //   $(".loading").show(100);
    // },
    hideLoading() {
      $(".loading").hide(100);
    },
    async getReportSLA() {
      $(".loading").show(100);
      var self = this;
      this.currentPage = 1;
      this.reportList = [];
      var selectedStage = this.selectedStage;
      var selectedReview = this.selectedReview;
      var perPage = this.perPage;
      var v_ID_NHOM_GIAI_DOAN = 18;
      var v_YEAR = Number(moment(this.month).format("YYYY"));
      var v_MONTH = Number(moment(this.month).format("MM"));
      var insert1 = {
        v_ID_NHOM_GIAI_DOAN,
        v_YEAR,
        v_MONTH,
        v_ID_GIAI_DOAN: 6,
      };
      var insert2 = {
        v_ID_NHOM_GIAI_DOAN,
        v_YEAR,
        v_MONTH,
        v_ID_GIAI_DOAN: 7,
      };
      var insert3 = {
        v_ID_NHOM_GIAI_DOAN,
        v_YEAR,
        v_MONTH,
        v_ID_GIAI_DOAN: 8,
      };

      if (this.selectedStage == 6) {
        await reportResponsitories
          .insertSLADetail1st(insert1)
          .then(function (response) {
            if (response && response.data.success) {
              Vue.$toast.success(response.data.message);
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
      } else if (this.selectedStage == 7) {
        await reportResponsitories
          .insertSLADetail2nd(insert2)
          .then(function (response) {
            if (response && response.data.success) {
              Vue.$toast.success(response.data.message);
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
      } else if (this.selectedStage == 8) {
        await reportResponsitories
          .insertSLADetail3rd(insert3)
          .then(function (response) {
            if (response && response.data.success) {
              Vue.$toast.success(response.data.message);
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
      }

      if (selectedReview == 0) {
        var data = {
          v_ID_NHOM_GIAI_DOAN,
          v_ID_GIAI_DOAN: this.selectedStage,
          v_TIME_WARNING: this.time.totaltime,
          v_TEN_NV: this.selectedExecutor,
        };
        await reportResponsitories
          .getWarningList(data)
          .then(function (response) {
            if (response.data.success) {
              self.pageOfItems = [];
              response.data.data.forEach((report, index) => {
                var delay =
                  Number(report.THOI_GIAN_THUC_HIEN) - Number(report.SLA);
                self.reportList.push(
                  Object.assign(report, {
                    value: report.TEN_KHACH_HANG,
                    id: index + 1,
                    SLA_CHUAN: Number(report.SLA),
                    ACTUAL_TIME: Number(report.THOI_GIAN_THUC_HIEN),
                    DELAY_TIME: delay > 0 ? delay : 0,
                    SELECTED_STAGE:
                      selectedStage == 6
                        ? "KSYC"
                        : selectedStage == 7
                        ? "TKHD"
                        : "DWST",
                    REVIEW:
                      selectedReview == 1
                        ? "Đúng hạn"
                        : selectedReview == 2
                        ? "Trễ hạn"
                        : selectedReview == 3
                        ? "Chưa bắt đầu"
                        : selectedReview == 4
                        ? "Chưa kết thúc"
                        : "Sắp trễ",
                    TINH_TRANG_PHIEU:
                      report.DONVI_BL_CUOI == 3
                        ? "CNTT đã phản hồi"
                        : "CNTT chưa phản hồi",
                  })
                );
                if (index < perPage) {
                  self.pageOfItems.push(
                    Object.assign(report, {
                      value: report.TEN_KHACH_HANG,
                      id: index + 1,
                      SLA_CHUAN: Number(report.SLA),
                      ACTUAL_TIME: Number(report.THOI_GIAN_THUC_HIEN),
                      DELAY_TIME: delay > 0 ? delay : 0,
                    })
                  );
                }
              });
              self.totalReport = response.data.data.length;
            }
          });
        $(".loading").hide(100);
      } else {
        var data = {
          v_ID_NHOM_GIAI_DOAN,
          v_YEAR,
          v_MONTH,
          v_ID_GIAI_DOAN: this.selectedStage,
          v_DANH_GIA: this.selectedReview,
          v_TEN_NV: this.selectedExecutor,
        };
        await reportResponsitories
          .filterByGD_DG(data)
          .then(function (response) {
            if (response && response.data.success) {
              self.pageOfItems = [];
              response.data.data.forEach((report, index) => {
                var totalSla =
                  report.SLA_KSYC + report.SLA_TKHD + report.SLA_DWST;
                var totalActual =
                  Number(report.THOI_GIAN_SLA_KSYC_CHITIET) +
                  Number(report.THOI_GIAN_SLA_TKHD_CHITIET) +
                  Number(report.THOI_GIAN_SLA_DWST_CHITIET);
                self.reportList.push(
                  Object.assign(report, {
                    TONG_SLA: totalSla,
                    TONG_THUC_HIEN: totalActual,
                    value: report.TEN_KHACH_HANG,
                    id: index + 1,
                    NGUOI_TIEP_NHAN: self.getExecutor(selectedStage, report),
                    THOI_GIAN_BAT_DAU: self.getStartDate(selectedStage, report),
                    THOI_GIAN_KET_THUC: self.getEndDate(selectedStage, report),
                    SLA_CHUAN: self.getSLAStandard(selectedStage, report),
                    ACTUAL_TIME: self.getSLAActual(selectedStage, report),
                    DELAY_TIME: self.getDelayTime(selectedStage, report),
                    SELECTED_STAGE:
                      selectedStage == 6
                        ? "KSYC"
                        : selectedStage == 7
                        ? "TKHD"
                        : "DWST",
                    REVIEW:
                      selectedReview == 1
                        ? "Đúng hạn"
                        : selectedReview == 2
                        ? "Trễ hạn"
                        : selectedReview == 3
                        ? "Chưa bắt đầu"
                        : selectedReview == 4
                        ? "Chưa kết thúc"
                        : "Sắp trễ",
                  })
                );
                if (index < perPage) {
                  self.pageOfItems.push(
                    Object.assign(report, {
                      NGUOI_TIEP_NHAN: self.getExecutor(selectedStage, report),
                      THOI_GIAN_BAT_DAU: self.getStartDate(
                        selectedStage,
                        report
                      ),
                      THOI_GIAN_KET_THUC: self.getEndDate(
                        selectedStage,
                        report
                      ),
                      SLA_CHUAN: self.getSLAStandard(selectedStage, report),
                      ACTUAL_TIME: self.getSLAActual(selectedStage, report),
                      DELAY_TIME: self.getDelayTime(selectedStage, report),
                      TONG_SLA: totalSla,
                      TONG_THUC_HIEN: totalActual,
                      value: report.TEN_KHACH_HANG,
                    })
                  );
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
      }
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
    querySearch(queryString, cb) {
      var list = this.reportList;
      var results = queryString
        ? list.filter(this.createFilter(queryString))
        : list;
      // call callback function to return suggestion objects
      cb(results);
    },
    createFilter(queryString) {
      return (report) => {
        return (
          report.value.toLowerCase().indexOf(queryString.toLowerCase()) === 0
        );
      };
    },
    handleSelect(item) {
      if (item) {
        this.pageOfItems = [];
        this.pageOfItems.push(item);
      }
    },
    handleClear() {
      var self = this;
      var perPage = this.perPage;
      var reportList = this.reportList;
      self.pageOfItems = [];
      reportList.forEach((report, index) => {
        if (index < this.perPage) {
          self.pageOfItems.push(report);
        }
      });
      self.totalReport = reportList.length;
    },
    filterByTKH(inputCustomer) {
      var self = this;
      this.btnTKHLoading = true;
      var selectedStage = this.selectedStage;
      var data = {
        v_ID_NHOM_GIAI_DOAN: 18,
        v_TEN_KHACH_HANG: inputCustomer,
      };
      if (inputCustomer) {
        reportResponsitories
          .getFilterByTKH(data)
          .then(function (response) {
            if (response.data.success) {
              self.pageOfItems = [];
              response.data.data.forEach((report) => {
                var totalSla =
                  report.SLA_KSYC + report.SLA_TKHD + report.SLA_DWST;
                var totalActual =
                  Number(report.THOI_GIAN_SLA_KSYC_CHITIET) +
                  Number(report.THOI_GIAN_SLA_TKHD_CHITIET) +
                  Number(report.THOI_GIAN_SLA_DWST_CHITIET);
                self.pageOfItems.push(
                  Object.assign(report, {
                    TONG_SLA: totalSla,
                    TONG_THUC_HIEN: totalActual,
                    NGUOI_TIEP_NHAN: self.getExecutor(selectedStage, report),
                    THOI_GIAN_BAT_DAU: self.getStartDate(selectedStage, report),
                    THOI_GIAN_KET_THUC: self.getEndDate(selectedStage, report),
                    SLA_CHUAN: self.getSLAStandard(selectedStage, report),
                    ACTUAL_TIME: self.getSLAActual(selectedStage, report),
                    DELAY_TIME: self.getDelayTime(selectedStage, report),
                  })
                );
              });
              self.totalReport = response.data.data.length;
              Vue.$toast.success(response.data.message);
              self.btnTKHLoading = false;
            } else {
              Vue.$toast.error(response.data.message);
              self.btnTKHLoading = false;
            }
          })
          .catch((error) => {
            var err = removeSpace(error.message.toUpperCase());
            Vue.$toast.error(
              this.$i18n.te(err) ? this.$i18n.t(err) : error.message
            );
            this.btnTKHLoading = false;
            $(".loading").hide(100);
          });
      } else {
        setTimeout(() => {
          this.btnTKHLoading = false;
        }, 500);
      }
    },
    filterByID(inputID) {
      var self = this;
      var selectedStage = this.selectedStage;
      this.btnIDLoading = true;
      if (typeof inputID !== Number) {
        var data = {
          v_ID_NHOM_GIAI_DOAN: 18,
          v_ID_YEU_CAU: Number(inputID),
        };
        reportResponsitories
          .getFilterByID(data)
          .then(function (response) {
            if (response.data.success) {
              self.pageOfItems = [];
              response.data.data.forEach((report) => {
                var totalSla =
                  report.SLA_KSYC + report.SLA_TKHD + report.SLA_DWST;
                var totalActual =
                  Number(report.THOI_GIAN_SLA_KSYC_CHITIET) +
                  Number(report.THOI_GIAN_SLA_TKHD_CHITIET) +
                  Number(report.THOI_GIAN_SLA_DWST_CHITIET);
                self.pageOfItems.push(
                  Object.assign(report, {
                    TONG_SLA: totalSla,
                    TONG_THUC_HIEN: totalActual,
                    NGUOI_TIEP_NHAN: self.getExecutor(selectedStage, report),
                    THOI_GIAN_BAT_DAU: self.getStartDate(selectedStage, report),
                    THOI_GIAN_KET_THUC: self.getEndDate(selectedStage, report),
                    SLA_CHUAN: self.getSLAStandard(selectedStage, report),
                    ACTUAL_TIME: self.getSLAActual(selectedStage, report),
                    DELAY_TIME: self.getDelayTime(selectedStage, report),
                  })
                );
              });
              self.totalReport = response.data.data.length;
              Vue.$toast.success(response.data.message);
              self.btnIDLoading = false;
            } else {
              Vue.$toast.error(response.data.message);
              self.btnIDLoading = false;
            }
          })
          .catch((error) => {
            var err = removeSpace(error.message.toUpperCase());
            Vue.$toast.error(
              this.$i18n.te(err) ? this.$i18n.t(err) : error.message
            );
            this.btnIDLoading = false;
            $(".loading").hide(100);
          });
      }
    },
    getExecutor(selectedStage, item) {
      if (selectedStage == 6) {
        return item.NGUOI_TIEP_NHAN_KSYC;
      } else if (selectedStage == 7) {
        return item.NGUOI_TIEP_NHAN_TKHD;
      } else if (selectedStage == 8) {
        return item.NGUOI_TIEP_NHAN_DWST;
      }
    },
    getStartDate(selectedStage, item) {
      if (selectedStage == 6) {
        return item.THOI_GIAN_BAT_DAU_KSYC;
      } else if (selectedStage == 7) {
        return item.THOI_GIAN_BAT_DAU_TKHD;
      } else if (selectedStage == 8) {
        return item.THOI_GIAN_BAT_DAU_DWST;
      }
    },
    getEndDate(selectedStage, item) {
      if (selectedStage == 6) {
        return item.THOI_GIAN_KET_THUC_KSYC;
      } else if (selectedStage == 7) {
        return item.THOI_GIAN_KET_THUC_TKHD;
      } else if (selectedStage == 8) {
        return item.THOI_GIAN_KET_THUC_DWST;
      }
    },
    getSLAStandard(selectedStage, item) {
      if (selectedStage == 6) {
        return item.SLA_KSYC;
      } else if (selectedStage == 7) {
        return item.SLA_TKHD;
      } else if (selectedStage == 8) {
        return item.SLA_DWST;
      }
    },
    getSLAActual(selectedStage, item) {
      if (selectedStage == 6) {
        return Number(item.THOI_GIAN_SLA_KSYC_CHITIET);
      } else if (selectedStage == 7) {
        return Number(item.THOI_GIAN_SLA_TKHD_CHITIET);
      } else if (selectedStage == 8) {
        return Number(item.THOI_GIAN_SLA_DWST_CHITIET);
      }
    },
    getDelayTime(selectedStage, item) {
      if (selectedStage == 6) {
        return Number(item.THOI_GIAN_SLA_KSYC_CHITIET) - Number(item.SLA_KSYC) >
          0
          ? Number(item.THOI_GIAN_SLA_KSYC_CHITIET) - Number(item.SLA_KSYC)
          : 0;
      } else if (selectedStage == 7) {
        return Number(item.THOI_GIAN_SLA_TKHD_CHITIET) - Number(item.SLA_TKHD) >
          0
          ? Number(item.THOI_GIAN_SLA_TKHD_CHITIET) - Number(item.SLA_TKHD)
          : 0;
      } else if (selectedStage == 8) {
        return Number(item.THOI_GIAN_SLA_DWST_CHITIET) - Number(item.SLA_DWST) >
          0
          ? Number(item.THOI_GIAN_SLA_DWST_CHITIET) - Number(item.SLA_DWST)
          : 0;
      }
    },
    getDetail(item) {
      var self = this;
      this.detailList = [];
      self.dialogTitle = `[${item.ID_YEU_CAU}] - ${item.TEN_KHACH_HANG}`;
      reportResponsitories
        .getThongTinYeuCau(item.ID_YEU_CAU)
        .then(function (response) {
          if (response.data.success) {
            self.list_detail_KSYC = [];
            self.list_detail_TKHD = [];
            self.list_detail_DWST = [];
            self.list_detail_DWSCT = [];
            self.detailList = response.data.data;
            self.detailList.forEach((detail) => {
              if (detail.ID_GIAI_DOAN == 6) {
                self.list_detail_KSYC.push(detail);
              } else if (detail.ID_GIAI_DOAN == 7) {
                self.list_detail_TKHD.push(detail);
              } else if (detail.ID_GIAI_DOAN == 8) {
                self.list_detail_DWST.push(detail);
              } else if (detail.ID_GIAI_DOAN == 9) {
                self.list_detail_DWSCT.push(detail);
              }
            });
            self.dialogDetailVisible = true;
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
    checkNullID() {
      var self = this;
      if (this.inputID == null || this.inputID == "") {
        var perPage = this.perPage;
        var reportList = this.reportList;
        this.pageOfItems = [];
        reportList.forEach((report, index) => {
          if (index < this.perPage) {
            this.pageOfItems.push(report);
          }
        });
        this.totalReport = reportList.length;
      }
    },
    checkNullTKH() {
      var self = this;
      if (this.inputCustomer == null || this.inputCustomer == "") {
        var perPage = this.perPage;
        var reportList = this.reportList;
        this.pageOfItems = [];
        reportList.forEach((report, index) => {
          if (index < this.perPage) {
            this.pageOfItems.push(report);
          }
        });
        this.totalReport = reportList.length;
      }
    },
    getByIdTime() {
      var self = this;
      var timeid;
      if (this.selectedStage == 6) {
        timeid = 1;
      } else if (this.selectedStage == 7) {
        timeid = 2;
      } else if (this.selectedStage == 8) {
        timeid = 3;
      }
      timerReponsitories.getById(timeid).then(function (response) {
        if (response.data.success) {
          self.time = response.data.data;
          self.time.totaltime = self.time.totaltime / 60;
        } else {
          Vue.$toast.error(response.data.message);
        }
      });
    },
    getUserByName(selectedExecutor) {
      var data = {
        v_ID_DON_VI: 3,
        name: this.selectedExecutor,
      };
    },
  },
  mounted() {
    this.hideLoading();
  },
  created() {
    // this.getReportSLA();
    this.getByIdTime();
  },
};
</script>
<style scoped>
@import url(./observation.css);
</style>
