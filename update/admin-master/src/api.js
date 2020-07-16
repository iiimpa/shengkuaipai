import Axios from 'axios'
import {Message, Loading} from 'element-ui';

//组件方法
export default {
    install(Vue) {
        Vue.prototype.yy = {
            showError,
            showSuccess,
            showWarning,
            showLoading,
            auth,
            query,
            debug,
            formatDt,
        };
        Date.prototype.format = function (format) {
            var o = {
                "M+": this.getMonth() + 1, //month
                "d+": this.getDate(), //day
                "h+": this.getHours(), //hour
                "m+": this.getMinutes(), //minute
                "s+": this.getSeconds(), //second
                "q+": Math.floor((this.getMonth() + 3) / 3), //quarter
                "S": this.getMilliseconds() //millisecond
            }

            if (/(y+)/.test(format)) {
                format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
            }

            for (var k in o) {
                if (new RegExp("(" + k + ")").test(format)) {
                    format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
                }
            }
            return format;
        }
    }
}
const formatDt = (dt, format = "yyyy-MM-dd hh:mm:ss", dv = "暂无") => {
    return dt ? (new Date(dt)).format(format) : dv;
};

const debug = (object) => {
    // eslint-disable-next-line no-undef
    if (process.env.VUE_APP_YYAPI_DEBUG === "true") {
        window.console.log("[DEBUG]", object)
    }
};

const showError = (txt, time = 1500) => {
    Message({
        message: txt,
        duration: time,
        type: 'error'
    });
};
const showSuccess = (txt, time = 1500) => {
    Message({
        message: txt,
        duration: time,
        type: 'success'
    });
};
const showWarning = (txt, time = 1500) => {
    Message({
        message: txt,
        duration: time,
        type: 'warning'
    });
};
const showLoading = (txt = "加载中...") => {
    const loading = Loading.service({
        fullscreen: true,
        text: txt
    });
    loading.show();
    return loading;
};

const auth = {
    login(token) {
        localStorage.setItem('token', token);
    },
    logout() {
        localStorage.removeItem('token');
        location.reload();
    },
    check(url = "/admin/user/info") {
        query(url).then(resp => this.user = resp);
    },
    getToken() {
        return localStorage.getItem('token');
    },
    user: {}
};

/**
 * 请求API接口封装方法
 * 调用方式: this.api(uri,params).then((res)=>{});
 * 非production环境会在console输出请求响应交互信息
 */
let http = Axios.create({
    // eslint-disable-next-line no-undef
    baseURL: process.env.VUE_APP_YYAPI_SERVER,
    timeout: 5000,
    headers: {'Content-Type': 'application/json'}
});
const query = (uri, params = {}) => {
    // eslint-disable-next-line no-undef
    if (null !== localStorage.getItem('token')) {
        http.defaults.headers.common['Token'] = localStorage.getItem('token');
    }
    // eslint-disable-next-line no-undef
    if (process.env.VUE_APP_YYAPI_DEBUG === "true") {
        let S4 = () => {
            return (((1 + Math.random()) * 0x10000 * Date.parse(new Date())) | 0).toString(16).substring(1);
        };
        let query_id = S4() + S4();
        http.defaults.headers.common['Query-Id'] = query_id;
        window.console.log('[REQUEST*]', query_id, '->', uri, 'AUTH:', localStorage.getItem('token'), params);
    }
    let resp = http.post(uri, params);
    return new Promise((resolve, reject) => {
        resp.then((res) => {
            // eslint-disable-next-line no-undef
            if (process.env.VUE_APP_YYAPI_DEBUG === "true") {
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
                    localStorage.removeItem('token');
                    showError("登录失效,请重新登录")
                    location.href = '/#/login';
                    break;
                default:
                    showError(res.data.message);
                    reject(res.data);
            }
        }).catch(() => {
            showError("网络请求发生问题");
        });
    });
};
