<template>
    <div>
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
                    label="账号"
                    width="150">
            </el-table-column>
            <el-table-column
                    prop="cell"
                    label="手机号"
                    width="150">
            </el-table-column>
            <el-table-column
                    prop="coin"
                    label="金币"
                    width="80">
            </el-table-column>
            <el-table-column
                    prop="totalRecharge"
                    label="累计充值"
                    width="80">
            </el-table-column>
            <el-table-column
                    prop="alipay"
                    label="支付宝账号">
            </el-table-column>
            <el-table-column label="操作" width="300">
                <template slot-scope="scope">
                    <el-button type="warning" size="small" @click="editUser(scope.row)">
                        编辑用户
                    </el-button>
                    <el-button v-if="!scope.row.isAgent" type="primary" size="small" @click="setAgent(scope.row.id)">
                        设置为代理
                    </el-button>
                    <el-button v-if="scope.row.level==0" type="danger" size="small" @click="setAdmin(scope.row.id)">
                        设置管理
                    </el-button>
                    <el-button v-if="scope.row.level==1" type="danger" size="small" @click="setAdmin(scope.row.id)">
                        取消管理
                    </el-button>
                </template>
            </el-table-column>
        </el-table>
        <el-dialog title="编辑用户" :visible.sync="editDialog" :close-on-click-modal="false">
            <el-form :model="editForm">
                <el-form-item label="手机号" label-width="120">
                    <el-input v-model="editForm.cell" autocomplete="off"></el-input>
                </el-form-item>
                <el-form-item label="金币" label-width="120">
                    <el-input v-model="editForm.coin" autocomplete="off"></el-input>
                </el-form-item>
                <el-form-item label="支付宝" label-width="120">
                    <el-input v-model="editForm.alipay" autocomplete="off"></el-input>
                </el-form-item>
                <el-form-item label="密码" label-width="120">
                    <el-input v-model="editForm.password" placeholder="[不修改请留空]" autocomplete="off"></el-input>
                </el-form-item>

            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button @click="editForm = false">取 消</el-button>
                <el-button type="primary" @click="doEdit">确 定</el-button>
            </div>
        </el-dialog>
    </div>
</template>

<script>
    export default {
        name: "UserList",
        data() {
            return {
                userList: [],
                editForm: {},
                editDialog: false
            }
        },
        methods: {
            getUserList() {
                this.yy.query("/admin/user/list").then(resp => {
                    this.userList = resp.data
                })
            },
            setAgent(id) {
                this.yy.query("/admin/user/set-agent", {id}).then(() => {
                    this.yy.showSuccess("设置代理成功");
                    this.getUserList();
                })
            },
            setAdmin(id) {
                this.yy.query("/admin/user/set-admin", {id}).then(() => {
                    this.yy.showSuccess('管理员操作成功');
                    this.getUserList();
                })
            },
            editUser(item) {
                item.password = "";
                this.editForm = item;
                this.editDialog = true;
            },
            doEdit() {
                this.yy.query("/admin/user/edit", {
                    id: this.editForm.id,
                    cell: this.editForm.cell,
                    coin: parseInt(this.editForm.coin),
                    alipay: this.editForm.alipay,
                    password: this.editForm.password
                }).then(() => {
                    this.yy.showSuccess("用户编辑完成");
                    this.getUserList();
                    this.editDialog = false;
                })
            }
        },
        mounted() {
            this.getUserList();
        }
    }
</script>

<style scoped>

</style>
