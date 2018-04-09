<template>
  <div class="Profile container-fluid">
    <div class="row">
      <navbar></navbar>
      <div class="col-sm-12">
        <h1>Welcome {{user.username}}</h1>
      </div>
    </div>
    <div class="row" v-for="vault in vaults">
      <vault :vault="vault"></vault>
    </div>
  </div>
</template>

<script>
  import navbar from "./Navbar"
  import vault from "./Vault"
  export default {
    name: 'Profile',
    mounted() {
      this.$store.dispatch('authenticate')
      this.$store.dispatch("getMyVaults", this.$route.params.profileId)
    },
    data() {
      return {
      }
    },
    methods: {
    },
    computed: {
      user() {
        return this.$store.state.user
      },
      vaults(){
        return this.$store.state.vaults
      },
    },
    beforeRouteUpdate(to, from, next) {
            this.getMyVaults = this.getMyVaults(to.params.profileId)
            next()
        },
    components: {
      navbar,
      vault
    },
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>