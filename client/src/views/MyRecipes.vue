<template>
    <div>
        <h3>Moje recepty</h3>
        <button class="btn btn-primary" @click="create">Vytvor recept</button>
        <div>
            <div class="row row-cols-auto">
                <recipe-card 
                v-for="(recipeInst, index) in myRecipes"
                :key="index"
                :recipe = recipeInst

                ></recipe-card>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import recipeCard from "@/components/RecipeCard.vue";
import { onMounted, reactive } from 'vue';
import { getMyRecipes } from "@/services/recipeService";
import type { Recipe } from '@/services/recipeService';
import { useRouter } from "vue-router";
import { createTextChangeRange } from "typescript";

let myRecipes = reactive<Recipe[]>([]);
let router = useRouter();
onMounted(async ()=> {
    let myRecipesLocal = await getMyRecipes();

    console.log(myRecipesLocal)

    Object.assign(myRecipes, myRecipesLocal)
});

function create() {
    router.push("/createRecipe");
}

</script>

<style lang="scss" scoped>

</style>