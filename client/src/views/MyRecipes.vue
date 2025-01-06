<template>
    <div class="container">
        <ErrorBanner :error="errors"></ErrorBanner>
        <h2>Moje recepty</h2>
        <button class="btn btn-primary" @click="create">Vytvor recept</button>
        <div>
            <div class="row row-cols-auto">
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
    </div>
</template>

<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue';
import { getMyRecipes } from "@/services/recipeService";
import type { Recipe } from '@/services/recipeService';
import { useRouter } from "vue-router";
import { isApiError, type ApiError } from "@/services/apiService";
import ErrorBanner from "@/components/ErrorBanner.vue";
import RecipeCard from "@/components/RecipeCard.vue";

let router = useRouter();

let recipes = ref<Recipe[]>([]);
let errors = ref<ApiError | null>(null)

let currentPage = ref<number>(1);
let maxPage = ref<number>(1);

const pageSize = 10;

let count = ref<number>(0);

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
	let response = await getMyRecipes(currentPage.value, pageSize);

	if (response[0]) {
		recipes.value = response[0].recipes;
		count.value = response[0].allCount;
		maxPage.value = Math.ceil(count.value / pageSize);
	}
	if (response[1] && isApiError(response[1])) {
		errors.value = response[1];
	}
	
};



onMounted(async ()=> {
    search();


});

function create() {
    router.push("/createrecipe");
}

</script>

<style lang="scss" scoped>
.page-item {
	cursor: pointer;
}
</style>