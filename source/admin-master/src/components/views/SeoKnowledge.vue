<template>
    <div>
        <el-select v-model="selectedCategory" @change="getData" placeholder="请选择">
            <el-option label="全部" value="全部">
            </el-option>
            <el-option
                    v-for="item in categories"
                    :key="item"
                    :label="item"
                    :value="item">
            </el-option>
        </el-select>
        <el-button type="primary" @click="addDialog=true" style="float: right;">新增</el-button>
        <el-table
                :data="list"
                border
                style="width: 100%; margin-top: 20px;">
            <el-table-column
                    prop="id"
                    label="ID"
                    width="50">
            </el-table-column>
            <el-table-column
                    prop="category"
                    label="分类">
            </el-table-column>
            <el-table-column
                    prop="title"
                    label="标题">
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
        <el-dialog title="添加" :visible.sync="addDialog" width="700">
            <el-form :model="addForm">
                <el-form-item label="分类" label-width="120">
                    <el-autocomplete
                            class="inline-input"
                            v-model="addForm.category"
                            :fetch-suggestions="querySearch"
                            placeholder="请输入或选择分类"
                    ></el-autocomplete>
                </el-form-item>
                <el-form-item label="标题" label-width="120">
                    <el-input v-model="addForm.title" autocomplete="off"></el-input>
                </el-form-item>
                <el-form-item label="图片" label-width="120">
                    <el-upload
                            class="avatar-uploader"
                            :action="upload.url"
                            :headers="upload.headers"
                            :on-success="resp=>addForm.image = resp.data"
                            :show-file-list="false">
                        <img v-if="addForm.image" :src="addForm.image" style="width: 100px;" class="avatar">
                        <i v-else class="el-icon-plus avatar-uploader-icon"></i>
                    </el-upload>
                </el-form-item>
                <el-form-item label="内容" label-width="120">
                    <!-- 图片上传组件辅助-->
                    <el-upload
                            class="avatar-uploader"
                            :action="upload.url"
                            name="img"
                            :headers="upload.headers"
                            :show-file-list="false"
                            :on-success="uploadSuccess"
                            :on-error="uploadError"
                            :before-upload="beforeUpload">
                    </el-upload>
                    <quill-editor
                            v-model="addForm.content"
                            ref="myQuillEditor"
                            :options="editorOption"
                            @change="onEditorChange($event)"
                    >
                    </quill-editor>
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
    const toolbarOptions = [
        ['bold', 'italic', 'underline', 'strike'],  // toggled buttons
        [{'header': 1}, {'header': 2}],    // custom button values
        [{'list': 'ordered'}, {'list': 'bullet'}],
        [{'indent': '-1'}, {'indent': '+1'}],   // outdent/indent
        [{'direction': 'rtl'}],       // text direction
        [{'size': ['small', false, 'large', 'huge']}], // custom dropdown
        [{'header': [1, 2, 3, 4, 5, 6, false]}],
        [{'color': []}, {'background': []}],   // dropdown with defaults from theme
        [{'font': []}],
        [{'align': []}],
        ['link', 'image'],
        ['clean']
    ]
    export default {
        data() {
            return {
                upload: {
                    url: window.uploadUrl,
                    headers: {token: this.yy.auth.getToken()},
                },
                addDialog: false,
                addForm: {image: ""},
                list: [],
                categories: [],
                selectedCategory: "全部",
                quillUpdateImg: false, // 根据图片上传状态来确定是否显示loading动画，刚开始是false,不显示
                content: null,
                editorOption: {
                    placeholder: '',
                    theme: 'snow', // or 'bubble'
                    modules: {
                        toolbar: {
                            container: toolbarOptions,
                            handlers: {
                                'image': function (value) {
                                    if (value) {
                                        // 触发input框选择图片文件
                                        document.querySelector('.avatar-uploader input').click()
                                    } else {
                                        this.quill.format('image', false);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        },
        methods: {
            querySearch(queryString, cb) {
                var datas = this.categories.map(resp => {
                    return {value: resp}
                });
                var results = queryString ? datas.filter(this.createFilter(queryString)) : datas;
                cb(results);
            },
            createFilter(queryString) {
                return (data) => {
                    return (data.value.toLowerCase().indexOf(queryString.toLowerCase()) === 0);
                };
            },
            getData() {
                this.yy.query("/admin/knowledge/list", {category: this.selectedCategory}).then(resp => this.list = resp.data);
            },
            addData() {
                this.yy.query("/admin/knowledge/add", this.addForm).then(() => {
                    this.addForm = {image: ""};
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
                    this.yy.query("/admin/knowledge/delete", {id}).then(() => {
                        this.yy.showSuccess("删除完成");
                        this.getData();
                    })
                })
            },
            getCategories() {
                this.yy.query("/public/kn/categories").then(resp => this.categories = resp.data);
            },
            onEditorChange({editor, html, text}) {//内容改变事件
                window.console.log("---内容改变事件---")
                this.content = html
                window.console.log(html, editor, text)
            },
            // 富文本图片上传前
            beforeUpload() {
                // 显示loading动画
                this.quillUpdateImg = true
            },

            uploadSuccess(res) {
                // res为图片服务器返回的数据
                // 获取富文本组件实例
                window.console.log(res);
                let quill = this.$refs.myQuillEditor.quill
                // 如果上传成功
                if (res.code == 200) {
                    // 获取光标所在位置
                    let length = quill.getSelection().index;
                    // 插入图片 res.url为服务器返回的图片地址
                    quill.insertEmbed(length, 'image', res.data);
                    // 调整光标到最后
                    quill.setSelection(length + 1)
                } else {
                    this.$message.error('图片插入失败')
                }
                // loading动画消失
                this.quillUpdateImg = false
            },
            // 富文本图片上传失败
            uploadError() {
                // loading动画消失
                this.quillUpdateImg = false
                this.$message.error('图片插入失败')
            }
        },
        mounted() {
            this.getCategories();
            this.getData();
        }
    }
</script>

<style scoped>

</style>
