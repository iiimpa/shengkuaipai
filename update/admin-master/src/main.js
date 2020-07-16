import Vue from 'vue'
import App from './App.vue'
import ElementUI from 'element-ui';
import 'element-ui/lib/theme-chalk/index.css';
import routes from './routes'
import VueRouter from "vue-router";
import YYApi from "./api"
import VueQuillEditor from 'vue-quill-editor'
import 'quill/dist/quill.core.css'
import 'quill/dist/quill.snow.css'
import 'quill/dist/quill.bubble.css'

Vue.use(VueQuillEditor);

Vue.use(YYApi);
Vue.use(ElementUI);
Vue.use(VueRouter);
Vue.config.productionTip = false;

window.uploadUrl = process.env.VUE_APP_YYAPI_SERVER + "/admin/upload";
new Vue({
    router: new VueRouter({routes}),
    render: h => h(App),
    mounted() {
        this.yy.auth.check();
    },
    watch: {
        '$route'(to) {
            if (["/login", "/register"].indexOf(to.path) === -1) {
                this.yy.auth.check();
            }
        }
    }
}).$mount('#app')
