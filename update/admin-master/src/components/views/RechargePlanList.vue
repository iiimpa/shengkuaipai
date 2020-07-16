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
                    prop="amount"
                    label="金额"
                    width="150">
            </el-table-column>
            <el-table-column
                    prop="realCoin"
                    label="实际金币"
                    width="150">
            </el-table-column>
            <el-table-column
                    prop="giftCoin"
                    label="赠送金币"
                    width="100">
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
                <el-form-item label="金额" label-width="120">
                    <el-input v-model="addForm.amount" autocomplete="off"></el-input>
                </el-form-item>
                <el-form-item label="实际金币" label-width="120">
                    <el-input v-model="addForm.realCoin" autocomplete="off"></el-input>
                </el-form-item>
                <el-form-item label="赠送金币" label-width="120">
                    <el-input v-model="addForm.giftCoin" autocomplete="off"></el-input>
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
                addForm: {},
                list: []
            }
        },
        methods: {
            getData() {
                this.yy.query("/admin/recharge/plan").then(resp => this.list = resp.data);
            },
            addData() {
                this.yy.query("/admin/recharge/add", {
                    name: this.addForm.name,
                    amount: parseFloat(this.addForm.amount),
                    realCoin: parseInt(this.addForm.realCoin),
                    giftCoin: parseInt(this.addForm.giftCoin)
                }).then(() => {
                    this.yy.showSuccess("添加成功");
                    this.getData();
                    this.addDialog = false;
                });
            },
            deleteData(id) {
                this.$confirm('此操作将删除该记录, 是否继续?', '提示', {
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    type: 'warning'
                }).then(() => {
                    this.yy.query("/admin/recharge/delete", {id}).then(() => {
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
