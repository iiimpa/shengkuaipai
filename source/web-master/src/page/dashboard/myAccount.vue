<template>
  <div>
    <table style="border: none; line-height: 50px;">
      <tr>
        <td>
          用户名： {{$root.user.account}} <a href="javascript:;" @click="changePasswordDialog=true">修改密码</a>
        </td>
        <td>
          账户余额: {{$root.user.coin}} <a href="javascript:;" @click="recharge">充值</a>
        </td>
      </tr>
      <tr>
        <td>
          注册时间： {{$root.user.createdAt}}
        </td>
      </tr>
      <tr>
        <td>
          最后登录: {{$root.user.updatedAt}}
        </td>
      </tr>
    </table>
    <el-dialog title="修改密码" :visible.sync="changePasswordDialog">
      <el-form :model="changePassword">
        <el-form-item label="当前密码" label-width="120">
          <el-input v-model="changePassword.Password" type="password" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="新的密码" label-width="120">
          <el-input v-model="changePassword.newPassword" type="password" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="在输入一次" label-width="120">
          <el-input v-model="changePassword.confirmPassword" type="password" autocomplete="off"></el-input>
        </el-form-item>

      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="changePasswordDialog = false">取 消</el-button>
        <el-button type="primary" @click="doChangePassword">确 定</el-button>
      </div>
    </el-dialog>
    <el-dialog title="请选择充值方案" :visible.sync="rechargeDialog" width="30%">
      <el-form>
        <el-form-item v-for="item in rechargePlans" :key="item.id">
          <el-button type="primary" plain style="width: 100%" @click="doRecharge(item.id)">{{item.name}}</el-button>
        </el-form-item>
      </el-form>
      <span slot="footer" class="dialog-footer">
        <el-button type="primary" @click="rechargeDialog = false">关 闭</el-button>
      </span>
    </el-dialog>
  </div>
</template>

<script>
  export default {
    name: "OrderList",
    data() {
      return {
        changePassword: {},
        rechargeDialog: false,
        changePasswordDialog: false,
        orders: [],
        plans: {},
        rechargePlans: [],
        platforms: ['PC - 百度', 'PC - 搜狗', 'PC - 360', 'Mobile - 百度', 'Mobile - 搜狗', 'Mobile - 360'],
        status: ['进行中', '已完成', '已退款']
      }
    },
    methods: {
      doChangePassword() {
        if (this.changePassword.newPassword != this.changePassword.confirmPassword) {
          this.$message({
            showClose: true,
            message: '两次输入的新密码不一致',
            type: 'error'
          });
          return;
        }
        this.api("/user/change-password", this.changePassword).then(resp => {
          this.changePasswordDialog = false;
          this.changePassword = {};
          this.$message({
            showClose: true,
            message: '密码修改成功',
          });
        })
      },
      recharge() {
        this.api("/user/recharge/plans").then(resp => {
          this.rechargePlans = resp.data;
          this.rechargeDialog = true;
        })
      },
      doRecharge(rid) {
        this.api("/user/recharge/query", {
          rid,
          backUrl: location.href
        }).then(resp => {
          location.href = resp.data;
        })
      }
    },

    mounted() {

    }
  }
</script>

<style scoped>
  table {
    width: 100%;
  }

  table tr {
    width: 100%;
  }

  table tr td {
    width: 50%;
  }
</style>
