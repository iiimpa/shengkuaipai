<template>
    <el-table
            :data="userList"
            border
            style="width: 100%">
        <el-table-column
                prop="id"
                label="ID"
                width="50">
        </el-table-column>
        <el-table-column
                prop="account"
                label="用户"
                width="150">
        </el-table-column>
        <el-table-column
                prop="qq"
                label="金额"
                width="150">
        </el-table-column>
        <el-table-column
                prop="email"
                label="金币余额"
                width="150">
        </el-table-column>
        <el-table-column
                prop="createdAt"
                label="充值时间">
        </el-table-column>
        <el-table-column label="操作" width="100">
        </el-table-column>
    </el-table>
</template>

<script>
    export default {
        data() {
            return {
                list: []
            }
        },
        methods: {
            getData() {
                this.yy.query("/admin/recharge/plan").then(resp => this.list = resp.data);
            }
        },
        mounted() {
            this.getData();
        }
    }
</script>

<style scoped>

</style>
