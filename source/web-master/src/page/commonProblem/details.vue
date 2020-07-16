<template>
  <div>
    <div class="lunb"><img src="../../assets/img/shengpaimiang19.png" alt="" /></div>
    <div class="box">
      <h2>网站SEO优化之关键词因素</h2>
      <div class="top flexs">
        <img src="../../assets/img/shengpaimiang50.png" alt="" />
        <span>发布时间：2019年11月17日 09:29:40</span>
      </div>
      <div class="center">说到关键词，很多人新手站长都存在误区，甚至包括一些做过一段时间SEO的都存在误区。为什么这样说呢？因为我接触到的很多人都提出过类似的问题。</div>
      <div class="flexs bottom">
        <div class="b_left flexs">
          <img src="../../assets/img/img01.png" alt="" />
          <!-- <img src="../../assets/img/img02.png" alt=""> -->
          <span>上一篇 大爱无疆2018全国分校年度盛典隆重开启！</span>
        </div>
        <div class="b_right flexs">
          <span>上一篇 大爱无疆2018全国分校年度盛典隆重开启！</span>
          <img src="../../assets/img/img03.png" alt="" />
          <!-- <img src="../../assets/img/img04.png" alt=""> -->
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  components: {},
  data() {
    return {
      knowId: 0
    };
  },
  mounted() {
    this.knowId = this.$route.query.id;
    console.log(this.knowId);
  },
  methods: {
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
