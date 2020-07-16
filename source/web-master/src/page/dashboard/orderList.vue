<template>
  <el-table
    :data="orders"
    border
    style="width: 100%">
    <el-table-column
      width="160"
      prop="orderNo"
      label="订单编号">
    </el-table-column>
    <el-table-column
      width="100"
      label="搜索引擎">
      <template slot-scope="scope">{{ platforms[scope.row.platform] }}</template>
    </el-table-column>
    <el-table-column
      min-width="200"
      prop="keyword"
      label="关键字">
    </el-table-column>
    <el-table-column
      min-width="200"
      prop="domain"
      label="域名">
    </el-table-column>
    <el-table-column
      min-width="100"
      prop="startRank"
      label="下单时排名">
    </el-table-column>
    <el-table-column
      min-width="100"
      prop="currentRank"
      label="当前域名">
    </el-table-column>
    <el-table-column
      label="方案">
      <template slot-scope="scope">{{ plans[scope.row.planId]?plans[scope.row.planId].name : "无" }}</template>
    </el-table-column>
    <el-table-column
      label="单价">
      <template slot-scope="scope">{{ plans[scope.row.planId]?plans[scope.row.planId].price:"无"}}</template>
    </el-table-column>

    <el-table-column
      prop="balance"
      label="余额">
    </el-table-column>

    <el-table-column
      prop="amount"
      label="总金额">
    </el-table-column>

    <el-table-column
      prop="days"
      label="天数">
    </el-table-column>

    <el-table-column
      label="状态">
      <template slot-scope="scope">{{ status[scope.row.status] }}</template>
    </el-table-column>
    <el-table-column
      width="230"
      prop="createdAt"
      label="创建时间">
    </el-table-column>
    <el-table-column
      fixed="right"
      label="操作"
      width="100">
      <template slot-scope="scope">
        <el-button @click="goTaskList(scope.row.id)" type="text" size="small">查看任务详情</el-button>
        <el-button @click="cancel(scope.row.id)" type="text" size="small" v-if="scope.row.status == 0">取消任务</el-button>
      </template>
    </el-table-column>
  </el-table>
</template>

<script>
  export default {
    name: "OrderList",
    data() {
      return {
        orders: [],
        plans: [],
        platforms: ['PC - 百度', 'PC - 搜狗', 'PC - 360', '手机 - 百度', '手机 - 搜狗', '手机 - 360'],
        status: ['进行中', '已完成', '已退款']
      }
    },
    methods: {
      cancel(id) {
        this.$confirm('此操作将清空所有没有执行的本订单任务，并退款?', '提示', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(() => {
          this.api("/user/order/cancel", {id}).then(() => {
            this.$message({
              showClose: true,
              message: '取消成功',
            });
            this.getPlans();
          })
        })
      },
      getOrders() {
        this.api("/user/order/list").then(resp => {
          this.orders = resp.data;
        })
      },
      getPlans() {
        this.api("/user/plan/list").then(resp => {
          for (var item in resp.data) {
            var tmp = resp.data[item];
            this.plans[tmp.id] = tmp;
          }
          this.getOrders();
        })
      },
      goTaskList(id) {
        this.$router.push("/user/task/list?id=" + id)
      }
    },
    mounted() {
      this.getPlans();
    }
  }
</script>

<style scoped>

</style>
