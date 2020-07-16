<template>
  <div>
    <div class="box flexs">
      <div v-for="(item, index) in list" :key="index" @click="goBd(item)" class="item cur_p">
        <div v-if="!item.image" :style="{ backgroundImage: 'url(' + backgroundImage() + ')' }" class="top">{{ item.name
          }}
        </div>
        <div v-if="item.image" :style="{ backgroundImage: 'url(' + item.image+')' }" class="top">{{ item.name }}</div>
        <div class="bottom">
          <p style="color: #FE8226;">{{ item.link }}</p>
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
        list: [],
        cur: 0,
        allBackgroundImages: [
          require('../assets/img/bj1.png'),
          require('../assets/img/bj2.png'),
          require('../assets/img/bj3.png'),
          require('../assets/img/bj4.png'),
          require('../assets/img/bj5.png')
        ]
      };
    },
    computed: {},
    mounted() {
      this.getCases();
    },
    methods: {
      backgroundImage() {
        const randIndex = Math.floor(Math.random() * this.allBackgroundImages.length);
        return this.allBackgroundImages[randIndex];
        // return {
        //   bg: `url(${this.allBackgroundImages[randIndex]})`
        // };
      },
      getCases() {
        this.api("/public/friendlink").then(resp => this.list = resp.data);
      },
      goBd(item) {
        window.open(item.link);
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
    flex-wrap: wrap;
  }

  .item {
    width: 276px;
    height: 190px;
    background: rgba(255, 255, 255, 1);
    border-radius: 10px;
    padding: 20px;
    box-sizing: border-box;
    margin-right: 25px;
    margin-bottom: 40px;
    box-shadow: 0px 3px 20px 0px rgba(183, 183, 183, 0.3);

    .top {
      text-align: center;
      width: 237px;
      height: 120px;
      line-height: 120px;
      color: #ffffff;
      overflow: hidden;
    }

    .pm {
      color: #666;
      margin-top: 30px;
      justify-content: space-between;
      text-align: center;
    }

    &:hover {
      transition: 0.1s;
      transform: translateY(-10px);
    }

    &:nth-child(4n) {
      margin-right: 0;
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

  .bottom {
    margin-top: 10px;
    font-size: 14px;
    text-align: center;
  }
</style>
