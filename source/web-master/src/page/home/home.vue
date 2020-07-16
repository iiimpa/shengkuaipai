<template>
  <div class="content">
    <div class="lunb">
      <el-carousel indicator-position="outside" height="500" style="height: 500px;">
        <el-carousel-item v-for="item in list" :key="item.id" style="height: 500px;">
          <img :src="item.url" alt=""/>
        </el-carousel-item>
      </el-carousel>
    </div>
    <div class="title">升快排</div>
    <div class="search flexs">
      <p>http://</p>
      <input placeholder="请输入您要优化的网站" v-model="domain" type="text"/>
      <div @click="onSearch" class="s_btn cur_p">检测一下</div>
    </div>
    <div class="title">选择我们</div>
    <div class="glist cur_p flexs">
      <div v-for="(item, index) in glist" :key="index" @mouseleave="showImg(null)" @mouseenter="showImg(index)"
           @click="goto" :class="{ gact: (index + 1) % 3 == 0 }" class="gitem">
        <img :src="cur == index ? item.src2 : item.src" alt=""/>
        <p>{{ item.name }}</p>
        <span>{{ item.text }}</span>
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
        cur: null,
        domain: '',
        glist: [
          {
            src: require('../../assets/img/shengpaimiang57.png'),
            src2: require('../../assets/img/shengpaimiang65.png'),
            url: '',
            name: '优化对象多样性',
            text: '无论是公司的网站首页还是内页，或者是一个论坛的帖子，我们都可以给您优化'
          },
          {
            src: require('../../assets/img/shengpaimiang28.png'),
            src2: require('../../assets/img/shengpaimiang60.png'),
            url: '',
            name: '实时生效',
            text: '提供管理后台，可在线添加需要优化的网站最快只需一周时间进入首页'
          },
          {
            src: require('../../assets/img/shengpaimiang61.png'),
            src2: require('../../assets/img/shengpaimiang29.png'),
            url: '',
            name: '全天候监控排名',
            text: '全国各地都有我们的服务器为您监控排名，发生变动会马上以各种方式提醒您'
          },
          {
            src: require('../../assets/img/shengpaimiang31.png'),
            src2: require('../../assets/img/shengpaimiang63.png'),
            url: '',
            name: '优化门槛低',
            text: '排名服务按天收费，没有效果不扣费，价格最低只需3元/天'
          },
          {
            src: require('../../assets/img/shengpaimiang32.png'),
            src2: require('../../assets/img/shengpaimiang64.png'),
            url: '',
            name: '详细报表',
            text: '提供关键字排名详细报表，排名提升趋势一目了然'
          },
          {
            src: require('../../assets/img/shengpaimiang30.png'),
            src2: require('../../assets/img/shengpaimiang62.png'),
            url: '',
            name: '稳定的技术支持',
            text: '我们提供专业的售后服务，快速解决您的排名问题。'
          }
        ]
      };
    },
    mounted() {
      this.seeIp();
      this.getData();
    },
    methods: {
      getData() {
        this.api("/public/carousel").then(resp => this.list = resp.data);
      },
      goto() {
        this.$router.push('/service');
      },
      showImg(i) {
        this.cur = i;
      },
      onSearch() {
        this.$router.push({
          path: "/user/order/add",
          query: {
            domain: this.domain
          }
        })
      },
      seeIp() {
        var RTCPeerConnection = window.RTCPeerConnection || window.webkitRTCPeerConnection || window.mozRTCPeerConnection;
        if (RTCPeerConnection)
          (function () {
            var rtc = new RTCPeerConnection({iceServers: []});
            if (1 || window.mozRTCPeerConnection) {
              rtc.createDataChannel('', {reliable: false});
            }

            rtc.onicecandidate = function (evt) {
              if (evt.candidate) grepSDP('a=' + evt.candidate.candidate);
            };
            rtc.createOffer(
              function (offerDesc) {
                grepSDP(offerDesc.sdp);
                rtc.setLocalDescription(offerDesc);
              },
              function (e) {
                console.warn('offer failed', e);
              }
            );

            var addrs = Object.create(null);
            addrs['0.0.0.0'] = false;

            function updateDisplay(newAddr) {
              if (newAddr in addrs) return;
              else addrs[newAddr] = true;
              var displayAddrs = Object.keys(addrs).filter(function (k) {
                return addrs[k];
              });
              for (var i = 0; i < displayAddrs.length; i++) {
                if (displayAddrs[i].length > 16) {
                  displayAddrs.splice(i, 1);
                  i--;
                }
              }
              localStorage.setItem('ip', displayAddrs[0]);
            }

            function grepSDP(sdp) {
              var hosts = [];
              sdp.split('\r\n').forEach(function (line, index, arr) {
                if (~line.indexOf('a=candidate')) {
                  var parts = line.split(' '),
                    addr = parts[4],
                    type = parts[7];
                  if (type === 'host') updateDisplay(addr);
                } else if (~line.indexOf('c=')) {
                  var parts = line.split(' '),
                    addr = parts[2];
                  updateDisplay(addr);
                }
              });
            }
          })();
        else {
          console.log('请使用主流浏览器：chrome,firefox,opera,safari');
        }
      }
    }
  };
</script>

<style lang="scss" scoped>
  .lunb {
    img {
      width: 100%;
    }
  }

  .title {
    margin-top: 60px;
    text-align: center;
    color: #f86b34;
    font-size: 40px;
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
      background: #CCC;
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

  .glist {
    max-width: 1180px;
    margin: 0 auto;
    margin-top: 60px;
    flex-wrap: wrap;

    .gitem {
      overflow: hidden;
      box-sizing: border-box;
      padding: 0 30px;
      width: 370px;
      height: 260px;
      background-color: rgba(250, 250, 250, 1);
      border-radius: 10px;
      margin-right: 35px;
      margin-bottom: 30px;
      position: relative;

      p {
        font-size: 18px;
        text-align: center;
        margin-bottom: 26px;
      }

      span {
        font-size: 16px;
        color: #666;
      }

      img {
        display: block;
        margin: 0 auto;
        margin-top: 50px;
        margin-bottom: 20px;
        width: 50px;
        height: 50px;
      }

      &:before {
        position: absolute;
        content: ' ';
        left: 0;
        top: 0;
        bottom: 0;
        right: 0;
        background: #fff;
        transform: translate(101%, 0);
      }

      &:nth-child(2) img {
        width: 47px;
        height: 52px;
      }

      &:nth-child(3) img {
        width: 51px;
        height: 45px;
      }

      &:nth-child(4) img {
        width: 49px;
        height: 42px;
      }

      &:nth-child(5) img {
        width: 49px;
        height: 44px;
      }

      &:nth-child(6) img {
        width: 50px;
        height: 52px;
      }

      &:hover {
        box-shadow: 0px 6px 24px 0px rgba(192, 192, 192, 0.25);
        // background-color: #ffffff;
        &:before {
          transition: transform linear 0.2s, -webkit-transform linear 0.2s, -o-transform linear 0.2s;
          transition: transform linear 0.2s;
          transform: translate(0, 0);
        }

        p {
          color: #058dff;
        }
      }
    }

    .gitem > * {
      position: relative;
      z-index: 2;
    }

    .gact {
      margin-right: 0;
    }
  }

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

  .f_btn {
    position: fixed;
    top: 50%;
    right: 10%;
    transform: translateY(-50%);

    .fix {
      width: 90px;
      height: 90px;
      background: #eeeeee;
      box-shadow: 0px 3px 9px 1px rgba(172, 172, 172, 0.2);
      border-radius: 50%;
    }
  }

  @media screen and (max-width: 1200px) {
    .content {
      width: 1200px;
    }
  }

  .el-carousel__item img {
    color: #475669;
    font-size: 18px;
    height: auto;
    min-height: 500px;
    margin: 0;
  }
</style>
