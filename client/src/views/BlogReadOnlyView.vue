<template>
    <div class="container">
        <img :src="thumbnailPhotoUrl" alt="thumbnail" class="w-75 rounded-2"/>
        <div class="my-3">
            <p class="d-inline px-3 py-2 mx-auto bg-primary text-white rounded-4">{{ blogPost.cardTag }}</p>
        </div>
        <p class="fs-6 text-black-50 mb-0">{{ blogPost.createdAt }}</p>
        <autor-label :autor="blogPost.autor"></autor-label>
        <h1>{{ blogPost.title }}</h1>
        <p>{{ blogPost.description }}</p>
        <div v-html="blogPost.content">
        </div>
    </div>
</template>

<script setup>
import AutorLabel from '@/components/AutorLabel.vue';

let {blogId} = defineProps(['id']);

let blogPost = {
    autor: {
        name: 'Michal Šovčík'
    },
    thumbnail: undefined,
    title: 'Big annouce blog post is so good do have',
	description:
		"Tvrdý chlieb či pečivo určite nemusia skončiť v odpadkoch. Využite ich celé.",
	cardTag: "Triky",
	createdAt: "24 august 2024 14:38",
    content: '<p class="sss"> This is some text </p>\n<h1>This is big text</h1><style>.sss{color: red;}</style>',
}

let thumbnailPhotoUrl = 'https://kuchynalidla.sk/storage/app/uploads/public/611/4e0/a87/6114e0a879e79890334467.jpg';
</script>

<style lang="scss" scoped>

</style>