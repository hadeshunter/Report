<template>
  <div class="k wf main-container">
    <div class="k wf card">
      <div class="k wf card-title">
        <span>{{this.$i18n.t("PERMISSION")}}</span>
      </div>
      <div class="k wf flex-row">
        <div class="k part">
          <div class="demonstration">{{ $t("GROUP_USER") }}</div>
          <el-select v-model="groupid" placeholder="Select Group" @change="handleChange">
            <el-option
              v-for="group in groupList"
              :key="group.groupid"
              :label="group.groupname"
              :value="group.groupid"
            >
              <span>{{group.groupname}}</span>
            </el-option>
          </el-select>
          <el-dropdown trigger="click">
            <el-button type="primary" icon="el-icon-s-tools"></el-button>
            <el-dropdown-menu slot="dropdown">
              <el-dropdown-item>
                <el-button type="text" @click="action = 1, addGroupModal = true, group = {}">
                  <i class="fas fa-plus"></i>
                  <span>{{this.$i18n.t("ADD")}}</span>
                </el-button>
              </el-dropdown-item>
              <el-dropdown-item>
                <el-button type="text" @click="getEditGroup(groupid)">
                  <i class="fas fa-edit"></i>
                  <span>{{this.$i18n.t("EDIT")}}</span>
                </el-button>
              </el-dropdown-item>
              <el-dropdown-item>
                <el-button type="text" @click="deleteGroup(groupid)">
                  <i class="far fa-trash-alt"></i>
                  <span>{{this.$i18n.t("DELETE")}}</span>
                </el-button>
              </el-dropdown-item>
            </el-dropdown-menu>
          </el-dropdown>
        </div>
      </div>
      <el-tabs v-model="selectComponent" @tab-click="handleChange(groupid)">
        <el-tab-pane
          v-bind:label="this.$i18n.t(txt.GROUP_USER)"
          name="grUserComponent"
          @click="selectComponent = 'grPerComponent'"
        ></el-tab-pane>
        <el-tab-pane
          v-bind:label="this.$i18n.t(txt.GROUP_PERMISSION)"
          name="grPerComponent"
          @click="selectComponent = 'grPerComponent'"
        ></el-tab-pane>
      </el-tabs>
      <div class="h wf">
        <component :is="selectComponent" ref="child"></component>
      </div>
    </div>
    <!-- ---------------Modal Add new group---------------- -->
    <el-dialog
      v-bind:title="action == 1 ? this.$i18n.t(txt.ADD_GROUP): this.$i18n.t(txt.EDIT_GROUP)"
      :visible.sync="addGroupModal"
    >
      <el-form :model="group" :rules="rules" ref="group">
        <el-form-item
          v-bind:label="this.$i18n.t(txt.GROUP_NAME)"
          :label-width="formLabelWidth"
          prop="groupname"
        >
          <el-input v-model="group.groupname" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item
          v-bind:label="this.$i18n.t(txt.NOTE)"
          :label-width="formLabelWidth"
          prop="note"
        >
          <el-input type="textarea" :rows="2" v-model="group.note"></el-input>
        </el-form-item>
        <el-form-item
          v-bind:label="this.$i18n.t(txt.GROUP_STATUS)"
          :label-width="formLabelWidth"
          prop="active"
        >
          <el-select v-model="group.active" placeholder="Please select a status">
            <el-option v-bind:label="this.$i18n.t(txt.ACTIVE)" value="true"></el-option>
            <el-option v-bind:label="this.$i18n.t(txt.NONE_ACTIVE)" value="false"></el-option>
          </el-select>
        </el-form-item>
      </el-form>
      <span slot="footer" class="dialog-footer">
        <el-button @click="addGroupModal = false">Cancel</el-button>
        <el-button type="primary" @click="submitForm('group', group)">Confirm</el-button>
      </span>
    </el-dialog>
  </div>
</template>
<script>
import Vue from "vue";
import { getDiff } from "../../utils/common";
import groupUser from "../group_user/group_user";
import groupPermission from "../group_permission/group_permission";
import responsitories from "../../service/factory/reponsitoryfactory";
const permissionResponsitories = responsitories.get("permission");

export default {
  name: "Permission",
  data() {
    return {
      txt: {
        ADD_GROUP: "ADD_GROUP",
        EDIT_GROUP: "EDIT_GROUP",
        DELETE_GROUP: "DELETE_GROUP",
        GROUP_USER: "GROUP_USER",
        GROUP_PERMISSION: "GROUP_PERMISSION",
        GROUP_NAME: "GROUP_NAME",
        GROUP_STATUS: "GROUP_STATUS",
        NOTE: "NOTE",
        ACTIVE: "ACTIVE",
        NONE_ACTIVE: "NONE_ACTIVE",
        UNASSIGNED: "UNASSIGNED",
        ASSIGNED: "ASSIGNED",
      },
      activeTab: "grPerComponent",
      selectComponent: "grUserComponent",
      action: 0, //1: add, 2: edit, 3:delete
      groupList: [],
      groupid: 1,
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
    };
  },
  methods: {
    getGroupList() {
      var self = this;
      permissionResponsitories.getAllGroup().then(function (response) {
        if (response.data.success) {
          self.groupList = response.data.data;
        }
      });
    },
    deleteGroup(groupid) {
      var self = this;
      var group = this.groupList.find((group) => group.groupid === groupid);
      if (group.groupname !== "admin") {
        permissionResponsitories.deleteGroup(groupid).then(function (response) {
          if (response.data.success) {
            Vue.$toast.success(response.data.message);
            self.getGroupList();
            self.groupid = 1;
          } else {
            Vue.$toast.error(response.data.message);
          }
        });
      } else {
        Vue.$toast.error(this.$i18n.t("DO_NOT_CHOOSE_ADMIN"));
      }
    },
    getEditGroup(groupid) {
      var group = this.groupList.find((group) => group.groupid === groupid);
      this.group = group;
      this.action = 2;
      this.addGroupModal = true;
    },
    editGroup(group) {
      var self = this;
      if (group.groupname != null && group.groupname.length > 0) {
        permissionResponsitories.editGroup(group).then(function (response) {
          if (response.data.success) {
            Vue.$toast.success(response.data.message);
            self.getGroupList();
            self.addGroupModal = false;
          } else {
            Vue.$toast.error(response.data.message);
          }
        });
      } else {
        Vue.$toast.error("Not empty");
      }
    },
    addGroup(group) {
      var self = this;
      if (group.groupname != null && group.groupname.length > 0) {
        permissionResponsitories.addGroup(group).then(function (response) {
          if (response.data.success) {
            Vue.$toast.success(response.data.message);
            self.getGroupList();
            self.addGroupModal = false;
          } else {
            Vue.$toast.error(response.data.message);
          }
        });
      } else {
        Vue.$toast.error("Not empty");
      }
    },
    submitForm(formName, group) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          if (this.action == 1) {
            this.addGroup(group);
          } else if (this.action == 2) {
            this.editGroup(group);
          }
        } else {
          return false;
        }
      });
    },
    handleChange() {
      console.log(this.groupid);
      this.$refs.child.getAll(this.groupid);
    },
  },
  mounted() {
    this.handleChange();
  },
  created() {
    this.getGroupList();
  },
  components: {
    grPerComponent: groupPermission,
    grUserComponent: groupUser,
  },
};
</script>
<style scoped>
@import url(./permission.css);
</style>