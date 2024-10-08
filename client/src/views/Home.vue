<template>
	<div class="container-fluid justify-content-center rubik">
		<banner class="mt-4"/>
		<div class="container text-center mt-5 py-5">
			<h5 class="text-primary roboto-medium fw-bolder">Recepty</h5>
			<h1 class="display-3 fw-normal">Top dňa</h1>
			<p class="fw-lighter text-black-50 mt-4 mb-3">
				Najnavštevovanejšie recepty týždňa
			</p>
			<div class="d-flex justify-content-evenly mx-auto">
				<recipe-card
					v-for="(tempRecipe, index) in topRecipes"
					:key="index"
					:recipe="tempRecipe"
					class="m-3"
				/>
			</div>
		</div>
		<div class="container text-center">
			<h5 class="text-primary roboto-medium fw-bolder">Príspevky</h5>
			<h2 class="">Najnovšie články</h2>
			<div class="d-flex align-items-center flex-column">
				<blog-card v-for="(blogPost,index) in recentBlogPosts" :key="index" :blogPost="blogPost" />
			</div>
		</div>
	</div>
</template>

<script setup>
import banner from "@/components/Banner.vue";
import recipeCard from "@/components/RecipeCard.vue";
import blogCard from "@/components/BlogCard.vue";

let topRecipes = [
	{
		name: "Špagety s mäsovými guličkami a paradajkovou omáčkou",
		thumbnailUrl:
			"https://gurman.zoznam.sk/wp-content/uploads/2024/02/gurman-spagety-s-masovymi-gulickami-spagety-735x600.jpg",
		autor: "Michal Šovčík",
		time: 45,
		portions: 20,
		dificulty: 3,
	},
	{
		name: "Bryndzové halušky s cibuľkou a slaninou",
		thumbnailUrl:
			"https://kuchynalidla.sk/storage/app/uploads/public/5fa/c0f/309/5fac0f309aa5a904284145.jpg",
		autor: "Michal Šovčík",
		time: 45,
		portions: 20,
		dificulty: 3,
	},
	{
		name: "Žemľovka z vianočky s tvarohom a jablkami",
		thumbnailUrl:
			"https://kuchynalidla.sk/storage/app/uploads/public/5fa/c0f/114/5fac0f114e070703027746.jpg",
		autor: "Michal Šovčík",
		time: 45,
		portions: 20,
		dificulty: 3,
	},
];

let recentBlogPosts = [
	{
		thumbnailUrl:
			"https://gurman.zoznam.sk/wp-content/uploads/2024/10/pancakes-2020863_1280-590x332.jpg",
		title: "4 praktické tipy, ako využiť tvrdé a suché pečivo",
		description:
			"Tvrdý chlieb či pečivo určite nemusia skončiť v odpadkoch. Využite ich celé.",
		cardTag: "Triky",
		autor: "Michal Šovčík",
		createdAt: "24 august 2024 14:38",
	},
	{
		thumbnailUrl:
			"https://gurman.zoznam.sk/wp-content/uploads/2024/10/pancakes-2020863_1280-590x332.jpg",
		title: "4 praktické tipy, ako využiť tvrdé a suché pečivo",
		description:
			"Tvrdý chlieb či pečivo určite nemusia skončiť v odpadkoch. Využite ich celé.",
		cardTag: "Triky",
		autor: "Michal Šovčík",
		createdAt: "24 august 2024 14:38",
	},
	{
		thumbnailUrl:
			"https://gurman.zoznam.sk/wp-content/uploads/2024/10/pancakes-2020863_1280-590x332.jpg",
		title: "4 praktické tipy, ako využiť tvrdé a suché pečivo",
		description:
			"Tvrdý chlieb či pečivo určite nemusia skončiť v odpadkoch. Využite ich celé.",
		cardTag: "Triky",
		autor: "Michal Šovčík",
		createdAt: "24 august 2024 14:38",
	},
];
</script>

<style lang="scss" scoped>
.recipelist {
	width: 80vw;
}
</style>