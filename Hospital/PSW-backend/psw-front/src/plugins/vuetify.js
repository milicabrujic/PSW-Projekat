import Vue from "vue";
import Vuetify from "vuetify/lib";

Vue.use(Vuetify);

//theme colors
const vuetify = new Vuetify({
    theme: {
        themes: {
            light: {
                primary: "#26A69A", //svetlo zelena
                secondary: "#F5F5F5", //svetlo siva
                success: "#A5D6A7", //svetlo zelena
                danger: "#EF9A9A", //svetlo crvena
            },
        },
    },
});

export default vuetify;