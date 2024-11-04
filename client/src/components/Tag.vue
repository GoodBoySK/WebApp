<template>
    <div
    class="text-primary mx-2"
    >
        <p class="d-inline">{{ tag }}</p>
        <button v-if="editable" type="button" class="btn btn-link">
            <i class="d-inline bi bi-x-octagon-fill " @click="remove"></i>
        </button>
    </div>
</template>

<script setup lang="ts">

const {tag, editable} = defineProps(['tag', 'editable']);
const emit = defineEmits(['removeTag']);
function remove() {
    emit('removeTag', tag);
}

</script>

<style lang="scss" scoped>

</style>