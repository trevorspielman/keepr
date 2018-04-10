<template>
  <div class="SingleKeep container-fluid">
    <div class="row">
      <navbar></navbar>
    </div>
    <div class="row">
      <div class="col-sm-12">
        <h3>{{keep.name}}</h3>
        <p>{{keep.description}}</p>
        <p>Saves: {{keep.saves}}</p>
        <p>Views: {{keep.views}}</p>
        <div class="dropdown">
          <button class="btn btn-info dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown">
            Add to Vault
          </button>
          <div class="dropdown-menu">
            <a class="dropdown-item" href="#" v-for="vault in vaults" @click="addToVault({user: user, keep: keep, vault: vault})">{{vault.name}}</a>
          </div>
        </div>
        <button class="btn btn-outline-danger" v-if="keep.userId == user.id">Edit</button>
      </div>
    </div>
  </div>
</template>

<script>
  import navbar from "./Navbar"
  export default {
    name: 'SingleKeep',
    // props: ['keep'],
    mounted() {
      this.$store.dispatch('getKeep', this.$route.params.keepId)
    },
    data() {
      return {

      }
    },
    methods: {
      addToVault({ user: user, keep: keep, vault: vault }) {
        this.$store.dispatch('addToVault', { user: user, keep: keep, vault: vault })
      }
    },
    computed: {
      vaults() {
        return this.$store.state.vaults
      },
      user() {
        return this.$store.state.user
      },
      keep() {
        return this.$store.state.keep
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