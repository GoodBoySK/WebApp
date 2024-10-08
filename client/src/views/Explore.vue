<template>
	<div class="container text-center">
		<h1 class="display-1 my-5">Objavuj nové recepty</h1>
		<form>
			<div class="d-flex rounded-5 search">
				<input
					class="form-control py-4 fs-4 px-5 rounded-5"
					type="text"
					placeholder="Názov receptu..."
				/>
				<button class="btn px-4 rounded-end-5">
					<i class="bi bi-search p-2"></i>
				</button>
			</div>
		</form>
	</div>
</template>

<script setup>
</script>

<style lang="scss" scoped>
.search {
    position: relative;
	button {
		position: absolute;
		right: 0;
        height: 100%;
	}
}
</style>