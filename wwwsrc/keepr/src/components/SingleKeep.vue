<template>
  <div class="SingleKeep">
    <navbar></navbar>
    <div class=" container-fluid">
      <div class="row">
        <div class="col-sm-12">
          <div class="header">
            <div class="div">
              <h3>{{keep.name}}</h3>
              <router-link :to="{ name: 'Profile', params: { profileId: this.keep.userId } }">
                  <p>{{userProfile.username}}</p>
                </router-link>
              <i class="fas fa-bookmark fa-2x"> {{keep.saves}}</i>
              <i class="fas fa-eye fa-2x"> {{keep.views}}</i>
            </div>
            <div class="buttons">
              <div class="dropdown" v-if="user.id">
                <button class="btn btn-info dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown">
                  Add to Vault
                </button>
                <div class="dropdown-menu">
                  <a class="dropdown-item" href="#" v-for="vault in vaults" @click="addToVault({user: user, keep: keep, vault: vault})">{{vault.name}}</a>
                </div>
              </div>
              <button class="btn btn-outline-danger" data-toggle="modal" data-target="#editKeep" v-if="keep.userId == user.id">Edit</button>
              <i class="fas fa-trash fa-2x" @click="removeKeep(keep)" v-if="keep.public == 1"></i>
            </div>
          </div>
          <p>{{keep.description}}</p>
        </div>
      </div>
      <div class="row">
        <div class="col-sm-12 image">
          <img :src="keep.picture" alt="">
        </div>
      </div>

      <!-- MODAL CONTENT -->
      <div class="modal fade" id="editKeep" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
          <div class="modal-content">
            <div class="modal-header">
              <h5 class="modal-title" id="exampleModalLabel">Edit Keep</h5>
              <button type="button" class="close" data-dismiss="modal">
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
            <div class="modal-body">
              <form action="submit" @submit.prevent="editKeep(keep)">
                <div class="form-row">
                  <div class="form-group col-sm-6">
                    <input type="text" v-model="keep.name" placeholder="Name">
                    <input type="text" v-model="keep.picture" placeholder="Image Url">
                  </div>
                  <div class="form-group col-sm-6">
                    <textarea v-model="keep.description" placeholder="Description" rows="4"></textarea>
                  </div>
                </div>
                <div class="form-row">
                  <div class="form-group col-sm-4">
                    <div class="form-check">
                      <input class="form-check-input" type="checkbox" id="private" v-model="keep.public">
                      <label class="form-check-label" for="private">
                        Private?
                      </label>
                    </div>
                  </div>
                  <div class="form-group col-sm-8">
                    <select class="custom-select" v-model="keep.vaultId">
                      <option v-for="vault in vaults" :value="vault.id">{{vault.name}}</option>
                    </select>
                  </div>
                </div>
                <button type="submit" class="btn btn-primary">Edit Keep</button>
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
      },
      editKeep(keep) {
        if (!this.keep.public) {
          this.keep.public = 0
        }
        else {
          this.keep.public = 1
        }
        this.$store.dispatch('updateKeep', keep)
      },
      removeKeep(keep) {
        this.$store.dispatch('removeKeep', keep)
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
      },
      userProfile(){
        return this.$store.state.userProfile
      }
    },
    components: {
      navbar,
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  img {
    display: flex;
    max-width: 90vw;
    align-self: center;
    margin: 1rem 2rem 2rem 2rem;
  }

  .image {
    display: flex;
    align-content: center;
    justify-content: center;
  }

  .header {
    display: flex;
    justify-content: space-between;
  }
</style>