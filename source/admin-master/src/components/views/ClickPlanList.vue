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
            <el-table-column label="计划内容">
                <template slot-scope="scope">
                    [{{scope.row.t0}},{{scope.row.t1}},{{scope.row.t2}},{{scope.row.t3}},{{scope.row.t4}},{{scope.row.t5}},{{scope.row.t6}},{{scope.row.t7}}
                    ,{{scope.row.t8}},{{scope.row.t9}},{{scope.row.t10}},{{scope.row.t11}},{{scope.row.t12}},{{scope.row.t13}},{{scope.row.t14}},
                    {{scope.row.t15}}
                    ,{{scope.row.t16}},{{scope.row.t17}},{{scope.row.t18}},{{scope.row.t19}},{{scope.row.t20}},{{scope.row.t21}},
                    {{scope.row.t22}},{{scope.row.t23}}]
                </template>
            </el-table-column>
            <el-table-column
                    prop="createdAt"
                    label="创建">
            </el-table-column>
            <el-table-column label="操作" width="80">
                <template slot-scope="scope">
                    <el-button type="danger" size="small" @click="deleteData(scope.row.id)">删除</el-button>
                </template>
            </el-table-column>
        </el-table>
        <el-dialog title="添加新的轮播图" :visible.sync="addDialog" width="800">
            <el-form :model="addForm">
                <el-form-item label="名称" label-width="120">
                    <el-input v-model="addForm.name" autocomplete="off"></el-input>
                </el-form-item>
                <el-form-item label="点击次数" prop="time">
                    <br>
                    <el-row>
                        <el-col :span="6">
                            <el-input v-model="addForm.t0" type="number" min="0">
                                <template slot="prepend">00:00</template>
                            </el-input>
                        </el-col>
                        <el-col :span="6">
                            <el-input v-model="addForm.t1" type="number" min="0">>
                                <template slot="prepend">01:00</template>
                            </el-input>
                        </el-col>
                        <el-col :span="6">
                            <el-input v-model="addForm.t2" type="number" min="0">
                                <template slot="prepend">02:00</template>
                            </el-input>
                        </el-col>
                        <el-col :span="6">
                            <el-input v-model="addForm.t3" type="number" min="0">
                                <template slot="prepend">03:00</template>
                            </el-input>
                        </el-col>
                        <el-col :span="6">
                            <el-input v-model="addForm.t4" type="number" min="0">
                                <template slot="prepend">04:00</template>
                            </el-input>
                        </el-col>
                        <el-col :span="6">
                            <el-input v-model="addForm.t5" type="number" min="0">
                                <template slot="prepend">05:00</template>
                            </el-input>
                        </el-col>
                    </el-row>
                    <el-row>
                        <el-col :span="6">
                            <el-input v-model="addForm.t6" type="number" min="0">
                                <template slot="prepend">06:00</template>
                            </el-input>
                        </el-col>
                        <el-col :span="6">
                            <el-input v-model="addForm.t7" type="number" min="0">
                                <template slot="prepend">07:00</template>
                            </el-input>
                        </el-col>
                        <el-col :span="6">
                            <el-input v-model="addForm.t8" type="number" min="0">
                                <template slot="prepend">08:00</template>
                            </el-input>
                        </el-col>
                        <el-col :span="6">
                            <el-input v-model="addForm.t9" type="number" min="0">
                                <template slot="prepend">09:00</template>
                            </el-input>
                        </el-col>
                        <el-col :span="6">
                            <el-input v-model="addForm.t10" type="number" min="0">
                                <template slot="prepend">10:00</template>
                            </el-input>
                        </el-col>
                        <el-col :span="6">
                            <el-input v-model="addForm.t11" type="number" min="0">
                                <template slot="prepend">11:00</template>
                            </el-input>
                        </el-col>
                    </el-row>
                    <el-row>
                        <el-col :span="6">
                            <el-input v-model="addForm.t12" type="number" min="0">
                                <template slot="prepend">12:00</template>
                            </el-input>
                        </el-col>
                        <el-col :span="6">
                            <el-input v-model="addForm.t13" type="number" min="0">
                                <template slot="prepend">13:00</template>
                            </el-input>
                        </el-col>
                        <el-col :span="6">
                            <el-input v-model="addForm.t14" type="number" min="0">
                                <template slot="prepend">14:00</template>
                            </el-input>
                        </el-col>
                        <el-col :span="6">
                            <el-input v-model="addForm.t15" type="number" min="0">
                                <template slot="prepend">15:00</template>
                            </el-input>
                        </el-col>
                        <el-col :span="6">
                            <el-input v-model="addForm.t16" type="number" min="0">
                                <template slot="prepend">16:00</template>
                            </el-input>
                        </el-col>
                        <el-col :span="6">
                            <el-input v-model="addForm.t17" type="number" min="0">
                                <template slot="prepend">17:00</template>
                            </el-input>
                        </el-col>
                    </el-row>
                    <el-row>
                        <el-col :span="6">
                            <el-input v-model="addForm.t18" type="number" min="0">
                                <template slot="prepend">18:00</template>
                            </el-input>
                        </el-col>
                        <el-col :span="6">
                            <el-input v-model="addForm.t19" type="number" min="0">
                                <template slot="prepend">19:00</template>
                            </el-input>
                        </el-col>
                        <el-col :span="6">
                            <el-input v-model="addForm.t20" type="number" min="0">
                                <template slot="prepend">20:00</template>
                            </el-input>
                        </el-col>
                        <el-col :span="6">
                            <el-input v-model="addForm.t21" type="number" min="0">
                                <template slot="prepend">21:00</template>
                            </el-input>
                        </el-col>
                        <el-col :span="6">
                            <el-input v-model="addForm.t22" type="number" min="0">
                                <template slot="prepend">22:00</template>
                            </el-input>
                        </el-col>
                        <el-col :span="6">
                            <el-input v-model="addForm.t23" type="number" min="0">
                                <template slot="prepend">23:00</template>
                            </el-input>
                        </el-col>
                    </el-row>
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
                this.yy.query("/admin/click-plan/list").then(resp => this.list = resp.data);
            },
            addData() {
                this.yy.query("/admin/click-plan/add", {
                    name: this.addForm.name,
                    t0: parseInt(this.addForm.t0),
                    t1: parseInt(this.addForm.t1),
                    t2: parseInt(this.addForm.t2),
                    t3: parseInt(this.addForm.t3),
                    t4: parseInt(this.addForm.t4),
                    t5: parseInt(this.addForm.t5),
                    t6: parseInt(this.addForm.t6),
                    t7: parseInt(this.addForm.t7),
                    t8: parseInt(this.addForm.t8),
                    t9: parseInt(this.addForm.t9),
                    t10: parseInt(this.addForm.t10),
                    t11: parseInt(this.addForm.t11),
                    t12: parseInt(this.addForm.t12),
                    t13: parseInt(this.addForm.t13),
                    t14: parseInt(this.addForm.t14),
                    t15: parseInt(this.addForm.t15),
                    t16: parseInt(this.addForm.t16),
                    t17: parseInt(this.addForm.t17),
                    t18: parseInt(this.addForm.t18),
                    t19: parseInt(this.addForm.t19),
                    t20: parseInt(this.addForm.t20),
                    t21: parseInt(this.addForm.t21),
                    t22: parseInt(this.addForm.t22),
                    t23: parseInt(this.addForm.t23)
                }).then(() => {
                    this.addForm = {};
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
                    this.yy.query("/admin/click-plan/delete", {id}).then(() => {
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
