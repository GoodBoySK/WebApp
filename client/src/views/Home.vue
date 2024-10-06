<template>
	<div
		class="container bg-body justify-content-center mt-5 rounded-2 shadow align-content-center"
		id="search"
	>
		<div
			class="px-2 pt-4 pb-4 justify-content-center text-center align-self-center mx-auto mb-5"
		>
			<h1 class="mb-4 mt-2 text-white display-2">Vyhľadaj si recept</h1>
			<form action="">
				<div class="input-group mb-3 shadow input-group-lg">
					<input
						placeholder="Hľadať recept"
						class="form-control px-3 py-3"
						type="text"
						name="name"
						id=""
					/>
					<select class="form-select">
						<option value="" disabled selected>Kategórie</option>
						<option value="">1</option>
					</select>
					<button class="btn btn-primary">
						<i class="bi bi-search p-2"></i>
					</button>
				</div>
			</form>
		</div>
	</div>
</template>

<script setup>
</script>

<style lang="scss" scoped>
@import "/node_modules/bootstrap/scss/functions";
@import "/node_modules/bootstrap/scss/variables";
@import "/node_modules/bootstrap/scss/mixins"; // This is important for mixins like media-breakpoint-up
#search {
	background-image: url(../assets/banner.jpg);
	background-size: cover;
	background-position: center;
	max-width: 65%;
	@include media-breakpoint-down(lg) {
		max-width: 80%;
	}
	@include media-breakpoint-down(md) {
		max-width: 90%;
	}
	@include media-breakpoint-down(sm) {
		max-width: 100%;
	}
	min-height: 30rem;
	> div {
		max-width: 70%;
		> * {
		}
	}
}
</style>