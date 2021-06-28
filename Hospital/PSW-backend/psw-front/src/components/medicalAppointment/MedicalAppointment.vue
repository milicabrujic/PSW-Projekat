<template>
  <div>
    <div>
      <v-card class="detailsBorderColor">
        <v-card-title class="justify-center">
          <span class="primary--text font-italic headline" primary-title>Choose appointment doctor</span>
        </v-card-title>
        <v-card-actions class="justify-center pr-10 pb-10">
          <v-btn color="primary" @click="regularDoctor">Regular doctor</v-btn>
          <v-btn color="primary" @click="specialistDoctor">Specialist doctor</v-btn>
        </v-card-actions>
      </v-card>
    </div>
    <v-card v-if="regular == true && checkP == false" class="detailsBorderColor">
      <v-card-title>
        <span class="primary--text font-italic headline" primary-title>Create appointment to Regular doctor</span>
      </v-card-title>
      <v-card-text>
        <v-container>
          <v-form ref="form">
            <v-text-field v-model="appointment.date" class="mt-n2" label="date*" type="datetime-local" color="primary"
              required></v-text-field>
            <v-select v-model="appointment.DoctorId" label="Choose regular doctor*" color="primary" :items="doctors"
              item-text="Id" required>
            </v-select>
            <v-select v-model="priority" label="Choose priority*" color="primary" :items="priorityList" required>
            </v-select>
          </v-form>
        </v-container>
      </v-card-text>
      <v-card-actions class="pr-10 pb-10">
        <v-btn color="primary" @click="findAppointment">Check dates</v-btn>
      </v-card-actions>
    </v-card>
    <v-card v-if="specialist == true && checkP == false" class="detailsBorderColor">
      <v-card-title>
        <span class="primary--text font-italic headline" primary-title>Use recommendation for specialist</span>
      </v-card-title>
      <v-card-text>
        <v-container>
          <v-form ref="form">
            <v-text-field v-model="appointment.date" class="mt-n2" label="date*" type="datetime-local" color="primary"
              required></v-text-field>
            <v-select v-model="appointment.DoctorId" label="Choose specialist*" color="primary" :items="specialists"
              item-text="Id" required>
            </v-select>
            <v-select v-model="priority" label="Choose priority*" color="primary" :items="priorityList" required>
            </v-select>
          </v-form>
        </v-container>
      </v-card-text>
      <v-card-actions class="pr-10 pb-10">
        <v-btn color="primary" @click="findAppointment">Check dates</v-btn>
      </v-card-actions>
    </v-card>
    <div v-if="checkP === true">
      <div v-if="noTime === true">
        <h2>you need to change time for your doctor</h2>
      </div>
      <v-card v-if="noTime === false" class="detailsBorderColor">
        <v-card-title>
          <span class="primary--text font-italic headline" primary-title>Save appointment</span>
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-form ref="form">
              <v-text-field class="mt-n2" label="date*" readonly type="datetime-local" v-model="appointment.date"
                color="primary" disabled></v-text-field>
            </v-form>
          </v-container>
        </v-card-text>
        <v-card-actions class="pr-10 pb-10">
          <v-btn color="primary" @click="createAppointment">Save appointment</v-btn>
        </v-card-actions>
      </v-card>
    </div>
  </div>
</template>

<script>
  import axios from "axios";
  export default {
    data: () => ({
      appointment: {
        Id: Number,
        PatientId: null,
        DoctorId: null,
        date: {},
      },
      doctors: [],
      specialists: [],
      priority: "",
      priorityList: ["time", "doctor"],
      checkP: false,
      regular: false,
      specialist: false,
      noTime: false,
      regularId: null
    }),

    methods: {
      regularDoctor() {
        this.regular = true;
        this.specialist = false;
        this.noTime = false;
        this.checkP = false;
      },
      specialistDoctor() {
        let specials = this.specialists;
        this.specialists = [];
        specials.forEach((special) => {
          this.specialists.push(special.specialistDoctorId);
        });
        this.specialist = true;
        this.noTime = false;
        this.checkP = false;
        this.regular = false;
      },
      findAppointment() {
        this.appointment.PatientId = this.$store.state.user.id;
        axios
          .post("/api/medicalAppointment/find/" + this.priority, this.appointment)
          .then((appointment) => {
            console.log(appointment.data);
            if (appointment.data === "") {
              console.log("usao  u if");
              this.noTime = true;
            }

            if (this.noTime === false) {
              this.appointment = appointment.data;
            }
            this.checkP = true;
          })
          .catch((error) => {
            console.log(error);
          });
      },
      createAppointment() {
        axios
          .post("/api/medicalAppointment/", this.appointment)
          .then((appointment) => {
            this.checkP = false;
            this.regular = false;
            this.specialist = false;
            console.log(appointment.data);
          })
          .catch((error) => {
            console.log(error);
          });
      },
    },
    mounted() {
      // this.$store.state.user.username = "damjan@gmail.com";
      axios
        .get(
          "/api/doctor/patientGeneralDoctor/" + this.$store.state.user.username
        )
        .then((doctor) => {
          console.log(doctor.data.id);
          this.regularId = doctor.data.id;
          this.doctors.push(doctor.data.id);
          console.log("from regular" + doctor.data);
          console.log(doctor.data);
        })
        .catch((error) => {
          console.log(error);
        });

      axios
        .get("/api/recommendation/" + this.$store.state.user.id)
        .then((doctors) => {
          this.specialists = doctors.data;
          console.log("ovo je recommendation" + this.specialists);
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