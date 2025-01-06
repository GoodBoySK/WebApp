<template>
	<div class="container-fluid justify-content-center rubik">
		<ErrorBanner :error="errors"/>
		<banner class="mt-4"/>
		<div class="container text-center mt-5 py-5">
			<h5 class="text-primary roboto-medium fw-bolder">Recepty</h5>
			<h1 class="display-3 fw-normal">Top dňa</h1>
			<p class="fw-lighter text-black-50 mt-4 mb-3">
				Najnovšie recepty
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

<script setup lang="ts">
import banner from "@/components/Banner.vue";
import recipeCard from "@/components/RecipeCard.vue";
import blogCard from "@/components/BlogCard.vue";
import { onMounted, reactive, ref } from "vue";
import { getRecipesByFilter, type Recipe } from "@/services/recipeService";
import ErrorBanner from "@/components/ErrorBanner.vue";
import { isApiError, type ApiError } from "@/services/apiService";

let topRecipes = reactive<Recipe[]>([]);

let errors = ref<ApiError | null>(null)

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

onMounted(async () => {
	let response = await getRecipesByFilter({ascending: false, order: "created_at", page: 1, pageSize: 3});

	if (response[0]) {
		Object.assign(topRecipes, response[0].recipes);
	}
	else if(response[1] && isApiError(response[1])) {
		errors.value = response[1];
	}

});
</script>

<style lang="scss" scoped>
.recipelist {
	width: 80vw;
}
</style>