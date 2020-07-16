<!--
 * @Date: 2020-03-11 12:35:28
 * @LastEditTime: 2020-03-13 12:07:06
 * @Description: file content
 -->
<template>
  <el-container class="dashboard">
    <el-aside class="sidebar" width="200px">
      <el-menu
        :default-active="$route.path"
        class="el-menu-vertical"
        @open="handleOpen"
        @close="handleClose"
        background-color="#0099FF"
        text-color="#FFFFFF"
        :router="true"
        active-text-color="#0000FF"
      >
        <el-menu-item index="/user/order/add">
          <i class="el-icon-s-flag"></i>
          <span slot="title">我要下单</span>
        </el-menu-item>
        <el-menu-item index="/user/order">
          <i class="el-icon-notebook-2"></i>
          <span slot="title">我的订单</span>
        </el-menu-item>
        <el-menu-item index="/user/mine">
          <i class="el-icon-setting"></i>
          <span slot="title">我的账户</span>
        </el-menu-item>
        <el-menu-item index="/user/agent" v-if="$root.user.isAgent==1">
          <i class="el-icon-user"></i>
          <span slot="title">代理中心</span>
        </el-menu-item>
        <el-menu-item index="/user/tasks">
          <i class="el-icon-date"></i>
          <span slot="title">任务中心</span>
        </el-menu-item>
        <el-menu-item>
          <i class="el-icon-close"></i>
          <span @click="logOut()" class="logOut cur_p">退出账户</span>
        </el-menu-item>
      </el-menu>
    </el-aside>
    <el-main>
      <router-view></router-view>
    </el-main>
  </el-container>
</template>

<script>
import upHeader from "../../components/upHeader";

export default {
  name: "layout",
  components: { upHeader },
  mounted() {
  },
  methods: {
    handleOpen(key, keyPath) {
      console.log(key, keyPath);
    },
    handleClose(key, keyPath) {
      console.log(key, keyPath);
    },
    logOut() {
      localStorage.removeItem('token');
      location.reload();
    }
  }
}
</script>

<style scoped>
.dashboard {
  border-top: solid 1px #eee;
  border-bottom: solid 1px #eee;
  margin-bottom: 30px;
  width: 100%;
  /* margin-left: auto;
  margin-right: auto; */
  min-height: 1100px;
}

.el-aside {
  width: 200px;
  overflow: hidden;
}
.el-menu-vertical {
  width: 200px;
  height: 1100px;
  /* height: calc(100vh - 100px); */
}
.el-main {
}
</style>
