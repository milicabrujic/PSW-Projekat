<template>
  <div>
    <v-container>
      <v-layout row wrap>
        <v-flex
          xs12
          sm6
          md4
          lg4
          v-for="malicious in blockedMaliciousPatient"
          :key="malicious.id"
        >
          <v-card
            class="mx-4 my-10 detailsBorderColor"
            width="300"
            height="100"
          >
            <v-card-text>
              <div class="text--primary">
                <h3>Malicious patient: {{ malicious.username }}</h3>
              </div>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn
                icon
                color="primary"
                class="pr-10 pb-10"
                style="position: left"
                @click="blockPatient(malicious.username)"
                >BLOCK</v-btn
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
      maliciousPatients: [],
      blockedMaliciousPatient: [],
    };
  },
  methods: {
    getMaliciousPatients() {
      axios
        .get("/api/patient/maliciousPatients")
        .then((maliciousPatients) => {
          this.maliciousPatients = maliciousPatients.data;
          console.log(this.maliciousPatients);
          this.getBlockedPatients();
        })
        .catch((error) => {
          console.log(error);
        });
    },
    blockPatient(username) {
      axios
        .put("/api/patient/block/" + username)
        .then((block) => {
          console.log(block);
          this.getMaliciousPatients();
        })
        .catch((error) => {
          console.log(error);
        });
    },
    getBlockedPatients() {
      this.blockedMaliciousPatient = this.maliciousPatients.filter(
        (malcious) => malcious.isBlocked === false
      );
    },
  },
  mounted() {
    this.getMaliciousPatients();
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