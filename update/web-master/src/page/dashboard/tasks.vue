<!--
 * @Date: 2020-03-11 13:49:23
 * @LastEditTime: 2020-03-18 19:06:42
 * @Description: file content
 -->
<template>
  <div class="tasks">
    <div>
      <TaskChart :searchForm="searchForm" @getOrdersList="getOrdersList">TaskChart</TaskChart>
    </div>
    <div style="margin-top: 10px;">
      <el-tabs lazy v-model="activeName" :before-leave="beforeLeave">
        <el-tab-pane label="所有订单" name="first">
          <OrdersList
            @cancelOrders="cancelOrders"
            :selectForm="selectForm"
            @submitRenewOrders="submitRenewOrders"
            :tableData="tableData"
          ></OrdersList>
        </el-tab-pane>
        <el-tab-pane label="当前订单" name="second">
          <OrdersList
            @cancelOrders="cancelOrders"
            :selectForm="selectForm"
            @submitRenewOrders="submitRenewOrders"
            :tableData="tableData"
          ></OrdersList>
        </el-tab-pane>
        <el-tab-pane label="已完成订单" name="third">
          <OrdersList
            @cancelOrders="cancelOrders"
            :selectForm="selectForm"
            @submitRenewOrders="submitRenewOrders"
            :tableData="tableData"
          ></OrdersList>
        </el-tab-pane>
        <el-tab-pane label="七天到期订单" name="fourth">
          <OrdersList
            @cancelOrders="cancelOrders"
            :selectForm="selectForm"
            @submitRenewOrders="submitRenewOrders"
            :tableData="tableData"
          ></OrdersList>
        </el-tab-pane>
        <el-tab-pane disabled>
          <span slot="label">
            <el-input style="width:95px;" size="mini" v-model="days" placeholder="续费天数"></el-input>
            <el-input
              @clear="getOrdersList"
              style="width:180px;"
              size="mini"
              clearable
              placeholder="请输关键词"
              prefix-icon="el-icon-search"
              v-model="searchForm.keyword"
            ></el-input>
            <el-button @click="getOrdersList" size="mini" type="primary">查询</el-button>
            <!-- <el-select style="width:95px;" size="mini" v-model="selectForm.multipleSelection.type" placeholder="请选择任务">
              <el-option label="续费订单" :value="1"></el-option>
              <el-option label="取消订单" :value="2"></el-option>
            </el-select> -->
            <el-button size="mini" @click="batchCancel" type="primary">批量取消订单</el-button>
          </span>
        </el-tab-pane>
      </el-tabs>
      <MyPagination :search-form="searchForm" @loadData="getOrdersList"></MyPagination>
    </div>
  </div>
</template>

<script>
import TaskChart from "../../components/TaskChart"
import MyPagination from "../../components/MyPagination"
import OrdersList from "../../components/OrdersList"
import { getTaskList, renewOrders, batchCancelOrders } from "../../api/tasks"
import { formatDate } from "../../utils/index"
export default {
  components: {
    TaskChart,
    OrdersList,
    MyPagination
  },
  name: "tasks",
  data() {
    return {
      selectForm: {
        ids: []
      },
      days: "",
      activeName: "first",
      searchForm: {
        timeBegin: "",
        timeOver: "",
        status: "",
        keyword: "",
        pageindex: 1,
        pagesize: 20,
        total: 0
      },
      ordersStatusDict: {
        first: "",
        second: "0",
        third: "1",
        fourth: "2"
      },
      taskStatusDict: {
        first: "",
        second: "0",
        third: "1",
        fourth: "2",
        fifth: "3",
        sixth: "4",
        seventh: "5"
      },
      tableData: []
    }
  },
  created() {
    this.getOrdersList()
  },
  methods: {
    formatDate,
    getTaskList,
    submitRenewOrders(orderId) {
      if (!this.days) {
        this.$message.error("请输入续费天数！")
        return
      }
      renewOrders({ OrderId: orderId, Days: this.days * 1 })
        .then(res => {
          if (res.code !== 200) {
            this.$message.success("续费失败")
            return
          }
          this.getOrdersList()
          this.$message.success("续费成功")
        })
        .catch(err => {
          console.log(err)
          this.$message.success("续费失败")
        })
    },
    cancelOrders(Orderlist) {
      batchCancelOrders({ Orderlist })
        .then(() => {
          this.getOrdersList()
          this.$message.success("取消成功")
        })
        .catch(err => {
          console.log(err)
          this.$message.error("取消失败")
        })
    },
    batchCancel() {
      if (!this.selectForm.ids.length) {
        this.$message.error("请选中需要取消的订单")
        return
      }
      const Orderlist = []
      this.selectForm.ids.forEach(row => Orderlist.push(row.id))
      this.cancelOrders(Orderlist)
    },
    getOrdersList() {
      const form = Object.assign({}, this.searchForm)
      form.pageindex += ""
      form.pagesize += ""
      delete form.total
      getTaskList(form)
        .then(res => {
          this.tableData = res.data || []
          this.searchForm.total = res.total || 0
        })
        .catch(err => {
          console.log(err)
        })
    },
    beforeLeave(value) {
      this.searchForm.status = this.ordersStatusDict[value]
      this.getOrdersList()
    }
  }
}
</script>
<style lang="scss" scoped>
.tasks {
}
</style>
<style></style>
