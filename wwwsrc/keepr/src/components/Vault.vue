<template>
  <div class="Vault">
    <navbar></navbar>
    <div class="container-fluid">
      <div class="row">
        <div class="col-sm-12">
          <h3>{{vault.name}}</h3>
          <p>{{vault.description}}</p>
        </div>
      </div>
      <div class="row">
        <keep :keep="keep" v-for="keep in vaultKeeps"></keep>
      </div>
    </div>
  </div>
</template>

<script>
  import navbar from "./Navbar"
  import keep from "./Keep"
  export default {
    name: 'Vault',
    mounted() {
      this.$store.dispatch("getVault", this.$route.params.vaultId)
      this.$store.dispatch("getVaults", this.$store.state.user.id)
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
      },
      vaultKeeps() {
        return this.$store.state.vaultKeeps
      },
      vaults() {
        return this.$store.state.vaults
      },
    },
    components: {
      navbar,
      keep,
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>