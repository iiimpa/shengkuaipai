<template>
  <div>
    <div class="f_btn">
      <!-- <a href="tencent://message/?uin=1912939243&Site=Sambow&Menu=yes">
        <div class="fix cur_p"><img src="../assets/img/shengpaimiang55.png" alt="" /></div>
      </a> -->
<!--      <div @click="gott()" style="margin-top: 30px;" class="fix cur_p"><img src="../assets/img/shengpaimiang55.png" alt="" /></div>
      <div @click="goCart()" style="margin-top: 30px;" class="fix cur_p">
        <img src="../assets/img/shengpaimiang56.png" alt="" />
        <i>1</i>
      </div> -->
    </div>
  </div>
</template>

<script>
export default {
  components: {},
  data() {
    return {};
  },
  methods: {
    goCart() {
      this.$router.push('/cart');
    },
    gott() {
      window.open('http://wpa.b.qq.com/cgi/wpa.php?ln=1&key=XzkzODAxMDEyM18yMDk5MThfNDAwNjE1MzcyMV8yXw');
    }
  }
};
</script>

<style lang="scss" scoped>
.f_btn {
  position: fixed;
  top: 50%;
  right: 4%;
  z-index: 9999;
  transform: translateY(-50%);
  .fix {
    display: flex;
    justify-content: center;
    align-items: center;
    width: 90px;
    height: 90px;
    background: #eeeeee;
    box-shadow: 0px 3px 9px 1px rgba(172, 172, 172, 0.2);
    border-radius: 50%;
    position: relative;
    i {
      position: absolute;
      display: block;
      width: 20px;
      height: 20px;
      right: 15px;
      top: 20px;
      background: rgba(248, 107, 52, 1);
      border-radius: 50%;
      font-size: 14px;
      font-style: normal;
      text-align: center;
      line-height: 20px;
      color: #fff;
    }
  }
}
</style>
