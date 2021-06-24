<template>
  <div>
    <v-btn text color="primary" class="mt-6" @click="hello">Hello</v-btn>
  </div>
</template>

<script>
import axios from "axios";
export default {
  name: "Home",
  data() {
    return {};
  },
  methods: {
    hello() {
      axios
        .get("/message")
        .then((response) => {
          console.log(response);
        })
        .catch((error) => {
          console.log(error);
        });
    },
  },
};
</script>