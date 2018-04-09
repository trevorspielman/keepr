<template>
  <div class="Profile container-fluid">
    <div class="row">
      <navbar></navbar>
      <div class="col-sm-12">
        <h1>Welcome {{user.username}}</h1>
        <button class="btn btn-info" data-toggle="modal" data-target="#createVault">Create Vault</button>
        <button class="btn btn-danger" data-toggle="modal" data-target="#createKeep">Create Keep</button>
      </div>
    </div>
    <div class="row" v-for="vault in vaults">
      <router-link :to="{ name: 'Vault', params: { vaultId: vault.id } }">
        <h3>{{vault.name}}</h3>
      </router-link>
      <p>{{vault.description}}</p>
    </div>
    <!-- Create Vault Modal -->
    <div class="modal fade" id="createVault" tabindex="-1" role="dialog">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">Create a New Vault</h5>
            <button type="button" class="close" data-dismiss="modal">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <form action="submit" @submit.prevent="createVault">
              <input type="text" v-model="newVault.name" placeholder="Name">
              <textarea v-model="newVault.description" placeholder="Description" rows="4"></textarea>
              <button type="submit" class="btn btn-primary">Add Vault</button>
            </form>
          </div>
        </div>
      </div>
    </div>
    <!-- Create Keep Modal -->
    <div class="modal fade" id="createKeep" tabindex="-1" role="dialog">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">Create a New Keep</h5>
            <button class="close" data-dismiss="modal">
              <span>&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <form action="submit" @submit.prevent="createKeep">
              <input type="text" v-model="newKeep.name" placeholder="Name">
              <textarea v-model="newKeep.description" placeholder="Description" rows="4"></textarea>
              <input type="text" v-model="newKeep.picture" placeholder="Image Url">
              <select class="custom-select" v-model="tempVaultId.vaultId">
                <option v-for="vault in vaults" :value="vault.id" >{{vault.name}}</option>
              </select>
              <button type="submit" class="btn btn-primary">Add Keep</button>
            </form>
          </div>
        </div>
      </div>
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
        newVault: {
          name: "",
          description: ""
        },
        newKeep: {
          name: "",
          description: "",
          picture: "",
        },
        tempVaultId: {
          vaultId: ""
        }
      }
    },
    methods: {
      createVault() {
        this.newVault.UserId = this.$store.state.user.id
        this.$store.dispatch('createVault', this.newVault)
      },
      createKeep() {
        this.newKeep.UserId = this.$store.state.user.id
        this.$store.dispatch('createKeep', {keep: this.newKeep, vault: this.tempVaultId.vaultId})
      }
    },
    computed: {
      user() {
        return this.$store.state.user
      },
      vaults() {
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