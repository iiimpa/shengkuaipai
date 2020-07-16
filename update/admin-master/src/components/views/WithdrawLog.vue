<template>
    <el-table
            :data="list"
            border
            style="width: 100%">
        <el-table-column
                prop="id"
                label="ID"
                width="50">
        </el-table-column>
        <el-table-column
                prop="name"
                label="用户"
                width="150">
        </el-table-column>
        <el-table-column
                prop="name"
                label="支付宝"
                width="150">
        </el-table-column>
        <el-table-column
                prop="name"
                label="金额"
                width="150">
        </el-table-column>
        <el-table-column
                prop="createdAt"
                label="申请时间">
        </el-table-column>
        <el-table-column label="操作" width="100">
            <template slot-scope="scope">
                <el-button type="text" size="small">通过审核{{scope.row.id}}</el-button>
            </template>
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
                this.yy.query("/admin/click-plan/list").then(resp => this.list = resp.data);
            }
        },
        mounted() {
            this.getData();
        }
    }
</script>

<style scoped>

</style>
