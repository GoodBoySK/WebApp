<template>
	<div v-if="loadingState == LoadingTypes.Loading" class="display-1">Loading ...</div>
	<div v-if="loadingState == LoadingTypes.Error" class="display-1">Error has occured during loading</div>
 	<div v-else class="container-lg rubik main">
		<ErrorBanner v-if="errors" :error="errors"></ErrorBanner>
		<!-- Menu -->
		<div class="d-flex w-100 flex-row-reverse">
			<button v-if="recipe.author?.id == loggedUser.id" @click="edit" class="btn btn-primary mx-2">
				<i class="bi bi-pencil"></i>
			</button>
		</div>	
		<!-- Hlavicka receptu -->
		<div class="row my-5 recipe-head">
			<!-- Deskripcia recepru -->
			<div class="col-lg h-auto">
				<div class="d-flex">
					<p
						class="text-primary mx-2"
						v-for="(tag, index) in recipe.tags"
						:key="index"
					>
						{{ tag.name }}
					</p>
				</div>
				<h1 class="fw-bold">{{ recipe.name }}</h1>
				<p class="">{{ recipe.description }}</p>
				<autor-label :autor="recipe.author"></autor-label>

				<div class="justify-content-evenly d-flex fs-6 text-center">
					<div class="col">
						<p
							class="text-center mb-0 text-uppercase fs-6 fw-light"
						>
							Trvanie
						</p>
						<div class="">
							<i class="bi bi-clock-history d-inline pe-1"></i>
							<p class="d-inline">{{ recipe.time + " min" }}</p>
						</div>
					</div>
					<div class="col">
						<p
							class="text-center mb-0 text-uppercase fs-6 fw-light"
						>
							Porcie
						</p>
						<div>
							<i class="fa-solid fa-bowl-food d-inline pe-2"></i>
							<p class="d-inline">{{ recipe.portions }}</p>
						</div>
					</div>
					<div class="col">
						<p
							class="text-center mb-0 text-uppercase fs-6 fw-light"
						>
							Náročnosť
						</p>
						<div>
							<i
								v-for="n in recipe.difficulty"
								:key="n"
								class="fa-solid fa-lemon"
							></i>
							<i
								v-for="n in 5 - recipe.difficulty"
								:key="n"
								class="fa-regular fa-lemon"
							></i>
						</div>
					</div>
				</div>
			</div>
			<!-- Thumbnail receptu -->
			<img
				class="col-lg thumbnail p-3 h-auto"
				:src="thumbnailPhotoUrl"
				alt="thumbnail"
			/>
		</div>
		<!-- TODO recenzie -->
		<!-- Suroviny -->
		<h1 class="display-6 text-primary fw-medium my-4">Suroviny</h1>
		<div class="bg-body-tertiary p-4 rounded-4 fs-5 shadow-sm">
			<ul class="list-unstyled row row-cols-1 row-cols-xl-3 mb-0">
				<li class="col my-2" v-for="(ingredient, index) in ingredients" :key="index">
					<ingredient v-model="ingredients[index]"></ingredient>
				</li>
			</ul>
		</div>
		<!-- Postup -->
		<h1 class="display-6 text-primary fw-medium my-4">Postup</h1>
		<div>
			<instruction
				v-for="(instruction, index)  in instructions"
				:key="instruction.position"
				v-model="instructions[index]"
			/>
		</div>

		<!-- New ranting/koments -->
		<div class="m-3 mx-5" v-if="loggedUser">
			<div class="row my-2 w-auto align-items-center row-cols-lg-2 row-cols-1">
				<button class="btn btn-primary col my-auto me-lg-5 me-0 rounded-pill w-auto mx-auto">Pridat do obľúbených receptov <i class="bi bi-heart-fill"></i></button>
				<div class="col bg-light rounded-4 d-flex my-1 row p-0 mx-0 mx-lg-2">
					<p class="text-primary h5 my-auto p-2 p-md-4 col text-md-start text-center">Ohodnotiť recept</p>
					<stat-chose class="fs-2 col-auto"></stat-chose>
				</div>
			</div>
			<form class="row">
				<textarea class="form-control rounded-4 bg-light my-3 fs-6 p-3" placeholder="Sem napiš text pre svoj komentár..." rows="5" v-model="commentText"></textarea>
				<button @click="addComment" class="ms-auto btn btn-primary w-auto rounded-pill px-3 py-2 me-4">Odoslať</button>
			</form>
		</div>
		<p v-else class="m-3 text-black-50">Ak chcete napísať komentár alebo pridať si recept medzi obľúbené recepty prosím prihláste sa</p>
		<!-- Komentare a recenzie-->
		<ul class="nav nav-tabs">
			<li class="nav-item">
				<a class="nav-link active" data-bs-toggle="tab" href="#comments" aria-controls="comments" aria-selected="true"
					>Komentáre</a
				>
			</li>
			<li class="nav-item">
				<a class="nav-link disabled" aria-disabled="true" data-bs-toggle="tab" href="#reviews" aria-controls="reviews"
					>Recenzie</a
				>
			</li>
		</ul>

		<div class="tab-content bg-body-tertiary">
			<div id="comments" class="tab-pane fade show active" aria-labelledby="comments" >
				<comment @deleted="reloadComments" v-for="(com,index) in comments" :key="index" :commentObject="com"></comment>
			</div>
			<div id="reviews" class="tab-pane fade" aria-labelledby="reviews">
				<review v-for="(rew,index) in reviews" :key="index" :reviewObject="rew"></review>
			</div>
		</div>
	</div>
</template>

<script setup lang="ts">
import autorLabel from "@/components/AutorLabel.vue";
import ingredient from "@/components/Ingredient.vue";
import instruction from "@/components/Instruction.vue";
import comment from "@/components/Comment.vue";
import review from "@/components/Review.vue";
import statChose from "@/components/StatChose.vue";
import imageChoser from "@/components/ImageChoser.vue";
import tag from "@/components/Tag.vue";
import {getRecipeById, deleteRecipeById, saveRecipeById, addCommentToRecipe} from "@/services/recipeService";
import {getLoggedUserInfo, type UserData} from "@/services/authenticationService";
import getUrlOfImage from "@/services/mediaFileService";
import { onMounted, reactive } from "vue";
import { ref } from "vue";
import type {Instruction, Recipe, Review, Comment, Tag, Ingredient} from "@/services/recipeService"
import { useRouter } from "vue-router";
import { LoadingTypes } from "@/LoadingTypes";
import ErrorBanner from "@/components/ErrorBanner.vue";
import {isApiError, type ApiError } from "@/services/apiService";


let { id } = defineProps(["id"]);

let recipe = reactive<Recipe>({id: "", name: "", description: "",difficulty: 0, portions: 0, time:0, spotPicture: {id: ""}});
let instructions = reactive<Instruction[]>([]);
let ingredients  = reactive<Ingredient[]>([]);
let thumbnailPhotoUrl = ref("");
let tags = reactive<Tag[]>([]);
let comments = reactive<Comment[]>([]);

let loadingState = ref<LoadingTypes>(LoadingTypes.Loading);
let loggedUser = reactive<UserData>({id: "", userName:"", email: ""});
let router = useRouter();

let recipeOriginal:Recipe;
let commentText = ref("");

let errors = ref<ApiError | null>(null);

onMounted(async () => {
	let response = await getRecipeById(id);
	if (response[0]) {
		recipe = response[0]; 
		recipeOriginal = JSON.parse(JSON.stringify(recipe));
		
		console.log(recipe);
		Object.assign(instructions, recipe.instructions ?? []);
		Object.assign(tags, recipe.tags ?? []);
		Object.assign(ingredients, recipe.ingredients ?? []);
		if (recipe.spotPicture) {
			thumbnailPhotoUrl.value = getUrlOfImage(recipe.spotPicture.id + "");
		}

		Object.assign(comments, recipe.comments ?? []);
		loggedUser = await getLoggedUserInfo() ?? {id: "", userName:"", email: ""};
		loadingState.value = LoadingTypes.Done;
	} 
	else {
		loadingState.value = LoadingTypes.Error;
	}
});

async function reloadComments() {
	let response = await getRecipeById(id);
	if (response[0]) {
		Object.assign(comments, response[0].comments ?? []);
	}
	else {
		loadingState.value = LoadingTypes.Error;
	}
	
}

async function addComment() {
	let response = await addCommentToRecipe(id, {text: commentText.value});

	if (response[0]) {
		reloadComments();
		commentText.value = "";
	}
	else if(response[1] && isApiError(response[1])) {
		errors.value = response[1];
	}
}

function edit() {
	router.push("/recipe/" + id + "/edit");
}

let reviews = [
	{
		autor: {
			name: "Michal Šovčík",
		},
		text: "Perfektne torta uplne uzasna este 20 rokov osm sa zalizoval ale chybalo tam trochu soli kvoli pocasiu.",
		pinned: false,
		createdAt: "19.1.2023 23:55",
		score: 3.5
	},
	{
		autor: {
			name: "Michal Šovčík",
		},
		text: "Perfektne torta uplne uzasna este 20 rokov osm sa zalizoval ale chybalo tam trochu soli kvoli pocasiu.",
		pinned: true,
		createdAt: "19.1.2023 23:55",
		score: 2.7
	},
	{
		autor: {
			name: "Michal Šovčík",
		},
		text: "Perfektne torta uplne uzasna este 20 rokov osm sa zalizoval ale chybalo tam trochu soli kvoli pocasiu.",
		pinned: false,
		createdAt: "19.1.2023 23:55",
		score: 3
	},
];

// let loggedUser = {}

// let thumbnailPhotoUrl =
// 	"https://gurman.zoznam.sk/wp-content/uploads/2024/09/gurman-zemiakovy-salat-pre-lenive-gazdinky-800x600.jpg";
</script>

<style lang="scss" scoped>
@import "../assets/main.scss";


.thumbnail {
	object-fit: scale-down;
	object-position: center;
	width: 100%;
	height: 100%;
	max-height: 20rem;
}
.recipe-head {
	//min-height: 20rem;
}
.main {
	max-width: 70%;
	@include media-breakpoint-down(lg) {
        max-width: 90%;
    }
 
}
</style>