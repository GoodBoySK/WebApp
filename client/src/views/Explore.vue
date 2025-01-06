<template>
	<div class="container text-center">
		<ErrorBanner :error="errors"></ErrorBanner>
		<h1 class="display-1 my-5">Objavuj nové recepty</h1>
		<form @submit.prevent="search" class="mb-5">
			<div class="d-flex rounded-5 search">
				<input
					class="form-control py-4 fs-4 px-5 rounded-5"
					type="text"
					placeholder="Názov receptu..."
					v-model="form.nameFilter"
				/>

				<button class="btn px-4 rounded-end-5">
					<i class="bi bi-search p-2"></i>
				</button>
			</div>
		</form>
		<div class="row row-cols-3 pt-5">
			<div v-for="(n,index) in recipes" :key="index" class="col p-2">
				<RecipeCard :recipe="n"></RecipeCard>
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

let recipes = ref<Recipe[]>([]);
let errors = ref<ApiError | null>(null)

let currentPage = ref<number>(1);
let maxPage = ref<number>(1);

const pageSize = 2;

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
	let response = await getRecipesByFilter({nameFilter: form.value.nameFilter, order: "created_at", ascending: false, page: currentPage.value, pageSize: pageSize});

	if (response[0]) {
		recipes.value = response[0].recipes;
		count.value = response[0].allCount;
		maxPage.value = Math.ceil(count.value / pageSize);
	}
	if (response[1] && isApiError(response[1])) {
		errors.value = response[1];
	}
	
};



</script>

<style lang="scss" scoped>
.search {
    position: relative;
	button {
		position: absolute;
		right: 0;
        height: 100%;
	}
}

.page-item {
	cursor: pointer;
}
</style>