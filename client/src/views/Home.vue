<template>
	<div class="container-fluid justify-content-center rubik">
		<banner />
		<div class="container text-center mt-5 py-5">
			<h5 class="text-primary roboto-medium fw-bolder">Recepty</h5>
			<h2 class="">Top dňa</h2>
			<p class="fw-lighter text-black-50 mt-4 mb-3">
				Najnavštevovanejšie recepty týždňa
			</p>
            <div class="d-flex justify-content-evenly  mx-auto ">
                <recipe-card v-for="(tempRecipe, index) in topRecipes" :key="index"  :recipe="tempRecipe" class="m-3" />
            </div>

		</div>
        <div class="container text-center">
            <h5 class="text-primary roboto-medium fw-bolder">Príspevky</h5>
			<h2 class="">Najnovšie články</h2>
        </div>
	</div>
</template>

<script setup>
import banner from "@/components/Banner.vue";
import recipeCard from "@/components/RecipeCard.vue";

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
		name: "Špagety s mäsovými guličkami a paradajkovou omáčkou",
		thumbnailUrl:
			"https://gurman.zoznam.sk/wp-content/uploads/2024/02/gurman-spagety-s-masovymi-gulickami-spagety-735x600.jpg",
		autor: "Michal Šovčík",
		time: 45,
		portions: 20,
		dificulty: 3,
	},
	{
		name: "Špagety s mäsovými guličkami a paradajkovou omáčkou",
		thumbnailUrl:
			"https://gurman.zoznam.sk/wp-content/uploads/2024/02/gurman-spagety-s-masovymi-gulickami-spagety-735x600.jpg",
		autor: "Michal Šovčík",
		time: 45,
		portions: 20,
		dificulty: 3,
	},
];
</script>

<style lang="scss" scoped>
.recipelist {
    width: 80vw;
}
</style>