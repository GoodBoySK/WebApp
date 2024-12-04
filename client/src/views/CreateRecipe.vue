<template>
    <div>
        <form class="">
            <div class="mb-3">
                <label for="recipeName" class="form-label">Názov receptu</label>
                <input v-model="name" type="email" class="form-control" id="recipeName" placeholder="Najlepší receptík na svete ...">
            </div>
            <button @click="create" type="submit" class="btn btn-primary">Vytvor</button>
        </form>
    </div>
</template>

<script setup lang="ts">
import { createRecipe } from '@/services/recipeService';
import { isGetAccessor } from 'typescript';
import { useRouter } from 'vue-router';


let name = "";
let router = useRouter();
async function create() {
    if (valid()) {
        let recipe = await createRecipe(name);
        router.push("/recipe/" + recipe.id + "/edit")
    }
}

function valid() {
    return name != "";
}


</script>

<style scoped>

</style>