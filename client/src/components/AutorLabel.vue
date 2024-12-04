<template>
    <a href="#">{{ autor?.name }}</a>
</template>

<script setup lang="ts">
let {autor} = defineProps(['autor']);
</script>

<style lang="scss" scoped>

</style>