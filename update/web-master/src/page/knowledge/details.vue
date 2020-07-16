<template>
  <div>
    <div class="lunb"><img src="../../assets/img/shengpaimiang5.png" alt=""/></div>
    <div class="box">
      <h2>{{detail.title}}</h2>
      <div class="top flexs">
        <img src="../../assets/img/shengpaimiang50.png" alt=""/>
        <span>发布时间：{{detail.createdAt}}</span>
      </div>
      <div class="center" v-html="detail.content"></div>
    </div>
  </div>
</template>

<script>
  export default {
    components: {},
    data() {
      return {
        knowId: 0,
        detail: {}
      };
    },
    mounted() {
      this.knowId = this.$route.query.id;
      this.getDetail();
    },
    methods: {
      getDetail() {
        this.api("/public/kn/detail", {id: parseInt(this.$route.query.id)}).then(resp => {
          this.detail = resp.data;
        })
      },
      tabs(index) {
        this.cur = index;
      }
    }
  };
</script>

<style scoped lang="scss">
  .lunb {
    img {
      width: 100%;
    }
  }

  .tabs {
    margin-top: 60px;
    justify-content: center;

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
    margin-top: 58px;
    margin-bottom: 80px;

    h2 {
      font-size: 36px;
      font-family: Microsoft YaHei;
      font-weight: bold;
      color: #058dff;
      text-align: center;
    }

    .top {
      justify-content: center;

      img {
        margin-right: 14px;
      }

      border-bottom: 1px dashed #999999;
      padding-bottom: 30px;
    }

    .center {
      font-size: 16px;
      padding: 40px 0;
      border-bottom: 1px dashed #999999;
    }

    .bottom {
      margin-top: 28px;
      justify-content: space-between;
    }

    .b_left,
    .b_right {
      font-size: 16px;
      color: #058dff;
    }

    .b_left {
      img {
        margin-right: 12px;
      }
    }

    .b_right {
      img {
        margin-left: 12px;
      }
    }
  }
</style>
