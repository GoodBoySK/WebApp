<template>
    <div>
        <button v-if="!isLoggedLocal" class="btn btn-primary ms-1 py-3 px-4" @click="login">
            <i class="bi bi-person-fill d-inline m-1"></i>
            <p class="d-inline m-1">Prihásiť sa</p>
        </button>
        <button v-if="isLoggedLocal" class="btn btn-outline-primary">
            <p class="d-inline m-1">{{ user.userName }}</p>            
        </button>
    </div>
</template>

<script setup>
import { getLoggedUserInfo, isLogged } from "@/services/authenticationService";
import { onMounted, ref } from "vue";
import { useRouter } from "vue-router";

const router = useRouter();

let user = ref({});
const isLoggedLocal = ref(true);

function login(){
	router.push('/login')
}

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