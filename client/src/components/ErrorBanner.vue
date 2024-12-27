<template>
    <div v-if="error" class="error-container">
        <h6 class="error-title">{{ error.type }}: {{ error.message }}</h6>
        <p v-if="error.details" class="error-details"> {{ error.details }}</p>
        <ul v-if="error.errors" class="error-list">
            <li v-for="(errorAtr, index) in error.errors" :key="index" class="error-item">
             {{ errorAtr.field }}:{{ errorAtr.message }}
            </li>
        </ul>
    </div>
  </template>
  
<script setup lang="ts">
    import type { ApiError } from '@/services/apiService';


    let {error} = defineProps<{error:ApiError | null}>();
  

</script>
  
<style scoped lang="scss">
    .error-container {
        padding: 1em;
        border: 1px solid red;
        background-color: #ffe6e6;
        border-radius: 5px;
        color: #cc0000;
    }
    
    .error-title {
        font-weight: bold;
        margin-bottom: 0.5em;
    }
    .error-details {
        margin-bottom: 0.5em;
    } 
  
    .error-list {
        list-style-type: disc;
        margin-left: 1.5em;
        padding: 0;
    }
  
    .error-item {
        margin-bottom: 0.5em;
    }
 </style>