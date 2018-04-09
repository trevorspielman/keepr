<template>
  <div class="home container-fluid">
    <div class="row">
      <navbar></navbar>
    </div>
    <div class="row" v-for="keep in keeps">
      <keep :keep="keep"></keep>
    </div>
  </div>
</template>

<script>
  import navbar from "./Navbar"
  import keep from "./Keep"
  export default {
    name: 'Home',
    mounted() {
      this.$store.dispatch("getKeeps")
    },
    data() {
      return {
      }
    },
    methods: {

    },
    computed: {
      user(){
        return this.$store.state.user
      },
      keeps(){
        return this.$store.state.keeps
      },
    },
    components: {
      navbar,
      keep
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>