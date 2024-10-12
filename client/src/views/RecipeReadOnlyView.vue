<template>
	<div class="container px-5 rubik main">
		<!-- Hlavicka receptu -->
		<div class="row my-5 recipe-head">
			<!-- Deskripcia recepru -->
			<div class="col">
				<div class="d-flex">
					<p
						class="text-primary mx-2"
						v-for="(tag, index) in recipe.tags"
						:key="index"
					>
						{{ tag }}
					</p>
				</div>
				<h1 class="fw-bold">{{ recipe.title }}</h1>
				<p class="">{{ recipe.description }}</p>
				<autor-label :autor="recipe.autor"></autor-label>

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
								v-for="n in recipe.dificulty"
								:key="n"
								class="fa-solid fa-lemon"
							></i>
							<i
								v-for="n in 5 - recipe.dificulty"
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
				:key="instruction.orderNum"
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
	</div>
</template>

<script setup>
import autorLabel from "@/components/AutorLabel.vue";
import ingredient from "@/components/Ingredient.vue";
import instruction from "@/components/Instruction.vue";
import comment from "@/components/Comment.vue";
import review from "@/components/Review.vue";
import statChose from "@/components/StatChose.vue";

let { id } = defineProps(["id"]);
let recipe = {
	title: "Chutný a jednoduchý zemiakový šalát – perfektný recept pre lenivé gazdinky",
	tags: ["Mnam", "Ham", "Pam"],
	description:
		"Ak ste milovníkov zemiakov a zemiakového šalátu, tento recept musíte vyskúšať! Chutný a jednoduchý zemiakový šalát z pár ingrediencii pre lenivé gazdinky môžete podávať klasicky s vyprážaným rezňom prípadne s „falošným rezňom“ – vyprážanou sekanou.",
	autor: {
		name: "Michal Šovčik",
	},
	dificulty: 3,
	portions: 5,
	time: 120,
};

let instructions = [
	{
		orderNum: 1,
		imgUrl: "https://gurman.zoznam.sk/wp-content/uploads/2024/02/gurman-mac-and-cheese-struhadlo-768x576.jpeg",
		description:
			"Makaróny dáme variť do osolenej vody, varíme do stavu „al dente“. Začneme s nastrúhaním cheddaru na strúhadle. Rúru dáme vyhrievať na 200 stupňov.",
	},
	{
		orderNum: 2,
		imgUrl: "https://gurman.zoznam.sk/wp-content/uploads/2024/02/gurman-mac-and-cheese-struhadlo-768x576.jpeg",
		description:
			"Makaróny dáme variť do osolenej vody, varíme do stavu „al dente“. Začneme s nastrúhaním cheddaru na strúhadle. Rúru dáme vyhrievať na 200 stupňov.",
	},
	{
		orderNum: 3,
		description:
			"Makaróny dáme variť do osolenej vody, varíme do stavu „al dente“. Začneme s nastrúhaním cheddaru na strúhadle. Rúru dáme vyhrievať na 200 stupňov.",
	},
	{
		orderNum: 15,
		imgUrl: "https://gurman.zoznam.sk/wp-content/uploads/2024/02/gurman-mac-and-cheese-struhadlo-768x576.jpeg",
		description:
			"Makaróny dáme variť do osolenej vody, varíme do stavu „al dente“. Začneme s nastrúhaním cheddaru na strúhadle. Rúru dáme vyhrievať na 200 stupňov.",
	},
	{
		orderNum: 25,
		imgUrl: "https://gurman.zoznam.sk/wp-content/uploads/2024/02/gurman-mac-and-cheese-struhadlo-768x576.jpeg",
		description:
			"Makaróny dáme variť do osolenej vody, varíme do stavu „al dente“. Začneme s nastrúhaním cheddaru na strúhadle. Rúru dáme vyhrievať na 200 stupňov.",
	},
];

let comments = [
	{
		autor: {
			name: "Michal Šovčík",
		},
		text: "Perfektne torta uplne uzasna este 20 rokov osm sa zalizoval ale chybalo tam trochu soli kvoli pocasiu.",
		pinned: false,
		createdAt: "19.1.2023 23:55"
	},
	{
		autor: {
			name: "Michal Šovčík",
		},
		text: "Perfektne torta uplne uzasna este 20 rokov osm sa zalizoval ale chybalo tam trochu soli kvoli pocasiu.",
		pinned: true,
		createdAt: "19.1.2023 23:55"
	},
	{
		autor: {
			name: "Michal Šovčík",
		},
		text: "Perfektne torta uplne uzasna este 20 rokov osm sa zalizoval ale chybalo tam trochu soli kvoli pocasiu.",
		pinned: false,
		createdAt: "19.1.2023 23:55"
	},
];

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

let loggedUser = {}

let thumbnailPhotoUrl =
	"https://gurman.zoznam.sk/wp-content/uploads/2024/09/gurman-zemiakovy-salat-pre-lenive-gazdinky-800x600.jpg";
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