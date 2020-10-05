<template>
  <div class="k main-form">
    <div class="k wf main-container">
      <div class="k wf card">
        <!----------------------------- sms list --------------------------->
        <div class="k wf card-titleb">
          <span>{{ $t("DANH_SACH_USER_NHAN_SMS").toUpperCase() }}</span>
        </div>
        <div class="k wf card-container">
          <table class="table">
            <tr class="table-title">
              <th class="table-title">{{ this.$i18n.t(title.PHONE) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.CREATEDAY) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.UPDATEDAY) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.STATUS) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.CONTENT) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.ID_YEU_CAU) }}</th>
            </tr>
            <tr v-for="phonsent in pageOfItems" :key="phonsent.phoneid">
              <th>{{ phonsent.phone }}</th>
              <th>{{ moment(phonsent.createday).format("DD-MM-YYYY") }}</th>
              <th>{{ moment(phonsent.updateday).format("DD-MM-YYYY") }}</th>
              <th>{{ phonsent.status==1?"Đã gửi" : "Chưa gửi"}}</th>
              <th>{{ phonsent.content }}</th>
              <th>{{ phonsent.id_yeu_cau }}</th>
            </tr>
          </table>
        </div>
        <el-pagination
          @current-change="handleCurrentChange"
          :current-page.sync="currentPage"
          :page-size="perPage"
          :total="totalphonesent"
          layout="total, prev, pager, next"
        ></el-pagination>
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
import phonesendedrepository from "../../service/Impl/phonesended";
import responsitories from "../../service/factory/reponsitoryfactory";
import pagination from "../../components/pagination/pagination";
const reportResponsitories = responsitories.get("getAllPhoneSended");

export default {
  name: "PhoneSended",
  data() {
    return {
      title: {
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
      phonesended: {
        phoneid: "",
        phone: "",
        processid: "",
        createday: "",
        updateday: "",
        status: "",
        timer: "",
        content: "",
      },
      phonesendedlist: [],
      perPage: 15,
      currentPage: 1,
      pageOfItems: [],
      totalphonesent: 0,
    };
  },
  created() {
    this.getAllPhoneSended();
  },
  methods: {
    getAllPhoneSended() {
      var selt = this;
      this.pageOfItems = [];
      (this.phonesendedlist = []), (this.currentPage = 1);
      var perPage = this.perPage;
      phonesendedrepository
        .getAllPhoneSended()
        .then(function (response) {
          //  console.log(response);
          if (response.data.success) {
            selt.phonesendedlist = [];
            selt.pageOfItems = [];
            response.data.data.forEach((phonsent, phoneid) => {
              selt.phonesendedlist.push({
                phoneid: phonsent.processid,
                phone: phonsent.phone,
                status: phonsent.status,
                processid: phonsent.processid,
                timer: phonsent.timer,
                createday: phonsent.createday,
                updateday: phonsent.updateday,
                content: phonsent.content,
                result: phonsent.result,
              });
              if (phoneid < perPage) {
                selt.pageOfItems.push({
                  phoneid: phonsent.processid,
                  phone: phonsent.phone,
                  status: phonsent.status,
                  processid: phonsent.processid,
                  timer: phonsent.timer,
                  createday: phonsent.createday,
                  updateday: phonsent.updateday,
                  content: phonsent.content,
                  result: phonsent.result,
                });
              }
            });
            selt.totalphonesent = response.data.data.length;
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
      this.phonesendedlist.forEach((phonsent) => {
        if (
          phonsent.phoneid > this.perPage * (val - 1) &&
          phonsent.phoneid < this.perPage * val + 1
        ) {
          this.pageOfItems.push(phonsent);
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
@import url(./phonesended.css);
</style>