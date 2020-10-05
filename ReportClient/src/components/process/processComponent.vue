<template>
  <div class="k main-form">
    <div class="k wf main-container">
      <div class="k wf card">
        <!----------------------------- sms list --------------------------->
        <div class="k wf card-titleb">
          <span>{{ $t("DANH_SACH_COMPANY").toUpperCase() }}</span>
        </div>
        <div class="k wf card-container">
          <table class="table">
            <tr class="table-title">
              <th class="table-title">{{ this.$i18n.t(title.ID_YEU_CAU) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.CREATEDAY) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.UPDATEDAY) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.STATUS) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.SENDTH) }}</th>
            </tr>
            <tr v-for="process in pageOfItems" :key="process.id">
              <th>{{ process.id_yeu_cau }}</th>
              <th>{{ moment(process.createday).format("DD-MM-YYYY") }}</th>
              <th>{{ moment(process.updateday).format("DD-MM-YYYY") }}</th>
              <th>{{ process.status==0?"Đã gửi" : "Chưa gửi"}}</th>
              <th>{{ process.sendth==1?"Cảnh báo" : "Trễ hạn" }}</th>
            </tr>
          </table>
        </div>
        <el-pagination
          @current-change="handleCurrentChange"
          :current-page.sync="currentPage"
          :page-size="perPage"
          :total="totalprocess"
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
import pagination from "../../components/pagination/pagination";
import processResponsitories from "../../service/Impl/processreponsitory";
const reportResponsitories = responsitories.get("report");
// const processResponsitories = responsitories.get("getAllProcess");

export default {
  name: "process",
  data() {
    return {
      title: {
        STT: "STT",
        SMSID: "SMSID",
        NAME: "NAME",
        PHONE: "PHONE",
        ACTIVI: "ACTIVI",
        SETTING: "SETTING",
        ID_GIAI_DOAN: "ID_GIAI_DOAN",
        ID_DO_UU_TIEN: "ID_DO_UU_TIEN",
        THOI_GIAN: "THOI_GIAN",
        NGAY_TAO: "NGAY_TAO",
        NGUOI_TAO: "NGUOI_TAO",
        NGAY_CAP_NHAT: "NGAY_CAP_NHAT",
        NGUOI_CAP_NHAT: "NGUOI_CAP_NHAT",
        TTHD: "TTHD",
        NGAY_KT: "NGAY_KT",
        TOTALTIME: "TOTALTIME",
        TIMEID: "TIMEID",
        PROCESSID: "PROCESSID",
        ID_YEU_CAU: "ID_YEU_CAU",
        STATUS: "STATUS",
        SENDTH: "SENDTH",
        NGUOI_NHAN_TIN: "NGUOI_NHAN_TIN",
        PHONE: "PHONE",
        CREATEDAY: "CREATEDAY",
        UPDATEDAY: "UPDATEDAY",
        CONTENT: "CONTENT",
      },
      process: {
        PROCESSID: "",
        ID_YEU_CAU: "",
        STATUS: "",
        ACTIVE: "",
        SENDTH: "",
        CREATEDAY: "",
        UPDATEDAY: "",
      },
      perPage: 15,
      currentPage: 1,
      processList: [],
      pageOfItems: [],
      totalprocess: 0,
      dialogDetailVisible: false,
      dialogTitle: "",
      detailList: [],
      list_detail_KSYC: [],
      list_detail_TKHD: [],
      list_detail_DWST: [],
      list_detail_DWSCT: [],
    };
  },
  created() {
    this.getDataAllProcess();
  },
  methods: {
    async getDataAllProcess() {
      var selt = this;
      this.pageOfItems = [];
      (this.processList = []), (this.currentPage = 1);
      var perPage = this.perPage;
      await processResponsitories
        .getAllProcess()
        .then(function (response) {
          if (response.data.success) {
            selt.processList = [];
            selt.pageOfItems = [];
            response.data.data.forEach((process, processid) => {
              selt.processList.push({
                processid: process.processid,
                id_yeu_cau: process.id_yeu_cau,
                status: process.status,
                active: process.active,
                sendth: process.sendth,
                createday: process.createday,
                updateday: process.updateday,
              });
              if (processid < perPage) {
                selt.pageOfItems.push({
                  processid: process.processid,
                  id_yeu_cau: process.id_yeu_cau,
                  status: process.status,
                  active: process.active,
                  sendth: process.sendth,
                  createday: process.createday,
                  updateday: process.updateday,
                });
              }
            });
            selt.totalprocess = response.data.data.length;
          }
        })
        .catch((error) => {
          var err = removeSpace(error.message.toUpperCase());
          Vue.$toast.error(
            this.$i18n.te(err) ? this.$i18n.t(err) : error.message
          );
        });
    },
    handleCurrentChange(val) {
      this.pageOfItems = [];
      this.processList.forEach((process) => {
        if (
          process.processid > this.perPage * (val - 1) &&
          process.processid < this.perPage * val + 1
        ) {
          this.pageOfItems.push(process);
        }
      });
    },
  },
  components: {
    pagination,
  },
};
</script>
<style scoped>
@import url(./process.css);
</style>