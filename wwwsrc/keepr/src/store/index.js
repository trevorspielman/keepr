import vue from 'vue'
import vuex from 'vuex'
import axios from 'axios'
import router from '../router'

var production = !window.location.host.includes('localhost')
var baseUrl = production ? '//brewbros.herokuapp.com/' : '//localhost:5000/'

var ourDB = axios.create({
  baseURL: baseUrl + 'api/',
  timeout: 10000,
  withCredentials: true
})

var auth = axios.create({
  baseURL: baseUrl + 'account/',
  timeout: 5000,
  withCredentials: true
})


vue.use(vuex)

export default new vuex.Store({
  state: {
    user: {}
  },
  mutations: {
    updateUser(state, payload) {
      state.user = payload
    },

  },
  actions: {
    createUser({ commit, dispatch, state }, payload) {
      auth.post('register', payload)
        .then(res => {
          delete res.data.Name
          delete res.data.Id
          res.data.Password = payload.password
          dispatch('login', res.data)
        })
        .catch(err => {
          console.error(err)
        })
    },

    login({ commit, dispatch, state }, payload) {
      auth.post('login', payload)
        .then(res => {
          commit('updateUser', res.data)
          router.push({ name: 'Profile' })
        })
        .catch(err => {
          console.error(err)
        })
    },
    logout({ commit, dispatch }, payload) {
      auth.delete('logout')
        .then(res => {
          commit('updateUser', {})
          router.push({ name: 'Home' })
        })
        .catch(err => {
          console.error(err)
        })
    },
    authenticate({ commit, dispatch }, payload) {
      auth.get('authenticate', payload).then(res => {
        commit('updateUser', res.data)
        if (res.data == "") {
          router.push({ name: 'Home' })
        }
        else {
          router.push({ name: 'Profile' })
        }
      })
        .catch(err => {
          console.error(err);
          router.push({ name: 'Home' })
        })
    },
  }
})