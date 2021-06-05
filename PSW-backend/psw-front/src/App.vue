<template>
  <!-- root vuetify component -->
  <v-app>
    <NavBar />
    <v-main class="mx-12">
      <router-view></router-view>
    </v-main>
  </v-app>
</template>

<script>
import NavBar from "@/components/NavBar";
export default {
  name: "App",
  components: { NavBar },
  data: () => ({
    logged: false
  })
};
</script>
