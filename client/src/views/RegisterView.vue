<template>
    <div>
        <div class="bg d-block p-1">
        <div class="container my-4">
            <form @submit.prevent="registerUser" class="bg-white p-5 mx-auto my-5 rounded-3 shadow-lg">
                <h1 class="fw-semibold text-center my-4">Registruj sa</h1>
                <div class=" mb-3 m-2">
                    <label class="h6">Emailová adresa</label>
                    <input type="email" class="form-control py-3" :class="{ 'is-valid': wrongCredentials.value, 'is-invalid': !wrongCredentials.value && form.email }" v-model="form.email" >
                </div>
                <div class="mb-3 m-2">
                    <label class="h6" >Meno</label>
                    <input type="text" class="form-control py-3" :class="{ 'is-valid': wrongCredentials.value, 'is-invalid': !wrongCredentials.value && form.name }" v-model="form.name">
                </div>
                <div class="mb-3 m-2">
                    <label class="h6" >Heslo</label>
                    <input type="password" class="form-control py-3" :class="{ 'is-valid': wrongCredentials.value, 'is-invalid': !wrongCredentials.value && form.password }" v-model="form.password">
                </div>
                <router-link class="d-block" to="/login">Už si regitrovaný? Prihlás sa tu</router-link>
                <button type="submit" class="btn btn-primary m-2 ms-auto px-4 py-2 ">Registrovať sa</button>
            </form>
        </div>
    </div>
    </div>
</template>

<script setup>
import { RouterLink, useRouter } from 'vue-router';
import { ref } from 'vue';
import { register } from '@/services/authenticationService';

let form = {
    email:"",
    name: "",
    password: "", 
}
const routerMan = useRouter();

let wrongCredentials = ref(false);

async function registerUser(){
    if (validate()) {
        let registerSuccefully = register(form.email, form.name, form.password);
        
        if (registerSuccefully) {
            routerMan.push("/login");
            wrongCredentials.value = false;
        }
        else {
            wrongCredentials.value = true;
        }
    }
    else {
        wrongCredentials.value = true;
    }
}
function validate() 
{
    if (!form.email && !form.meno && !form.password) return false;

    //copied from gpt
    var re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(form.email);
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