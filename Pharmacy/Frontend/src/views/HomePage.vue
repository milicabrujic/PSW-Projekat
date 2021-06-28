<template>
  <div>
    <v-container>
      <h2 class="mt-12">Perscriptions</h2>
      <v-layout row wrap>
        <v-flex
          xs12
          sm6
          md4
          lg4
          v-for="prescription in prescriptions"
          :key="prescription.id"
        >
          <v-card
            class="detailsBorderColor mx-4 my-10 mt-12"
            width="350"
            height="300"
          >
            <v-card-text>
              <p class="display-1 text--primary">
                {{ prescription.text }}
              </p>
              <div class="text--primary pt-12">
                <h3>Prescribed by: {{ prescription.doctorUsername }}</h3>
              </div>
              <v-select
                label="Drugs"
                color="primary"
                :items="prescription.DrugNames"
                item-disabled="disable"
                outlined
                class="pt-4 mb-n8"
              >
              </v-select>
            </v-card-text>
            <v-card-actions class="pb-10">
              <v-spacer></v-spacer>
              <v-btn color="primary" @click="prescribeTherapy(prescription)"
                >PRESCRIBE THERAPY</v-btn
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
  name: "Home",
  data() {
    return {
      prescriptions: [],
    };
  },
  methods: {
    getPrescriptions() {
      axios
        .get("/prescriptions")
        .then((prescriptions) => {
          this.prescriptions = prescriptions.data;
          console.log(this.prescriptions);
        })
        .catch((error) => {
          console.log(error);
        });
    },
    prescribeTherapy(prescription) {
      console.log(prescription);
      axios
        .put("/drugs", prescription)
        .then((response) => {
          console.log(response);
          this.getPrescriptions();
        })
        .catch((error) => {
          console.log(error);
          alert("Not enough drugs!");
        });
    },
  },
  mounted() {
    this.getPrescriptions();
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
  border-left: 20px solid #26a69a;
  border-top: 2px solid #26a69a;
  border-right: 2px solid #26a69a;
  border-bottom: 2px solid #26a69a;
}
</style>