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
                    prop="name"
                    label="名称"
                    width="150">
            </el-table-column>
            <el-table-column
                    prop="price"
                    label="单次价格"
                    width="150">
            </el-table-column>
            <el-table-column
                    prop="times"
                    label="方案详情"
                    width="150">
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
                <el-form-item label="名称" label-width="120">
                    <el-input v-model="addForm.name" autocomplete="off"></el-input>
                </el-form-item>
                <el-form-item label="价格" label-width="120">
                    <el-input v-model="addForm.price" autocomplete="off"></el-input>
                </el-form-item>
                <el-form-item label="方案 （每个数字代表一次停留时间，用逗号隔开）" label-width="120">
                    <el-input v-model="addForm.times" autocomplete="off"></el-input>
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
                this.yy.query("/admin/plan/list").then(resp => this.list = resp.data);
            },
            addData() {
                this.yy.query("/admin/plan/add", {
                    name: this.addForm.name,
                    times: '[' + this.addForm.times + ']',
                    price: parseInt(this.addForm.price)
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
                    this.yy.query("/admin/plan/delete", {id}).then(() => {
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
