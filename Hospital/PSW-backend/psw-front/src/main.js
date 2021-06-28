import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import vuetify from "./plugins/vuetify";
import axios from "axios";

axios.defaults.baseURL = "https://localhost:44393/";
Vue.config.productionTip = false;

new Vue({
    router,
    store: store,
    vuetify,
    render: (h) => h(App),
}).$mount("#app");