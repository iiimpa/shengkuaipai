<!--
 * @Date: 2020-03-11 14:02:06
 * @LastEditTime: 2020-03-18 19:21:33
 * @Description: file content
 -->
<template>
  <div class="task-chart">
    <div style="text-align: center;margin-bottom: 10px;">
      <span v-if="startDate"
        ><span style="padding-right: 10px;">{{ startDate }}</span
        >到 <span style="padding-right: 10px;">{{ endDate }}</span></span
      ><span v-else>所有</span>任务数据
    </div>
    <div class="top flex-wrap">
      <div class="pie" v-loading="loading" element-loading-text="下载数据绘制图形中">
        <div id="pieReport" style="width: 480px;height: 350px;"></div>
      </div>
      <div class="my-calender">
        <Calendar ref="Calendar" @choseDay="clickDay" style="margin: 0px;background:#ffffff;color:#5e6d82;height:340px;"></Calendar>
      </div>
    </div>
  </div>
</template>

<script>
import {
  getOrdersCalc //饼图数据
} from "../api/tasks"
import Calendar from "vue-calendar-component"
import { formatDate } from "../utils/index"
export default {
  components: {
    Calendar
  },
  computed: {
    startDate() {
      return this.searchForm.timeBegin
    },
    endDate() {
      return this.searchForm.timeOver
    }
  },
  props: {
    searchForm: {
      type: Object,
      default: () => {
        return {}
      }
    }
  },
  name: "TaskChart",
  data() {
    return {
      loading: false,
      chartOpinionData: [],
      colorList: ["#67C23A", "#409EFF", "#F56C6C", "#A94F2F", "#E6A23C", "#C23531", "#7fc6fa", "#1ec6c6"],
      choiceDate: [],
      value: new Date(),
      charts: "",
      opinion: ["已完成订单数", "已付款订单数", "已退款订单数"]
    }
  },
  created() {
    this.getTaskChart()
  },

  methods: {
    formatDate,
    getTaskChart() {
      this.loading = true
      getOrdersCalc({
        timeBegin: this.searchForm.timeBegin,
        timeOver: this.searchForm.timeOver
      })
        .then(res => {
          console.log(res)
          if (res.data && res.data) {
            const data = res.data
            this.chartOpinionData = [
              { value: data.finish || 0, name: "已完成订单数", itemStyle: "#67C23A" },
              {
                value: data.paied || 0,
                name: "已付款订单数",
                itemStyle: "#409EFF"
              },
              { value: data.refund || 0, name: "已退款订单数", itemStyle: "#F56C6C" }
            ]
          }
          this.$nextTick(function() {
            this.drawPie("pieReport")
            this.loading = false
          })
        })
        .catch(err => console.log(err))
    },
    setDate(date) {
      const today = new Date().getTime()
      const choiceDate = new Date(date).getTime()
      const choiceStr = this.formatDate(date, "yyyy-mm-dd")
      const todayStr = this.formatDate(today, "yyyy-mm-dd")
      if (choiceDate > today) {
        this.searchForm.timeBegin = todayStr
        this.searchForm.timeOver = choiceStr
        return
      }
      if (choiceDate < today) {
        this.searchForm.timeBegin = choiceStr
        this.searchForm.timeOver = todayStr
        return
      }
      if (choiceDate == today) {
        this.searchForm.timeBegin = choiceStr
        this.searchForm.timeOver = choiceStr
        return
      }
    },
    clickDay(date) {
      this.setDate(date)
      this.getTaskChart()
      this.$emit("getOrdersList")
    },
    drawPie(id) {
      let self = this
      this.charts = this.$echarts.init(document.getElementById(id))
      this.charts.setOption({
        tooltip: {
          trigger: "item",
          formatter: "{a} <br/>{b}: {c} ({d}%)"
        },
        legend: {
          orient: "horizontal",
          x: "left", //图例显示在右边
          data: this.opinion
        },
        series: [
          {
            name: "任务类型",
            type: "pie",
            top: 25,
            radius: ["40%", "70%"],
            avoidLabelOverlap: false,
            label: {
              normal: {
                show: true,
                position: "left"
              },
              emphasis: {
                show: true,
                textStyle: {
                  // fontSize: '10'
                  // fontWeight: 'bold'
                }
              }
            },
            labelLine: {
              normal: {
                show: false
              }
            },
            itemStyle: {
              emphasis: {
                shadowBlur: 10,
                shadowOffsetX: 0,
                shadowColor: "rgba(0, 0, 0, 0.5)"
              },
              color: function(params) {
                //自定义颜色
                return self.colorList[params.dataIndex]
              }
            },
            data: this.chartOpinionData
          }
        ]
      })
    }
  }
}
</script>
<style lang="scss" scoped>
.task-chart {
  .pie,
  .date {
    width: 60%;
    height: 350px;
  }
  .flex-wrap {
    display: -webkit-box;
    display: -webkit-flex;
    display: flex;
    -webkit-box-pack: center;
    -webkit-justify-content: center;
    justify-content: center;
    -webkit-box-align: center;
    -webkit-align-items: center;
    align-items: center;
  }
}
</style>
<style scoped>
.my-calender .content {
  padding: 15px;
}

.my-calender .cardsp {
  height: 28px;
  line-height: 28px;
  position: relative;
  padding-left: 15px;
  margin-bottom: 15px;
}
.cardsp img {
  position: absolute;
  top: 7px;
}
.cardsp .spantwo {
  margin-left: 16px;
}
.cardsp .spanthere {
  margin-left: 15px;
}
.circular {
  display: inline-block;
  width: 12px;
  height: 12px;
  border: 2px solid #136aa7;
  border-radius: 50%;
}
.my-calender .titleP {
  height: 14px;
  line-height: 14px;
  font-size: 14px;
  font-weight: bold;
  border-left: 3px solid #136aa7;
  padding-left: 12px;
  position: relative;
  margin-bottom: 15px;
}

.my-calender .title {
  background-color: #2fd85e;
  font-size: 16px;
}

.my-calender .div {
  margin: auto;
  width: 220px;
  height: 30px;
  line-height: 30px;
  background: red;
  color: black;
  font-size: 17px;
  text-align: center;
  margin-top: 20px;
}

.my-calender .wh_container >>> .wh_content_all {
  background-color: #ffffff !important;
  border: 1px solid #dfe0e6;
  width: 400px;
  /* height: 370px; */
  border-radius: 6px;
}
.my-calender .wh_container {
  margin: 0px !important;
}

.my-calender .wh_container >>> .wh_item_date {
  color: #211d48;
}
.my-calender .wh_container >>> .wh_item_date:hover {
  color: #ffffff;
  background: #136aa7;
  border-radius: 50%;
}
.my-calender .wh_container >>> .wh_other_dayhide {
  color: #cccccc;
}

.my-calender .wh_container >>> .wh_content_item {
  margin-bottom: 0px;
  margin-top: 0px;
}

.my-calender .wh_container >>> .wh_content {
  color: black;
}

.my-calender .wh_container >>> .wh_top_tag {
  color: black;
}
.my-calender .wh_container >>> .wh_content_li {
  color: #162947;
  font-weight: bold;
}
.my-calender .wh_container >>> .wh_jiantou1 {
  border-top: 2px solid #162947;
  border-left: 2px solid #162947;
}
.my-calender .wh_container >>> .wh_jiantou2 {
  border-top: 2px solid #162947;
  border-right: 2px solid #162947;
}

.my-calender .wh_container >>> .wh_content_item > .wh_isMark {
  background-color: rgba(19, 105, 167, 0.15);
  /*border-radius: 0px;*/
}
.my-calender .wh_container >>> .wh_content_item .wh_isToday {
  background-color: rgba(19, 105, 167, 1);
  /*border-radius: 0px;*/
  color: #ffffff;
}
.my-calender .wh_container >>> .wh_content_item .wh_chose_day {
  background-color: rgba(19, 105, 167, 1);
  /*border-radius: 0px;*/
  color: #ffffff;
}
</style>
