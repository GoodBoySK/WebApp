<template>
    <nav class="navbar navbar-expand-lg border-bottom">
        <div class="container-fluid" >

            <a href="#" class="navbar-brand">
                <img src="../assets/logo.png" alt="logo" class="d-inline-block align-text-top" width="40">
            </a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">  
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse " id="navbarSupportedContent">
                <ul class="navbar-nav me-auto mb-2 mb-lg-0">
                    
                    <NavItem 
                        v-for="(page,index) in pages"
                        :key="index"
                        :page = "page"
                    />
                </ul>
                <form class="d-flex" role="search">
                    <input class="form-control me-2" type="search" placeholder="Search" aria-label="Search">
                    <button class="btn btn-outline-success" type="submit">Search</button>
                </form>
            </div>
        </div>

    </nav>
    
    
</template>

<script setup>
    import NavItem from './NavItem.vue';

    let pages = [
    {
        title: 'Domov',
        link: '/'
    },
    {
        title: 'Objav',
        link: '/explore'
    }
    ]
</script>

<style>

</style>