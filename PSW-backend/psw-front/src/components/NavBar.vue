<template>
  <nav>
    <!-- snackbar -->
    <v-snackbar v-model="snackbarSuccess" :timeout="4000" top color="success">
      <span>{{ snackbarSuccessText }}</span>
      <v-btn text @click="snackbarSuccess = false">Close</v-btn>
    </v-snackbar>

    <v-snackbar v-model="snackbarDanger" :timeout="4000" top color="danger">
      <span>{{ snackbarDangerText }}</span>
      <v-btn text @click="snackbarDanger = false">Close</v-btn>
    </v-snackbar>

    <!-- gornji toolbar -->
    <v-toolbar flat class="mx-12">
      <v-spacer></v-spacer>
      <div class="mx-2">
        <LoginComponent
          @loggedIn="
            snackbarSuccess = true;
            snackbarSuccessText = 'You are logged in!';
          "
          @notLoggedIn="
            snackbarDanger = true;
            snackbarDangerText = 'Can not log in. There is no such user!';
          "
        />
      </div>
      <div class="mx-2">
        <RegistrationComponent
          @registered="
            snackbarSuccess = true;
            snackbarSuccessText = 'You are registered! Please login.';
          "
          @notRegistered="
            snackbarDanger = true;
            snackbarDangerText = 'Can not register.';
          "
        />
      </div>
      <div class="mx-2" v-if="this.$store.state.user.role == 'Patient'">
        <router-link to="/createAppointment" class="item"
          >Create Appointment</router-link
        >
      </div>
      <div class="mx-2" v-if="this.$store.state.user.role == 'Doctor'">
        <router-link to="/doctorAppointments" class="item"
          >Doctor Appointments</router-link
        >
      </div>
      <div class="mx-2" v-if="this.$store.state.user.role == 'Patient'">
        <router-link to="/patientAppointments" class="item"
          >Patient Appointments</router-link
        >
      </div>
      <div class="mx-2" v-if="this.$store.state.user.role == 'Administrator'">
        <router-link to="/postedPatientFeedbacksAdmin" class="item"
          >Posted Patient Feedbacks</router-link
        >
      </div>
      <div class="mx-2" v-if="this.$store.state.user.role == 'Administrator'">
        <router-link to="/notPostedPatientFeedbacksAdmin" class="item"
          >Not Posted Patient Feedbacks</router-link
        >
      </div>
      <v-btn
        to="/"
        text
        color="primary"
        v-if="this.$store.state.user.role != 'None'"
      >
        <span @click="logout()">Logout</span>
        <LogoutIcon></LogoutIcon>
      </v-btn>
    </v-toolbar>
  </nav>
</template>

<script>
import RegistrationComponent from "@/components/homePage/RegistrationComponent.vue";
import LoginComponent from "@/components/homePage/LoginComponent.vue";
import LogoutIcon from "vue-material-design-icons/Logout.vue";
export default {
  components: {
    RegistrationComponent,
    LoginComponent,
    LogoutIcon,
  },
  data() {
    return {
      snackbarSuccess: false,
      snackbarSuccessText: "",
      snackbarDanger: false,
      snackbarDangerText: "",
    };
  },
  methods: {
    logout() {
      //localStorage.removeItem("loggedUser");
      this.$store.commit("logout");

      this.setSnackbarSuccess("You are logged out!");
      this.LoginDialog = false;
      console.log("ROLE: " + this.$store.state.user.role + " OVO JE ROLA");
      console.log("TOKEN: " + this.$store.state.token + " OVO JE TOKEN");
    },
    setSnackbarSuccess(message) {
      this.snackbarSuccess = true;
      this.snackbarSuccessText = message;
    },
    setSnackbarDanger(message) {
      this.snackbarDanger = true;
      this.snackbarDangerText = message;
    },
  },
};
</script>