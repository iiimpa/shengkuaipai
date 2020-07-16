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
                prop="descirption"
                label="描述"
                width="250">
        </el-table-column>
        <el-table-column label="值">
            <template slot-scope="scope">
                <el-input v-model="scope.row.value" placeholder="请输入内容"></el-input>
            </template>
        </el-table-column>
        <el-table-column label="操作" width="100">
            <template slot-scope="scope">
                <el-button type="text" @click="doSave(scope.row)" size="small">保存</el-button>
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
                this.yy.query("/admin/config/list").then(resp => this.list = resp.data);
            },
            doSave() {

            }
        },
        mounted() {
            this.getData();
        }
    }
</script>

<style scoped>

</style>
