<template>
    <div class="btn-group">
        <button v-if="!isLoggedLocal" class="btn btn-primary ms-1 py-3 px-4" @click="login">
            <i class="bi bi-person-fill d-inline m-1"></i>
            <p class="d-inline m-1">Prihásiť sa</p>
        </button>
        <button v-if="isLoggedLocal" class="btn btn-outline-primary dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">
            <p class="d-inline m-1">{{ user.userName }}</p>            
        </button>
        <ul v-if="isLoggedLocal" class="dropdown-menu dropdown-menu-end">
            <li><router-link class="dropdown-item" to="/myrecipes">Moje recepty</router-link></li>
            <li><router-link class="dropdown-item" to="/usersettings">Nastavenia</router-link></li>
            <li><a class="dropdown-item" @click.prevent="logout" href="#">Odhlásiť sa</a></li>
        </ul>

    </div>
</template>

<script setup>
import { isApiError } from "@/services/apiService";
import { MountOnLogOut, getLoggedUserInfo, isLogged, logOut } from "@/services/authenticationService";
import { onMounted, ref } from "vue";
import { useRouter } from "vue-router";
import { RouterLink } from "vue-router";

const router = useRouter();

let user = ref({});
const isLoggedLocal = ref(true);

function login(){
	router.push('/login')
}

async function logout() {
    var errors = await logOut();
    if (errors && isApiError(errors)) {
        console.error(errors.message);
    }
}

MountOnLogOut(() => {
    router.replace("/");
    isLoggedLocal.value = false;
})

onMounted(async () => {

    try {
        isLoggedLocal.value = await isLogged()

        if (isLoggedLocal.value) {
            const response = await getLoggedUserInfo();
            user.value = response;
        }
    } catch (error) {
    console.error("Error loading data:", error)
    }
});
</script>

<style lang="scss" scoped>

</style>