<template>
  <div>
    <div class="lunb"><img src="../../assets/img/shengpaimiang11.png" alt="" /></div>
    <div class="tabs flexs">
      <div @click="tabs(index)" v-for="(item, index) in tabList" :class="{ active: cur == index }" class="tabitem cur_p" :key="index">
        <span>{{ item.t }}</span>
      </div>
    </div>
    <div v-show="cur == 0" class="box">
      <p class="upp">
        升排名提供百度排名优化服务，为用户分析SEO优化数据。实行关键词按日付费推广，在投入成本最小化的同时实现客户利益的最大化。通过对搜索引擎目标关键词的优化，使目标网站的相关信息显示在百度首页，为企业挖掘更多的潜在客户的同时带来海量的商机，客户按照每日实际展示的关键词数量与排名情况支付推广服务费用。
      </p>
      <div v-for="(item, index) in list" :key="index" class="item flexs cur_p">
        <img :src="item.src" alt="" />
        <p>{{ item.txt }}</p>
        <span>{{ item.span }}</span>
      </div>
    </div>
    <div v-show="cur == 1" class="box">
      <div class="server flexs">
        <div class="b_left">
          <h3>竞价排名（营销面临困难）</h3>
          <p>
            <i></i>
            成本高
          </p>
          <span>按点击付费，大量无效或恶意点击导致营销成本提升</span>
        </div>
        <div class="b_right">
          <h3>升排名（全面解决方案）</h3>
          <p>
            <i></i>
            成本高
          </p>
          <span>按点击付费，大量无效或恶意点击导致营销成本提升</span>
        </div>
      </div>
      <div class="table">
        <el-table :data="tableData" stripe style="width: 100%">
          <el-table-column label="比较项目" prop="date"></el-table-column>
          <el-table-column label="升排名(按日收费)" prop="name"></el-table-column>
          <el-table-column label="竞技排名" prop="address"></el-table-column>
          <el-table-column align="right"><template slot-scope="scope"></template></el-table-column>
        </el-table>
      </div>
    </div>
    <div v-show="cur == 2" class="box">
      <div v-for="(item, index) in list" :key="index" class="item flexs cur_p">
        <img :src="item.src" alt="" />
        <div class="">
          <p>{{ item.txt }}</p>
          <span>{{ item.span }}</span>
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
      tabList: [
        {
          t: '产品介绍'
        },
        {
          t: '产品特征比较'
        },
        {
          t: '产品优势'
        }
      ],
      list: [
        {
          id: 1,
          src: require('../../assets/img/shengpaimiang45.png'),
          txt: '自主',
          span: 'SEM操作面板 自主添加关键词 关键词日均价格估算'
        }
        // {
        //   id: 2,
        //   src: require('../../assets/img/shengpaimiang46.png'),
        //   txt: '实惠',
        //   span: '关键字优化费用远低于百度竞价的花费   关键词一页外，无需付费   点击不付费   充值消费'
        // }
      ],
      tableData: [
        {
          date: '收费说明',
          name: '王小虎',
          address: '上海市普陀区金沙江路 1518 弄'
        },
        {
          date: '波动性说明',
          name: '王小虎',
          address: '上海市普陀区金沙江路 1517 弄'
        },
        {
          date: '显示时间说明',
          name: '王小虎',
          address: '上海市普陀区金沙江路 1519 弄'
        }
      ],
      cur: 0
    };
  },
  mounted() {
    console.log(this.$route);
  },
  methods: {
    tabs(index) {
      this.cur = index;
    }
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
  margin-top: 58px;
  margin-bottom: 80px;
}
.item {
  position: relative;
  width: 1180px;
  height: 120px;
  border: 1px solid rgba(238, 238, 238, 1);
  border-radius: 10px;
  box-sizing: border-box;
  padding-left: 90px;
  margin-bottom: 30px;
  img {
    width: 38px;
    height: 37px;
    margin-right: 30px;
  }
  p {
    font-size: 18px;
    margin-right: 23px;
  }
  span {
    color: $fontColor;
  }
  &:hover {
    border: 1px solid #058dff;
  }
}
.item_r {
  width: calc(100% - 366px);
  p {
    color: #999999;
    font-size: 28px;
    margin-bottom: 25px;
  }
  span {
    display: -webkit-box;
    -webkit-box-orient: vertical;
    -webkit-line-clamp: 2;
    overflow: hidden;
    font-size: 16px;
    color: #666;
  }
}
.upp {
  margin-bottom: 40px;
  font-size: 16px;
  color: $fontColor;
}
.server {
  justify-content: space-between;
  .b_left,
  .b_right {
    width: 580px;
    height: 380px;
    box-sizing: border-box;
    padding-left: 18px;
    border: 2px solid rgba(238, 238, 238, 1);
    border-radius: 10px;
    h3 {
      text-align: center;
    }
    p {
      margin-bottom: 14px;
      font-weight: 600;
      font-size: 18px;
    }
    span {
      font-size: 18px;
      color: #666;
    }
    i {
      display: inline-block;
      width: 8px;
      height: 8px;
      background: rgba(51, 51, 51, 1);
      border-radius: 50%;
    }
  }
  .b_right {
    h3 {
      color: #058dff;
    }
    border: 2px solid rgba(5, 141, 255, 1);
  }
}
.table {
  margin-top: 60px;
}
@keyframes namemf {
  from {
    transform: rotate(0deg);
  }
  to {
    transform: rotate(360deg);
  }
}

</style>
