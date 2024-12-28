<template>
    <div>
        <h3>Reset hesla bol úspešný</h3>
        <p>Za pár sekúnd budeš presmerovaný na hlavnú stránku...</p>
    </div>
</template>

<script setup lang="ts">
import { onMounted } from 'vue';
import { useRouter } from 'vue-router';

    const router = useRouter();

    onMounted(() => {
        setTimeout(() => {
            router.push('/login');
        }, 3000);
    });
</script>

<style scoped>

</style>