<template>
    <div>
        <button v-if="!authState.isLoggedIn" class="btn btn-primary ms-1 py-3 px-4" @click="login">
            <i class="bi bi-person-fill d-inline m-1"></i>
            <p class="d-inline m-1">Prihásiť sa</p>
        </button>
        <button v-if="authState.isLoggedIn" class="btn btn-outline-primary">
            <p class="d-inline m-1">{{ user.userName }}</p>            
        </button>
    </div>
</template>

<script setup>
import { authState, getLoggedUserInfo } from "@/services/authenticationService";
import { onMounted, ref, toRaw } from "vue";
import { useRouter } from "vue-router";

let user = ref({});
const router = useRouter();

const gg = ref(true);

function login(){
	router.push('/login')
}

onMounted(async () => {

    try {
    if (authState.isLoggedIn) {
        const response = await getLoggedUserInfo();
        user.value = response;
    }
  } catch (error) {
    console.error("Error loading data:", error)
  }

    console.log(toRaw(user.value));
    console.log( authState.isLoggedIn );
    console.log(user != undefined );
    console.log(  user.value != null);
    gg.value = !gg.value;
});
</script>

<style lang="scss" scoped>

</style>