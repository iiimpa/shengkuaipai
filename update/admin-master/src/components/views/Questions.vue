<template>
    <div>
        <el-button type="primary" @click="addDialog=true">新增</el-button>
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
                    prop="title"
                    label="问题">
            </el-table-column>
            <el-table-column
                    prop="content"
                    label="答案">
            </el-table-column>
            <el-table-column
                    prop="createdAt"
                    label="创建时间">
            </el-table-column>
            <el-table-column label="操作" width="80">
                <template slot-scope="scope">
                    <el-button type="danger" size="small" @click="deleteData(scope.row.id)">删除</el-button>
                </template>
            </el-table-column>
        </el-table>
        <el-dialog title="添加" :visible.sync="addDialog">
            <el-form :model="addForm">
                <el-form-item label="问题" label-width="120">
                    <el-input v-model="addForm.title" autocomplete="off"></el-input>
                </el-form-item>
                <el-form-item label="答案" label-width="120">
                    <el-input type="textarea" :rows="5" v-model="addForm.content" autocomplete="off"></el-input>
                </el-form-item>
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button @click="addDialog = false">取 消</el-button>
                <el-button type="primary" @click="addData">确 定</el-button>
            </div>
        </el-dialog>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                addDialog: false,
                addForm: {url: ""},
                list: []
            }
        },
        methods: {
            getData() {
                this.yy.query("/admin/qa/list").then(resp => this.list = resp.data);
            },
            addData() {
                this.yy.query("/admin/qa/add", this.addForm).then(() => {
                    this.addForm = {url: ""};
                    this.getData();
                    this.yy.showSuccess("添加成功");
                    this.addDialog = false;
                });
            },
            deleteData(id) {
                this.$confirm('此操作将永久删除该文件, 是否继续?', '提示', {
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    type: 'warning'
                }).then(() => {
                    this.yy.query("/admin/qa/delete", {id}).then(() => {
                        this.yy.showSuccess("删除完成");
                        this.getData();
                    })
                })
            }
        },
        mounted() {
            this.getData();
        }
    }
</script>

<style scoped>

</style>
