<template>
  <div>
    <table style="border: none; line-height: 50px;">
      <tr>
        <td>
          用户名： {{$root.user.account}} <a href="javascript:;" @click="changePasswordDialog=true">修改密码</a>
        </td>
        <td>
          账户余额: {{$root.user.coin}}
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
    <!-- <el-card class="box-card">
      <h3>深圳以太亮点区块链科技有限公司</h3>
      <div>
        <div class="left">
          <p class="s-font">可用余额(元)</p>
          <p class="s-font">金牌代理</p>
          <br />
          <span class="money">{{$root.user.coin}}</span>
        </div>
        <el-divider class="fiddle" direction="vertical" content-position="right"></el-divider>
        <div class="right">
          <p class="s-font">客服电话</p>
          <p class="s-font">&nbsp;</p>
          <br />
          <span class="tel">+86 176 7041 5275</span>
        </div>
      </div>
      <div class="tips s-font">
        <p>提示：根据《中华人民共和国网络安全法》相关规定，请您尽快填写真实的身份信息进行实名认证！</p>
      </div>
    </el-card>
    <el-card class="box-card">
      <h3>功能区</h3>
      <div>
        <el-row>
          <el-button size="small" type="primary">主要按钮</el-button>
          <el-button size="small" type="success">成功按钮</el-button>
          <el-button size="small" type="info">信息按钮</el-button>
          <el-button size="small" type="warning">警告按钮</el-button>
        </el-row>
        <br />
        <el-row>
          <el-button size="small" type="primary">主要按钮</el-button>
          <el-button size="small" type="success">成功按钮</el-button>
          <el-button size="small" type="info">信息按钮</el-button>
          <el-button size="small" type="warning">警告按钮</el-button>
        </el-row>
      </div>
    </el-card>
    <el-card class="box-card">
      <h3>待办事项</h3>
      <div>
        <el-collapse v-model="activeNames" @change="handleChange">
          <el-collapse-item title="工单信息" name="1"></el-collapse-item>
          <el-collapse-item title="收支明细" name="2"></el-collapse-item>
          <el-collapse-item title="登录日志" name="3"></el-collapse-item>
        </el-collapse>
      </div>
    </el-card>
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
          <el-button
            type="primary"
            plain
            style="width: 100%"
            @click="doRecharge(item.id)"
          >{{item.name}}</el-button>
        </el-form-item>
      </el-form>
      <span slot="footer" class="dialog-footer">
        <el-button type="primary" @click="rechargeDialog = false">关 闭</el-button>
      </span>
    </el-dialog> -->
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
      platforms: [
        "PC - 百度",
        "PC - 搜狗",
        "PC - 360",
        "Mobile - 百度",
        "Mobile - 搜狗",
        "Mobile - 360"
      ],
      status: ["进行中", "已完成", "已退款"]
    };
  },
  methods: {
    doChangePassword() {
      if (
        this.changePassword.newPassword != this.changePassword.confirmPassword
      ) {
        this.$message({
          showClose: true,
          message: "两次输入的新密码不一致",
          type: "error"
        });
        return;
      }
      this.api("/user/change-password", this.changePassword).then(resp => {
        this.changePasswordDialog = false;
        this.changePassword = {};
        this.$message({
          showClose: true,
          message: "密码修改成功"
        });
      });
    },
    recharge() {
      this.api("/user/recharge/plans").then(resp => {
        this.rechargePlans = resp.data;
        this.rechargeDialog = true;
      });
    },
    doRecharge(rid) {
      this.api("/user/recharge/query", {
        rid,
        backUrl: location.href
      }).then(resp => {
        location.href = resp.data;
      });
    }
  },

  mounted() {}
};
</script>

<style scoped>
table {
  width: 100%;
}
.fiddle {
  height: 100px;
}
.box-card {
  margin: 10px;
  min-width: 400px;
  padding: 5px;
  float: left;
  min-height: 300px;
}
.right {
  min-height: 100px;
  width: 50%;
  float: right;
}
.task {
  margin: 5px;
  max-height: 30px;
}
.span-left {
  float: left;
}
.span-right {
  float: right;
}
.s-font {
  font-size: 13px;
}
.money {
  font-size: 18px;
  color: #e74c3c;
}
.tips {
  margin-top: 10px;
  width: 350px;
}
.tips p {
  color: #e74c3c;
}
.tel {
  font-size: 18px;
  color: #2ecc71;
}
.left {
  min-height: 100px;
  width: 40%;
  float: left;
}
table tr {
  width: 100%;
}

table tr td {
  width: 50%;
}
</style>
