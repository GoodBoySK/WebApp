<script setup la
  components: { Home },ng="ts">
import Navigation from './components/Navigation.vue';
import { RouterView } from 'vue-router';
</script>

<template>
    <navigation ></navigation>
    <router-view></router-view>
    <footer class="blockquote-footer">
      <h1>FOOTER</h1>
    </footer>
</template>

<style lang="scss">
@import 'assets/main.scss';

</style>
