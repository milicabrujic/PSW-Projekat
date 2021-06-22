<template>
  <div>
    <div>
      <v-card class="detailsBorderColor mt-12">
        <v-card-title>
          <span class="primary--text font-italic headline" primary-title
            >Add new feedback</span
          >
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-form ref="form">
              <v-text-field
                v-model="feedbackDto.text"
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
          <v-btn color="primary" @click="addFeedback">Add feedback</v-btn>
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
      feedbackDto: {
        text: "",
        date: new Date().toLocaleString(),
        isPosted: false,
        patientId: this.$store.state.user.id,
      },
    };
  },
  methods: {
    addFeedback() {
      console.log(this.feedbackDto);
      axios
        .post("/api/patientfeedback", this.feedbackDto)
        .then((response) => {
          console.log(response.data);
          this.feedbackDto.text = "";
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