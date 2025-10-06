<template>
  <h1>Hello, {{ authStore.user?.firstName }} {{ authStore.user?.lastName }}!</h1>
</template>

<script setup lang="ts">
import { useAuthStore } from "@/core/stores/auth";

const authStore = useAuthStore();
</script>
