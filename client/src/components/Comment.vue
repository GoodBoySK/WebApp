<template>
    <div class="position-relative w-100 p-3 border-bottom">
        <p class="text-black-50 mb-0 fst-normal ">{{ commentObject.createdAt }}</p>
        <p class="fw-medium mb-0">{{ commentObject.createdBy.userName }}</p>
        <p class="">{{ commentObject.text }}</p>
        <i v-if="false" class="pinned - p-2">dsasdasda</i>
        <div class="d-flex justify-content-end">
            <!-- <button class="btn btn-outline-primary">Edit</button> -->
            <button @click="delCom()" class="btn btn-outline-danger">Delete</button>

        </div>
    </div>
</template>

<script setup lang="ts">
import { deleteComment, type Comment } from '@/services/recipeService';

let {commentObject} = defineProps<{commentObject: Comment}>();

let emit = defineEmits(['deleted']);

async function delCom(){
    console.log(commentObject.id);
    if (commentObject.id) {
        await deleteComment(commentObject.id);
    }
    emit('deleted');
}

</script>

<style lang="scss" scoped>
.pinned {
    position: absolute;
    top: 0;
    right: 0;
}
</style>