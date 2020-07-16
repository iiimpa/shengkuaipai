<!--
 * @Date: 2020-03-17 01:42:09
 * @LastEditTime: 2020-03-17 04:29:08
 * @Description: file content
 -->
<template>
  <div class="task-list">
    <template>
      <el-table max-height="600px" :data="tableData" border style="width: 100%">
        <el-table-column fixed prop="createAt" label="创建时间" min-width="170">
          <template slot-scope="scope">{{ formatDate(scope.row.createdAt, "yyyy-mm-dd hh:MM:ss") }}</template>
        </el-table-column>
        <el-table-column prop="orderId" label="订单编号" min-width="120"> </el-table-column>
        <el-table-column prop="id" label="任务编号" min-width="120"> </el-table-column>
        <el-table-column prop="realCost" label="实际消费" min-width="100"> </el-table-column>
        <el-table-column prop="cost" label="预计消费" min-width="100"> </el-table-column>
        <el-table-column prop="proxy" label="代理IP" min-width="170"> </el-table-column>
        <el-table-column prop="resolution" label="分辨率" min-width="130"> </el-table-column>
        <el-table-column prop="ua" label="请求头" min-width="320"> </el-table-column>
        <el-table-column label="状态" min-width="170">
          <template slot-scope="scope">{{ statusDict[scope.row.status] }}</template>
        </el-table-column>
        <el-table-column label="更新时间" min-width="170">
          <template slot-scope="scope">{{ formatDate(scope.row.updatedAt, "yyyy-mm-dd hh:MM:ss") }}</template>
        </el-table-column>
        <el-table-column label="调度时间" min-width="170">
          <template slot-scope="scope">{{ formatDate(scope.row.scheduleTime, "yyyy-mm-dd hh:MM:ss") }}</template>
        </el-table-column>
        <el-table-column label="请求时间" min-width="170">
          <template slot-scope="scope">{{ formatDate(scope.row.requestTime, "yyyy-mm-dd hh:MM:ss") }}</template>
        </el-table-column>
        <el-table-column label="结束时间" min-width="170">
          <template slot-scope="scope">{{ formatDate(scope.row.finishTime, "yyyy-mm-dd hh:MM:ss") }}</template>
        </el-table-column>
      </el-table>
    </template>
  </div>
</template>

<script>
import { formatDate } from "../utils/index"
import MyPagination from "./MyPagination"
export default {
  components: { MyPagination },
  props: {
    tableData: {
      type: Array,
      default: () => {
        return []
      }
    }
  },
  name: "TaskList",
  data() {
    return {
      statusDict: {
        0: "待执行",
        1: "执行中",
        2: "失败-未找到",
        3: "半成功，内页错误",
        4: "成功",
        5: "失败"
      }
    }
  },
  methods: {
    formatDate
  }
}
</script>
<style lang="scss" scoped></style>
