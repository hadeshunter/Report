<template>
  <div class="k main-form">
    <div class="k wf main-container">
      <div class="k wf card">
        <div class="k wf card-title">
          <span>{{ $t("REPORT_LIST_SLA").toUpperCase() }}</span>
        </div>

        <div class="k wf flex-row">
          <div class="k part">
            <div class="demonstration">{{ $t("PICK_MONTH") }}</div>
            <el-date-picker
              class="date-time-picker"
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
            <br class="mobile" />
            <el-button type="primary" @click="getReportSLA()">
              <i class="fas fa-database"></i>
              {{ $t("GET_DATA") }}
            </el-button>
            <vue-excel-xlsx
              class="primary"
              :data="reportList"
              :columns="columns"
              :filename="excel_file_name"
              :sheetname="sheet_name"
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
              <th class="table-title">
                {{ this.$i18n.t(title.TEN_KHACH_HANG) }}
              </th>
              <th class="table-title">{{ this.$i18n.t(title.NGAY_TAO) }}</th>
              <th class="table-title">
                {{ this.$i18n.t(title.TEN_DO_UU_TIEN) }}
              </th>
              <th class="table-title">{{ this.$i18n.t(title.TONG_SLA) }}</th>
              <th class="table-title">
                {{ this.$i18n.t(title.TONG_THUC_HIEN) }}
              </th>
              <th class="color-blue">
                {{ this.$i18n.t(title.NGUOI_TIEP_NHAN_KSYC) }}
              </th>
              <th class="color-blue">
                {{ this.$i18n.t(title.THOI_GIAN_BAT_DAU_KSYC) }}
              </th>
              <th class="color-blue">
                {{ this.$i18n.t(title.THOI_GIAN_KET_THUC_KSYC) }}
              </th>
              <th class="color-blue">
                {{ this.$i18n.t(title.SLA_KSYC) }} (ph)
              </th>
              <th class="color-blue">
                {{ this.$i18n.t(title.THOI_GIAN_SLA_KSYC_CHITIET) }} (ph)
              </th>
              <th class="color-blue">
                {{ this.$i18n.t(title.DANH_GIA_SLA_KSYC) }}
              </th>
              <th class="color-green">
                {{ this.$i18n.t(title.NGUOI_TIEP_NHAN_TKHD) }}
              </th>
              <th class="color-green">
                {{ this.$i18n.t(title.THOI_GIAN_BAT_DAU_TKHD) }}
              </th>
              <th class="color-green">
                {{ this.$i18n.t(title.THOI_GIAN_KET_THUC_TKHD) }}
              </th>
              <th class="color-green">
                {{ this.$i18n.t(title.SLA_TKHD) }} (ph)
              </th>
              <th class="color-green">
                {{ this.$i18n.t(title.THOI_GIAN_SLA_TKHD_CHITIET) }} (ph)
              </th>
              <th class="color-green">
                {{ this.$i18n.t(title.DANH_GIA_SLA_TKHD) }}
              </th>
              <th class="color-gold">
                {{ this.$i18n.t(title.NGUOI_TIEP_NHAN_DWST) }}
              </th>
              <th class="color-gold">
                {{ this.$i18n.t(title.THOI_GIAN_BAT_DAU_DWST) }}
              </th>
              <th class="color-gold">
                {{ this.$i18n.t(title.THOI_GIAN_KET_THUC_DWST) }}
              </th>
              <th class="color-gold">
                {{ this.$i18n.t(title.SLA_DWST) }} (ph)
              </th>
              <th class="color-gold">
                {{ this.$i18n.t(title.THOI_GIAN_SLA_DWST_CHITIET) }} (ph)
              </th>
              <th class="color-gold">
                {{ this.$i18n.t(title.DANH_GIA_SLA_DWST) }}
              </th>
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
              <th class="color-blue"></th>
              <th class="color-blue"></th>
              <th class="color-blue"></th>
              <th class="color-blue"></th>
              <th class="color-blue"></th>
              <th class="color-blue"></th>
              <th class="color-green"></th>
              <th class="color-green"></th>
              <th class="color-green"></th>
              <th class="color-green"></th>
              <th class="color-green"></th>
              <th class="color-green"></th>
              <th class="color-gold"></th>
              <th class="color-gold"></th>
              <th class="color-gold"></th>
              <th class="color-gold"></th>
              <th class="color-gold"></th>
              <th class="color-gold"></th>
            </tr>
            <tr
              v-for="(item, index) in pageOfItems"
              :key="index"
              @click="getDetail(item)"
            >
              <th class="fs-14">{{ item.STT }}</th>
              <th class="fs-14">{{ item.ID_YEU_CAU }}</th>
              <th class="fs-14">
                <span class="w-tkh">{{ item.TEN_KHACH_HANG }}</span>
              </th>
              <th class="fs-14">{{ item.NGAY_TAO }}</th>
              <th class="fs-14">{{ item.TEN_DO_UU_TIEN }}</th>
              <th class="fs-14">{{ item.TONG_SLA }}</th>
              <th class="fs-14">{{ item.TONG_THUC_HIEN }}</th>
              <th class="fs-14">{{ item.NGUOI_TIEP_NHAN_KSYC }}</th>
              <th class="fs-14">{{ item.THOI_GIAN_BAT_DAU_KSYC }}</th>
              <th class="fs-14">{{ item.THOI_GIAN_KET_THUC_KSYC }}</th>
              <th class="fs-14">{{ item.SLA_KSYC }}</th>
              <th class="fs-14">{{ item.THOI_GIAN_SLA_KSYC_CHITIET }}</th>
              <th
                v-if="item.DANH_GIA_SLA_KSYC === 2"
                v-bind:class="{ high: true, 'fs-14': true }"
              >
                {{ getReview(item.DANH_GIA_SLA_KSYC) }}
              </th>
              <th v-else-if="item.DANH_GIA_SLA_KSYC !== 2" class="fs-14">
                {{ getReview(item.DANH_GIA_SLA_KSYC) }}
              </th>
              <th class="fs-14">{{ item.NGUOI_TIEP_NHAN_TKHD }}</th>
              <th class="fs-14">{{ item.THOI_GIAN_BAT_DAU_TKHD }}</th>
              <th class="fs-14">{{ item.THOI_GIAN_KET_THUC_TKHD }}</th>
              <th class="fs-14">{{ item.SLA_TKHD }}</th>
              <th class="fs-14">{{ item.THOI_GIAN_SLA_TKHD_CHITIET }}</th>
              <th
                v-if="item.DANH_GIA_SLA_TKHD === 2"
                v-bind:class="{ high: true, 'fs-14': true }"
              >
                {{ getReview(item.DANH_GIA_SLA_TKHD) }}
              </th>
              <th v-else-if="item.DANH_GIA_SLA_TKHD !== 2" class="fs-14">
                {{ getReview(item.DANH_GIA_SLA_TKHD) }}
              </th>
              <th class="fs-14">{{ item.NGUOI_TIEP_NHAN_DWST }}</th>
              <th class="fs-14">{{ item.THOI_GIAN_BAT_DAU_DWST }}</th>
              <th class="fs-14">{{ item.THOI_GIAN_KET_THUC_DWST }}</th>
              <th class="fs-14">{{ item.SLA_DWST }}</th>
              <th class="fs-14">{{ item.THOI_GIAN_SLA_DWST_CHITIET }}</th>
              <th
                v-if="item.DANH_GIA_SLA_DWST === 2"
                v-bind:class="{ high: true, 'fs-14': true }"
              >
                {{ getReview(item.DANH_GIA_SLA_DWST) }}
              </th>
              <th v-else-if="item.DANH_GIA_SLA_DWST !== 2" class="fs-14">
                {{ getReview(item.DANH_GIA_SLA_DWST) }}
              </th>
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
      <el-dialog
        :visible.sync="dialogDetailVisible"
        fullscreen
        :show-close="false"
      >
        <div slot="title" class="wf">
          <span class="dialog-title">{{ dialogTitle }}</span>
          <button
            class="button-close primary"
            @click="dialogDetailVisible = false"
          >
            {{ this.$i18n.t("CLOSE") }}
          </button>
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
import { removeSpace, getSLAReview } from "../../utils/common";
import responsitories from "../../service/factory/reponsitoryfactory";
const reportResponsitories = responsitories.get("report");
import pagination from "../pagination/pagination";

export default {
  name: "ReportSLA",
  data() {
    return {
      title: {
        STT: "STT",
        ID_YEU_CAU: "ID_YEU_CAU",
        NGAY_TAO: "DATE_CREATED",
        TEN_KHACH_HANG: "CUSTOMER",
        TEN_DO_UU_TIEN: "PRIORITY",
        NGUOI_TIEP_NHAN_KSYC: "NGUOI_TIEP_NHAN_KSYC",
        NGUOI_TIEP_NHAN_TKHD: "NGUOI_TIEP_NHAN_TKHD",
        NGUOI_TIEP_NHAN_DWST: "NGUOI_TIEP_NHAN_DWST",
        TONG_SLA: "TONG_SLA",
        TONG_THUC_HIEN: "TONG_THUC_HIEN",
        THOI_GIAN_BAT_DAU_KSYC: "THOI_GIAN_BAT_DAU_KSYC",
        THOI_GIAN_KET_THUC_KSYC: "THOI_GIAN_KET_THUC_KSYC",
        SLA_KSYC: "SLA_KSYC",
        THOI_GIAN_SLA_KSYC_CHITIET: "THOI_GIAN_SLA_KSYC_CHITIET",
        DANH_GIA_SLA_KSYC: "DANH_GIA_SLA_KSYC",
        THOI_GIAN_BAT_DAU_TKHD: "THOI_GIAN_BAT_DAU_TKHD",
        THOI_GIAN_KET_THUC_TKHD: "THOI_GIAN_KET_THUC_TKHD",
        SLA_TKHD: "SLA_TKHD",
        THOI_GIAN_SLA_TKHD_CHITIET: "THOI_GIAN_SLA_TKHD_CHITIET",
        DANH_GIA_SLA_TKHD: "DANH_GIA_SLA_TKHD",
        THOI_GIAN_BAT_DAU_DWST: "THOI_GIAN_BAT_DAU_DWST",
        THOI_GIAN_KET_THUC_DWST: "THOI_GIAN_KET_THUC_DWST",
        SLA_DWST: "SLA_DWST",
        THOI_GIAN_SLA_DWST_CHITIET: "THOI_GIAN_SLA_DWST_CHITIET",
        DANH_GIA_SLA_DWST: "DANH_GIA_SLA_DWST",
        THOI_GIAN_BAT_DAU_DWSCT: "THOI_GIAN_BAT_DAU_DWSCT",
        THOI_GIAN_KET_THUC_DWSCT: "THOI_GIAN_KET_THUC_DWSCT",
        SLA_DWSCT: "SLA_DWSCT",
        DANH_GIA_SLA_DWSCT: "DANH_GIA_SLA_DWSCT"
      },
      columns: [
        {
          label: "STT",
          field: "STT"
        },
        {
          label: "ID_YEU_CAU",
          field: "ID_YEU_CAU"
        },
        {
          label: "NGAY_TAO",
          field: "NGAY_TAO"
        },
        {
          label: "TEN_KHACH_HANG",
          field: "TEN_KHACH_HANG"
        },
        {
          label: "TEN_DO_UU_TIEN",
          field: "TEN_DO_UU_TIEN"
        },
        {
          label: "TONG_SLA",
          field: "TONG_SLA"
        },
        {
          label: "TONG_THUC_HIEN",
          field: "TONG_THUC_HIEN"
        },
        {
          label: "NGUOI_TIEP_NHAN_KSYC",
          field: "NGUOI_TIEP_NHAN_KSYC"
        },
        {
          label: "THOI_GIAN_BAT_DAU_KSYC",
          field: "THOI_GIAN_BAT_DAU_KSYC"
        },
        {
          label: "THOI_GIAN_KET_THUC_KSYC",
          field: "THOI_GIAN_KET_THUC_KSYC"
        },
        {
          label: "SLA_KSYC",
          field: "SLA_KSYC"
        },
        {
          label: "TG_DA_THUC_HIEN_KSYC",
          field: "THOI_GIAN_SLA_KSYC_CHITIET"
        },
        {
          label: "DANH_GIA_SLA_KSYC",
          field: "DANH_GIA_SLA_KSYC",
          dataFormat: this.getReview
        },
        {
          label: "NGUOI_TIEP_NHAN_TKHD",
          field: "NGUOI_TIEP_NHAN_TKHD"
        },
        {
          label: "THOI_GIAN_BAT_DAU_TKHD",
          field: "THOI_GIAN_BAT_DAU_TKHD"
        },
        {
          label: "THOI_GIAN_KET_THUC_TKHD",
          field: "THOI_GIAN_KET_THUC_TKHD"
        },
        {
          label: "SLA_TKHD",
          field: "SLA_TKHD"
        },
        {
          label: "TG_DA_THUC_HIEN_TKHD",
          field: "THOI_GIAN_SLA_TKHD_CHITIET"
        },
        {
          label: "DANH_GIA_SLA_TKHD",
          field: "DANH_GIA_SLA_TKHD",
          dataFormat: this.getReview
        },
        {
          label: "NGUOI_TIEP_NHAN_DWST",
          field: "NGUOI_TIEP_NHAN_DWST"
        },
        {
          label: "THOI_GIAN_BAT_DAU_DWST",
          field: "THOI_GIAN_BAT_DAU_DWST"
        },
        {
          label: "THOI_GIAN_KET_THUC_DWST",
          field: "THOI_GIAN_KET_THUC_DWST"
        },
        {
          label: "SLA_DWST",
          field: "SLA_DWST"
        },
        {
          label: "TG_DA_THUC_HIEN_DWST",
          field: "THOI_GIAN_SLA_DWST_CHITIET"
        },
        {
          label: "DANH_GIA_SLA_DWST",
          field: "DANH_GIA_SLA_DWST",
          dataFormat: this.getReview
        }
      ],
      excel_file_name: "THONG_KE_SLA_CHI_TIET",
      sheet_name: "total_SLA",
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
          label: this.$i18n.t("REQUEST")
        },
        {
          value: 7,
          label: this.$i18n.t("DESIGN")
        },
        {
          value: 8,
          label: this.$i18n.t("BUILD_WEBSITE_DEMO")
        }
      ],
      SLA_review: "SLA_REVIEW",
      SLA_reviews: [
        {
          value: 1,
          label: "Đúng hạn"
        },
        {
          value: 2,
          label: "Trễ hạn"
        },
        {
          value: 3,
          label: "Chưa bắt đầu"
        },
        {
          value: 4,
          label: "Chưa kết thúc"
        }
      ],
      selectedStage: 6,
      selectedReview: 2,
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
      list_detail_DWSCT: []
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
      var perPage = this.perPage;
      var v_ID_NHOM_GIAI_DOAN = 18;
      var v_YEAR = Number(moment(this.month).format("YYYY"));
      var v_MONTH = Number(moment(this.month).format("MM"));
      var insert1 = {
        v_ID_NHOM_GIAI_DOAN,
        v_YEAR,
        v_MONTH,
        v_ID_GIAI_DOAN: 6
      };
      var insert2 = {
        v_ID_NHOM_GIAI_DOAN,
        v_YEAR,
        v_MONTH,
        v_ID_GIAI_DOAN: 7
      };
      var insert3 = {
        v_ID_NHOM_GIAI_DOAN,
        v_YEAR,
        v_MONTH,
        v_ID_GIAI_DOAN: 8
      };
      var data = {
        v_ID_NHOM_GIAI_DOAN,
        v_YEAR,
        v_MONTH,
        v_ID_GIAI_DOAN: this.selectedStage,
        v_DANH_GIA: this.selectedReview
      };
      await reportResponsitories
        .insertSLADetail1st(insert1)
        .then(function(response) {
          if (response && response.data.success) {
            Vue.$toast.success(response.data.message);
          } else {
            Vue.$toast.error(response.data.message);
            $(".loading").hide(100);
          }
        })
        .catch(error => {
          var err = removeSpace(error.message.toUpperCase());
          Vue.$toast.error(
            this.$i18n.te(err) ? this.$i18n.t(err) : error.message
          );
          $(".loading").hide(100);
        });
      await reportResponsitories
        .insertSLADetail2nd(insert2)
        .then(function(response) {
          if (response && response.data.success) {
            Vue.$toast.success(response.data.message);
          } else {
            Vue.$toast.error(response.data.message);
            $(".loading").hide(100);
          }
        })
        .catch(error => {
          var err = removeSpace(error.message.toUpperCase());
          Vue.$toast.error(
            this.$i18n.te(err) ? this.$i18n.t(err) : error.message
          );
          $(".loading").hide(100);
        });
      await reportResponsitories
        .insertSLADetail3rd(insert3)
        .then(function(response) {
          if (response && response.data.success) {
            Vue.$toast.success(response.data.message);
          } else {
            Vue.$toast.error(response.data.message);
            $(".loading").hide(100);
          }
        })
        .catch(error => {
          var err = removeSpace(error.message.toUpperCase());
          Vue.$toast.error(
            this.$i18n.te(err) ? this.$i18n.t(err) : error.message
          );
          $(".loading").hide(100);
        });
      await reportResponsitories
        .filterByGD_DG(data)
        .then(function(response) {
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
                  value: report.TEN_KHACH_HANG
                })
              );
              if (index < perPage) {
                self.pageOfItems.push(
                  Object.assign(report, {
                    TONG_SLA: totalSla,
                    TONG_THUC_HIEN: totalActual,
                    value: report.TEN_KHACH_HANG
                  })
                );
              }
            });
            self.totalReport = response.data.data.length;
            $(".loading").hide(100);
            Vue.$toast.success(response.data.message);
          } else {
            Vue.$toast.error(response.data.message);
            $(".loading").hide(100);
          }
        })
        .catch(error => {
          var err = removeSpace(error.message.toUpperCase());
          Vue.$toast.error(
            this.$i18n.te(err) ? this.$i18n.t(err) : error.message
          );
          $(".loading").hide(100);
        });
    },
    getReview(id) {
      return getSLAReview(id);
    },
    handleCurrentChange(val) {
      this.pageOfItems = [];
      this.reportList.forEach(report => {
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
      return report => {
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
      var data = {
        v_ID_NHOM_GIAI_DOAN: 18,
        v_TEN_KHACH_HANG: inputCustomer
      };
      if (inputCustomer) {
        reportResponsitories
          .getFilterByTKH(data)
          .then(function(response) {
            if (response.data.success) {
              self.pageOfItems = [];
              response.data.data.forEach(report => {
                var totalSla =
                  report.SLA_KSYC + report.SLA_TKHD + report.SLA_DWST;
                var totalActual =
                  Number(report.THOI_GIAN_SLA_KSYC_CHITIET) +
                  Number(report.THOI_GIAN_SLA_TKHD_CHITIET) +
                  Number(report.THOI_GIAN_SLA_DWST_CHITIET);
                self.pageOfItems.push(
                  Object.assign(report, {
                    TONG_SLA: totalSla,
                    TONG_THUC_HIEN: totalActual
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
          .catch(error => {
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
      this.btnIDLoading = true;
      if (typeof inputID !== Number) {
        var data = {
          v_ID_NHOM_GIAI_DOAN: 18,
          v_ID_YEU_CAU: Number(inputID)
        };
        reportResponsitories
          .getFilterByID(data)
          .then(function(response) {
            if (response.data.success) {
              self.pageOfItems = [];
              response.data.data.forEach(report => {
                var totalSla =
                  report.SLA_KSYC + report.SLA_TKHD + report.SLA_DWST;
                var totalActual =
                  Number(report.THOI_GIAN_SLA_KSYC_CHITIET) +
                  Number(report.THOI_GIAN_SLA_TKHD_CHITIET) +
                  Number(report.THOI_GIAN_SLA_DWST_CHITIET);
                self.pageOfItems.push(
                  Object.assign(report, {
                    TONG_SLA: totalSla,
                    TONG_THUC_HIEN: totalActual
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
          .catch(error => {
            var err = removeSpace(error.message.toUpperCase());
            Vue.$toast.error(
              this.$i18n.te(err) ? this.$i18n.t(err) : error.message
            );
            this.btnIDLoading = false;
            $(".loading").hide(100);
          });
      }
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
    getDetail(item) {
      var self = this;
      this.detailList = [];
      self.dialogTitle = `[${item.ID_YEU_CAU}] - ${item.TEN_KHACH_HANG}`;
      reportResponsitories
        .getThongTinYeuCau(item.ID_YEU_CAU)
        .then(function(response) {
          if (response.data.success) {
            self.list_detail_KSYC = [];
            self.list_detail_TKHD = [];
            self.list_detail_DWST = [];
            self.list_detail_DWSCT = [];
            self.detailList = response.data.data;
            self.detailList.forEach(detail => {
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
        .catch(error => {
          var err = removeSpace(error.message.toUpperCase());
          Vue.$toast.error(
            this.$i18n.te(err) ? this.$i18n.t(err) : error.message
          );
          $(".loading").hide(100);
        });
    }
  },
  mounted() {
    this.hideLoading();
  },
  created() {
    // this.getReportSLA();
  }
};
</script>
<style scoped>
@import url(./reportListSLA.css);
</style>
