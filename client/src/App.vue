<script setup la
  components: { Home },ng="ts">
import Navigation from './components/Navigation.vue';
import { RouterView } from 'vue-router';
</script>

<template>
  <div class="container-fluid p-0 m-0">
    <navigation ></navigation>
    <router-view></router-view>
  </div>
</template>

<style lang="scss">
@import 'assets/main.scss';
@import '/node_modules/bootstrap/scss/bootstrap.scss';  // Bootstrap SCSS
@import "/node_modules/bootstrap/scss/functions";
@import "/node_modules/bootstrap/scss/variables";
@import "/node_modules/bootstrap/scss/mixins"; // This is important for mixins like media-breakpoint-up

</style>
