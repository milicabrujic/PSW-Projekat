<template>
  <div>
    <v-container>
      <h2>Appointments for doctor</h2>
      <v-row v-if="startAppointment == false">
        <v-col v-for="h in history" :key="h.id" md="4">
          <v-card class="mx-auto detailsBorderColor mt-8" max-width="344">
            <v-card-text>
              <div>Medical Appointment</div>
              <p class="display-1 text--primary">{{ h.patientId }}</p>
              <p>{{ h.date }}</p>
              <div class="text--primary">
                Doctor<br />
                <h3>{{ h.doctorId }}</h3>
              </div>
            </v-card-text>

            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="primary" @click="start(h.id)">
                Start appointment
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-col>
      </v-row>
      <v-card
        v-if="createRecommendation == true"
        class="detailsBorderColor mt-8"
      >
        <v-card-title>
          <span class="primary--text font-italic headline" primary-title
            >Add doctor for recommendation</span
          >
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-form ref="form">
              <v-select
                v-model="recommendation.SpecialistDoctorId"
                outlined
                label="Choose specialist*"
                color="primary"
                :items="specialists"
                item-text="Id"
                required
              >
              </v-select>
            </v-form>
          </v-container>
        </v-card-text>
        <v-card-actions class="pr-10 pb-10">
          <v-btn color="primary" @click="startRecommendation()"
            >Create recommendation</v-btn
          >
          <v-spacer></v-spacer>
          <v-btn color="primary" @click="end()">End Appointment</v-btn>
        </v-card-actions>
      </v-card>
      <v-card
        v-if="startAppointment == true && closeAppointment == false"
        class="detailsBorderColor mt-8"
      >
        <v-card-title>
          <span class="primary--text font-italic headline" primary-title
            >Appointment</span
          >
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-form ref="form">
              <v-text-field
                v-model="appointment.date"
                class="mt-n2"
                label="date"
                type="datetime-local"
                color="primary"
                disabled
              ></v-text-field>
              <v-text-field
                v-model="appointment.PatientId"
                label="Patient"
                color="primary"
                disabled
              >
              </v-text-field>
            </v-form>
          </v-container>
        </v-card-text>
        <v-card-actions class="pr-10 pb-10">
          <v-btn color="primary" @click="clickRecommendation()"
            >Choose Specialists</v-btn
          >
          <v-btn color="primary" @click="clickPrescription()"
            >Create Prescription</v-btn
          >
          <v-spacer></v-spacer>
          <v-btn color="primary" @click="end()">End Appointment</v-btn>
        </v-card-actions>
      </v-card>

      <!-- Prescriptions -->
      <v-card v-if="createPrescription == true" class="detailsBorderColor">
        <v-card-title>
          <span class="primary--text font-italic headline" primary-title
            >Create prescription</span
          >
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-form ref="form">
              <v-text-field
                v-model="prescription.Text"
                class="mt-n2"
                label="text*"
                color="primary"
                required
              ></v-text-field>
              <v-select
                v-model="prescription.DrugNames"
                label="Choose drugs*"
                color="primary"
                :items="drugs"
                item-text="name"
                multiple
                clearable
                outlined
                class="pt-4 mb-n8"
                required
              >
              </v-select>
            </v-form>
          </v-container>
        </v-card-text>
        <v-card-actions class="pr-10 pb-10">
          <v-spacer></v-spacer>
          <v-btn color="primary" @click="savePrescription()"
            >SAVE PRESCRIPTION</v-btn
          >
        </v-card-actions>
      </v-card>
      <!-- EndPrescriptions -->
    </v-container>
  </div>
</template>

<script>
import axios from "axios";
export default {
  data: () => ({
    history: [],
    startAppointment: false,
    closeAppointment: false,
    createRecommendation: false,
    createPrescription: false,
    appointment: {
      Id: null,
      PatientId: null,
      DoctorId: null,
      date: {},
    },
    recommendation: {
      Id: Number,
      PatientId: null,
      SpecialistDoctorId: null,
    },
    prescription: {
      Text: "",
      PatientId: null,
      DoctorId: null,
      DoctorUsername: "",
      DrugNames: [],
    },
    drugs: [],
  }),
  computed: {},
  methods: {
    start(id) {
      let specials = this.specialists;
      this.specialists = [];
      specials.forEach((special) => {
        this.specialists.push(special.id);
      });
      this.history.forEach((h) => {
        if (h.id === id) {
          this.appointment.Id = h.id;
          this.appointment.DoctorId = h.DoctorId;
          this.appointment.date = h.date;
          this.appointment.PatientId = h.patientId;
        }
      });
      this.startAppointment = true;
    },
    end() {
      axios
        .post("/api/medicalAppointment/end", this.appointment)
        .then((appointment) => {
          console.log(appointment.data);
          this.createRecommendation = false;
          this.startAppointment = false;
          this.closeAppointment = false;
          window.location.reload();
        })
        .catch((error) => {
          console.log(error);
        });
    },
    clickRecommendation() {
      this.createRecommendation = true;
      this.closeAppointment = true;
    },
    clickPrescription() {
      this.createPrescription = true;
    },
    startRecommendation() {
      this.recommendation.PatientId = this.appointment.PatientId;
      axios
        .post("/api/recommendation/", this.recommendation)
        .then((recommendation) => {
          console.log(recommendation.data);
          this.closeAppointment = false;
          this.createRecommendation = false;
        })
        .catch((error) => {
          console.log(error);
        });
    },
    savePrescription() {
      this.prescription.PatientId = this.appointment.PatientId;
      this.prescription.DoctorId = this.$store.state.user.id;
      this.prescription.DoctorUsername = this.$store.state.user.username;

      axios
        .post("/api/prescription/", this.prescription)
        .then((prescription) => {
          console.log(prescription.data);
          this.createPrescription = false;
          this.prescription.Text = "";
          this.prescription.DrugNames = [];
        })
        .catch((error) => {
          console.log(error);
        });
    },
    getDrugs() {
      axios
        .get("/api/drug")
        .then((drugs) => {
          this.drugs = drugs.data;
          console.log(this.drugs);
        })
        .catch((error) => {
          console.log(error);
        });
    },
  },
  mounted() {
    //  this.$store.state.user.username = 2;
    axios
      .get(
        "/api/medicalAppointment/activeAppointmentsDoctor/" +
          this.$store.state.user.id
      )
      .then((appointments) => {
        this.history = appointments.data;
        console.log(this.history);
      })
      .catch((error) => {
        console.log(error);
      });

    axios
      .get("/api/doctor/specialists")
      .then((doctors) => {
        this.specialists = doctors.data;
        console.log(this.specialists);
      })
      .catch((error) => {
        console.log(error);
      });

    this.getDrugs();
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