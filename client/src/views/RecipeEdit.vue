<template>
    <div v-if="true" class="display-1">Loading ...</div>
    <form v-else class="container px-5 rubik main">
		<!-- Hlavicka receptu -->
		<div class="row my-5 recipe-head">
			<!-- Deskripcia recepru -->
			<div class="col">
				<div class="d-flex">
					<tag
						v-for="(tag, index) in recipe.Tags"
						:key="index"
                        :tag = tag
                        editable
					>
					</tag>
				</div>
				<h1 class="fw-bold">{{ recipe.Name }}</h1>
				<p class="">{{ recipe.Description }}</p>
				<autor-label :autor="recipe.Author"></autor-label>

				<div class="justify-content-evenly d-flex fs-6 text-center">
					<div class="col">
						<p
							class="text-center mb-0 text-uppercase fs-6 fw-light"
						>
							Trvanie
						</p>
						<div class="">
							<i class="bi bi-clock-history d-inline pe-1"></i>
							<p class="d-inline">{{ recipe.Time + " min" }}</p>
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
							<p class="d-inline">{{ recipe.Portions }}</p>
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
								v-for="n in recipe.Difficulty"
								:key="n"
								class="fa-solid fa-lemon"
							></i>
							<i
								v-for="n in 5 - recipe.Difficulty"
								:key="n"
								class="fa-regular fa-lemon"
							></i>
						</div>
					</div>
				</div>
			</div>
			<!-- Thumbnail receptu -->
			<img
				class="col thumbnail"
				:src="thumbnailPhotoUrl"
				alt="thumbnail"
			/>
		</div>
		<!-- TODO recenzie -->
		<!-- Suroviny -->
		<h1 class="display-6 text-primary fw-medium my-4">Suroviny</h1>
		<div class="bg-body-tertiary p-4 rounded-4 fs-5 shadow-sm">
			<ul class="row row-cols-3">
				<li class="col my-2" v-for="index in 15" :key="index">
					<ingredient>Surovina 2ks</ingredient>
				</li>
			</ul>
		</div>
		<!-- Postup -->
		<h1 class="display-6 text-primary fw-medium my-4">Postup</h1>
		<div>
			<instruction
				v-for="instruction in instructions"
				:key="instruction.Position"
				:instruction="instruction"
			/>
		</div>

		<!-- New ranting/koments -->
		<div class="m-3 mx-5" v-if="loggedUser">
			<div class="row my-2">
				<button class="btn btn-primary col my-auto me-5 rounded-pill w-auto">Pridat do obľúbených receptov <i class="bi bi-heart-fill"></i></button>
				<div class="col bg-light rounded-4 d-flex">
					<p class="text-primary h5 my-auto p-4">Ohodnotiť recept</p>
					<stat-chose class="fs-2"></stat-chose>
				</div>
			</div>
			<form class="row">
				<textarea class="form-control rounded-4 bg-light my-3 fs-6 p-3" placeholder="Sem napiš text pre svoj komentár..." rows="5"></textarea>
				<button class="ms-auto btn btn-primary w-auto rounded-pill px-3 py-2 me-4">Odoslať</button>
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
				<a class="nav-link" data-bs-toggle="tab" href="#reviews" aria-controls="reviews"
					>Recenzie</a
				>
			</li>
		</ul>

		<div class="tab-content bg-body-tertiary">
			<div id="comments" class="tab-pane fade show active" aria-labelledby="comments" >
				<comment v-for="(com,index) in comments" :key="index" :commentObject="com"></comment>
			</div>
			<div id="reviews" class="tab-pane fade" aria-labelledby="reviews">
				<review v-for="(rew,index) in reviews" :key="index" :reviewObject="rew"></review>
			</div>
		</div>
	</form>
</template>

<script setup lang="ts">
import autorLabel from "@/components/AutorLabel.vue";
import ingredient from "@/components/Ingredient.vue";
import instruction from "@/components/Instruction.vue";
import comment from "@/components/Comment.vue";
import review from "@/components/Review.vue";
import statChose from "@/components/StatChose.vue";
import tag from "@/components/Tag.vue";
import {getRecipeById} from "@/services/recipeService";
import {getLoggedUserInfo} from "@/services/authenticationService";
import getUrlOfImage from "@/services/mediaFileService";
import { onMounted, reactive } from "vue";
import { ref } from "vue";
import type {Instruction, Recipe, Review, Comment} from "@/services/recipeService"
let { id } = defineProps(["id"]);

let recipe = reactive<Recipe>({Id: "", Name: "", Description: "",Difficulty: 0, Portions: 0, Time:0});
let instructions = reactive<Instruction[]>([]);
let comments = reactive<Comment[]>([]);
let reviews = reactive<Review[]>([]);
let thumbnailPhotoUrl = ref("");
let loggedUser = reactive({});
let loading = ref(true);

onMounted(async () => {
    console.log("begin");
    recipe = await getRecipeById(id);
    instructions = recipe.Instructions ?? [];
    comments = recipe.Comments ?? [];
    reviews = recipe.Reviews?.AllReviews ?? [];
    thumbnailPhotoUrl.value = getUrlOfImage(recipe.SpotPicture?.Id + "");
    loggedUser = getLoggedUserInfo();
    loading.value = false;
});

</script>

<style lang="scss" scoped>

.thumbnail {
	object-fit: cover;
	object-position: center;
	width: 100%;
	height: 100%;
}
.recipe-head {
	max-height: 40rem;
}
.main {
	max-width: 70%;
}

</style>