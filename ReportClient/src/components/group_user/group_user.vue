<template>
  <div class="k main-form">
    <div class="k wf card">
      <div class="k wf card-title">
        <span>{{ this.$i18n.t("GROUP_USER") }}</span>
      </div>
      <div class="k wf card-container align-center">
        <el-transfer
          filterable
          class="align-left"
          v-bind:titles="[
            this.$i18n.t(txt.UNASSIGNED),
            this.$i18n.t(txt.ASSIGNED),
          ]"
          v-model="assignChanged"
          :render-content="renderContent"
          :props="{
            key: 'usid',
            label: 'username',
          }"
          @change="handleChange"
          :data="allUser"
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
  name: "GroupUser",
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
      userInGroup: [],
      userNotInGroup: [],
      allUser: [],
      leftCheck: [],
      rightCheck: [],
    };
  },
  methods: {
    async getUserInGroupList(groupid) {
      var self = this;
      this.userInGroup = [];
      await permissionResponsitories
        .getUserInGroup(groupid)
        .then(function (response) {
          if (response.data.success) {
            response.data.data.forEach((e) => {
              self.userInGroup.push(e.user);
            });
          }
        });
    },
    async getUserNotInGroupList(groupid) {
      var self = this;
      this.userNotInGroup = [];
      await permissionResponsitories
        .getUserNotInGroup(groupid)
        .then(function (response) {
          if (response.data.success) {
            response.data.data.forEach((e) => {
              self.userNotInGroup.push(e.user);
            });
          }
        });
    },
    async getAll(groupid) {
      var self = this;
      this.groupid = groupid;
      this.allUser = [];
      this.assignChanged = [];
      await this.getUserInGroupList(groupid);
      await this.getUserNotInGroupList(groupid);
      this.allUser = this.userInGroup.concat(this.userNotInGroup);
      this.userInGroup.forEach((user) => {
        this.assignChanged.push(user.usid);
      });
    },
    async handleChange(value, direction, movedKeys) {
      console.log(value, direction, movedKeys);
      var change = [];
      var data = [];
      movedKeys.forEach((usid) => {
        var user = this.allUser.find((user) => user.usid === usid);
        change.push(user);
      });
      movedKeys.forEach((usid) => {
        data.push({
          usid: usid,
          groupid: this.groupid,
        });
      });
      if (direction == "right") {
        await permissionResponsitories
          .addUserToGroup(data)
          .then(function (response) {
            if (response.data.success) {
              Vue.$toast.success(response.data.message);
            } else {
              Vue.$toast.error(response.data.message);
            }
          });
      } else if (direction == "left") {
        await permissionResponsitories
          .removeUserToGroup(data)
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
          {this.$i18n.te(option.username)
            ? this.$i18n.t(option.username)
            : option.username}
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
@import url(./group_user.css);
</style>