<template>
  <div class="Vault container-fluid">
    <div class="row">
      <navbar></navbar>
      <div class="col-sm-12">
        <h3>{{vault.name}}</h3>
        <p>{{vault.description}}</p>
      </div>
    </div>
  </div>
</template>

<script>
  import navbar from "./Navbar"
  export default {
    name: 'Vault',
    mounted() {
      this.$store.dispatch("getVault", this.$route.params.vaultId)
    },
    data() {
      return {

      }
    },
    methods: {

    },
    computed: {
      vault() {
        return this.$store.state.vault
      }
    },
    components: {
      navbar,
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>