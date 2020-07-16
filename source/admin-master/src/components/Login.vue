<template>
    <el-card class="box-card" shadow="hover">
        <div slot="header" class="clearfix">
            <span>后台管理系统</span>
        </div>
        <el-form :model="loginForm" status-icon label-width="50px" class="demo-ruleForm">
            <el-form-item label="用户" prop="pass">
                <el-input type="text" v-model="loginForm.account" autocomplete="off"></el-input>
            </el-form-item>
            <el-form-item label="密码" prop="checkPass">
                <el-input type="password" v-model="loginForm.password" autocomplete="off"></el-input>
            </el-form-item>
            <el-form-item>
                <el-button type="primary" @click="doLogin">登录系统</el-button>
            </el-form-item>
        </el-form>
    </el-card>
</template>

<script>
    export default {
        name: "Login",
        data() {
            return {
                loginForm: {}
            }
        },
        methods: {
            doLogin() {
                this.yy.query("/auth/login", this.loginForm, true).then((resp) => {
                    this.yy.auth.login(resp.token);
                    this.$router.replace('/');
                })
            }
        }
    }
</script>

<style scoped>
    .box-card {
        width: 400px;
        margin-top: calc(50vh - 150px);
        margin-left: auto;
        margin-right: auto;
    }
</style>
