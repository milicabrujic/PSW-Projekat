import axios from "axios";
import store from "./store";

export default function setup() {
    // Add a request interceptor
    axios.interceptors.request.use(
        (config) => {
            let token = store.state.token;
            console.log(token);
            if (token != "") {
                config.headers["Content-Type"] = "application/json";
                config.headers["Authorization"] = `${token}`;
            } else {
                config.headers["Content-Type"] = "application/json";
                config.headers["Authorization"] = ``;
            }

            console.log(config.headers["Authorization"]);
            console.log(config.headers["Content-Type"]);

            return config;
        },

        (error) => {
            return Promise.reject(error);
        }
    );

    // Add a response interceptor
    // axios.interceptors.response.use(
    //     (response) => {
    //         console.log("Response", response);
    //         return response;
    //     },
    //     (error) => {
    //         alert("Whoops! Something went wrong!");
    //         console.log(error);
    //         return Promise.reject(error);
    //     }
    // );
}