import vue from 'vue'
import vuex from 'vuex'
import axios from 'axios'
import router from '../router'

var production = !window.location.host.includes('localhost')
var baseUrl = production ? '//brewbros.herokuapp.com/' : '//localhost:5000/'

var myDB = axios.create({
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
    user: {},
    keeps: [],
    vaults: [],
    vault: {},
    vaultKeeps: [],
  },
  mutations: {
    updateUser(state, payload) {
      state.user = payload
    },
    setKeeps(state, payload) {
      state.keeps = payload
    },
    setMyVaults(state, payload) {
      state.vaults = payload
    },
    setVault(state, payload) {
      state.vault = payload
    },
    setVaultKeeps(state, payload){
      state.vaultKeeps = payload
    }

  },
  actions: {

    //Keeps Actions
    getKeeps({ commit, dispatch, state }) {
      myDB.get('keeps')
        .then(res => {
          commit('setKeeps', res.data)
        })
        .catch(err => {
          console.log(err)
        })
    },
    createKeep({ commit, dispatch, state }, payload) {
      myDB.post('keeps', payload.keep)
        .then(res => {
          dispatch('createVaultKeep', { keepId: res.data.id, vaultId: payload })
          dispatch('getKeeps')
        })
        .catch(err => {
          console.log(err)
        })
    },
    //Vault Actions
    getMyVaults({ commit, dispatch, state }, payload) {
      myDB.get('vaults/user/' + payload, payload)
        .then(res => {
          commit('setMyVaults', res.data)
        })
        .catch(err => {
          console.log(err)
        })
    },
    createVault({ commit, dispatch, state }, payload) {
      myDB.post('vaults', payload)
        .then(res => {
          dispatch('getMyVaults', res.data.userId)
        })
        .catch(err => {
          console.log(err)
        })
    },
    getVault({ commit, dispatch, state }, payload) {
      myDB.get('vaults/' + payload)
        .then(res => {
          commit('setVault', res.data)
        })
        .catch(err => {
          console.log(err)
        })
    },

    createVaultKeep({ commit, dispatch, state }, payload) {
      myDB.post('vaultkeeps', { userId: state.user.id, keepId: payload.keepId, vaultId: payload.vaultId.vault })
        .then(res => {
          console.log(res.data)
          // dispatch('getVaultKeeps', res.data.vaultId)
        })
        .catch(err => {
          console.log(err)
        })
    },
    getVaultKeeps({commit, dispatch, state}, payload){
      myDB.get('vaultkeeps/' + payload, payload)
      .then(res=>{
        console.log(res.data)
        commit('setVaultKeeps', res.data)
      })
      .catch(err => {
        console.log(err)
      })
    },



    //User Auth Methods
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
          router.push({ name: 'Profile', params: { profileId: state.user.id } })
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