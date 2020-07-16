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
                    prop="keyword"
                    label="关键词"
                    width="150">
            </el-table-column>
            <el-table-column
                    prop="domain"
                    label="域名"
                    width="150">
            </el-table-column>
            <el-table-column
                    prop="origin"
                    label="原始排名"
                    width="150">
            </el-table-column>
            <el-table-column
                    prop="now"
                    label="当前排名"
                    width="100">
            </el-table-column>
            <el-table-column
                    prop="up"
                    label="上升流量"
                    width="100">
            </el-table-column>
            <el-table-column
                    prop="date"
                    label="抓取时间">
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
                <el-form-item label="关键词" label-width="120">
                    <el-input v-model="addForm.keyword" autocomplete="off"></el-input>
                </el-form-item>
                <el-form-item label="链接" label-width="120">
                    <el-input v-model="addForm.domain" autocomplete="off"></el-input>
                </el-form-item>
                <el-form-item label="初始排名" label-width="120">
                    <el-input v-model="addForm.origin" autocomplete="off"></el-input>
                </el-form-item>
                <el-form-item label="当前排名" label-width="120">
                    <el-input v-model="addForm.now" autocomplete="off"></el-input>
                </el-form-item>
                <el-form-item label="提升流量" label-width="120">
                    <el-input v-model="addForm.up" autocomplete="off"></el-input>
                </el-form-item>
                <el-form-item label="时间" label-width="120">
                    <el-input v-model="addForm.date" autocomplete="off"></el-input>
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
                this.yy.query("/admin/case/list").then(resp => this.list = resp.data);
            },
            addData() {
                this.yy.query("/admin/case/add", {
                    keyword: this.addForm.keyword,
                    domain: this.addForm.domain,
                    origin: parseInt(this.addForm.origin),
                    now: parseInt(this.addForm.now),
                    up: parseInt(this.addForm.up),
                    date: this.addForm.date
                }).then(() => {
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
                    this.yy.query("/admin/case/delete", {id}).then(() => {
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
