<template>
  <div class="Keep col-sm-3">
    <h3>{{keep.name}}</h3>
    <p>{{keep.description}}</p>
    <div class="dropdown">
      <button class="btn btn-info dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown">
        Add to Vault
      </button>
      <div class="dropdown-menu">
        <a class="dropdown-item" href="#" v-for="vault in vaults" @click="addToVault({user: user, keep: keep, vault: vault})" value="vault.id">{{vault.name}}</a>
      </div>
    </div>
  </div>
</template>

<script>
  import vault from "./Vault"
  export default {
    name: 'Keep',
    props: ['keep'],
    mounted() {
    },
    data() {
      return {

      }
    },
    methods: {
      addToVault({user: user, keep: keep, vault: vault}){
        this.$store.dispatch('addToVault', {user: user, keep: keep, vault: vault})
      }
    },
    computed: {
      vaults() {
        return this.$store.state.vaults
      },
      user() {
        return this.$store.state.user
      },
    },
    components: {
      vault
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>