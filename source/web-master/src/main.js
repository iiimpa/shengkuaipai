import Vue from 'vue'
import App from './App'
import router from './router'
import Axios from 'axios'
import ElementUI from 'element-ui';
import echarts from 'echarts'
import "babel-polyfill"
import 'element-ui/lib/theme-chalk/index.css'

Vue.config.productionTip = false
Vue.use(ElementUI)
Vue.prototype.$echarts = echarts

let api = Axios.create({
  baseURL: process.env.SERVER_URL,
  timeout: 30000,
  headers: {'Content-Type': 'application/json'}
});

/**
 * 请求API接口封装方法
 * 调用方式: this.api(uri,params).then((res)=>{});
 * 非production环境会在console输出请求响应交互信息
 */
Vue.prototype.api = (uri, params = {}, redirectLogin = true) => {
  if (null !== localStorage.getItem('token')) {
    api.defaults.headers.common['Token'] = localStorage.getItem('token');
  }
  if (process.env.NODE_ENV !== 'production') {
    let S4 = () => {
      return (((1 + Math.random()) * 0x10000 * Date.parse(new Date())) | 0).toString(16).substring(1);
    };
    let query_id = S4() + S4();
    api.defaults.headers.common['Query-Id'] = query_id;
    window.console.log('[REQUEST*]', query_id, '->', uri, 'AUTH:', localStorage.getItem('token'), params);
  }
  let resp = api.post(uri, params);
  return new Promise((resolve, reject) => {
    resp.then((res) => {
      if (process.env.NODE_ENV !== 'production') {
        window.console.log('[RESPONSE]', res.config.headers['Query-Id'], '->', res.data)
      }
      /**
       * API返回全局拦截
       */
      switch (res.data.code) {
        case 200:
          resolve(res.data);
          break;
        //需要登录
        case 401:
          localStorage.setItem('token', null);
          if (redirectLogin) {
            location.href = window.document.location.pathname + '#/login';
          }
          break;
        default:
          new Vue().$message.error(res.data.message);
      }
      reject(res.data);
    });
  });
};

window.vue = new Vue({
  el: '#app',
  router,
  data() {
    return {
      user: {}
    }
  },
  render: function (h) {
    return h(App)
  }
}).$mount('#app')
