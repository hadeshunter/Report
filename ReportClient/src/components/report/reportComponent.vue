<template>
  <div class="k main-form">
    <div class="k wf main-container">
      <div class="k wf card">
        <div class="k wf card-title">
          <span>{{ $t("REPORT").toUpperCase() }}</span>
        </div>
        <div class="k wf flex-row">
          <div class="k part">
            <div class="demonstration">{{ $t("PICK_MONTH") }}</div>
            <el-date-picker
              class="picker"
              format="MM-yyyy"
              value-format="yyyyMM"
              v-model="month"
              type="month"
              v-bind:placeholder="this.$i18n.t(pickMonth)"
              @change="getReportSLA(month)"
              align="right"
            ></el-date-picker>
         
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
              <th  class="table-title">{{ this.$i18n.t(title.CHUA_TIEP_NHAN) }}</th>
              <th  class="table-title">{{ this.$i18n.t(title.DA_TIEP_NHAN) }}</th>
              <th  class="table-title">{{ this.$i18n.t(title.HOAN_TAT) }}</th>
              <th  class="table-title">{{ this.$i18n.t(title.DA_NGHIEM_THU) }}</th>
              <th  class="table-title">{{ this.$i18n.t(title.DA_HUY) }}</th>
            </tr>
             <tr v-for="(totalsum, index) in totalsum" :key="index">
              <th  class="fs-145">{{ totalsum.CHUA_TIEP_NHAN }}</th>
              <th  class="fs-145">{{ totalsum.DA_TIEP_NHAN }}</th>
              <th  class="fs-145">{{ totalsum.HOAN_TAT }}</th>
              <th  class="fs-145">{{ totalsum.DA_NGHIEM_THU }}</th>
              <th  class="fs-145">{{ totalsum.DA_HUY }}</th>
            </tr>
          </table>
        </div>
        <div class="k wf card-container">
          <table class="table">
            <tr class="table-title">
              <th rowspan="2" class="table-title">{{ this.$i18n.t(title.NGUOI_TIEP_NHAN) }}</th>
              <th colspan="5" class="table-title">{{ this.$i18n.t(title.KHAO_SAT_YEU_CAU) }}</th>
              <th colspan="4" class="table-title">{{ this.$i18n.t(title.THIET_KE_HOA_DON) }}</th>
              <th colspan="4" class="table-title">{{ this.$i18n.t(title.DUNG_WEBSITE_TEST) }}</th>
            </tr>
            <tr class="table-title" >
              <th class="table-title">{{ this.$i18n.t(title.ON_TIME) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.DEADLINE) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.PENDENT) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.HAVE_NOT_SEEN) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.TOTAL) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.ON_TIME1) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.DEADLINE1) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.PENDENT1) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.TOTAL) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.ON_TIME2) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.DEADLINE2) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.PENDENT2) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.TOTAL) }}</th>
            </tr>
            
            <tr v-for="(report, index) in pageOfItems" :key="index">
              <th class="fs-145">{{ (report.NGUOI_TIEP_NHAN==null && report.GIAIDOAN==6)? "Chưa chọn Hastab KSYC" 
                : (report.NGUOI_TIEP_NHAN==null && report.GIAIDOAN==7)?"Không có giai đoạn TKHD" 
                : (report.NGUOI_TIEP_NHAN==null && report.GIAIDOAN==8)? "Không có giai đoạn DWST" : report.NGUOI_TIEP_NHAN}}</th>
              <th class="fs-145">{{ report.GIAIDOAN==6? report.DUNGHAN: ''}}</th>
              <th class="fs-145">{{ report.GIAIDOAN==6? report.TREHAN :''}}</th>
              <th class="fs-14">{{ report.GIAIDOAN==6? report.CHUAKETTHUC : ''}}</th>
              <th class="fs-14">{{ report.GIAIDOAN==6? report.CHUABATDAU : ''}}</th>
              <th class="fs-145">{{ report.GIAIDOAN==6? report.TONG : '' }}</th>
              <th class="fs-145">{{ report.GIAIDOAN==7? report.DUNGHAN : ''}}</th>
              <th class="fs-145">{{ report.GIAIDOAN==7? report.TREHAN : '' }}</th>
              <th class="fs-14">{{ report.GIAIDOAN==7? report.CHUAKETTHUC : ''}}</th>
              <th class="fs-145">{{ report.GIAIDOAN==7? report.TONG : '' }}</th>
              <th class="fs-145">{{ report.GIAIDOAN==8? report.DUNGHAN : ''}}</th>
              <th class="fs-145">{{ report.GIAIDOAN==8? report.TREHAN : '' }}</th>
              <th class="fs-14">{{ report.GIAIDOAN==8? report.CHUAKETTHUC : ''}}</th>
              <th class="fs-145">{{ report.GIAIDOAN==8? report.TONG : '' }}</th>
            </tr>
          </table>
        </div>
         <el-pagination
              @current-change="handleCurrentChange"
              :current-page.sync="currentPage"
              :page-size="perPage"
              :total="totalreport"
              layout="total, prev, pager, next"
            ></el-pagination>
      </div>
    </div>
  </div>
</template>
<script>
import Vue from "vue";
import $ from "jquery";
import moment from "moment";
import { removeSpace } from "../../utils/common";
import responsitories from "../../service/factory/reponsitoryfactory";
import reportResponsitories from "../../service/Impl/reportResponsitory";
import pagination from "../pagination/pagination";

export default {
  name: "Observation",
  data() {
    return {
      title: {
        NGUOI_TIEP_NHAN: "NGUOI_TIEP_NHAN",
        KHAO_SAT_YEU_CAU: "KHAO_SAT_YEU_CAU",
        THIET_KE_HOA_DON: "THIET_KE_HOA_DON",
        DUNG_WEBSITE_TEST: "DUNG_WEBSITE_TEST",
        ON_TIME:"ON_TIME",
        DEADLINE:"DEADLINE",
        PENDENT:"PENDENT",
        HAVE_NOT_SEEN:"HAVE_NOT_SEEN",
        ON_TIME1:"ON_TIME1",
        DEADLINE1:"DEADLINE1",
        PENDENT1:"PENDENT1",
        HAVE_NOT_SEEN1:"HAVE_NOT_SEEN1",
        ON_TIME2:"ON_TIME2",
        DEADLINE2:"DEADLINE2",
        PENDENT2:"PENDENT2",
        HAVE_NOT_SEEN2:"HAVE_NOT_SEEN2",
        TOTAL:"TOTAL",
        CHUA_TIEP_NHAN:"CHUA_TIEP_NHAN",
        DA_TIEP_NHAN:"DA_TIEP_NHAN",
        HOAN_TAT:"HOAN_TAT",
        DA_NGHIEM_THU:"DA_NGHIEM_THU",
        DA_HUY:"DA_HUY"
      },
       columns: [
        {
          label: "STT",
          field: "STT"
        },
        {
          label: "NGUOI_TIEP_NHAN",
          field: "NGUOI_TIEP_NHAN"
        },
        {
          label: "Tiếp nhận đúng hạn",
          field: "DUNG_HAN"
        },
        {
          label: "Tiếp nhận trễ hạn",
          field: "TRE_HAN"
        },
        {
          label: "Đang thực hiện",
          field: "CHUAKETTHUC"
        },
        {
          label: "Chưa tiếp nhận",
          field: "CHUABATDAU"
        },
        {
          label: "Tổng thực hiện",
          field: "TONG"
        },
      ],
      reportList:[],
      perPage: 10,
      currentPage: 1,
      pageOfItems: [],
      totalreport: 0,
      report:{
        NGUOI_TIEP_NHAN:"",
        DANH_GIA_SLA:"",
        SO_LUONG:"",
        GIAIDOAN:"",
        DUNGHAN:"",
        TREHAN:"",
        CHUABATDAU:"",
        CHUAKETTHUC:"",
        TONG:"",
        GIAIDOAN:"",
      },
      totalsum:{
        CHUA_TIEP_NHAN:"",
        DA_TIEP_NHAN:"",
        HOAN_TAT:"",
        DA_NGHIEM_THU:"",
        DA_HUY:"",
      },
      isHigh: true,
      month: new Date(),
      pickMonth: "PICK_MONTH",
      btnIDLoading: false,
      btnTKHLoading: false,
      excel_file_name: "THONG_KE_SLA",
      sheet_name: "total_SLA",
    };
  },
  methods: {
    async getReportSum(){
      var self = this;
      var v_YEAR = Number(moment(this.month).format("YYYY"));
      var v_MONTH = Number(moment(this.month).format("MM"));
      var month={
        v_YEAR,
        v_MONTH,
      };
      reportResponsitories.ReportSum(month).then(function(response){
         if (response.data.success){
           self.totalsum=response.data.data;
           self.$message({ type: 'success',message:("Lấy dữ liệu thành công")});
         }else{
           self.$message({ type: 'error',message:("Lấy dữ liệu thành công thất bại")});
         }
      }).catch((error) => {
          var err = removeSpace(error.message.toUpperCase());
          Vue.$toast.error(
            self.$message({ type: 'error',message:("Lấy dữ liệu thành công thất bại")})
          );
        });
    },
    async getReportSLA() {
      var self = this;
      this.pageOfItems = [];
      this.reportList = [],
      this.currentPage = 1;
      var perPage = this.perPage;
      var v_YEAR = Number(moment(this.month).format("YYYY"));
      var v_MONTH = Number(moment(this.month).format("MM"));
      var month={
        v_YEAR,
        v_MONTH,
      };
      reportResponsitories.ReportSLA(month).then(function(response){
        if (response.data.success){
          self.reportList = [];
            self.pageOfItems = [];
            response.data.data.forEach((report, index) => {
              self.reportList.push({
                STT: index + 1,
                NGUOI_TIEP_NHAN: report.NGUOI_TIEP_NHAN,
                DUNGHAN: report.DUNGHAN,
                TREHAN: report.TREHAN,
                CHUABATDAU: report.CHUABATDAU,
                CHUAKETTHUC: report.CHUAKETTHUC,
                TONG:report.TONG,
                GIAIDOAN:report.GIAIDOAN
              });
              if (index < perPage) {
                self.pageOfItems.push({
                STT: index + 1,
                NGUOI_TIEP_NHAN: report.NGUOI_TIEP_NHAN,
                DUNGHAN: report.DUNGHAN,
                TREHAN: report.TREHAN,
                CHUABATDAU: report.CHUABATDAU,
                CHUAKETTHUC: report.CHUAKETTHUC,
                TONG:report.TONG,
                GIAIDOAN:report.GIAIDOAN
                });
            }
            });
            self.totalreport = response.data.data.length;
        }
      }).catch((error) => {
          var err = removeSpace(error.message.toUpperCase());
          Vue.$toast.error(
            this.$i18n.te(err) ? this.$i18n.t(err) : error.message
          );
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
  created() {
    this.getReportSLA();
    this.getReportSum();
  },
  components: {
    pagination,
  },
};
</script>
<style scoped>
@import url(./report.css);
</style>
