<template>
    <div>
        <div v-if="loadingState == LoadingTypes.Loading" class="display-1">Loading ...</div>
        <div v-if="loadingState == LoadingTypes.Error" class="display-1">Error has occured during loading</div>
        <div v-else class="container">
            <error-banner :error="errors"></error-banner>
            <h1 class="display-1">Nastavenia účtu</h1>
            <form @submit.prevent="save">
                <div class=" mb-3 m-2">
                    <label class="h6">Meno</label>
                    <input type="text" class="form-control py-3" :class="{ 'is-invalid': errors && errors.errors && errors?.errors?.some(x => x.field == 'username') }" v-model="form.name" >
                </div>
                <div class=" mb-3 m-2">
                    <label class="h6">Email</label>
                    <input type="email" class="form-control py-3 disabled" v-model="form.email" disabled>
                </div>
                <button type="submit" class="btn btn-primary m-2 ms-auto px-4 py-2 ">Uložiť zmeny</button>
                <button @click.prevent="deleteUser" class="btn btn-outline-primary m-2 px-4 py-2">Vymazať účet</button>
            </form>
        </div>
    </div>
</template>

<script setup lang="ts">
import { LoadingTypes } from '@/LoadingTypes';
import { isApiError, type ApiError } from '@/services/apiService';
import { getLoggedUserInfo, logOut, unregister, updateUserInfo } from '@/services/authenticationService';
import { onMounted, reactive, ref } from 'vue';
import ErrorBanner from '@/components/ErrorBanner.vue';
import { useRouter } from 'vue-router';

let form = reactive({
    name:"",
    email:"",
});

let loadingState = ref(LoadingTypes.Loading);

let errors = ref<ApiError | null>(null)

let router = useRouter();
onMounted(async ()=>{
    let userinfo = await getLoggedUserInfo();
    if (userinfo) {
        form.email = userinfo.email;
        form.name = userinfo.userName;

        loadingState.value = LoadingTypes.Done;
    }
    else {
        loadingState.value = LoadingTypes.Error;
    }
    
});

async function save() {
    var result = await updateUserInfo({newName: form.name});

    if (result && isApiError(result)) {
        errors.value = result;
    }
    else {
        window.location.reload();
    }
}

async function deleteUser() {
    if (confirm("Ste si istý že chcete odstrániť svoj účet?")) {
        var result = await unregister();
        
        if (result && isApiError(result)) {
            errors.value = result;
        }
        else {
            await logOut()
        }
    }
}

</script>

<style scoped>

</style>