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

<style scoped>

</style>
