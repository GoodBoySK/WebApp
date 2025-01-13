<template>
    <div v-if="!editable" class="d-flex align-top main-container py-4">
        <p class="border px-3 me-3 py-2 my-auto rounded-3 align-self-center order text-center">{{ instruction?.position }}</p>
        <div class="d-flex flex-wrap flex-lg-row flex-column w-100 h-100 align-self-center">
            <div  v-if="instruction?.media && instruction.media.isPresent" class="flex-shrink-0">
                <img class="border img-fluid" :src="photoUrl" />
            </div>
            <div class=" flex-grow-1 col">
                <p class=" h-100 w-100  p-1 border border-1">{{ instruction?.description }}</p>
            </div>
        </div>
    </div>
    <div v-else class="d-flex align-top main-container py-4">
        <input class="border px-3 me-3 py-2 my-auto rounded-3 align-self-center order text-center" type="text" placeholder="0" v-model="instruction!.position"></input>
        
        <div class="d-flex flex-wrap flex-lg-row flex-column w-100 h-100">
            <div  v-if="instruction?.media" class="">
                <imageChoser  v-model="instruction.media" class="imgChoser img-fluid"></imageChoser>
            </div>
            <div class=" flex-grow-1">
                <textarea class="mb-0 w-100 h-100 p-4 border " type="text" placeholder="Postup receptu..." v-model="instruction!.description"></textarea>
            </div>
        </div>
        
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
    max-height: 16rem;
    object-fit: scale-down;
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
    max-height: 45rem;
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