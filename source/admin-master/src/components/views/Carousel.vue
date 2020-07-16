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
<!--            <el-table-column-->
<!--                    prop="link"-->
<!--                    label="链接">-->
<!--            </el-table-column>-->
            <el-table-column
                    label="图片">
                <template slot-scope="scope">
                    <img :src="scope.row.url" width="100%">
                </template>
            </el-table-column>
            <el-table-column label="操作" width="80">
                <template slot-scope="scope">
                    <el-button type="danger" size="small" @click="deleteData(scope.row.id)">删除</el-button>
                </template>
            </el-table-column>
        </el-table>
        <el-dialog title="添加新的轮播图" :visible.sync="addDialog">
            <el-form :model="addForm">
                <el-form-item label="名称" label-width="120">
                    <el-input v-model="addForm.name" autocomplete="off"></el-input>
                </el-form-item>
                <el-form-item label="链接" label-width="120">
                    <el-input v-model="addForm.link" autocomplete="off"></el-input>
                </el-form-item>
                <el-form-item label="图片" label-width="120">
                    <el-upload
                            class="avatar-uploader"
                            :action="upload.url"
                            :headers="upload.headers"
                            :on-success="uploadSuccess"
                            :show-file-list="false">
                        <img v-if="addForm.url" :src="addForm.url" style="width: 100%;" class="avatar">
                        <i v-else class="el-icon-plus avatar-uploader-icon"></i>
                    </el-upload>
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
                upload: {
                    url: window.uploadUrl,
                    headers: {token: this.yy.auth.getToken()},
                },
                addDialog: false,
                addForm: {url: ""},
                list: []
            }
        },
        methods: {
            uploadSuccess(response) {
                this.addForm.url = response.data;
            },
            getData() {
                this.yy.query("/admin/carousel/list").then(resp => this.list = resp.data);
            },
            addData() {
                this.yy.query("/admin/carousel/add", {
                    name: this.addForm.name,
                    link: this.addForm.link,
                    url: this.addForm.url
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
                    this.yy.query("/admin/carousel/delete", {id}).then(() => {
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
