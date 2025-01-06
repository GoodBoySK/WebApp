<template>
    <div>
        <form class="">
            <ErrorBanner :error="errors"></ErrorBanner>
            <div class="mb-3">
                <label for="recipeName" class="form-label">Názov receptu</label>
                <input v-model="name" type="email" class="form-control" id="recipeName" placeholder="Najlepší receptík na svete ...">
            </div>
            <button @click="create" type="submit" class="btn btn-primary">Vytvor</button>
        </form>
    </div>
</template>

<script setup lang="ts">
import ErrorBanner from '@/components/ErrorBanner.vue';
import { isApiError, type ApiError } from '@/services/apiService';
import { createRecipe } from '@/services/recipeService';
import { isGetAccessor } from 'typescript';
import { ref } from 'vue';
import { useRouter } from 'vue-router';


let name = "";
let router = useRouter();

let errors = ref<ApiError | null>(null)

async function create() {
    if (valid()) {
        let recipe = await createRecipe(name);
        
        if(recipe[0]) {
            router.push("/recipe/" + recipe[0].id + "/edit")
        }
        else if (recipe[1] && isApiError(recipe[1])) {
            errors.value = recipe[1];
        }

    }
}

function valid() {
    return name != "";
}


</script>

<style scoped>

</style>