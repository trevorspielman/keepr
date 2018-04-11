<template>
  <div class="Keep col-sm-3">
    <h3>{{keep.name}}</h3>
    <img :src="keep.picture" alt="">
    <p>{{keep.description}}</p>
    <p>Saves: {{keep.saves}}</p>
    <p>Views: {{keep.views}}</p>
    <div class="row">
      <div class="col-sm-12">
        <div class="dropdown">
          <button class="btn btn-info dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown">
            Add to Vault
          </button>
          <div class="dropdown-menu">
            <a class="dropdown-item" href="#" v-for="vault in vaults" @click="addToVault({user: user, keep: keep, vault: vault})">{{vault.name}}</a>
          </div>
        </div>
        <router-link :to="{ name: 'SingleKeep', params: { keepId: keep.id } }">
          <button class="btn btn-success" @click="viewKeep(keep)">View</button>
        </router-link>
      </div>
    </div>
  </div>
</template>

<script>
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
      addToVault({ user: user, keep: keep, vault: vault }) {
        this.$store.dispatch('addToVault', { user: user, keep: keep, vault: vault })
      },
      viewKeep(keep){
        keep.views += 1
        this.$store.dispatch('updateKeep', keep)
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
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>