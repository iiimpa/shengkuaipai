<template>
  <el-table
    :data="list"
    border
    style="width: 100%">
    <el-table-column
      width="100"
      prop="id"
      label="任务ID">
    </el-table-column>
    <el-table-column
      width="230"
      prop="scheduleTime"
      label="调度时间">
    </el-table-column>
    <el-table-column
      width="230"
      prop="requestTime"
      label="请求时间">
    </el-table-column>
    <el-table-column
      width="230"
      prop="finishTime"
      label="结束时间">
    </el-table-column>
    <el-table-column
      prop="cost"
      label="预计消费">
    </el-table-column>
    <el-table-column
      prop="realCost"
      label="实际花费">
    </el-table-column>
    <el-table-column
      width="150"
      label="状态">
      <template slot-scope="scope">{{ status[scope.row.status] }}</template>
    </el-table-column>
    <el-table-column
      width="230"
      prop="createdAt"
      label="创建时间">
    </el-table-column>
  </el-table>
</template>

<script>
  export default {
    name: "taskList",
    data() {
      return {
        list: [],
        status: [
          '等待中',
          '执行中',
          '失败-未找到',
          '半成功-内页错误',
          '成功',
          '失败'
        ]
      }
    },
    mounted() {
      this.getList(this.$route.query.id);
    },
    methods: {
      getList(orderId) {
        this.api("/user/task/list", {orderId: parseInt(orderId)}).then(resp => {
          this.list = resp.data;
        });
      }
    }
  }
</script>

<style scoped>

</style>
