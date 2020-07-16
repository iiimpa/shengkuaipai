<template>
  <div class>
    <div class="heard flexs">
      <div class="logo cur_p" @click="toIndex">
        <!-- <span style="font-size: 36px;">升快排</span> -->
      </div>
      <!-- <el-menu :default-active="activeIndex" active-text-color="#409EFF" class="el-menu-demo flexs" mode="horizontal"
               @select="handleSelect">
        <div v-for="(item, index) in list" :key="index" :class="{ twoAct: index == 1 }" class="cur_p">
          <el-menu-item :index="item.url">{{ item.name }}</el-menu-item>
        </div>
      </el-menu>-->
      <div style="margin-left: 300px;">
        <div class="flexs" v-if="!$root.user.account">
          <div @click="go_res" class="btnx cur_p">注册</div>
          <div @click="login" class="btnx l_btn cur_p">登录</div>
        </div>
        <el-popover v-if="$root.user.account" placement="bottom" width="140"
                    trigger="hover">
          <el-button @click="goto()" slot="reference">{{ $root.user.account }}您好！</el-button>
          <div @click="logOut()" class="logOut cur_p">退出账户</div>
        </el-popover>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'upHeader',
  components: {},
  data() {
    return {
      list: [
        {
          name: '首页',
          url: '/home'
        },
        {
          name: 'SEO知识',
          url: '/knowledge'
        },
        {
          name: '服务介绍',
          url: '/service'
        },
        {
          name: '成功案例',
          url: '/case'
        },
        {
          name: '常见问题',
          url: '/common'
        }
      ],
      show: true,
      activeIndex: '/home',
      name: '哈哈哈'
    };
  },
  mounted() {
    this.activeIndex = this.$route.fullPath;
  },
  methods: {
    handleSelect(key, keyPath) {
      this.$router.push(key);
    },
    login() {
      this.$router.push('/login');
    },
    toIndex() {
      this.activeIndex = '/home';
      this.$router.push('/');
    },
    go_res() {
      this.$router.push('/register');
    },
    goto() {
      this.$router.push('/user/order');
      this.activeIndex = '';
    },
    logOut() {
      localStorage.removeItem('token');
      location.reload();
    }
  }
};
</script>

<style scoped lang="scss">
.heard {
  max-width: 1200px;
  margin: 0 auto;
  height: 70px;
  font-size: 16px;

  .logo {
    margin-right: 150px;

    img {
      width: 99px;
      height: 32px;
      object-fit: cover;
    }
  }

  .headItem {
    padding: 0 20px;
  }

  /deep/ .el-submenu__icon-arrow {
    position: static;
  }
}

.btnx {
  width: 80px;
  height: 40px;
  line-height: 40px;
  text-align: center;
  background: rgba(5, 141, 255, 1);
  border-radius: 20px;
  color: #ffffff;
}

.l_btn {
  background-color: #ffffff;
  border: 1px solid rgba(5, 141, 255, 1);
  color: #000000;
  margin-left: 20px;
}

.el-menu.el-menu--horizontal {
  border: none;
}

.user {
  margin-left: 100px;
  height: 56px;
  font-size: 14px;
  line-height: 56px;
  color: #058dff;
}

.logOut {
  width: 140px;
  height: 58px;
  line-height: 58px;
  text-align: center;
}
</style>
