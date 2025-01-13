<template>
	<div class="card text-start rounded-5 m-4">
		<div class="row h-100 row-cols-1 align-items-center justify-content-center p-1">
			<div class="float-image overflow-hidden rounded-5 px-0 col">
				<RouterLink to="/blog/{{ blogPost.id }}">
					<img
						:src="urlSpotPicture"
						alt="thubmbnail"
						class="overflow-hidden"
					/>
				</RouterLink>
			</div>
			<div class="col-lg-8 col">
				<div class="card-body px-4 py-3">
					<p class="card-tag text-primary fw-medium">
						{{ blogPost.tag.name }}
					</p>
					<p class="mb-0 text-black-50 fw-light">
						{{ blogPost.createdAt }}
					</p>
					<a class="card-title h3 text-decoration-none" href="@">{{ blogPost.title }}</a>
					<p class="card-text fw-light">{{ blogPost.desctiption }}</p>
					<button @click="clicked" class="btn btn-primary rounded-pill px-3">
						Čítaj viac
					</button>
				</div>
			</div>
		</div>
	</div>
</template>

<script setup lang="ts">
import type { BlogPost } from "@/services/blogservice";
import getUrlOfImage from "@/services/mediaFileService";
import { computed } from "vue";
import { useRouter } from "vue-router";

let { blogPost } = defineProps<{blogPost:BlogPost}>();

let router = useRouter();

let urlSpotPicture = computed(() => {
	if(blogPost?.thumbnail)
		return getUrlOfImage(blogPost?.thumbnail.id);
	else "";
});

function clicked() {
	router.push("/blog/" + blogPost.id);
}

</script>

<style lang="scss" scoped>
@import "../assets/main.scss";

img {
	object-position: center;
	object-fit: cover;
	width: 100%;
	height: 100%;
    transform: scale(1.03);
    transition-property: transform;
}
img:hover
{
    transform: scale(1);
}
.card {
	height: 16rem;
	width: 60rem;
	@include media-breakpoint-down(lg) {
		height: auto;
		width: 16rem;
	}
}
.float-image {
	width: 20rem;
	height: 18rem;
	@include media-breakpoint-up(lg) {
		transform: translate(-1rem, -1rem);
	}
}

.card-title
{
    transition: color 0.2s ease-in-out  ;
}

.card-title,
img
{
    transition-duration: $transition-base-duration;
    transition-timing-function: $transition-base-function;
}



.card-title:hover {
    color: $primary;

}
</style>