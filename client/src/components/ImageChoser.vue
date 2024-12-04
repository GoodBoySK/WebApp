<template>
    <div class="col border border-1 border-black border-opacity-50 d-flex px-0 flex-column justify-content-end">
            <img
                class="thumbnail"
                :src="url"
                alt="Nexistuje ziadny obrazok"
            />
            <div class="d-flex">
                <input @change="onFileChange" class="form-control border border-1 border-black border-opacity-50 rounded-0 align-self-end mt-auto" type="file" id="formFile" >
                <button type="button" class="btn btn-primary rounded-0" @click="removePicture"><i class="bi bi-trash-fill"></i></button>
            </div>
        </div>
</template>

<script setup lang="ts">
import getUrlOfImage from '@/services/mediaFileService';
import type { MediaFile } from '@/services/recipeService';
import { onMounted, ref } from 'vue';


let mediaFile = defineModel<MediaFile>();

let url = ref("");

onMounted(async () => {
    if (mediaFile.value ) {
    	url.value = getUrlOfImage(mediaFile.value.id + "");
	}
});

function onFileChange(event: any) {
    const file = event.target.files[0];
    const formData = new FormData();

    if (file) {

        formData.append("file", file);

        mediaFile.value!.image = formData;

        const reader = new FileReader();
        reader.onload = () => {
            if (typeof reader.result === 'string')
                url.value = reader.result ?? "";
        };
        reader.readAsDataURL(file);
    }
    else {
        formData.append("file", "");
        mediaFile.value!.image = formData;
    }
}

function removePicture() {
    const formData = new FormData();
    formData.append("file", "");

    mediaFile.value!.image = formData;
    url.value = "";
    
}

</script>

<style scoped>

.thumbnail {
	object-fit: scale-down;
	object-position: center;
	height: 100%;
}
</style>