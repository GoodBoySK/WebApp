<template>
    <div class="position-relative w-100 p-3 border-bottom">
        <p class="text-black-50 mb-0 fst-normal ">{{ commentObject.createdAt }}</p>
        <p class="fw-medium mb-0">{{ commentObject.autor.name }}</p>
        <p class="">{{ commentObject.text }}</p>
        <i v-if="commentObject.pinned" class="pinned - p-2">dsasdasda</i>
    </div>
</template>

<script setup>
let {commentObject} = defineProps(['commentObject']);
</script>

<style lang="scss" scoped>
.pinned {
    position: absolute;
    top: 0;
    right: 0;
}
</style>