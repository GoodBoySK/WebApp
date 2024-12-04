<template>
    <div v-if="loading" class="display-1">Loading ...</div>
	<div v-else class="container main">
		<!-- Menu -->
		<div class="d-flex w-100 flex-row-reverse">
			<button @click="save" class="btn btn-primary mx-2">
				<i class="bi bi-floppy-fill"></i>
			</button>
			<button @click="deleteRecipe" class="btn btn-primary mx-2">
				<i class="bi bi-trash-fill"></i>
			</button>
		</div>	
		<form class="px-5 rubik ">
				<!-- Hlavicka receptu -->
				<div class="row my-5 recipe-head">
					<!-- Deskripcia recepru -->
					<div class="col">
						<div class="d-flex">
							<tag
								v-for="(tag, index) in tags"
								:key="index"
							v-model = "tags[index]"
							editable
								@remove-tag="removeTag"
							>
							</tag>
							<add-tag @add-tag="addNewTag"></add-tag>
						</div>
						<input class="form-control mb-2 fs-1" v-model="recipe.name">
						<textarea class="w-100 description form-control " v-model="recipe.description" placeholder="Popis receptu..."></textarea>
							
						<autor-label :autor="recipe.author"></autor-label>
		
						<div class="justify-content-evenly d-flex fs-6 text-center mt-2">
							<div class="col input-group">
								<i class="input-group-text bi bi-clock-history px-2"></i>
								<div class="form-floating">
									<label
										class="fw-light pt-1"
										for="timeInput"
									>
										TRVANIE
									</label>
									<input id="timeInput" placeholder="minúty..." type="number" class="form-control" min="0" v-model="recipe.time">
								</div>
								<p class="input-group-text text-center mb-0">min</p>
							</div>
							<div class="col input-group mx-2">
								<i class="faIconCenter input-group-text fa-solid fa-bowl-food px-2"></i>
								<div class="form-floating">
									<label
										class="pt-1 fw-light"
										for="portionsInput"
									>
										Porcie
									</label>
									<input id="portionsInput" class="form-control" v-model="recipe.portions" type="number" min="0"></input>
								</div>
							</div>
							<div class="col input-group">
								<i class="faIconCenter input-group-text fa-solid fa-lemon px-2"></i>
								<div class="form-floating">
									<label
										class="pt-2 fw-light"
										for="diificultyInput"
									>
										Náročnosť
									</label>
									<input id="diificultyInput" class="form-control" v-model="recipe.difficulty" type="number" min="0" max="5"></input>
								</div>
								<p class="input-group-text mb-0">/5</p>
							</div>
						</div>
					</div>
					<!-- Thumbnail receptu -->
					<image-choser class="col h-100" v-model="recipe.spotPicture"></image-choser>
				</div>
				<!-- TODO recenzie -->
				<!-- Suroviny -->
				<h1 class="display-6 text-primary fw-medium my-4">Suroviny</h1>
				<div class="bg-body-tertiary p-4 rounded-4 fs-5 shadow-sm">
					<ul class="row row-cols-3">
						<li class="col my-2" v-for="(ingredient, index) in ingredients" :key="index">
							<ingredient editable v-model="ingredients[index]" @remove-ingredient="removeIngredient"></ingredient>
						</li>
						<li class="col my-2">
							<button @click="addIngredient" class="btn btn-primary"><i class="bi bi-plus-circle-fill"></i></button>
						</li>
					</ul>
				</div>
				<!-- Postup -->
				<h1 class="display-6 text-primary fw-medium my-4">Postup</h1>
				<div>
					<instruction
						v-for="(instructionVar, index) in instructions"
						:key="instructionVar.position"
						v-model="instructions[index]"
						editable
					/>
					<button @click="addInstriuction" class="btn btn-primary"><i class="bi bi-plus-circle-fill"></i></button>
		
				</div>
			</form>
	</div>
</template>

<script setup lang="ts">
import autorLabel from "@/components/AutorLabel.vue";
import imageChoser from "@/components/ImageChoser.vue";
import ingredient from "@/components/Ingredient.vue";
import instruction from "@/components/Instruction.vue";
import tag from "@/components/Tag.vue";
import AddTag from "@/components/AddTag.vue";
import {getRecipeById, deleteRecipeById, saveRecipeById} from "@/services/recipeService";
import {getLoggedUserInfo} from "@/services/authenticationService";
import getUrlOfImage from "@/services/mediaFileService";
import {saveImg} from "@/services/mediaFileService";
import { onMounted, reactive } from "vue";
import { ref } from "vue";
import type {Instruction, Recipe, Review, Comment, Tag, Ingredient, UpdateRecipe} from "@/services/recipeService"
import apiService from "@/services/apiService";
import { useRouter } from "vue-router";


let { id } = defineProps(["id"]);

let recipe = reactive<Recipe>({id: "", name: "", description: "",difficulty: 0, portions: 0, time:0, spotPicture: {id: ""}});
let instructions = reactive<Instruction[]>([]);
let ingredients  = reactive<Ingredient[]>([]);
let thumbnailPhotoUrl = ref("");
let loggedUser = reactive({});
let loading = ref(true);
let tags = reactive<Tag[]>([]);

let router = useRouter();

let recipeOriginal:Recipe;

onMounted(async () => {
    recipe = await getRecipeById(id);
	recipeOriginal = JSON.parse(JSON.stringify(recipe));
	
	console.log(recipe);
    Object.assign(instructions, recipe.instructions ?? []);
	Object.assign(tags, recipe.tags ?? []);
	Object.assign(ingredients, recipe.ingredients ?? []);
	if (recipe.spotPicture) {
    	thumbnailPhotoUrl.value = getUrlOfImage(recipe.spotPicture.id + "");
	}

	loggedUser = getLoggedUserInfo();
    loading.value = false;
});

function valid() {
	if (recipe.name == "") return false;

	return true;
}
async function save() {
	if (!valid()) return;
	
	if (recipe.spotPicture.image) {
		recipe.spotPicture.id = await saveImg(recipe.spotPicture.image);
	}

	for(let i = 0;instructions && i < instructions?.length; i++) {
		let x = instructions[i];
		console.log(i)
		if(x.media.image){
			x.media.id = await saveImg(x.media.image);
		}
	};

	let updateObject:UpdateRecipe = {
		name: recipe.name,
		description: recipe.description,
		difficulty: recipe.difficulty,
		portions: recipe.portions,
		spotPicture: recipe.spotPicture,
		time: recipe.time,
		dishTypeId: recipe.dishType ? recipe.dishType.id : -1,

		ingredients: JSON.parse(JSON.stringify(ingredients)),
		instructions: JSON.parse(JSON.stringify(instructions)),
		tags: JSON.parse(JSON.stringify(tags)),
	}  

	saveRecipeById(recipe.id, updateObject);
}

function deleteRecipe(){
	if (confirm("Naozaj chceš zmazať tento recept?")) {
		deleteRecipeById(recipe.id);
		router.push("/");
	}
}

function removeTag(tag:Tag)
{
	const index = tags.findIndex(item => item == tag);
	if (index !== -1) {
		tags.splice(index, 1);
	}
}

function addNewTag(tag:Tag) {
	tags.push(tag);
}

function addIngredient() {
	let ingredient:Ingredient = {name: "", description:""};
	ingredients.push(ingredient);
}

function removeIngredient(obj:Ingredient)
{
	const index = ingredients.findIndex(item => item == obj);
	if (index !== -1) {
		ingredients.splice(index, 1);
	}
}

function addInstriuction() {
	const formData = new FormData();
    formData.append("file", "");

	let instruction:Instruction = {position: instructions.length + 1, description: "", media: {id: "", image: formData}};
	instructions.push(instruction);
}


</script>

<style lang="scss" scoped>

.recipe-head {
	height: 30rem;
}
.main {
	max-width: 70%;
}

.description {
	min-height: 10rem;
}

.faIconCenter{
	display: flex;
    align-items: center;
    justify-content: center;
    height: 100%; 
}
</style>