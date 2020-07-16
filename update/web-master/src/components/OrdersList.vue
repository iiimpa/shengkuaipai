<!--
 * @Date: 2020-03-11 13:30:51
 * @LastEditTime: 2020-03-18 21:56:21
 * @Description: file content
 -->
<template>
  <div class="orders-list">
    <el-table
      v-loading="loading"
      element-loading-text="获取数据中......"
      @selection-change="handleSelectionChange"
      ref="multipleTable"
      max-height="600px"
      style="width: 100%"
      :data="tableData"
      border
    >
      <el-table-column :selectable="canCancel" type="selection" width="55"></el-table-column>
      <el-table-column prop="orderNo" label="订单流水号" min-width="150"></el-table-column>
      <el-table-column fixed prop="date" label="创建日期" min-width="170">
        <template slot-scope="scope">{{ formatDate(scope.row.createdAt, "yyyy-mm-dd hh:MM:ss") }}</template>
      </el-table-column>
      <el-table-column label="状态" min-width="80">
        <template slot-scope="scope">{{ statusDict[scope.row.status] }}</template>
      </el-table-column>
      <el-table-column prop="id" label="订单编号" min-width="120"></el-table-column>
      <el-table-column prop="platform" label="搜索平台" min-width="100">
        <template slot-scope="scope">
          {{ scope.row.platform !== "" ? platFromDict[scope.row.platform] : "未知" }}
        </template>
      </el-table-column>
      <el-table-column prop="planId" label="方案编号" min-width="80"></el-table-column>
      <el-table-column prop="keyword" label="关键词" min-width="180"></el-table-column>
      <el-table-column prop="domain" label="域名" min-width="250"></el-table-column>
      <el-table-column prop="startRank" label="初始排名" min-width="80"></el-table-column>
      <el-table-column prop="currentRank" label="当前排名" min-width="80"></el-table-column>
      <el-table-column prop="amount" label="金额" min-width="80"></el-table-column>
      <el-table-column prop="balance" label="余额" min-width="80"></el-table-column>
      <el-table-column prop="days" label="订单天数" min-width="80"></el-table-column>
      <el-table-column fixed="right" label="操作" min-width="70">
        <template slot-scope="scope">
          <div><el-button @click="getDetails(scope.row.id)" type="text" size="mini">查看</el-button></div>
          <div><el-button @click="submitRenewOrders(scope.row.id)" :disabled="!canRenew(scope.row)" type="text" size="mini">续费</el-button></div>
          <div><el-button @click="cancelOrders(scope.row.id)" :disabled="!canCancel(scope.row)" type="text" size="mini">取消</el-button></div>
        </template>
      </el-table-column>
    </el-table>
    <el-dialog @close="close" width="90%" top="30px" title="任务详情" :visible.sync="taskDetailShow">
      <TaskList v-loading="loading" element-loading-text="获取数据中......" :tableData="tasksTableData">TaskList</TaskList>
      <MyPagination :search-form="searchForm" @loadData="getDetails"></MyPagination>
    </el-dialog>
  </div>
</template>

<script>
import { getTaskDetails } from "../api/tasks"
import { formatDate } from "../utils/index"
import TaskList from "./TaskList"
import MyPagination from "./MyPagination"
export default {
  components: {
    TaskList,
    MyPagination
  },
  props: {
    tableData: {
      type: Array,
      default: () => {
        return []
      }
    },
    selectForm: {
      type: Object,
      default: () => {
        return {}
      }
    }
  },
  name: "OrdersList",
  data() {
    return {
      tasksActiveName: "first",
      loading: false,
      tasksTableData: [],
      searchForm: { pageindex: 1, pagesize: 20, total: 0, OrderId: "" },
      statusDict: {
        0: "已付款",
        1: "已完成",
        2: "已取消"
      },
      tasksDict: {
        0: "",
        1: "first",
        2: "second",
        3: "third",
        4: "fourth",
        5: "fifth",
        5: "sixth"
      },
      platFromDict: {
        0: "百度",
        1: "搜狗",
        2: "360",
        3: "手机端百度",
        4: "手机端搜狗",
        5: "手机端360"
      },
      taskDetailShow: false
    }
  },
  methods: {
    formatDate,
    close() {
      this.loading = false
    },
    cancelOrders(id) {
      this.$emit("cancelOrders", [id])
    },
    handleSelectionChange(val) {
      console.log(val)
      this.selectForm.ids = val
    },
    submitRenewOrders(id) {
      this.$emit("submitRenewOrders", id)
    },
    showDetails(row) {
      this.taskDetailShow = true
    },
    // 可续费
    canRenew(row) {
      return row.days <= 7
    },
    // 可取消
    canCancel(row) {
      return row.status === 0
    },
    // 切换tab
    tasksBeforeLeave(value) {
      this.searchForm.status = this.ordersStatusDict[value]
      this.getDetails(this.searchForm.id)
    },
    // 拉取详情
    getDetails(id) {
      this.searchForm.OrderId = id
      this.taskDetailShow = true
      this.loading = true
      const form = Object.assign({}, this.searchForm)
      form.pageindex += ""
      form.pagesize += ""
      getTaskDetails(form)
        .then(res => {
          console.log(res)
          this.tasksTableData = res.data || []
          this.searchForm.total = res.total || 0
          this.loading = false
        })
        .catch(err => {
          console.log(err)
          this.loading = false
        })
    }
  }
}
</script>
<style lang="scss" scoped>
.orders-list {
  .action-btn {
    margin-top: 10px;
  }
}
</style>
<style>
.el-dialog .el-tabs__item {
  padding: 0 13px;
}
</style>
