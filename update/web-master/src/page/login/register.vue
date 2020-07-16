<template>
  <div class="content flexs j_center">
    <div class="box">
      <div class="loginImg">
        <img src="../../assets/img/shengpaimiang27.png" alt />
      </div>
      <div class="login_r">
        <div style="font-size: 22px;text-align: center;margin-bottom: 20px;">注册</div>
        <div class="login_inp">
          <span style="width: 80px;display: inline-block;">用户名</span>
          <input placeholder="请输入用户名" v-model="form.account" type="text" />
        </div>
        <div class="login_inp">
          <span style="width: 80px;display: inline-block;">密码</span>
          <input placeholder="请输入密码" v-model="form.password" type="password" />
        </div>
        <div class="login_inp">
          <span style="width: 80px;display: inline-block;">确认密码</span>
          <input placeholder="请确认输入密码" v-model="form.rpassword" type="password" />
        </div>
        <div class="login_inp">
          <span style="width: 80px;display: inline-block;">手机号</span>
          <input placeholder="请输入手机号" maxlength="11" v-model="form.tel" type="text" />
        </div>
        <div class="login_inp">
          <span style="width: 80px;display: inline-block;">邮箱</span>
          <input placeholder="请输入邮箱" v-model="form.email" type="text" />
        </div>
        <div @click="regist" class="btn cur_p">注册</div>
        <!-- <div
          style="cursor: pointer;font-size: 14px; color: #008bfe;"
          @click="showProtocol=true"
        >《用户协议》</div> -->
        <div class="db">
          已有账号？
          <span class="cur_p" @click="go_login">立即登录</span>
        </div>
      </div>
    </div>
    <el-dialog title="用户注册使用协议" :visible.sync="showProtocol">
      <h4>前言</h4>本网站由深圳市以太亮点区块链科技有限公司所有和负责运营。
      <h4>一、用户注册</h4>
      <p>1、为了使用升排名提供的服务，用户需要创建一个升排名账户。</p>
      <p>2、注册用户具备完全民事权利能力和与所从事的民事行为相适应的行为能力的自然人、法人，或者是具有合法经营资格的实体组织。无民事行为能力人、限制民事行为能力人以及无经营或特定经营资格的组织不当注册为升快排用户或超过其民事权利或行为能力范围而从事交易的，其与升快排之间的服务协议自动无效，一经发现，升快排有权立即注销该用户，并追究其使用升快排中一切“服务”的任何法律责任。如您代表一家公司或其他法律主体在升快排注册登记，则您声明和保证，您有权使该公司或该法律主体受本协议“条款”的约束。</p>

      <h4>二、服务使用</h4>
      <p>用户同意：</p>
      <p>（1）提供准确的用户资料。</p>
      <p>（2）不恶意创建任务导致网站速度变慢或是卡死，其带来的损失将由恶意用户承担。</p>
      <p>（3）用户同意遵守《中华人民共和国保守国家秘密法》、《中华人民共和国计算机信息系统安全保护条例》、《计算机软件保护条例》等有关计算机及互联网规定的法律和法规、实施办法。在任何情况下，升快排合理地认为用户的行为可能违反上述法律、法规，升快排可以在任何时候，不经事先通知终止向该用户提供服务，不需对用户或第三方负责。</p>

      <h4>三、免责条款</h4>
      <p>对用户使用本站带来的风险不承担任何责任。</p>
      <p>升快排保留随时修改此用户服务协议内容及其它相关文件的权利，本用户服务协议、其它相关文件以及与其相关的内容如有变动恕不另行通知。</p>

      <h4>四、解释权</h4>
      <p>本注册协议的解释权归深圳市以太亮点区块链科技有限公司所有。如果其中有任何条款与国家的有关法律相抵触，则以国家法律的明文规定为准。</p>
    </el-dialog>
  </div>
</template>

<script>
export default {
  components: {},
  data() {
    return {
      showProtocol: false,
      checked: false,
      form: {
        account: "",
        nickname:"kzbi",
        password: "",
        rpassword: "",
        tel: "",
        email: "",
        url:window.location.href
      },
      tips: {
        account: "请输入用户名",
        password: "请输入密码",
        rpassword: "请输入确认密码",
        cell: "请输入手机号",
        email: "请输入邮箱"
      }
    };
  },
  methods: {
    go_login() {
      this.$router.replace("/login");
    },
    regist() {
      if (!/^1[3456789]\d{9}$/.test(this.form.tel)) {
        this.$message({
          message: "手机号格式错误，请重新输入！",
          type: "warning"
        });
        return false;
      }
      if (this.form.password != this.form.rpassword) {
        this.$message({
          message: "两次密码输入不一致！",
          type: "warning"
        });
        this.form.password = "";
        this.form.rpassword = "";
        return false;
      }
      this.form.invite = this.$route.query.invite
        ? parseInt(this.$route.query.invite)
        : 0;
      this.api("/auth/register", this.form).then(res => {
        localStorage.setItem("token", res.token);
        this.api("/user/info", null, false).then(res => {
          this.$root.user = res.data;
          this.$message({
            message: "注册成功！",
            type: "success"
          });
          this.$router.replace("/home");
        });
      });
    }
  }
};
</script>

<style scoped lang="scss">
.content {
  width: 100vw;
  height: 100vh;
  color: #666;
  background-color: rgba($color: #008bfe, $alpha: 0.2);

  .box {
    padding: 0 70px;
    padding-top: 55px;
    display: flex;
    justify-content: space-between;
    box-sizing: border-box;
    width: 80%;
    height: 70%;
    border-radius: 5px;
    background: #ffffff;
  }

  .loginImg {
    img {
      width: 650px;
    }
  }

  .login_r {
    .login_inp {
      margin-top: 30px;
      width: 300px;
      font-size: 14px;
      border-bottom: 1px solid #dadada;
      padding-bottom: 10px;
    }

    .db {
      text-align: center;
      font-size: 12px;

      span {
        color: #058dff;
      }
    }
  }
}

.btn {
  width: 300px;
  height: 40px;
  line-height: 40px;
  font-size: 12px;
  color: #ffffff;
  text-align: center;
  background: linear-gradient(
    -82deg,
    rgba(68, 102, 228, 1),
    rgba(5, 141, 255, 1)
  );
  border-radius: 1px;
  margin-top: 14px;
  margin-bottom: 30px;
}

@media screen and (max-width: 1400px) {
  .box {
    width: 1200px !important;
  }
}
</style>
