<template>
	<div class="mx-4">
		<div class="form-check d-flex p-0">
			<input v-if="!editable" class="form-check-input" type="checkbox" value=""/>
			<button v-else type="button" class="btn btn-link d-inline">
				<i class="d-inline bi bi-x-octagon-fill " @click="remove"></i>
			</button>
			<label v-if="!editable"  class="form-check-label" for="flexCheckDefault">
				{{ ingredient?.name }}
			</label>
			<input v-else type="text" class="form-control d-inline" placeholder="Name..." v-model="ingredient!.name">
		</div>
	</div>
</template>

<script setup lang="ts">
import type { Ingredient } from '@/services/recipeService';


let ingredient = defineModel<Ingredient>();
const {editable} = defineProps({editable:Boolean});

const emit = defineEmits(['removeIngredient']);

function remove() {
    emit('removeIngredient', ingredient.value);
}

</script>

<style lang="scss" scoped>
</style>