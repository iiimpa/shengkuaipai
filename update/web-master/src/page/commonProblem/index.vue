<template>
  <div>
    <div class="lunb">
      <img src="../../assets/img/shengpaimiang19.png" alt=""/></div>
    <p style="text-align: center;margin: 50px 0;">常见问题</p>
    <div class="box">
      <div v-for="(item, index) in list" :key="index" class="item cur_p">
        <div class="top">
          <!-- <b v-if="index + 1 < 10">0</b> -->
          <b>{{ index + 1 }}</b>
          <span>{{ item.title }}</span>
          <div class="hong"></div>
        </div>
        <span style="color: #666;font-size: 16px;">{{ item.content }}</span>
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    components: {},
    data() {
      return {
        list: []
      };
    },
    methods: {
      getList() {
        this.api("/public/qas").then(resp => this.list = resp.data);
      }
    },
    mounted() {
      this.getList();
    }
  };
</script>

<style scoped lang="scss">
  $fontColor: #666;
  .lunb {
    img {
      width: 100%;
    }
  }

  .tabs {
    justify-content: center;
    margin-top: 60px;

    span {
      padding: 0 20px;
      font-size: 18px;
      border-right: 2px solid #333;
    }

    .tabitem {
      &:last-child span {
        border: none;
      }
    }

    .active {
      color: #058dff;
    }
  }

  .box {
    max-width: 1200px;
    margin: 0 auto;
    background: #fbfbfb;
    margin-bottom: 80px;
    box-sizing: border-box;
    padding: 50px 90px;
  }

  .item {
    margin-top: 50px;

    &:hover {
      transition: 1s;

      .top {
        color: #058dff;
      }

      span {
        &:before {
          width: 100%;
          transition: width 1s;
          -moz-transition: width 1s; /* Firefox 4 */
          -webkit-transition: width 1s; /* Safari 和 Chrome */
          -o-transition: width 1s; /* Opera */
          background: #058dff;
        }
      }
    }
  }

  .top {
    font-weight: 600;
    font-size: 22px;

    p {
      // letter-spacing: -2px;
    }

    span {
      display: inline-block;
      position: relative;
      margin-bottom: 40px;

      &:before {
        content: '';
        position: absolute;
        bottom: -20px;
        left: 0px;
        width: 10%;
        height: 2px;
        // margin: 20px 0;
        background: rgba(51, 51, 51, 1);
      }
    }

    .hong {
      width: 100%;
      height: 40;
    }
  }
</style>
