<template>
  <div>
    <v-container>
      <h2>History of appointments</h2>
      <v-row>
        <v-col v-for="h in history" :key="h.id" md="4">
          <v-card class="mx-auto detailsBorderColor mt-8" max-width="344">
            <v-card-text>
              <div>Medical appointment</div>
              <p class="display-1 text--primary">
                {{ $store.state.user.username }}
              </p>
              <p v-if="h.status === 1">Done</p>
              <p v-if="h.status === 0">Active</p>
              <p v-if="h.status === 2">Canceled</p>
              <p>{{ h.date }}</p>
              <div class="text--primary">
                doctor id<br />
                <h3>{{ h.doctorId }}</h3>
              </div>
            </v-card-text>

            <v-card-actions v-if="h.status === 0 && h.cancelled === true">
              <v-btn
                text
                @click="cancelAppointment(h.id)"
                color="deep-purple accent-4"
              >
                Cancel appointment
              </v-btn>
            </v-card-actions>
            <div
              v-if="h.cancelled === false && h.status === 0"
              class="text--primary"
            >
              <h3 style="text-align: center">
                Can't cancel appointment, there is no enough time
              </h3>
            </div>
            <div v-if="h.status === 1 || h.status === 2" class="text--primary">
              <h3 style="text-align: center">
                Appointments is over or cancelled
              </h3>
            </div>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>

<script>
import axios from "axios";
export default {
  data: () => ({
    history: [],
  }),
  methods: {
    cancelAppointment(id) {
      axios
        .post("/api/medicalAppointment/cancelAppointment/" + id)
        .then((appointment) => {
          axios
            .post("/api/patient/malicious/" + this.$store.state.user.username)
            .then((response) => {
              console.log(response);
            })
            .catch((error) => {
              console.log(error);
            });
          console.log(appointment.data);
          window.location.reload();
        })
        .catch((error) => {
          console.log(error);
        });
    },
  },
  mounted() {
    axios
      .get(
        "/api/medicalAppointment/appointmentsPatient/" +
          this.$store.state.user.id
      )
      .then((appointments) => {
        this.history = appointments.data;
        console.log(this.history);
      })
      .catch((error) => {
        console.log(error);
      });
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