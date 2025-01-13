<template>
    
    <div class="bg d-block p-1">
        <div class="container my-4">
            <error-banner :error="errors"></error-banner>
            <form @submit.prevent="reset" class="bg-white p-4 p-lg-5 mx-auto my-5 rounded-3 shadow-lg">
                <h1 class="fw-semibold text-center my-4">Prihlás sa</h1>
                <div class="mb-3 m-2">
                    <label class="h6" >Staré heslo</label>
                    <input type="password" class="form-control py-3" :class="{ 'is-valid':errors && !errors?.errors?.some(x => x.field == 'oldpassword'), 'is-invalid': errors && errors.errors && errors?.errors?.some(x => x.field == 'oldpassword') }" v-model="form.oldpassword">
                </div>
                <div class="mb-3 m-2">
                    <label class="h6" >Nové heslo</label>
                    <input type="password" class="form-control py-3" :class="{ 'is-valid':errors && !errors?.errors?.some(x => x.field == 'newpassword'), 'is-invalid': errors && errors.errors && errors?.errors?.some(x => x.field == 'newpassword') }" v-model="form.newpassword">
                </div>
                <button type="submit" class="btn btn-primary m-2 ms-auto px-4 py-2 ">Resetuj heslo</button>
            </form>
        </div>
    </div>
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router';
import { resetPassword} from '@/services/authenticationService';
import { ref } from 'vue';
import { isApiError } from '@/services/apiService';
import ErrorBanner from '@/components/ErrorBanner.vue';
import type {ApiError} from '@/services/apiService';

let form = {
    oldpassword:"",
    newpassword: "", 
}
const routerMan = useRouter();

let errors = ref<ApiError | null>(null)

let {token, email} = defineProps<{token:string, email:string}>();

async function reset(){
    if (validate()) {
        let error = await resetPassword(form.oldpassword, form.newpassword, token, email);
        
        if (error && isApiError(error)) {
            errors.value = error;
        }
        else {
            routerMan.push("/login");
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
@import "../assets/main.scss";

form {
    width: 40rem;
    @include media-breakpoint-down(lg) {
        width: 35rem;
    }
    @include media-breakpoint-down(md) {
        width: 25rem;
    }
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