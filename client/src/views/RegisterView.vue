<template>
    <div>
        <div class="bg d-block p-1">
            <error-banner :error="errors ?? null"></error-banner>
            <div class="container my-4">
                <form @submit.prevent="registerUser" class="bg-white p-5 mx-auto my-5 rounded-3 shadow-lg">
                    <h1 class="fw-semibold text-center my-4">Registruj sa</h1>
                    <div class=" mb-3 m-2">
                        <label class="h6">Emailová adresa</label>
                        <input type="email" class="form-control py-3" :class="{ 'is-valid': errors && !errors?.errors?.some(x => x.field == 'email'), 'is-invalid': errors && errors.errors && errors?.errors?.some(x => x.field == 'email') }" v-model="form.email" >
                    </div>
                    <div class="mb-3 m-2">
                        <label class="h6" >Meno</label>
                        <input type="text" class="form-control py-3" :class="{ 'is-valid': errors && !errors?.errors?.some(x => x.field == 'usernname'), 'is-invalid': errors && errors.errors && errors?.errors?.some(x => x.field == 'username') }" v-model="form.username">
                    </div>
                    <div class="mb-3 m-2">
                        <label class="h6" >Heslo</label>
                        <input type="password" class="form-control py-3" :class="{ 'is-valid':errors && !errors?.errors?.some(x => x.field == 'password'), 'is-invalid': errors && errors.errors && errors?.errors?.some(x => x.field == 'password') }" v-model="form.password">
                    </div>
                    <router-link class="d-block" to="/login">Už si regitrovaný? Prihlás sa tu</router-link>
                    <button type="submit" class="btn btn-primary m-2 ms-auto px-4 py-2 ">Registrovať sa</button>
                </form>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { RouterLink, useRouter } from 'vue-router';
import { ref } from 'vue';
import { register } from '@/services/authenticationService';
import { isApiError, type ApiError } from '@/services/apiService';
import ErrorBanner from '@/components/ErrorBanner.vue';
let form = {
    email:"",
    username: "",
    password: "", 
}
const routerMan = useRouter();

let errors = ref<ApiError | null>();

async function registerUser(){
    if (validate()) {
        let errorFromApi = await register(form.email, form.username, form.password);
        console.log(errorFromApi)
        if (errorFromApi && isApiError(errorFromApi)) {
            errors.value = errorFromApi;
        }
        else {
            errors.value = null;
            routerMan.push("/login");
        }
    }
}
function validate() 
{
    let tempErrors: ApiError = {message: 'Validation errors ha occured!!!', type: 'validation', errors: []};
        
    if (!form.email || form.email === '') tempErrors.errors?.push({field: 'email', message:'Field must have at least 1 character'})
    if (!form.username || form.username === '') tempErrors.errors?.push({field: 'username', message:'Field must have at least 1 character'})
    if (!form.password || form.password === '') tempErrors.errors?.push({field: 'password', message:'Field must have at least 1 character'})

    //copied from gpt
    var re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    
    if (form.email && !re.test(form.email)) {
        tempErrors.errors?.push({field:'email', message: 'Text in field is not a email'})
    }
    if (tempErrors.errors?.length != 0) {
        errors.value = tempErrors;
        return false;
    }
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