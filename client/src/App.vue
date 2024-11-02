
<template>
    <navigation v-if="!hideNabvar"></navigation>
    <router-view></router-view>
    <footer class="blockquote-footer bg-primary  mb-0 mt-3 text-center text-white-50">
      <div class="fs-6 footer-main d-flex justify-content-center py-3">
        <a class="me-3" href="#">Ochrana osobných údajov</a>
        <a class="" href="#">Kontakt</a>
      </div>
      <div class="d-flex footer-mentions justify-content-center py-3 bg-black bg-opacity-25 mt-3">
          <p class="mb-0">&copy; kucharka.sk all rights reserverd</p>
          <div class="vr mx-3"></div>
          <p class="mb-0">Designed by <strong>Michal Šovčík</strong> </p>
          <div class="vr mx-3"></div>
          <p class="mb-0">Developed by <strong>Michal Šovčík</strong> </p>
      </div>
    </footer>
</template>

<script setup>
import { computed } from 'vue';
import Navigation from './components/Navigation.vue';
import { RouterView, useRoute } from 'vue-router';

const route = useRoute();
const hideNabvar = computed(() => route.meta.hideNavbar === true);

</script>

<style lang="scss">
@import 'assets/main.scss';
  .footer-main a{
    text-decoration: none;
    color: $white;
    opacity: 0.5;
    &:hover {
      opacity: 1;
    }
  }


</style>
