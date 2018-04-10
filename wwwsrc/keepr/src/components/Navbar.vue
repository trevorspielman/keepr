<template>
  <div class="Navbar col-sm-12">
    <router-link :to="{name: 'Home'}">
      <button class="btn btn-success">Home</button>
    </router-link>
    <router-link :to="{ name: 'Profile', params: { profileId: user.id } }">
      <button class="btn btn-primary">My Profile</button>
    </router-link>
    <div v-if="!user.id">
      <h1>Please Register:</h1>
      <div>
        <form action="submit" @submit.prevent="register">
          <input type="text" v-model="newUser.name" placeholder="Name">
          <input type="text" v-model="newUser.email" placeholder="Email" required>
          <input type="password" v-model="newUser.password" placeholder="Password" required>
          <button class="btn btn-primary" type="submit">Register</button>
        </form>
      </div>
      <h1>Please Login:</h1>
      <div>
        <form action="submit" @submit.prevent="login">
          <input type="text" v-model="loginUser.email" placeholder="Email" required>
          <input type="password" v-model="loginUser.password" placeholder="Password" required>
          <button class="btn btn-primary" type="submit">Login</button>
        </form>
      </div>
    </div>
    <div v-else>
      <button class="btn btn-danger" @click="logout">Log Out</button>
    </div>
  </div>
</template>

<script>
  export default {
    name: 'Navbar',
    mounted() {
      this.$store.dispatch('authenticate')
    },
    data() {
      return {
        newUser: {
          name: '',
          email: '',
          password: ''
        },
        loginUser: {
          email: '',
          password: ''
        },
      }
    },
    methods: {
      register() {
        this.$store.dispatch('createUser', this.newUser)
      },
      login() {
        this.$store.dispatch('login', this.loginUser)
      },
      logout() {
        this.$store.dispatch('logout')
      }
    },
    computed: {
      user() {
        return this.$store.state.user
      }
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  .Navbar{
    background-color: gray;
    display: flex;
    justify-content: space-between;
  }
</style>