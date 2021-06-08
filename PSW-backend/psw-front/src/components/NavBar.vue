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
      <div>
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
    </v-toolbar>
  </nav>
</template>

<script>
import RegistrationComponent from "@/components/homePage/RegistrationComponent.vue";
export default {
  components: {
    RegistrationComponent,
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