<template>
    <div
    class="text-primary mx-2"
    >
        <p class="d-inline">{{ tagVar?.name }}</p>
        <button v-if="editable" type="button" class="btn btn-link">
            <i class="d-inline bi bi-x-octagon-fill " @click="remove"></i>
        </button>
    </div>
</template>

<script setup lang="ts">
import type { Tag } from '@/services/recipeService';

const tagVar = defineModel<Tag>();
const { editable} = defineProps({editable:Boolean});
const emit = defineEmits(['removeTag']);

function remove() {
    emit('removeTag', tagVar.value);
}

</script>

<style lang="scss" scoped>

</style>