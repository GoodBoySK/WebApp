<template>
    <div v-if="!editable" class="d-flex align-top main-container py-4">
        <p class="border px-3 me-3 py-2 my-auto rounded-3 align-self-center order text-center">{{ instruction?.position }}</p>
        <img class="border" v-if="instruction?.media" :src="photoUrl" />
        <p class="mb-0 w-auto p-4 border ">{{ instruction?.description }}</p>
    </div>
    <div v-else class="d-flex align-top main-container py-4">
        <input class="border px-3 me-3 py-2 my-auto rounded-3 align-self-center order text-center" type="text" placeholder="0" v-model="instruction!.position"></input>
        <imageChoser v-if="instruction?.media" v-model="instruction.media" class="imgChoser"></imageChoser>
        <textarea class="mb-0 w-auto p-4 border flex-fill" type="text" placeholder="Postup receptu..." v-model="instruction!.description"></textarea>
    </div>
</template>

<script setup lang="ts">
import getUrlOfImage from '@/services/mediaFileService';
import type { Instruction } from '@/services/recipeService';
import { onMounted, ref } from 'vue';
import imageChoser from './ImageChoser.vue';

const {editable} = defineProps({editable:Boolean});
let instruction = defineModel<Instruction>();
let photoUrl = ref("");

onMounted(async () => {
    if (instruction.value && instruction.value.media ) {
    	photoUrl.value = getUrlOfImage(instruction.value.media.id + "");
	}
});

</script>

<style lang="scss" scoped>
img
{
    width: 20rem;
    height: 16rem;
}
textarea {
    resize: none;
}

.order {
    width: 3rem;;
}
.main-container{
    position: relative;
    min-height: 15rem;
    max-height: 25rem;
}
.main-container:not(:last-child):after
{
    content: '';
  position: absolute;
  width: 2px;
  background-color: rgba(186, 186, 186, 0.543);
  top: calc(50% + 40px);
  bottom: 0;
  left: 1.5rem;
  margin-left: -3px;
}
.main-container:not(:first-child)::before
{
    content: '';
  position: absolute;
  width: 2px;
  background-color: rgba(186, 186, 186, 0.543);
  top: 0;
  bottom: calc(50% + 40px);
  left: 1.5rem;
  margin-left: -3px;
}

.imgChoser {
    max-width: 20rem;
}
</style>