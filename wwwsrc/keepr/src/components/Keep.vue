<template>
  <div class="Keep col-sm-3">
    <div class="keepContent">
      <h2>{{keep.name}}</h2>
      <img :src="keep.picture" alt="">
      <h4>{{keep.description}}</h4>
      <div class="col-sm-12 icons">
        <i class="fas fa-bookmark fa-2x">   {{keep.saves}}</i>
        <i class="fas fa-eye fa-2x">   {{keep.views}}</i>
      </div>
      <div class="col-sm-12 hoverContent">
        <div class="dropdown">
          <button class="btn btn-info dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown">
            Add to Vault
          </button>
          <div class="dropdown-menu">
            <a class="dropdown-item" href="#" v-for="vault in vaults" @click="addToVault({user: user, keep: keep, vault: vault})">{{vault.name}}</a>
          </div>
        </div>
        <router-link :to="{ name: 'SingleKeep', params: { keepId: keep.id } }">
          <button class="btn btn-success ml-2" @click="viewKeep(keep)">View</button>
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
      viewKeep(keep) {
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
  h2 {
    display: flex;
    justify-self: center;
    margin-bottom: 2rem;
  }
  img{
    max-width: 100%;
  }
  .Keep {
    padding: 1rem 1rem;

  }
  .icons{
    display: flex;
    justify-content: space-around;
    margin: 1rem 0rem;
    padding-bottom: 2rem;
  }

  .hoverContent {
    display: none;
    position: fixed;
    top: 25px;
    right: 10px;
  }
  .keepContent{
    /* margin: 1rem; */
    background-color: aliceblue;
  }

  .Keep:hover .keepContent {
    background-color: rgba(0, 0, 0, .5);
  }

  .Keep:hover .hoverContent {
    display: inline-flex;
    position: absolute;
    align-content: flex-end;
    justify-content: flex-end;
    /* flex-direction: column; */
  }
</style>