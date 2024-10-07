<template>
	<nav class="navbar navbar-expand-lg">
		<div class="container-fluid">
			<a href="#" class="navbar-brand">
				<img
					src="../assets/logo.png"
					alt="logo"
					class="d-inline-block align-text-top"
					width="40"
				/>
			</a>
			<button
				class="navbar-toggler"
				type="button"
				data-bs-toggle="collapse"
				data-bs-target="#navbarSupportedContent"
				aria-controls="navbarSupportedContent"
				aria-expanded="false"
				aria-label="Toggle navigation"
			>
				<span class="navbar-toggler-icon"></span>
			</button>
			<div class="collapse navbar-collapse " id="navbarSupportedContent">
				<ul
					class="navbar-nav mb-2 mb-lg-0 mx-auto d-flex justify-content-evenly flex-grow-1 navlist"
				>
					<NavItem
						v-for="(page, index) in pages"
						:key="index"
						:page="page"
					/>
				</ul>
			</div>
            <form class="input-group w-auto p-2" role="search">
                <input
                    class="form-control py-3 ps-3"
                    type="search"
                    placeholder="Search"
                    aria-label="Search"
                />
                <button class="btn btn-primary px-4" type="submit">
                    <i class="bi bi-search"></i>
                </button>
            </form>
            <button class="btn btn-primary ms-1 py-3 px-4">
                <i class="bi bi-person-fill d-inline m-1"></i>
                <p class="d-inline m-1">Prihásiť sa</p>
            </button>
		</div>
	</nav>
</template>

<script setup>
import NavItem from "./NavItem.vue";

let pages = [
	{
		title: "Domov",
		link: "/",
	},
	{
		title: "Objav",
		link: "/explore",
	},
	{
		title: "Tipy a triky",
		link: "/tips",
	},
	{
		title: "Blog",
		link: "/blog",
	},
];
</script>

<style lang="scss" scoped>
@import "/node_modules/bootstrap/scss/functions";
@import "/node_modules/bootstrap/scss/variables";
@import "/node_modules/bootstrap/scss/mixins";

nav {
	.navlist {
		max-width: 60rem;
	}
	> div {
		padding: 0rem 6rem;
		@include media-breakpoint-down(xl) {
			padding: 0rem 6rem;
		}
		@include media-breakpoint-down(md) {
			padding: 0rem 2rem;
		}
	}
}
</style>