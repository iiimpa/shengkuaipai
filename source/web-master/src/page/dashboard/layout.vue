<template>
  <el-container class="dashboard">
    <el-aside class="sidebar" width="200px">
      <el-menu
        default-active="/user/order"
        class="el-menu-vertical"
        @open="handleOpen"
        @close="handleClose"
        background-color="#545c64"
        text-color="#fff"
        :router="true"
        active-text-color="#ffd04b">
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
    components: {upHeader},
    methods: {
      handleOpen(key, keyPath) {
        console.log(key, keyPath);
      },
      handleClose(key, keyPath) {
        console.log(key, keyPath);
      }
    }
  }
</script>

<style scoped>
  .dashboard {
    border-top: solid 1px #545c64;
    border-bottom: solid 1px #545c64;
    margin-bottom: 30px;
    width: 1200px;
    margin-left: auto;
    margin-right: auto;
  }

  el-aside {
    width: 200px;
  }

  .el-menu-vertical {
    width: 200px;
    height: calc(100vh - 100px);
  }
</style>
