<template>
    <div class="bg d-block p-1">
        <div class="container my-4">
            <error-banner :error="errors"></error-banner>
            <form @submit.prevent="login" class="bg-white p-5 mx-auto my-5 rounded-3 shadow-lg">
                <h1 class="fw-semibold text-center my-4">Prihlás sa</h1>
                <div class=" mb-3 m-2">
                    <label class="h6">Emailová adresa</label>
                    <input type="email" class="form-control py-3" :class="{ 'is-valid': errors && !errors?.errors?.some(x => x.field == 'email'), 'is-invalid': errors && errors.errors && errors?.errors?.some(x => x.field == 'email') }" v-model="form.email" >
                </div>
                <div class="mb-3 m-2">
                    <label class="h6" >Heslo</label>
                    <input type="password" class="form-control py-3" :class="{ 'is-valid':errors && !errors?.errors?.some(x => x.field == 'password'), 'is-invalid': errors && errors.errors && errors?.errors?.some(x => x.field == 'password') }" v-model="form.password">
                </div>
                <router-link  class="d-block" to="/resetpasswordrequest">Zabudol som heslo</router-link>
                <router-link class="d-block" to="/register">Niesi regitrovaný? Zaregistruj sa tu</router-link>
                <button type="submit" class="btn btn-primary m-2 ms-auto px-4 py-2 ">Prihlásiť sa</button>
            </form>
        </div>
    </div>
</template>

<script setup lang="ts">
import { RouterLink, useRouter } from 'vue-router';
import {logIn} from '@/services/authenticationService';
import { ref } from 'vue';
import { isApiError } from '@/services/apiService';
import ErrorBanner from '@/components/ErrorBanner.vue';
import type {ApiError} from '@/services/apiService';

let form = {
    email:"",
    password: "", 
}
const routerMan = useRouter();

let errors = ref<ApiError | null>(null)

async function login(){
    console.log("Logging in....");
    if (validate()) {
        let error = await logIn(form.email, form.password);
        
        if (error && isApiError(error)) {
            errors.value = error;
        }
        else {
            routerMan.push("/");
            errors.value = null;
        }
    }
}
function validate() 
{
    // set also errors
    return true;
}
</script>

<style lang="scss" scoped>
form {
    width: 40rem;
}
div.container {
    min-height: 75vh;
}

div.bg {
    background-image: url(../assets/loginBg.jpg);
	background-size: cover;
	background-position: center;
}
</style>