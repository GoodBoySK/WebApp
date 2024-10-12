<template>
	<div class="m-auto">
		<i
			v-for="index in 5"
			:key="index"
			class="start"
			:class="(hoveredStar == -1 ? index < selectedRating : index < hoveredStar)? 'bi bi-star-fill' : 'bi bi-star'"
			@click="selectStart(index + 1)"
            @mouseover="hoverStar(index + 1)"
            @mouseleave="reset()"
		></i>
	</div>
</template>

<script setup>
import { ref } from "vue";

let selectedRating = ref(0);
let hoveredStar = ref(-1);

const emit = defineEmits(["rating-changed"]);

function selectStart(rating) {
	selectedRating.value = rating;
	emit("rating-changed", rating);
}

function hoverStar(star) {
    hoveredStar.value = star;
}
function reset() {
    hoveredStar.value = -1;
}
</script>

<style lang="scss" scoped>
@import "../assets/main.scss";

.start {
	color: $warning;
}
</style>