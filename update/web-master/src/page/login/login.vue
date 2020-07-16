<template>
  <div class="content flexs j_center">
    <div class="box">
      <div class="loginImg"><img src="../../assets/img/shengpaimiang27.png" alt=""/></div>
      <div class="login_r">
        <div style="font-size: 22px;text-align: center;margin-bottom: 20px;">登录</div>
        <div class="login_inp">
          <span style="width: 50px;display: inline-block;">用户名</span>
          <input v-model="form.account" type="text"/>
        </div>
        <div class="login_inp">
          <span style="width: 50px;display: inline-block;">密码</span>
          <input v-model="form.password" type="password"/>
        </div>
        <div style="margin-top: 15px;" class="flexs j_bew">
          <el-checkbox v-model="checked"><span style="font-size: 12px;">一个月内免登录</span></el-checkbox>
          <el-button type="text" @click="open"><span style="font-size: 12px;">忘记密码</span></el-button>
        </div>
        <div @click="onIndex" class="btn cur_p">登录</div>
        <div class="db">
          还没账号吗？
          <span class="cur_p" @click="go_res">立即注册</span>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  import CryptoJS from 'crypto-js';

  export default {
    components: {},
    data() {
      return {
        checked: false,
        test:{
          content:""
        },
        form:{
          account:"",
          password:""
        },
        required:{
          Token:"e09b4497f482714d1b7e4460c9333cad"
        }
      };
    },
    created() {
    },
    mounted() {
    },
    methods: {
      open() {
        this.$alert('暂不提供自主找回功能，请联系客服处理', '忘记密码', {
          confirmButtonText: '确定',
          callback: action => {
          }
        });
      },
      go_res() {
        this.$router.replace('/register');
      },
      onIndex() {
        var that = this;
        if (this.form.account == '' || this.form.password == '') {
          this.$message({
            message: '请输入用户名和密码',
            type: 'warning'
          });
          return;
        }
        this.api("/auth/login", this.form).then(res => {
          localStorage.setItem('token', res.token);
          this.api("/user/info", null, false).then(res => {
            this.$root.user = res.data;
            this.$message({
              message: '登录成功',
              type: 'success'
            });
            this.$router.replace("/user/order")
          });
        });
      },
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
        // max-width: 100%;
        width: 650px;
      }
    }

    .login_r {
      .login_inp {
        margin-top: 30px;
        width: 300px;
        font-size: 16px;
        border-bottom: 1px solid #dadada;
        padding-bottom: 10px;

        input {
          width: 200px;
        }
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
    background: linear-gradient(-82deg, rgba(68, 102, 228, 1), rgba(5, 141, 255, 1));
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
