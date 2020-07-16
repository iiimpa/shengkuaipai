<template>
  <div class="">
    <div>
      <div class="pm flexs">
        <div class="pmL">
          <span style="font-size: 36px;">升快排</span>
          <div>服务时间：周一至周日 9:00-18:00</div>
          <div>
            客服电话：
            <span style="font-size: 24px;">400-68-18178</span>
          </div>
        </div>
        <div class="pmR flexs">
          <div v-for="(item, index) in uplist" :key="index" class="uplist">
            <p style="font-size: 18px;margin-bottom: 30px;">{{ item.tit }}</p>
            <div v-for="(value, idx) in item.name" style="font-size: 14px;color: #666;" :key="idx">
              <p v-if="!value.link">{{ value.txt }}</p>
              <a v-if="value.link" :href="value.link">{{ value.txt }}</a>
            </div>
          </div>
        </div>
      </div>
      <div class="db">
        <p>Copyright © 2013-2016www.skp.cn. All Rights Reserved. 升快排 版权所有</p>
      </div>
    </div>
  </div>
</template>
<script>
  export default {
    name: 'upFooter',
    data() {
      return {
        uplist: [
          {
            tit: '升排名',
            name: [
              {
                txt: '搜索'
              },
              {
                txt: '选择我们'
              }
            ]
          },
          {
            tit: '排名优化',
            name: [
              {
                txt: '百度排名优化'
              },
              {
                txt: '百度移动端'
              },
              {
                txt: 'PC下拉'
              }
            ]
          },
          {
            tit: '服务介绍',
            name: [
              {
                txt: '产品介绍'
              },
              {
                txt: '产品特征比较'
              },
              {
                txt: '产品优势'
              }
            ]
          },
          {
            tit: 'SEO知识',
            name: [
              {
                txt: '成功案例'
              },
              {
                txt: '常见问题'
              },
              {
                txt: '友情链接',
                link: "/#/friend"
              },
            ]
          }
        ]
      };
    },
    methods: {}
  };
</script>

<style scoped lang="scss">
  .db {
    width: 470px;
    padding-bottom: 30px;
    margin: 0 auto;
    font-size: 12px;
    text-align: center;
    color: #a9a9a9;
  }

  .pm {
    width: 1200px;
    margin: 0 auto;
    align-items: flex-start;
    margin-bottom: 30px;
    border-bottom: 1px solid #e4e4e4;
    padding-bottom: 60px;

    .pmL {
      font-size: 14px;
      color: #666666;

      img {
        width: 99px;
        height: 32px;
      }
    }

    .pmR {
      margin-left: 170px;
      align-items: flex-start;

      .uplist {
        margin-right: 80px;
      }
    }
  }
</style>
