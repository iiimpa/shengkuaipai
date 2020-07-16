<template>
  <div>
    <div class="lunb"><img src="assets/img/shengpaimiang2.png" alt=""/></div>
    <div class="buzhou">
      <ul class="flexs">
        <li style="color: #058DFF;">添加关键字</li>
        <li class="hong"></li>
        <li>支付费用</li>
        <li class="hong"></li>
        <li>开始优化</li>
      </ul>
    </div>
    <div class="search flexs">
      <p>http://</p>
      <input v-model="key" placeholder="请输入您要优化的网站" type="text"/>
      <div @click="onSearch" onselectstart="return false" class="cur_p s_btn">查询一下</div>
    </div>
    <!-- 关键字 -->
    <div class="keys">
      <p style="text-align: center;font-size: 22px;margin-bottom: 24px;">关键字排名</p>
      <div class="flexs sel">
        选择搜索引擎：
        <el-radio-group v-model="platform">
          <el-radio :label="0">PC百度</el-radio>
          <el-radio :label="1">PC搜狗</el-radio>
          <el-radio :label="2">PC360</el-radio>
          <el-radio :label="3">手机百度</el-radio>
          <el-radio :label="4">手机搜狗</el-radio>
          <el-radio :label="5">手机360</el-radio>
        </el-radio-group>
        <div @click="onShow" class="shoud cur_p" style="color: red;">添加关键字</div>
      </div>
      <!-- 表格 -->
      <div class="table">
        <div class="top">
          <span v-for="item in tabtop">{{ item }}</span>
        </div>
        <div v-for="(item, index) in tableData" :key="index" class="tbox flexs">
          <div class="titem tsel cur_p flexs">{{ item.keyword }}</div>
          <div class="titem">{{ item.rank }}</div>
          <div class="titem">{{ item.status }}</div>
          <div class="titem flexs xx">{{item.time}}</div>
          <div class="titem">{{ item.hard }}</div>
          <div style="color: #F86B34;" class="titem">
            <span style="cursor: pointer;" @click="reCheck(index)">重新查询</span>
            <span style="cursor: pointer;">删除</span>
          </div>
        </div>
      </div>
    </div>
    <div @click="addToCart()" class="join cur_p"><p>加入购物车</p></div>
    <!-- 弹框 -->
    <div v-show="show" class="tank">
      <div class="tktop flexs">
        <span>手动添加关键字</span>
        <img class="cur_p" @click="show = false" src="assets/img/shengpaimiang35.png" alt=""/>
      </div>
      <!--      <div class="tkbox">-->
      <!--        <span>网址：</span>-->
      <!--        <input placeholder="请输入网址" type="text"/>-->
      <!--      </div>-->
      <div class="tkbox">
        <span>关键字：</span>
        <textarea placeholder="请输入关键字" v-model="keys"></textarea>
      </div>
      <div class="tkbox"><span>说明：</span></div>
      <div class="txt flexs">
        <i></i>
        <span>关键词输入 一行一个关键词</span>
      </div>
      <div class="txt flexs">
        <i></i>
        <span>卡如果系统没有列出你需要优化的关键字，请在这里自行添加</span>
      </div>
      <div @click="okn()" class="tkbtn cur_p">确定</div>
    </div>
  </div>
</template>

<script>
  export default {
    components: {},
    data() {
      return {
        radio: 0,
        show: false,
        key: '',
        keys: [],
        platform: 0,
        tabtop: ['关键字', '当前排名', '状态', '查询时间', '优化难度', '操作'],
        tableData: []
      };
    },
    watch: {
      show(e) {
        if (e) {
          document.body.addEventListener(
            'touchmove',
            function (e) {
              e.preventDefault();
            },
            {passive: false}
          );
        }
      }
    },
    mounted() {
      this.key = this.$route.query.domain;
    },
    methods: {
      addToCart() {
        var checkedKeys = [];
        for (var item in this.tableData) {
          if (this.tableData[item].check == true) {
            checkedKeys.push(this.tableData[item].key)
          }

        }
        this.$router.push({
          path: "/cart",
          query: {
            domain: this.key,
            keywords: checkedKeys.join(",")
          }
        })
      },
      onShow() {
        this.show = true;
      },
      okn() {
        this.show = false;
        var keysArr = this.keys.split("\n");
        for (var i in keysArr) {
          var line = {
            keyword: keysArr[i],
            rank: '查询中',
            status: '查询中',
            time: '查询中',
            hard: '中等难度'
          };
          this.tableData.push(line);
          this.reCheck(this.tableData.indexOf(line));
        }
      },
      reCheck(index) {
        this.tableData[index].rank = '查询中';
        this.tableData[index].status = '查询中';
        this.tableData[index].time = '查询中';
        this.api("/user/rank", {
          platform: parseInt(this.platform),
          domain: this.key,
          keyword: this.tableData[index].keyword
        }).then(resp => {
          this.tableData[index].rank = resp.data == 0 ? '无' : resp.data;
          this.tableData[index].status = (resp.data > 0 && resp.data < 51) ? '可以优化' : '不可优化';
          this.tableData[index].time = (new Date()).toLocaleTimeString();
        })
      },
      goCart() {
        this.$router.push('/cart');
      },
      onSearch() {
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

  .search {
    max-width: 1200px;
    margin: 0 auto;
    margin-top: 60px;
    justify-content: space-between;

    p {
      font-size: 38px;
      color: #058dff;
    }

    input {
      width: 800px;
      height: 70px;
      box-sizing: border-box;
      padding: 0 30px;
      background: rgba(255, 255, 255, 1);
      box-shadow: 0px 6px 24px 0px rgba(192, 192, 192, 0.25);
      border-radius: 6px;
      font-size: 22px;
    }

    .s_btn {
      width: 180px;
      height: 70px;
      line-height: 70px;
      text-align: center;
      color: #ffffff;
      font-size: 22px;
      background: linear-gradient(-69deg, rgba(68, 102, 228, 1), rgba(5, 141, 255, 1));
      box-shadow: 0px 4px 14px 0px #62aeee;
      border-radius: 6px;
    }
  }

  .keys {
    max-width: 1200px;
    margin: 0 auto;
    margin-top: 58px;
    margin-bottom: 60px;

    .sel {
      font-size: 14px;
      justify-content: center;
      line-height: 1;

      .shoud {
        margin-left: 30px;
      }
    }

    .top {
      margin-top: 24px;

      span {
        display: inline-block;
        width: 200px;
        text-align: center;
        height: 70px;
        line-height: 70px;
        background: #f8f8f8;
        font-size: 18px;
      }
    }
  }

  .tbox {
    font-size: 16px;
    height: 60px;
    line-height: 60px;
    background-color: #fdfdfd;

    .titem {
      width: 200px;
      text-align: center;
    }

    .tsel {
      justify-content: center;

      img {
        margin-right: 10px;
      }
    }

    &:nth-child(odd) {
      background: #f8f8f8;
    }

    .xx {
      justify-content: center;

      img {
        width: 20px;
        height: 20px;
        margin-left: 10px;
      }
    }
  }

  .join {
    max-width: 1200px;
    margin: 0 auto;
    text-align: right;
    margin-bottom: 70px;

    p {
      display: inline-block;
      width: 180px;
      height: 70px;
      line-height: 70px;
      text-align: center;
      font-size: 22px;
      color: #058dff;
      border: 1px solid rgba(5, 141, 255, 1);
      border-radius: 6px;
    }
  }

  .buzhou {
    max-width: 1200px;
    margin: 0 auto;
    margin-top: 30px;
    font-size: 22px;
    color: #999999;

    ul {
      justify-content: center;

      .hong {
        width: 20px;
        height: 2px;
        background-color: #999999;
        margin: 0 20px;
      }
    }
  }

  .zhezhao {
    position: fixed;
    width: 100vw;
    height: 100vh;
    background-color: rgba($color: #000000, $alpha: 0.4);
    z-index: 100;
  }

  .tank {
    position: fixed;
    top: 50%;
    left: 50%;
    z-index: 999;
    transform: translate(-50%, -50%);
    width: 750px;
    height: 460px;
    background: rgba(255, 255, 255, 1);
    box-shadow: 0px 3px 20px 0px rgba(183, 183, 183, 0.3);

    .tktop {
      font-size: 18px;
      justify-content: space-between;
      padding: 0 38px;
      height: 60px;
      line-height: 60px;
      border-bottom: 1px solid #eeeeee;
    }

    .txt {
      margin-left: 14%;
      margin-bottom: 15px;

      span {
        margin-left: 10px;
        font-size: 16px;
        color: #666;
      }

      i {
        width: 6px;
        height: 6px;
        background: rgba(5, 141, 255, 1);
        border-radius: 50%;
      }
    }

    .tkbtn {
      width: 130px;
      height: 44px;
      background: rgba(77, 161, 255, 1);
      border-radius: 4px;
      line-height: 44px;
      text-align: center;
      color: #ffffff;
      float: right;
      margin-right: 122px;
    }
  }

  .tkbox {
    display: flex;
    align-items: flex-start;
    padding: 0 50px;
    margin-top: 25px;

    span {
      margin-top: 10px;
      width: 80px;
      font-size: 16px;
    }

    input {
      box-sizing: border-box;
      width: 500px;
      height: 44px;
      padding-left: 30px;
      background: rgba(255, 255, 255, 1);
      border: 1px solid rgba(238, 238, 238, 1);
      border-radius: 4px;
      font-size: 16px;
    }

    textarea {
      box-sizing: border-box;
      width: 500px;
      height: 120px;
      padding-top: 14px;
      padding-left: 30px;
      background: rgba(255, 255, 255, 1);
      border: 1px solid rgba(238, 238, 238, 1);
      border-radius: 4px;
      font-size: 16px;
    }
  }
</style>
