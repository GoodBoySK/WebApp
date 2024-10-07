<template>
	<div class="card text-start rounded-4">
		<img class="card-img-top" :src="recipe.thumbnailUrl" alt="Thumbnail" />
		<div class="card-body roboto d-flex flex-column">
			<h4 class="card-title">{{ recipe.name }}</h4>
			<a class="card-subtitle flex-grow-1">{{ recipe.autor }}</a>
			<div class="justify-content-evenly d-flex fs-6 text-center">
				<div class="col">
					<i class="bi bi-clock-history d-inline pe-1"></i>
					<p class="d-inline">{{ recipe.time + " min" }}</p>
				</div>
				<div class="col">
					<i class="fa-solid fa-bowl-food d-inline pe-2"></i>
					<p class="d-inline">{{ recipe.portions }}</p>
				</div>
				<div class="col">
					<i
						v-for="n in recipe.dificulty"
						:key="n"
						class="fa-solid fa-lemon"
					></i>
					<i
						v-for="n in 5 - recipe.dificulty"
						:key="n"
						class="fa-regular fa-lemon"
					></i>
				</div>
			</div>
		</div>
	</div>
</template>

<script setup>
let { recipe } = defineProps(["recipe"]);
</script>

<style lang="scss" scoped>
@import "../assets/main.scss";

$card-width: 26rem;
$card-height: 26rem;
.card {
	width: $card-width;
	height: $card-height;
	box-shadow: $box-shadow-sm;
	transition-duration: $transition-base-duration;
	h4,a {
		transition-property: color;
	}

	.card-body {
		transition-property: border-top;
		transition-duration: $transition-base-duration;
		transition-timing-function: $transition-base-function;
	}
}

.card,
.card h4,
.card a 
.card .card-body{
	transition-duration: $transition-base-duration;
	transition-timing-function: $transition-base-function;
}
img {
	height: 60%;
	object-fit: cover;
	object-position: center;
}

.card:hover {
	box-shadow: $box-shadow-lg;
	cursor: pointer;
    transform: translateY(-10px); // Lift card up by 10px
	transform: rotate(2deg);
	h4,a{
		color: $primary;
	}
	.card-body {
		border-top: $primary solid 5px;
	}
}
</style>