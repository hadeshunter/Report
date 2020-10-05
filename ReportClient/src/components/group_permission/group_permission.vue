<template>
  <div class="k main-form">
    <div class="k wf card">
      <div class="k wf card-title">
        <span>{{ this.$i18n.t("GROUP_PERMISSION") }}</span>
      </div>
      <div class="k wf card-container align-center">
        <el-transfer
          class="align-left"
          v-bind:titles="[
            this.$i18n.t(txt.UNASSIGNED),
            this.$i18n.t(txt.ASSIGNED),
          ]"
          v-model="assignChanged"
          :render-content="renderContent"
          :props="{
            key: 'perid',
            label: 'pername',
          }"
          @change="handleChange"
          :data="allPer"
        ></el-transfer>
      </div>
    </div>
  </div>
</template>
<script>
import Vue from "vue";
import { getDiff } from "../../utils/common";
import responsitories from "../../service/factory/reponsitoryfactory";
const permissionResponsitories = responsitories.get("permission");
export default {
  name: "GroupPermission",
  data() {
    return {
      txt: {
        ADD_GROUP: "ADD_GROUP",
        EDIT_GROUP: "EDIT_GROUP",
        DELETE_GROUP: "DELETE_GROUP",
        GROUP_NAME: "GROUP_NAME",
        GROUP_STATUS: "GROUP_STATUS",
        NOTE: "NOTE",
        ACTIVE: "ACTIVE",
        NONE_ACTIVE: "NONE_ACTIVE",
        UNASSIGNED: "UNASSIGNED",
        ASSIGNED: "ASSIGNED",
      },
      action: 0, //1: add, 2: edit, 3:delete
      assignChanged: [],
      groupList: [],
      addGroupModal: false,
      formLabelWidth: "120px",
      group: {
        groupname: "",
        active: "true",
        note: "",
      },
      rules: {
        groupname: [
          {
            required: true,
            message: "Please input groupname",
            trigger: ["blur", "change"],
          },
        ],
      },
      perInGroup: [],
      perNotInGroup: [],
      allPer: [],
      leftCheck: [],
      rightCheck: [],
    };
  },
  methods: {
    async getPerInGroupList(groupid) {
      var self = this;
      await permissionResponsitories
        .getPerMissionByGroupId(groupid)
        .then(function (response) {
          if (response.data.success) {
            self.perInGroup = response.data.data;
          }
        });
    },
    async getPerNotInGroupList(groupid) {
      var self = this;
      await permissionResponsitories
        .getPerMissionNotInGroup(groupid)
        .then(function (response) {
          if (response.data.success) {
            self.perNotInGroup = response.data.data;
          }
        });
    },
    async getAll(groupid) {
      var self = this;
      this.groupid = groupid;
      this.allPer = [];
      this.assignChanged = [];
      await this.getPerInGroupList(groupid);
      await this.getPerNotInGroupList(groupid);
      this.allPer = this.perInGroup.concat(this.perNotInGroup);
      this.perInGroup.forEach((per) => {
        this.assignChanged.push(per.perid);
      });
    },
    async handleChange(value, direction, movedKeys) {
      console.log(value, direction, movedKeys);
      var change = [];
      var data = [];
      movedKeys.forEach((perid) => {
        var per = this.allPer.find((per) => per.perid === perid);
        change.push(per);
      });
      movedKeys.forEach((perid) => {
        data.push({
          groupid: this.groupid,
          perid: perid,
        });
      });
      if (direction == "right") {
        await permissionResponsitories
          .insertGroupPermission(data)
          .then(function (response) {
            if (response.data.success) {
              Vue.$toast.success(response.data.message);
            } else {
              Vue.$toast.error(response.data.message);
            }
          });
      } else if (direction == "left") {
        await permissionResponsitories
          .removeGroupPermission(data)
          .then(function (response) {
            if (response.data.success) {
              Vue.$toast.success(response.data.message);
            } else {
              Vue.$toast.error(response.data.message);
            }
          });
      }
      await this.getAll(this.groupid);
    },
    renderContent(h, option) {
      return (
        <span>
          {this.$i18n.te(option.pername)
            ? this.$i18n.t(option.pername)
            : option.pername}
        </span>
      );
    },
  },
  created() {
    // this.getAll(this.groupid);
  },
};
</script>
<style scoped>
@import url(./group_permission.css);
</style>