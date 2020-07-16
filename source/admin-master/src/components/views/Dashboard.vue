<template>
    <div>
        <el-row :gutter="20">
            <el-col :span="12">
                <el-card class="box-card">
                    总用户量：{{data.allUserCount}}
                </el-card>
            </el-col>
            <el-col :span="12">
                <el-card class="box-card">
                    总代理数：{{data.allAgentCount}}
                </el-card>
            </el-col>
        </el-row>
        <br>
        <el-row :gutter="20">
            <el-col :span="12">
                <el-card class="box-card">
                   今日注册：{{data.todayRegisterCount}}
                </el-card>
            </el-col>
            <el-col :span="12">
                <el-card class="box-card">
                   今日充值：{{data.todayRechargeCount}}
                </el-card>
            </el-col>
        </el-row>
        <br>
        <el-row :gutter="20">
            <el-col :span="12">
                <el-card class="box-card">
                   今日跑词数：{{data.todayKeywordCount}}
                </el-card>
            </el-col>
            <el-col :span="12">

            </el-col>
        </el-row>
    </div>
</template>

<script>
    export default {
        name: "Dashboard",
        data() {
            return {
                data: {}
            }
        },
        mounted() {
            this.getData();
        },
        methods: {
            getData() {
                this.yy.query("/admin/dashboard").then(resp => this.data = resp);
            }
        }
    }
</script>

<style scoped>

</style>
