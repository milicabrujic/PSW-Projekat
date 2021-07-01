<template>
  <div>
    <div>
      <v-card class="detailsBorderColor mt-12">
        <v-card-title>
          <span class="primary--text font-italic headline" primary-title
            >Get drug from pharmacy</span
          >
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-form ref="form">
              <v-text-field
                v-model="drugName"
                class="mt-n2"
                label="text*"
                color="primary"
                required
              ></v-text-field>
            </v-form>
          </v-container>
        </v-card-text>
        <v-card-actions class="pr-10 pb-10">
          <v-spacer></v-spacer>
          <v-btn color="primary" @click="getDrug">Get drug</v-btn>
        </v-card-actions>
      </v-card>
    </div>
    <div>
      <v-card v-if="openDrug === true" class="detailsBorderColor mt-12">
        <v-card-title>
          <span class="primary--text font-italic headline" primary-title
            >Save drug from pharmacy</span
          >
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-form ref="form">
              <v-text-field
                v-model="drugDto.name"
                class="mt-n2"
                color="primary"
                disabled
              >
                {{ drugDto.name }}</v-text-field
              >
              <v-text-field
                v-model="drugDto.amount"
                class="mt-n2"
                color="primary"
                disabled
                >{{ drugDto.amount }}</v-text-field
              >
            </v-form>
          </v-container>
        </v-card-text>
        <v-card-actions class="pr-10 pb-10">
          <v-spacer></v-spacer>
          <v-btn color="primary" @click="saveDrug()">Save new Drug</v-btn>
        </v-card-actions>
      </v-card>
    </div>
  </div>
</template>

<script>
import axios from "axios";
export default {
  data() {
    return {
      drugName: "",
      drugDto: {
        id: null,
        name: "",
        amount: null,
      },
      openDrug: false,
    };
  },
  methods: {
    getDrug() {
      axios
        .get("/api/drug/" + this.drugName)
        .then((response) => {
          this.drugDto = response.data;
          console.log("ovo je drug dto" + this.drugDto.name);
          this.drugName = "";
          this.openDrug = true;
        })
        .catch((error) => {
          console.log(error);
        });
    },
    saveDrug() {
      axios
        .post("/api/drug/", this.drugDto)
        .then((drug) => {
          console.log(drug.data);
          this.openDrug = false;
        })
        .catch((error) => {
          console.log(error);
        });
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