<template>
  <div class="Navbar col-sm-12">
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
      <h2 class="navbar-brand">Keepr</h2>
      <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false"
        aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarNav">
        <ul class="navbar-nav">
          <li class="nav-item active">
            <router-link :to="{name: 'Home'}">
              <a class="nav-link" href="#">Home
                <span class="sr-only">(current)</span>
              </a>
            </router-link>
          </li>
          <li class="nav-item">
            <router-link :to="{ name: 'Profile', params: { profileId: user.id } }">
              <a class="nav-link" href="#">My Profile</a>
            </router-link>
          </li>
        </ul>
      </div>
      <div v-if="!user.id">
        <button class="btn btn-outline-success" data-toggle="modal" data-target="#register">Register</button>
        <!-- BEGINNING OF REGISTER MODAL -->
        <div class="modal fade" id="register" tabindex="-1" role="dialog">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">Please Register:</h5>
                <button type="button" class="close" data-dismiss="modal">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                <form action="submit" @submit.prevent="register">
                  <input type="text" v-model="newUser.userName" placeholder="Name">
                  <input type="text" v-model="newUser.email" placeholder="Email" required>
                  <input type="password" v-model="newUser.password" placeholder="Password" required>
                  <button class="btn btn-primary" type="submit">Register</button>
                </form>
              </div>
            </div>
          </div>
        </div>
        <button class="btn btn-outline-info" data-toggle="modal" data-target="#login">Login</button>
        <!-- BEGINNING OF REGISTER MODAL -->
        <div class="modal fade" id="login" tabindex="-1" role="dialog">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">Please Login:</h5>
                <button type="button" class="close" data-dismiss="modal">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                <form action="submit" @submit.prevent="login">
                  <input type="text" v-model="loginUser.email" placeholder="Email" required>
                  <input type="password" v-model="loginUser.password" placeholder="Password" required>
                  <button class="btn btn-primary" type="submit">Login</button>
                </form>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="v-else" v-else>
        <h1>{{user.username}}</h1>
        <button class="btn btn-danger" @click="logout">Log Out</button>
      </div>
    </nav>
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
          username: '',
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
  h1 {
    color: white;
    margin-right: 2rem;
  }

  .v-else {
    display: inline-flex;
  }
</style>