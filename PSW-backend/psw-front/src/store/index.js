import Vue from "vue";
import Vuex from "vuex";
import createPersistedState from "vuex-persistedstate";

Vue.use(Vuex);

export default new Vuex.Store({
    plugins: [createPersistedState()],
    state: {
        user: {
            role: "None",
            username: "",
            active: true,
        },
        loggedUser: false,
        token: "",
    },
    mutations: {
        login(state, item) {
            state.user = item;
        },
        logout(state) {
            state.user = {};
            state.user.role = "None";
            state.user.active = true;
            state.user.username = "";
            state.loggedUser = false;
        },
        addToken(state, token) {
            state.token = token;
        },
        removeToken(state) {
            state.token = "";
        },
    },
    actions: {},
    modules: {},
});