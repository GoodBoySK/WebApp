<template>
	<div class="card text-start rounded-5 m-4">
		<div class="row h-100">
			<div class="float-image overflow-hidden rounded-5 px-0 ">
				<a href="@">
					<img
						:src="blogPost.thumbnailUrl"
						alt="thubmbnail"
						class="overflow-hidden"
					/>
				</a>
			</div>
			<!-- <div class="col-4 overflow-hidden">
				<img
					:src="blogPost.thumbnailUrl"
					alt="thubmbnail"
					class="overflow-hidden"
				/>
			</div> -->
			<div class="col-8">
				<div class="card-body px-4 py-3">
					<p class="card-tag text-primary fw-medium">
						{{ blogPost.cardTag }}
					</p>
					<p class="mb-0 text-black-50 fw-light">
						{{ blogPost.createdAt }}
					</p>
					<a class="card-title h3 text-decoration-none" href="@">{{ blogPost.title }}</a>
					<p class="card-text fw-light">{{ blogPost.description }}</p>
					<button class="btn btn-primary rounded-pill px-3">
						Čítaj viac
					</button>
				</div>
			</div>
		</div>
	</div>
</template>

<script setup>
import autorLabel from "@/components/AutorLabel.vue";
let { blogPost } = defineProps(["blogPost"]);
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
}
.float-image {
	width: 20rem;
	height: 18rem;
	transform: translate(-1rem, -1rem);
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