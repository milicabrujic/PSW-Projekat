<template>
  <v-row justify="center">
    <v-dialog
      v-model="LoginDialog"
      max-width="600px"
      v-if="this.$store.state.user.role == 'None'"
    >
      <template v-slot:activator="{ on }">
        <v-btn class="mx-1" text color="primary" v-on="on">
          <span>Login</span>
          <LoginIcon right>lock_open</LoginIcon>
        </v-btn>
      </template>

      <div class="detailsBorderColor">
        <v-card>
          <v-card-title>
            <span class="primary--text font-italic headline" primary-title
              >Login</span
            >
            <v-spacer></v-spacer>
            <v-btn icon color="primary" @click="LoginDialog = false">
              <CloseIcon></CloseIcon>
            </v-btn>
          </v-card-title>
          <v-card-text>
            <v-container>
              <v-form ref="form">
                <v-text-field
                  label="username*"
                  color="primary"
                  v-model="loginDto.username"
                  required
                ></v-text-field>

                <v-text-field
                  color="primary"
                  label="password"
                  v-model="loginDto.password"
                  type="password"
                  required
                  :rules="PRules"
                ></v-text-field>
              </v-form>
            </v-container>
          </v-card-text>
          <v-card-actions class="pr-10 pb-10">
            <v-spacer></v-spacer>
            <v-btn text color="primary" @click="close">Cancel</v-btn>
            <v-btn color="primary" @click="login">Login</v-btn>
          </v-card-actions>
        </v-card>
      </div>
    </v-dialog>
  </v-row>
</template>

<script>
import axios from "axios";
import LoginIcon from "vue-material-design-icons/Login.vue";
import CloseIcon from "vue-material-design-icons/CloseCircle.vue";
export default {
  components: {
    LoginIcon,
    CloseIcon,
  },
  data: () => ({
    LoginDialog: false,
    PRules: [
      (v) => !!v || "P is required",
      (v) => /\d/.test(v) || "P must contain a number",
      (v) => /[a-z]/.test(v) || "P must contain lower case letter",
      (v) => /[A-Z]/.test(v) || "P must contain upper case letter",
      (v) => /[!@#$%^&*)(+=._-]/.test(v) || "P must contain special character",
    ],
    loginDto: {
      username: "",
      password: "",
      id: "",
    },
  }),
  methods: {
    login() {
      if (this.$refs.form.validate()) {
        axios
          .post("/api/user/login", this.loginDto)
          .then((response) => {
            this.setSession(response.data);
          })
          .catch((error) => {
            console.log(error);
            this.$emit("notLoggedIn");
          });
      } else {
        console.log("not valid");
      }
    },
    setSession(responseData) {
      localStorage.setItem("loggedUser", JSON.stringify(responseData));
      this.$store.commit(
        "login",
        JSON.parse(localStorage.getItem("loggedUser"))
      );
      this.$store.commit("addToken", responseData.authenticationToken);
      console.log("ROLE: " + this.$store.state.user.role + " OVO JE ROLA");
      console.log("TOKEN: " + this.$store.state.token + " OVO JE TOKEN");
      this.$emit("loggedIn");
    },
    close() {
      this.LoginDialog = false;
      this.$refs.form.reset();
    },
  },
};
</script>

<style scoped>
.cardBorderColor {
  border-left: 1px solid #26a69a;
  border-top: 1px solid #26a69a;
  border-right: 1px solid #26a69a;
  border-bottom: 1px solid #26a69a;
}
.detailsBorderColor {
  border-left: 2px solid #26a69a;
  border-top: 2px solid #26a69a;
  border-right: 2px solid #26a69a;
  border-bottom: 2px solid #26a69a;
}
</style>