<template>
  <div>
    <v-container>
      <v-layout row wrap>
        <v-flex
          xs12
          sm6
          md4
          lg4
          v-for="patientFeedback in postedPatientFeedbacks"
          :key="patientFeedback.id"
        >
          <v-card
            class="mx-4 my-10 detailsBorderColor"
            width="350"
            height="250"
          >
            <v-card-text>
              <p class="display-1 text--primary">
                {{ patientFeedback.text }}
              </p>
              <div class="text--primary">
                <h3>Posted by: {{ patientFeedback.patientUsername }}</h3>
              </div>
              <p>
                Posted on: {{ String(patientFeedback.date).substring(0, 10) }}
              </p>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn
                icon
                color="primary"
                class="mt-12 mr-4"
                @click="changeStatus(patientFeedback)"
                >REMOVE</v-btn
              >
            </v-card-actions>
          </v-card>
        </v-flex>
      </v-layout>
    </v-container>
  </div>
</template>

<script>
import axios from "axios";
export default {
  data() {
    return {
      patientFeedbacks: [],
      postedPatientFeedbacks: [],
    };
  },
  methods: {
    getPatientFeedbacks() {
      axios
        .get("/api/patientfeedback")
        .then((patientFeedbacks) => {
          this.patientFeedbacks = patientFeedbacks.data;
          console.log(this.patientFeedbacks);
          this.getPostedPatientFeedbacks();
        })
        .catch((error) => {
          console.log(error);
        });
    },
    changeStatus(patientFeedback) {
      axios
        .put("/api/patientfeedback", patientFeedback)
        .then((patientFeedback) => {
          console.log(patientFeedback.data);
          this.getPatientFeedbacks();
        })
        .catch((error) => {
          console.log(error);
        });
    },
    getPostedPatientFeedbacks() {
      this.postedPatientFeedbacks = this.patientFeedbacks.filter(
        (patientFeedback) => {
          return patientFeedback.isPosted == true;
        }
      );
    },
  },
  mounted() {
    this.getPatientFeedbacks();
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