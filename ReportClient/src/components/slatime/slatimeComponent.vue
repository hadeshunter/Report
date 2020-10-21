<template>
  <div class="k main-form">
    <div class="k wf main-container">
      <div class="k wf card">
        <!----------------------------- sms list --------------------------->
        <div class="k wf card-titleb">
          <span>{{ $t("LIST_SLA_TRUE").toUpperCase() }}</span>
        </div>
        <div class="k wf card-container">
          <table class="table">
            <tr class="table-title">
              <!-- <th class="table-title">{{ this.$i18n.t(title.STT) }}</th> -->
              <th class="table-title">{{ this.$i18n.t(title.ID_GIAI_DOAN) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.ID_DO_UU_TIEN) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.THOI_GIAN) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.NGAY_TAO) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.NGUOI_TAO) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.NGAY_CAP_NHAT) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.NGUOI_CAP_NHAT) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.TTHD) }}</th>
              <th class="table-title">{{ this.$i18n.t(title.NGAY_KT) }}</th>
              <th class="table-title"><i class="el-icon-circle-plus-outline" @click="modalCreateSLA=true"></i></th>
            </tr>
            <tr v-for="(slatime,index)  in pageOfItems" :key="index">
              <th>
                {{ slatime.ID_GIAI_DOAN==6?"Khảo sát yêu cầu": slatime.ID_GIAI_DOAN==7?"Thiết kế hóa đơn"
                : slatime.ID_GIAI_DOAN==8?"Dựng website demo" : "Dựng website chính thức"}}
              </th>
              <th>{{ slatime.ID_DO_UU_TIEN==1?"Thấp" : slatime.ID_DO_UU_TIEN==2?"Trung bình" : "Cao"}}</th>
              <th>{{ slatime.THOI_GIAN }}</th>
              <th>{{ moment(slatime.NGAY_TAO).format("DD-MM-YYYY") }}</th>
              <th>{{ slatime.NGUOI_TAO==1?"Admin":"User" }}</th>
              <th>{{ moment(slatime.NGAY_CAP_NHAT).format("DD-MM-YYYY") }}</th>
              <th>{{ slatime.NGUOI_CAP_NHAT==1?"Admin":"User" }}</th>
              <th>
                    <el-switch  v-model="slatime.TTHD" @change="updateTT(slatime)"></el-switch>
              </th>
              <th>{{ slatime.NGAY_KT ==null?"Không có": moment(slatime.NGAY_KT).format("DD-MM-YYYY") }}</th>
              <th>
                <el-button type="text">
                  <span class="tooltipedit" v-on:click="GetById(slatime.ID)">
                    <i class="far fa-edit"></i>
                  </span>
                </el-button>
                <el-button type="text">
                  <span class="tooltipedit" v-on:click="deleteSla(slatime.ID)">
                     <i class="far fa-trash-alt"></i>
                  </span>
                </el-button>
              </th>
            </tr>
          </table>

          <!-- ------------------------ end add time sla ----------------------------------->
          <b-modal
            v-model="modalCreateSLA"
            ref="my-modal modal-xl"
            hide-footer
            title="Thêm mới giai đoạn SLA"
          >
        
            <div class="form-group row">
              <label
                for="name"
                class="col-sm-4 col-form-label"
              >{{ this.$i18n.t(title.ID_GIAI_DOAN) }}</label>
              <div class="col-sm-8" aria-placeholder="csdmsl">
                <select
                  class="custom-select my-1 mr-sm-2"
                  v-model="sltime.ID_GIAI_DOAN"
                  id="inlineFormCustomSelectPref"
                >
                  <option select="selected">chọn...</option>
                  <option value="6">Khảo sát yêu cầu</option>
                  <option value="7">Thiết kế hóa đơn</option>
                  <option value="8">Dựng site demo</option>
                </select>
              </div>
            </div>
            <div class="form-group row">
              <label
                for="name"
                class="col-sm-4 col-form-label"
              >{{ this.$i18n.t(title.ID_DO_UU_TIEN) }}</label>
              <div class="col-sm-8" aria-placeholder="csdmsl">
                <select
                  class="custom-select my-1 mr-sm-2"
                  v-model="sltime.ID_DO_UU_TIEN"
                  id="inlineFormCustomSelectPref"
                >
                  <option select="selected">chọn...</option>
                  <option value="1">Thấp</option>
                  <option value="2">Trung bình</option>
                  <option value="3">Cao</option>
                </select>
              </div>
            </div>
            <div class="form-group row">
              <label for="name" class="col-sm-4 col-form-label">{{ this.$i18n.t(title.NGAY_TAO) }}</label>
              <div class="col-sm-8">
                <el-date-picker v-model="sltime.NGAY_TAO" type="date" placeholder="Ngày tạo"></el-date-picker>
              </div>
            </div>
             <div class="form-group row">
              <label
                for="name"
                class="col-sm-4 col-form-label"
              >{{ this.$i18n.t(title.NGUOI_TAO) }}</label>
                <div class="col-sm-8">
                 <input
                  type="text"
                  class="form-control"
                  v-model="sltime.NGUOI_TAO"
                  placeholder="Người tạo"
                  maxlength="30"
                />
              </div>
            </div>
            <div class="form-group row">
              <label
                for="name"
                class="col-sm-4 col-form-label"
              >{{ this.$i18n.t(title.THOI_GIAN) }}</label>
                <div class="col-sm-8">
                 <input
                  type="text"
                  class="form-control"
                  v-model="sltime.THOI_GIAN"
                  placeholder="Thời gian"
                  maxlength="30"
                />
              </div>
            </div>
            <div class="form-group row">
              <label
                for="name"
                class="col-sm-4 col-form-label"
              >{{ this.$i18n.t(title.NGAY_CAP_NHAT) }}</label>
              <div class="col-sm-8">
                <div class="block">
                  <el-date-picker
                    v-model="sltime.NGAY_CAP_NHAT"
                    type="date"
                    placeholder="Ngày cập nhật"
                  ></el-date-picker>
                </div>
              </div>
            </div>
            <div class="form-group row">
              <label
                for="name"
                class="col-sm-4 col-form-label"
              >{{ this.$i18n.t(title.NGUOI_CAP_NHAT) }}</label>
              <div class="col-sm-8">
                 <input
                  type="text"
                  class="form-control"
                  v-model="sltime.NGUOI_CAP_NHAT"
                  placeholder="Người cập nhật"
                  maxlength="30"
                />
              </div>
            </div>
            <div class="form-group row">
              <label for="name" class="col-sm-4 col-form-label">{{ this.$i18n.t(title.TTHD) }}</label>
              <div class="col-sm-8" aria-placeholder="csdmsl">
                <select
                  class="custom-select my-1 mr-sm-2"
                  v-model="sltime.TTHD"
                  id="inlineFormCustomSelectPref"
                >
                  <option select="selected">chọn...</option>
                  <option value="1">Kích hoạt</option>
                  <option value="2">Chưa kích hoạt</option>
                </select>
              </div>
            </div>

             <div class="form-group row">
              <label for="name" class="col-sm-4 col-form-label">{{ this.$i18n.t(title.THEO_VB) }}</label>
              <div class="col-sm-8">
                <input
                  type="text"
                  class="form-control"
                  v-model="sltime.THEO_VB"
                  placeholder="Theo văn bản"
                  maxlength="30"
                />
              </div>
            </div>
            <button
              type="button"
              style="width:100%"
              @click="createSLA()"
              class="btn btn-success"
            >Tạo mới</button>
          </b-modal>
          <!-- ------------------------ end add time sla ----------------------------------->
           <b-modal
            v-model="modalUpdateSLA"
            ref="my-modal modal-xl"
            hide-footer
            title="Cập nhật giai đoạn SLA"
          >
            <div class="form-group row">
              <label
                for="name"
                class="col-sm-4 col-form-label"
              >{{ this.$i18n.t(title.ID_GIAI_DOAN) }}</label>
              <div class="col-sm-8" aria-placeholder="csdmsl">
               <select
                class="custom-select my-1 mr-sm-2"
                v-model="sltime.ID_GIAI_DOAN"
                id="inlineFormCustomSelectPref"
              >
                <option select="selected">chọn...</option>
                <option value="6">Khảo sát yêu cầu</option>
                <option value="7">Thiết kế hóa đơn</option>
                <option value="8">Dựng web site demo</option>
              </select>
              </div>
            </div>
            <div class="form-group row">
              <label
                for="name"
                class="col-sm-4 col-form-label"
              >{{ this.$i18n.t(title.ID_DO_UU_TIEN) }}</label>
              <div class="col-sm-8" aria-placeholder="csdmsl">
                <select
                  class="custom-select my-1 mr-sm-2"
                  v-model="sltime.ID_DO_UU_TIEN"
                  id="inlineFormCustomSelectPref"
                >
                  <option select="selected">chọn...</option>
                  <option value="1">Thấp</option>
                  <option value="2">Trung bình</option>
                  <option value="3">Cao</option>
                </select>
              </div>
            </div>
            <div class="form-group row">
              <label for="name" class="col-sm-4 col-form-label">{{ this.$i18n.t(title.NGAY_TAO) }}</label>
              <div class="col-sm-8">
                <el-date-picker v-model="sltime.NGAY_TAO" type="date" placeholder="Ngày tạo"></el-date-picker>
              </div>
            </div>
             <div class="form-group row">
              <label
                for="name"
                class="col-sm-4 col-form-label"
              >{{ this.$i18n.t(title.NGUOI_TAO) }}</label>
                <div class="col-sm-8">
                 <input
                  type="text"
                  class="form-control"
                  v-model="sltime.NGUOI_TAO"
                  placeholder="Người tạo"
                  maxlength="30"
                />
              </div>
            </div>
            <div class="form-group row">
              <label
                for="name"
                class="col-sm-4 col-form-label"
              >{{ this.$i18n.t(title.THOI_GIAN) }}</label>
                <div class="col-sm-8">
                 <input
                  type="text"
                  class="form-control"
                  v-model="sltime.THOI_GIAN"
                  placeholder="Thời gian"
                  maxlength="30"
                />
              </div>
            </div>
            <div class="form-group row">
              <label
                for="name"
                class="col-sm-4 col-form-label"
              >{{ this.$i18n.t(title.NGAY_CAP_NHAT) }}</label>
              <div class="col-sm-8">
                <div class="block">
                  <el-date-picker
                    v-model="sltime.NGAY_CAP_NHAT"
                    type="date"
                    placeholder="Ngày cập nhật"
                  ></el-date-picker>
                </div>
              </div>
            </div>
            <div class="form-group row">
              <label
                for="name"
                class="col-sm-4 col-form-label"
              >{{ this.$i18n.t(title.NGUOI_CAP_NHAT) }}</label>
              <div class="col-sm-8">
                 <input
                  type="text"
                  class="form-control"
                  v-model="sltime.NGUOI_CAP_NHAT"
                  placeholder="Người cập nhật"
                  maxlength="30"
                />
              </div>
            </div>
            <div class="form-group row">
              <label for="name" class="col-sm-4 col-form-label">{{ this.$i18n.t(title.TTHD) }}</label>
              <div class="col-sm-8" aria-placeholder="csdmsl">
                <select
                  class="custom-select my-1 mr-sm-2"
                  v-model="sltime.TTHD"
                  id="inlineFormCustomSelectPref"
                >
                  <option select="selected">chọn...</option>
                  <option value="1">Kích hoạt</option>
                  <option value="2">Chưa kích hoạt</option>
                </select>
              </div>
            </div>

             <div class="form-group row">
              <label for="name" class="col-sm-4 col-form-label">{{ this.$i18n.t(title.THEO_VB) }}</label>
              <div class="col-sm-8">
                <input
                  type="text"
                  class="form-control"
                  v-model="sltime.THEO_VB"
                  placeholder="Theo văn bản"
                  maxlength="30"
                />
              </div>
            </div>
            <button
              type="button"
              style="width:100%"
              @click="updateSla()"
              class="btn btn-success"
            >Tạo mới</button>
          </b-modal>
          <!-- ------------------------ end update time sla ----------------------------------->
        </div>
          <el-pagination
              @current-change="handleCurrentChange"
              :current-page.sync="currentPage"
              :page-size="perPage"
              :total="totalslatime"
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
import pagination from "../pagination/pagination";
import { removeSpace } from "../../utils/common";
import slatimereponsitory from "../../service/Impl/slatimerepository";
import responsitories from "../../service/factory/reponsitoryfactory";
const reportResponsitories = responsitories.get("getAllSla");

export default {
  name: "slatime",
  data() {
    return {
      title: {
        ID:"ID",
        STT:"STT",
        SMSID: "SMSID",
        DESCRIPTION: "DESCRIPTION",
        ID_GIAI_DOAN: "ID_GIAI_DOAN",
        ID_DO_UU_TIEN: "ID_DO_UU_TIEN",
        THOI_GIAN: "THOI_GIAN",
        NGAY_TAO: "NGAY_TAO",
        NGUOI_TAO: "NGUOI_TAO",
        NGAY_CAP_NHAT: "NGAY_CAP_NHAT",
        NGUOI_CAP_NHAT: "NGUOI_CAP_NHAT",
        TTHD: "TTHD",
        NGAY_KT: "NGAY_KT",
        THEO_VB: "THEO_VB"
      },
      sltime: {
        ID:"",
        STT: "",
        ID_NHOM_GIAI_DOAN: "18",
        ID_GIAI_DOAN: "",
        ID_DO_UU_TIEN: "",
        THOI_GIAN: "",
        NGAY_TAO: "",
        NGUOI_TAO: "",
        NGAY_CAP_NHAT: "",
        NGUOI_CAP_NHAT: "",
        TTHD: "",
        NGAY_KT: "",
        THEO_VB:""
      },
     
      slatimeList: [],
      perPage: 15,
      currentPage: 1,
      pageOfItems: [],
      totalslatime: 0,
      modalUpdateSLA:false,
      modalCreateSLA: false,
    };
  },
  created() {
    this.getAllSla();
  },
  methods: {
    getAllSla() {
      var selt = this;
      this.pageOfItems = [];
      this.slatimeList = [],
      this.currentPage = 1;
      var perPage = this.perPage;
      slatimereponsitory
        .getAllSla()
        .then(function (response) {
          if (response.data.success) {
            selt.slatimeList = [];
            selt.pageOfItems = [];
            response.data.data.forEach((slatime, index) => {
              selt.slatimeList.push({
                ID:slatime.ID,
                STT: index + 1,
                ID_NHOM_GIAI_DOAN: slatime.ID_NHOM_GIAI_DOAN,
                ID_GIAI_DOAN: slatime.ID_GIAI_DOAN,
                ID_DO_UU_TIEN: slatime.ID_DO_UU_TIEN,
                THOI_GIAN: slatime.THOI_GIAN,
                NGAY_TAO: slatime.NGAY_TAO,
                NGUOI_TAO: slatime.NGUOI_TAO,
                NGAY_CAP_NHAT: slatime.NGAY_CAP_NHAT,
                NGUOI_CAP_NHAT: slatime.NGUOI_CAP_NHAT,
                TTHD: slatime.TTHD == 1 ? true : false,
                NGAY_KT: slatime.NGAY_KT,
                THEO_VB: slatime.THEO_VB,
              });
              if (index < perPage) {
                selt.pageOfItems.push({
                  ID:slatime.ID,
                  STT: index + 1,
                  ID_NHOM_GIAI_DOAN: slatime.ID_NHOM_GIAI_DOAN,
                  ID_GIAI_DOAN: slatime.ID_GIAI_DOAN,
                  ID_DO_UU_TIEN: slatime.ID_DO_UU_TIEN,
                  THOI_GIAN: slatime.THOI_GIAN,
                  NGAY_TAO: slatime.NGAY_TAO,
                  NGUOI_TAO: slatime.NGUOI_TAO,
                  NGAY_CAP_NHAT: slatime.NGAY_CAP_NHAT,
                  NGUOI_CAP_NHAT: slatime.NGUOI_CAP_NHAT,
                  TTHD: slatime.TTHD== 1 ? true : false,
                  NGAY_KT: slatime.NGAY_KT,
                  THEO_VB: slatime.THEO_VB,
                });
              }
            });
            selt.totalslatime = response.data.data.length;
          }
        })
        .catch((error) => {
          var err = removeSpace(error.message.toUpperCase());
          Vue.$toast.error(
            this.$i18n.te(err) ? this.$i18n.t(err) : error.message
          );
        });
    },
    createSLA(){
      var self=this;
      var daytime={
        NGAY_TAO: this.moment(this.NGAY_TAO).format("DD-MM-YYYY"),
        NGAY_CAP_NHAT: this.moment(this.NGAY_CAP_NHAT).format("DD-MM-YYYY"),
        NGAY_KT: this.moment(this.NGAY_CAP_NHAT).format("DD-MM-YYYY"),
        ID_GIAI_DOAN: this.sltime.ID_GIAI_DOAN,
        ID_NHOM_GIAI_DOAN:this.sltime.ID_NHOM_GIAI_DOAN,
        ID_DO_UU_TIEN: this.sltime.ID_DO_UU_TIEN,
        THOI_GIAN: this.sltime.THOI_GIAN,
        NGUOI_CAP_NHAT: this.sltime.NGUOI_CAP_NHAT,
        TTHD:this.sltime.TTHD,
        THEO_VB:this.sltime.THEO_VB,
        NGUOI_TAO: this.sltime.NGUOI_TAO,
      };
      slatimereponsitory.createSLA(daytime).then(function(response){
        if(response.data.success){
          self.modalCreateSLA=false;
          self.getAllSla();
          Vue.$toast.success(response.data.message);
          
        }
        else {
              Vue.$toast.error(response.data.message)
            }
          })
          .catch((error) => {
            var err = removeSpace(error.message.toUpperCase());
            Vue.$toast.error(
              this.$i18n.te(err) ? this.$i18n.t(err) : error.message
            );
          });
      },
      GetById(id){
        this.modalUpdateSLA=true;
        var self=this;
        slatimereponsitory.GetById(id).then(function (response) {
        if (response.data.success) {
            self.sltime=response.data.data[0];
          }
        })
      },
      updateSla(){
      var self = this;
      var sltime=this.sltime;
      if(self.sltime.NGAY_TAO){
          self.sltime.NGAY_TAO = moment(self.sltime.NGAY_TAO).format("YYYY-MM-DD");
      };
      if(self.sltime.NGAY_CAP_NHAT){
         self.sltime.NGAY_CAP_NHAT = moment(self.sltime.NGAY_CAP_NHAT).format("YYYY-MM-DD");
      };
      if(self.sltime.NGAY_KT){
          self.sltime.NGAY_KT = moment(self.sltime.NGAY_KT).format("YYYY-MM-DD");
      };
      slatimereponsitory.updateSla(this.sltime).then(function(response){
        if(response.data.success){
          self.modalUpdateSLA=false;
          self.getAllSla();
           Vue.$toast.success(response.data.message);
        } else {
              Vue.$toast.error(response.data.message)
            }
          })
          .catch((error) => {
            var err = removeSpace(error.message.toUpperCase());
            Vue.$toast.error(
              this.$i18n.te(err) ? this.$i18n.t(err) : error.message
            );
          });
      },
      deleteSla(id){
      var self = this;
      bootbox.confirm({
          message: "Bạn có muốn xóa SLA này không?",
          buttons: {
              confirm: {
                  label: 'Có',
                  className: 'btn-success'
              },
              cancel: {
                  label: 'Không',
                  className: 'btn-danger'
              }
          },
          callback: function (result) {
             if(result){
              slatimereponsitory.deleteSlatime(id).then(function(response){
                if(response.data.success){
                    self.getAllSla();
                    Vue.$toast.success(response.data.message); 
                }else{
                    Vue.$toast.error(response.data.message)
                }
              }).catch((error) => {
                var err = removeSpace(error.message.toUpperCase());
                Vue.$toast.error(
                  this.$i18n.te(err) ? this.$i18n.t(err) : error.message
                );
              });
             }
          }
      });
      },
      handleCurrentChange(val) {
        this.pageOfItems = [];
        this.slatimeList.forEach((slatime) => {
          if (
            slatime.STT > this.perPage * (val - 1) &&
            slatime.STT < this.perPage * val + 1
          ) {
            this.pageOfItems.push(slatime);
          }
        });
      },
      updateTT(slatime) {
        this.sltime=[];
        if (slatime.TTHD != false) {
          slatime.TTHD = 1;
        }
        else{
          slatime.TTHD=2;
        }
        this.sltime=slatime;
        this.updateSla();
      }
      
  },
  components: {
    pagination,
  },
};
</script>
<style scoped>
@import url(./slatime.css);
</style>