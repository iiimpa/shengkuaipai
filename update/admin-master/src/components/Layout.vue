<template>
    <el-container>
        <el-aside width="200px" style="background-color: rgb(238, 241, 246)">
            <el-menu>
                <div v-for="(item,index) in getMenu()" :key="index">
                    <!--                    有子菜单的-->
                    <el-submenu :index="item.name+index" v-if="item.children">
                        <template slot="title">
                            <i v-if="item.menu.icon" :class="item.menu.icon"></i>{{item.menu.name}}
                        </template>
                        <el-menu-item v-for="(sitem,index) in item.children" :key="index"
                                      :index="sitem.name+index" @click="$router.push({name:sitem.name})">
                            {{sitem.menu.name}}
                        </el-menu-item>
                    </el-submenu>
                    <!--                    没有子菜单的-->
                    <el-menu-item :index="item.name+index" v-if="!item.children"
                                  @click="$router.push({name:item.name})">
                        <i v-if="item.menu.icon" :class="item.menu.icon"></i>
                        <span slot="title">{{item.menu.name}}</span>
                    </el-menu-item>
                </div>
                <el-menu-item index="changePassword" @click="changePassword">
                    <i class="el-icon-user"></i><span slot="title">修改密码</span>
                </el-menu-item>
                <el-menu-item index="logout" @click="logout">
                    <i class="el-icon-close"></i><span slot="title">退出登录</span>
                </el-menu-item>
            </el-menu>
        </el-aside>

        <el-container>
            <el-header style="text-align: right; font-size: 12px">
                {{yy.auth.user.account}}
            </el-header>

            <el-main>
                <router-view></router-view>
            </el-main>
        </el-container>
    </el-container>
</template>

<script>
    import routes from "../routes";

    export default {
        name: "Layout",
        methods: {
            getMenu() {
                let layout = routes.find(res => res.path === '/');
                return layout.children;
            },
            logout() {
                this.yy.auth.logout();
            },
            changePassword() {

            }
        },
        mounted() {

        },
        data() {
            return {}
        }
    }
</script>

<style scoped>
    .el-header {
        background-color: #B3C0D1;
        color: #333;
        line-height: 60px;
    }

    .el-aside {
        color: #333;
    }
</style>
