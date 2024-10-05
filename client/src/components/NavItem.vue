<template>
    <li class="nav-item">
        <router-link
        class="nav-link py-2 fs-5"
        aria-current="page"
        :to="page.link"
        >
        {{ page.title }}
        </router-link>
    </li>
</template>

<script setup>
import { RouterLink } from 'vue-router';

let {page}= defineProps(['page'])



</script>

<style></style>