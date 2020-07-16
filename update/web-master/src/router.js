import Vue from 'vue'
import Router from 'vue-router'

const originalPush = Router.prototype.push;
Router.prototype.push = function push(location, onResolve, onReject) {
  if (onResolve || onReject) return originalPush.call(this, location, onResolve, onReject)
  return originalPush.call(this, location).catch(err => err)
}
// 首页
import home from './page/home/home'
// 登录
import login from './page/login/login'
//注册
import register from './page/login/register'
// 百度排行优化
import checkKeyword from './CheckKeyword'
// seo知识
import knowledge from './page/knowledge/knowledge'
// seo知识详情
import knowDetails from './page/knowledge/details'
// 服务介绍
import service from './page/service/index'
// 成功案例
import successCase from './page/case/index'
// 常见问题
import common from './page/commonProblem/index'
// 问题详情
import commonDetails from './page/commonProblem/details'

import Index from './page/home/index'

import Dashboard from './page/dashboard/layout'
import orderList from "./page/dashboard/orderList";
import addOrder from "./page/dashboard/addOrder";
import taskList from "./page/dashboard/taskList";
import tasks from "./page/dashboard/tasks";
import myAccount from "./page/dashboard/myAccount";
import agent from "./page/dashboard/agent";
import Friend from './page/friend';

Vue.use(Router)
export default new Router({
  routes: [{
      path: '/',
      name: 'login',
      redirect: "/login",
      component: Index,

      children: [
        //  {
        //   path: '/home',
        //   name: "home",
        //   component: home
        // }, {
        //   path: '/checkKeyword',
        //   name: "checkKeyword",
        //   component: checkKeyword
        // },
        //   {
        //     path: '/friend',
        //     name: "friend",
        //     component: Friend
        //   },
        //   {
        //     path: '/knowledge',
        //     name: "knowledge",
        //     component: knowledge
        //   },
        //   {
        //     path: '/knowDetails',
        //     name: "knowDetails",
        //     component: knowDetails
        //   }, {
        //     path: '/service',
        //     name: "service",
        //     component: service
        //   }, {
        //     path: '/case',
        //     name: "case",
        //     component: successCase
        //   },

        //   {
        //     path: '/common',
        //     name: "common",
        //     component: common
        //   },
        //   {
        //     path: '/commonDetails',
        //     name: "commonDetails",
        //     component: commonDetails
        //   },
        {
          path: '/user',
          name: 'user',
          component: Dashboard,
          children: [{
              path: 'order/add',
              name: 'order-add',
              component: addOrder
            },
            {
              path: 'task/list',
              name: 'task-list',
              component: taskList
            },
            {
              path: 'tasks',
              name: 'tasks',
              component: tasks
            },
            {
              path: 'order',
              name: 'order',
              component: orderList
            },
            {
              path: 'trade',
              name: 'trade',
              component: orderList
            },
            {
              path: 'agent',
              name: 'agent',
              component: agent
            },
            {
              path: 'mine',
              name: 'mine',
              component: myAccount
            }
          ]
        }
      ]
    },
    {
      path: '/login',
      name: "login",
      component: login
    },
    {
      path: '/register',
      name: "register",
      component: register
    },

  ]
})
