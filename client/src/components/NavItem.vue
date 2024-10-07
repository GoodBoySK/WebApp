<template>
    <li class="nav-item">
        <router-link
        class="nav-link py-2 fs-5 text-nowrap"
        aria-current="page"
        :to="page.link"
        >
        {{ page.title }}
        </router-link>
    </li>
    <div class="vr"></div>
</template>

<script setup>
import { RouterLink } from 'vue-router';

let {page}= defineProps(['page'])



</script>

<style lang="scss" scoped>
    div.vr:last-child {
        display: none;
        visibility: hidden;
    }
</style>