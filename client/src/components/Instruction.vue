<template>
    <div class="d-flex align-top main-container py-4">
        <p class="border px-3 me-3 py-2 my-auto rounded-3 align-self-center order text-center">{{ instruction.orderNum }}</p>
        <img class="border" v-if="instruction.imgUrl" :src="instruction.imgUrl" />
        <p class="mb-0 w-auto p-4 border ">{{ instruction.description }}</p>
    </div>
</template>

<script setup>
let {instruction} = defineProps(['instruction']);
</script>

<style lang="scss" scoped>
img
{
    width: 20rem;
    height: 16rem;
}
.order {
    width: 3rem;;
}
.main-container{
    position: relative;
}
.main-container:not(:last-child):after
{
    content: '';
  position: absolute;
  width: 2px;
  background-color: rgba(186, 186, 186, 0.543);
  top: 60%;
  bottom: 0;
  left: 1.5rem;
  margin-left: -3px;
}
.main-container:not(:first-child)::before
{
    content: '';
  position: absolute;
  width: 2px;
  background-color: rgba(186, 186, 186, 0.543);
  top: 0;
  bottom: 60%;
  left: 1.5rem;
  margin-left: -3px;
}
</style>