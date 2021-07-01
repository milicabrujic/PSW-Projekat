import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import vuetify from "./plugins/vuetify";
import axios from "axios";
import interceptorsSetup from "./interceptors";

axios.defaults.baseURL = "http://localhost:5000/";
interceptorsSetup();
Vue.config.productionTip = false;

new Vue({
    router,
    store: store,
    vuetify,
    render: (h) => h(App),
}).$mount("#app");