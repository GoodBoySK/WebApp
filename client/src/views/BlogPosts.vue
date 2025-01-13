<template>
  <div class="container text-center">
		<h1 class="display-1 my-5">Objavuj nové príspevky</h1>
		<form>
			<div class="d-flex rounded-5 search">
				<input
					class="form-control py-4 fs-4 px-5 rounded-5"
					type="text"
					placeholder="Názov receptu..."
				/>
				<button class="btn px-4 rounded-end-5">
					<i class="bi bi-search p-2"></i>
				</button>
			</div>
		</form>
		<div class="row row-1 pt-5">
			<div v-for="(n,index) in blogposts" :key="index" class="col p-2">
				<BlogCard :blogPost="n"></BlogCard>
			</div>
		</div>
		<nav aria-label="Page navigation example">
			<ul class="pagination">
				<li class="page-item"><a class="page-link" @click="previousPage">Predchádzajúca</a></li>
				<li class="page-item" v-for="i in maxPage" :key="i" :class="{'active': i == currentPage}"><a class="page-link" @click="goToPage(i)">{{ i }}</a></li>
				<li class="page-item"><a class="page-link" @click="nextPage">Nadchádzajúca</a></li>
			</ul>
		</nav>
	</div>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue';
import { getRecipesByFilter, type Recipe } from '@/services/recipeService';
import ErrorBanner from '@/components/ErrorBanner.vue';
import { isApiError, type ApiError } from '@/services/apiService';
import RecipeCard from '@/components/RecipeCard.vue';
import { getBlogPost, getBlogPosts, type BlogPost } from '@/services/blogservice';
import BlogCard from '@/components/BlogCard.vue';

let blogposts = ref<BlogPost[]>([]);
let errors = ref<ApiError | null>(null)

let currentPage = ref<number>(1);
let maxPage = ref<number>(1);

const pageSize = 10;

let count = ref<number>(0);

let form = ref({
	nameFilter: ""
});

function nextPage()
{
	if (currentPage.value < maxPage.value) {
		currentPage.value++;
		search();
	}
}

function previousPage()
{
	if (currentPage.value > 1) {
		currentPage.value--;
		search();
	}
}

function goToPage(page: number)
{
	if (page > 0 && page <= maxPage.value) {
		currentPage.value = page;
		search();
	}
}


onMounted(async () => {
	search();
});



async function search() {
	let response = await getBlogPosts({nameFilter: form.value.nameFilter, order: "created_at", ascending: false, page: currentPage.value, pageSize: pageSize});

	if (response[0]) {
		blogposts.value = response[0].blogs;
		count.value = response[0].count;
		maxPage.value = Math.ceil(count.value / pageSize);
	}
	if (response[1] && isApiError(response[1])) {
		errors.value = response[1];
	}
	
};

</script>

<style lang="scss" scoped>

</style>