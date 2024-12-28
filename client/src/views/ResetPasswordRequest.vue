<template>
    
    <div class="bg d-block p-1">
        <div class="container my-4">
            <error-banner :error="errors"></error-banner>
            <form @submit.prevent="resetPassword" class="bg-white p-5 mx-auto my-5 rounded-3 shadow-lg">
                <h1 class="fw-semibold text-center my-4">Resetuj si heslo</h1>
                <div class=" mb-3 m-2">
                    <label class="h6">Emailová adresa</label>
                    <input type="email" class="form-control py-3" v-model="form.email" >
                </div>
                <button type="submit" class="btn btn-primary m-2 ms-auto px-4 py-2 ">Resetovat</button>
            </form>
        </div>
    </div>
</template>

<script setup lang="ts">
import { RouterLink, useRouter } from 'vue-router';
import {logIn, ResetPasswordRequest} from '@/services/authenticationService';
import { ref } from 'vue';
import { isApiError } from '@/services/apiService';
import ErrorBanner from '@/components/ErrorBanner.vue';
import type {ApiError} from '@/services/apiService';
import ResetPassword from './ResetPassword.vue';

let form = {
    email:"",
}
const routerMan = useRouter();

let errors = ref<ApiError | null>(null)

async function resetPassword(){
    console.log("Logging in....");
    if (validate()) {
        let error = await ResetPasswordRequest(form.email);
        
        if (error && isApiError(error)) {
            errors.value = error;
        }
        else {
            routerMan.push("/resetpasswordsuccesfull");
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