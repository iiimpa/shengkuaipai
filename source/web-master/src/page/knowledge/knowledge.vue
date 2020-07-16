<template>
  <div>
    <div class="lunb"><img src="../../assets/img/shengpaimiang5.png" alt=""/></div>
    <div class="tabs flexs">
      <div @click="tabs(index)" v-for="(item, index) in tabList" :class="{ active: cur == index }" class="tabitem cur_p"
           :key="index">
        <span>{{ item}}</span>
      </div>
    </div>
    <div class="box">
      <div v-for="(item, index) in list" :key="index" @click="details(item)" class="item cur_p flexs">
        <img :src="item.image" alt=""/>
        <div class="item_r">
          <p>{{ item.createdAt }}</p>
          <p style="font-size: 22px;">{{ item.title }}</p>
        </div>
      </div>
      <!--      <div class="flexs" style="justify-content: center;">-->
      <!--        <el-pagination @current-change='currChange' background layout="prev, pager, next" :total="100"></el-pagination>-->
      <!--      </div>-->
    </div>
  </div>
</template>

<script>
  export default {
    components: {},
    data() {
      return {
        tabList: [],
        list: [],
        cur: 0
      };
    },
    methods: {
      tabs(index) {
        this.cur = index;
        this.getList(this.tabList[index]);
      },
      details(item) {
        this.$router.push({path: '/knowDetails', query: {id: item.id}});
      },
      getCategory(cb) {
        this.api("/public/kn/categories").then(resp => {
          this.tabList = resp.data;
          cb(resp.data[0]);
        })
      },
      getList(cat) {
        this.api("/public/kn/list", {category: cat}).then(resp => {
          this.list = resp.data;
        })
      }
    },
    mounted() {
      this.getCategory(this.getList)
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
  }

  .item {
    padding: 25px 0;
    margin-bottom: 60px;

    img {
      width: 306px;
      height: 209px;
      border-radius: 10px;
      margin-right: 60px;
    }

    border-bottom: 1px solid #eeeeee;

    &:hover {
      p {
        color: #058dff;
      }

      border-bottom: 1px solid #058dff;
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
</style>
