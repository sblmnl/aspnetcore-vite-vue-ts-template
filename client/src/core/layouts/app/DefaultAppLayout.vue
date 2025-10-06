<template>
  <slot></slot>
</template>

<script setup lang="ts">
import { useAuthStore } from "@/core/stores/auth";
import { onMounted } from "vue";

const authStore = useAuthStore();

onMounted(async () => {
  await authStore.setupWithAutoRefresh();
});
</script>
