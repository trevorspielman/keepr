<template>
  <div class="Profile ">
    <navbar></navbar>
    <div class="container-fluid">
      <div class="row">
        <div class="col-sm-12 header">
          <div>
            <button type="button" class="btn btn-success" @click="setDisplayVault">Vaults</button>
            <button type="button" class="btn btn-danger" @click="setDisplayKeep">Keeps</button>
          </div>
          <h1 v-if="this.$route.params.profileId == user.id">Welcome {{user.username}}</h1>
          <h1 v-else="userProfile.id != user.id">{{userProfile.username}}'s  Profile</h1>
          <div v-if="user.id == userProfile.id">
            <button class="btn btn-info" data-toggle="modal" data-target="#createVault">Create Vault</button>
            <button class="btn btn-danger" data-toggle="modal" data-target="#createKeep">Create Keep</button>
          </div>
        </div>
      </div>
      <div class="row" v-if="this.display.showing == 'vault'">
        <div class="col-sm-4" v-for="vault in profileVaults">
          <router-link :to="{ name: 'Vault', params: { vaultId: vault.id } }">
            <h3>{{vault.name}}</h3>
          </router-link>
          <p>{{vault.description}}</p>
        </div>
      </div>
      <div class="row" v-else>
        <keep :keep="keep" v-for="keep in keeps" v-if="keep.userId == userProfile.id"></keep>
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
                <div class="form-row">
                  <div class="form-group col-sm-6">
                    <input type="text" v-model="newKeep.name" placeholder="Name">
                    <input type="text" v-model="newKeep.picture" placeholder="Image Url">
                  </div>
                  <div class="form-group col-sm-6">
                    <textarea v-model="newKeep.description" placeholder="Description" rows="4"></textarea>
                  </div>
                </div>
                <div class="form-row">
                  <div class="form-group col-sm-4">
                    <div class="form-check">
                      <input class="form-check-input" type="checkbox" id="private" v-model="newKeep.public">
                      <label class="form-check-label" for="private">
                        Private?
                      </label>
                    </div>
                  </div>
                  <div class="form-group col-sm-8">
                    <select class="custom-select" v-model="tempVaultId.vaultId">
                      <option v-for="vault in vaults" :value="vault.id">{{vault.name}}</option>
                    </select>
                  </div>
                </div>
                <button type="submit" class="btn btn-primary">Add Keep</button>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  import navbar from "./Navbar"
  import vault from "./Vault"
  import keep from "./Keep"
  export default {
    name: 'Profile',
    mounted() {
      this.$store.dispatch('authenticate')
      this.$store.dispatch('getProfileUser', this.$route.params.profileId)
      this.$store.dispatch("getProfileUserVaults", this.$route.params.profileId)
      this.$store.dispatch("getKeeps")
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
          public: false
        },
        tempVaultId: {
          vaultId: ""
        },
        display: {
          showing: "vault"
        }
      }
    },
    methods: {
      createVault() {
        this.newVault.UserId = this.$store.state.user.id
        this.$store.dispatch('createVault', this.newVault)
      },
      createKeep() {
        if (!this.newKeep.public) {
          this.newKeep.public = 0
        }
        else {
          this.newKeep.public = 1
        }
        this.newKeep.UserId = this.$store.state.user.id
        this.$store.dispatch('createKeep', { keep: this.newKeep, vault: this.tempVaultId.vaultId })
      },
      setDisplayKeep() {
        this.display.showing = "keep"
      },
      setDisplayVault() {
        this.display.showing = "vault"
      },
      getProfileUser(userId) {
        this.$store.dispatch('getProfileUser', userId)
      },
      getProfileUserVaults(userId){
        this.$store.dispatch("getProfileUserVaults", userId)
      }
    },
    computed: {
      user() {
        return this.$store.state.user
      },
      vaults() {
        return this.$store.state.vaults
      },
      profileVaults() {
        return this.$store.state.profileVaults
      },
      keeps() {
        return this.$store.state.keeps
      },
      userProfile() {
        return this.$store.state.userProfile
      }
    },
    beforeRouteUpdate(to, from, next) {
      this.getProfileUser = this.getProfileUser(to.params.profileId)
      this.getProfileUserVaults = this.getProfileUserVaults(to.params.profileId)
      next()
    },
    components: {
      navbar,
      vault,
      keep
    },
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  .header {
    display: inline-flex;
    justify-content: space-around;
    align-items: center;
    height: 5rem;
  }
</style>