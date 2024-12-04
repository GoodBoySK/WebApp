<template>
    <div
    class="text-primary m-2 input-group"
    >
        <input class="d-inline form-control" v-model="tag" placeholder="Add tag..."></input>
        <button type="button" class="btn btn-primary" @click="addTag">
            <i class="d-inline bi bi-plus-square-fill"></i>
        </button>
    </div>
</template>

<script setup lang="ts">

import type {Tag} from "@/services/recipeService"

let tag = "";

const emit = defineEmits<{
    (event: 'addTag', payload: Tag):void
}>();
function addTag() {
    const tagEntity:Tag = {name: tag};
    emit('addTag', tagEntity);
}

</script>

<style lang="scss" scoped>
div {
    width: 10rem;
}
</style>