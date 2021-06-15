<template>
  <v-row justify="center">
    <v-dialog
      v-model="RegisterDialog"
      max-width="600px"
      v-if="this.$store.state.user.role == 'None'"
    >
      <template v-slot:activator="{ on }">
        <v-btn text color="primary" v-on="on">
          <span>Register</span>
          <RegistrationIcon></RegistrationIcon>
        </v-btn>
      </template>
      <v-card class="detailsBorderColor">
        <v-card-title>
          <span class="primary--text font-italic headline" primary-title
            >Registration</span
          >
          <v-spacer></v-spacer>
          <v-btn icon color="primary" @click="RegisterDialog = false">
            <CloseIcon></CloseIcon>
          </v-btn>
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-form ref="form">
              <v-text-field
                class="mt-n2"
                label="Name*"
                color="primary"
                v-model="user.name"
                required
                :rules="requiredRules"
              ></v-text-field>
              <v-text-field
                label="Surname*"
                color="primary"
                v-model="user.surname"
                required
                :rules="requiredRules"
              ></v-text-field>
              <v-text-field
                label="Username*"
                color="primary"
                v-model="user.username"
                required
                :rules="requiredRules"
              ></v-text-field>
              <v-text-field
                label="Email*"
                color="primary"
                v-model="user.email"
                required
                :rules="emailRules"
              ></v-text-field>
              <v-text-field
                color="primary"
                label="Password*"
                v-model="user.password"
                type="password"
                required
                :rules="passwordRules"
              ></v-text-field>
              <v-text-field
                color="primary"
                label="Confirm password*"
                v-model="confirmation"
                type="password"
                required
                :rules="[passwordConfirmationRule]"
              ></v-text-field>
              <v-text-field
                label="Address*"
                color="primary"
                v-model="user.address"
                required
                :rules="requiredRules"
              ></v-text-field>
              <v-text-field
                label="Phone number*"
                color="primary"
                v-model="user.phoneNumber"
                required
                :rules="requiredRules"
              ></v-text-field>
            </v-form>
          </v-container>
        </v-card-text>
        <v-card-actions class="pr-10 pb-10">
          <v-spacer></v-spacer>
          <v-btn text color="primary" @click="close">Cancel</v-btn>
          <v-btn color="primary" @click="register">Register</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-row>
</template>
<script>
import axios from "axios";
import RegistrationIcon from "vue-material-design-icons/AccountCircle.vue";
import CloseIcon from "vue-material-design-icons/CloseCircle.vue";
export default {
  components: {
    RegistrationIcon,
    CloseIcon,
  },
  data: () => ({
    RegisterDialog: false,
    user: {
      name: "",
      surname: "",
      username: "",
      email: "",
      password: "",
      adrress: "",
      phoneNumber: "",
      role: "Patient",
      isBlocked: false,
      isMalicious: false,
      canceledMedicalAppointments: 0,
      generalDoctorId: 3,
    },
    confirmation: "",
    requiredRules: [(v) => !!v || "This field is required"],
    passwordRules: [
      (v) => !!v || "This is required",
      //(v) => v == this.confirmation || "Passwords do not match",
      (v) => /\d/.test(v) || "Password must contain a number",
      (v) => /[a-z]/.test(v) || "Password must contain lower case letter",
      (v) => /[A-Z]/.test(v) || "Password must contain upper case letter",
      (v) =>
        /[!@#$%^&*)(+=._-]/.test(v) ||
        "Password must contain special character",
    ],
    emailRules: [
      (v) => !!v || "This field is required",
      (v) => /.+@.+\..+/.test(v) || "E-mail must be valid",
    ],
  }),
  computed: {
    passwordConfirmationRule() {
      return () =>
        this.user.password === this.confirmation || "Password must match";
    },
  },
  methods: {
    register() {
      if (this.$refs.form.validate()) {
        axios
          .post("/api/patient", this.user)
          .then((response) => {
            this.$emit("registered");
            this.RegisterDialog = false;
            this.$refs.form.reset();
            console.log(response.data);
          })
          .catch((error) => {
            console.log(error);
            this.$emit("notRegistered");
          });
      } else {
        console.log("not valid");
      }
    },
    close() {
      this.RegisterDialog = false;
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