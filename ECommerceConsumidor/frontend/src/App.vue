<template>
  <div id="app">
    <nav class="navbar is-primary" role="navigation" aria-label="main navigation">
      <div class="navbar-brand">
        <router-link class="navbar-item" to="/"> <strong><i class="mdi mdi-home"></i> Home</strong> </router-link>
        <router-link class="navbar-item" to="/pedidos"> <strong><i class="mdi mdi-file-document"></i> Pedidos</strong> </router-link>
      </div>
    </nav>
    <router-view />
  </div>
</template>

<style>
  .container {
    padding: 0 !important;
  }
</style>

<script>
  export default {
    name: 'App'
  };
</script>