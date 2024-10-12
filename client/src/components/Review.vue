<template>
    <div class="position-relative w-100 p-3 border-bottom">
        <p class="text-black-50 mb-0 fst-normal ">{{ reviewObject.createdAt }}</p>
        <p class="fw-medium mb-0">{{ reviewObject.autor.name }}</p>
        <p class="">{{ reviewObject.text }}</p>
        <i v-if="reviewObject.pinned" class="pinned - p-3 bi bi-pin-angle-fill"></i>
        <div class="rating d-flex px-3">
            <p class="px-3">{{ reviewObject.score + '/5.0' }}</p>
            <div class="d-flex text-warning">
                <i v-for="index in starts" :key="index" class="bi bi-star-fill"></i>
                <i v-for="index in halfStarts" :key="index" class="bi bi-star-half"></i>
                <i v-for="index in emptyStarts" :key="index" class="bi bi-star"></i>
            </div>
        </div>
    </div>
</template>

<script setup>
let {reviewObject} = defineProps(['reviewObject']);

let starts = Math.floor(reviewObject.score);
let emptyStarts = 5 - Math.ceil(reviewObject.score);
let halfStarts = 5 - starts - emptyStarts   ;

</script>

<style lang="scss" scoped>
.pinned {
    position: absolute;
    top: 0;
    right: 0;
}
.rating {
    position: absolute;
    bottom: 0;
    right: 0;
}
</style>