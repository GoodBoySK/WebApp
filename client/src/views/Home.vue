<template>
	<div class="container-fluid justify-content-center rubik">
		<ErrorBanner :error="errors"/>
		<banner class="mt-4"/>
		<div class="container-fluid text-center mt-5 py-5">
			<h5 class="text-primary roboto-medium fw-bolder">Recepty</h5>
			<h1 class="display-3 fw-normal">Top dňa</h1>
			<p class="fw-lighter text-black-50 mt-4 mb-3">
				Najnovšie recepty
			</p>
			<div class="row row-cols-1 row-cols-xl-3 mx-auto justify-content-center align-items-center ">
				<div class="col py-3"
					v-for="(tempRecipe, index) in topRecipes"
					:key="index">
					<recipe-card :recipe="tempRecipe" class="mx-auto"/>
				</div>
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
import { getBlogPosts, type BlogPost } from "@/services/blogservice";

let topRecipes = reactive<Recipe[]>([]);

let errors = ref<ApiError | null>(null)

let recentBlogPosts = ref<BlogPost[] | null>(null);

onMounted(async () => {
	let response = await getRecipesByFilter({ascending: false, order: "created_at", page: 1, pageSize: 3});

	if (response[0]) {
		Object.assign(topRecipes, response[0].recipes);
	}
	else if(response[1] && isApiError(response[1])) {
		errors.value = response[1];
	}

	let reponseBlogs = await getBlogPosts({ascending: false, order: "created_at", page: 1, pageSize: 3});

	if (reponseBlogs[0]) {
		recentBlogPosts.value = reponseBlogs[0].blogs;
	}
	else if(reponseBlogs[1] && isApiError(reponseBlogs[1])) {
		errors.value = reponseBlogs[1];
	}
});
</script>

<style lang="scss" scoped>
.recipelist {
	width: 80vw;
}
</style>