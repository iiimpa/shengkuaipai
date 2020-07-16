# YY-Admin-Template

基于VUE3和ElementUI的一个简易的后台模板

自带Axios封装后的API请求方法

路由配置在 src/routes.js 中

```javascript
{
        path: '/',
        name: 'layout',
        component: Layout,
        children: [
            {
                path: 'dashboard',
                name: 'dashboard',
                component: Dashboard,
                menu: {                      //包含menu字段,表示这个路由会在导航菜单中显示
                    name: "控制中心",  		 //菜单的名称
                    icon: "el-icon-menu"	 //如果是一级菜单,可以指定一个ElementUI中的ICON
                }
            },
            {
                path: 'member',				
                name: 'member',
                component: App,			     //此处复用App.vue,所以,本文件中不要写任何mounted()等生命周期方法,避免触发两次
                menu: {
                    name: "用户管理",
                    icon: "el-icon-user-solid"
                },
                children: [
                    {
                        path: 'user',
                        name: 'user',				
                        component: UserList,
                        menu: {
                            name: "用户管理",
                        }
                    }
                ]
            },
        ]
    },
```

## Project setup
```
yarn install
```

### Compiles and hot-reloads for development
```
yarn serve
```

### Compiles and minifies for production
```
yarn build
```

### Lints and fixes files
```
yarn lint
```

### Customize configuration
See [Configuration Reference](https://cli.vuejs.org/config/).
