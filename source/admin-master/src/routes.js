//系统登录
import Login from "./components/Login";

//核心框架
import Layout from "./components/Layout";
import Dashboard from "./components/views/Dashboard";
import App from "./App";
import RechargePlanList from "./components/views/RechargePlanList";
import ClickPlanList from "./components/views/ClickPlanList";
import RunPlanList from "./components/views/RunPlanList";
import SeoKnowledge from "./components/views/SeoKnowledge";
import Cases from "./components/views/Cases";
import RechargeLog from "./components/views/RechargeLog";
import WithdrawLog from "./components/views/WithdrawLog";
import Config from "./components/views/Config";
import Questions from "./components/views/Questions";
import Carousel from "./components/views/Carousel";
import UserList from "./components/views/UserList";
import FriendLink from "./components/views/FriendLink";

export default [
    {
        path: '/login',
        name: 'login',
        component: Login
    },
    {
        path: '/',
        name: 'layout',
        component: Layout,
        children: [
            {
                path: 'dashboard',
                name: 'dashboard',
                component: Dashboard,
                menu: {
                    name: "控制中心",
                    icon: "el-icon-menu"
                }
            },
            {
                path: '/user/list',
                name: 'user-list',
                component: UserList,
                menu: {
                    name: "用户管理",
                    icon: "el-icon-user-solid"
                }
            },
            {
                path: '/carousel/list',
                name: 'carousel-list',
                component: Carousel,
                menu: {
                    name: "首页轮播图管理",
                    icon: "el-icon-picture"
                }
            },
            {
                path: '/recharge-plan/list',
                name: 'recharge-plan-list',
                component: RechargePlanList,
                menu: {
                    name: "充值计划管理",
                    icon: "el-icon-circle-plus"
                }
            },
            {
                path: '/click-plan/list',
                name: 'click-plan-list',
                component: ClickPlanList,
                menu: {
                    name: "点击次数方案管理",
                    icon: "el-icon-plus"
                }
            },
            {
                path: '/run-plan/list',
                name: 'run-plan-list',
                component: RunPlanList,
                menu: {
                    name: "运行方案管理",
                    icon: "el-icon-s-promotion"
                }
            },
            {
                path: '/knowledge/list',
                name: 'knowledge-list',
                component: SeoKnowledge,
                menu: {
                    name: "SEO知识管理",
                    icon: "el-icon-s-management"
                }
            },
            {
                path: '/case/list',
                name: 'case-list',
                component: Cases,
                menu: {
                    name: "历史案例管理",
                    icon: "el-icon-s-opportunity"
                }
            },
            {
                path: '/qa/list',
                name: 'qa-list',
                component: Questions,
                menu: {
                    name: "问答管理",
                    icon: "el-icon-s-comment"
                }
            },
            {
                path: '/friend/list',
                name: 'friend-list',
                component: FriendLink,
                menu: {
                    name: "友情链接管理",
                    icon: "el-icon-picture"
                }
            },
            {
                path: '/system/config',
                name: 'system-config',
                component: Config,
                menu: {
                    name: "系统参数设置",
                    icon: "el-icon-s-tools"
                }
            },
            {
                path: 'finance',
                name: 'finance',
                component: App,
                menu: {
                    name: "财务管理",
                    icon: "el-icon-s-finance"
                },
                children: [
                    {
                        path: 'recharge/log',
                        name: 'recharge-log',
                        component: RechargeLog,
                        menu: {
                            name: "充值记录",
                        }
                    },
                    {
                        path: 'withdraw/log',
                        name: 'withdraw-log',
                        component: WithdrawLog,
                        menu: {
                            name: "提现列表",
                        }
                    }
                ]
            }

        ]
    },
]
