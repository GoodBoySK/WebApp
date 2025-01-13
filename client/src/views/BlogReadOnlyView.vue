<template>
    <div class="container">
        <ErrorBanner :error="errors"></ErrorBanner>
        <img :src="thumbnailPhotoUrl" alt="thumbnail" class="w-75 rounded-2"/>
        <div class="my-3">
            <p class="d-inline px-3 py-2 mx-auto bg-primary text-white rounded-4">{{ blogPost?.tag.name }}</p>
        </div>
        <p class="fs-6 text-black-50 mb-0">{{ blogPost?.createdAt }}</p>
        <autor-label :autor="blogPost?.autor"></autor-label>
        <h1>{{ blogPost?.title }}</h1>
        <p>{{ blogPost?.desctiption }}</p>
        <div v-html="blogPost?.content">
        </div>
    </div>
</template>

<script setup lang="ts">
import AutorLabel from '@/components/AutorLabel.vue';
import ErrorBanner from '@/components/ErrorBanner.vue';
import { isApiError, type ApiError } from '@/services/apiService';
import { getBlogPost, type BlogPost } from '@/services/blogservice';
import getUrlOfImage from '@/services/mediaFileService';
import { onMounted, ref } from 'vue';
import { defineProps } from 'vue';


let {id} = defineProps(['id']);

let blogPost = ref<BlogPost | null>(null);

let errors = ref<ApiError | null>(null);
let thumbnailPhotoUrl = ref("");

onMounted(async () => {
    let response  = await getBlogPost(id);

    if (response[1] && isApiError(response[1])) {
        errors.value = response[1];
    } else if(response[0]) {
        blogPost.value = response[0];
        thumbnailPhotoUrl.value = getUrlOfImage(blogPost.value.thumbnail.id);
    }

});


</script>

<style lang="scss" scoped>

</style>