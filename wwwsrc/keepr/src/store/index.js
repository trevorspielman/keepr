import vue from 'vue'
import vuex from 'vuex'
import axios from 'axios'
import router from '../router'

var production = !window.location.host.includes('localhost')
var baseUrl = production ? '//trevor-keepr.herokuapp.com/' : '//localhost:5000/'

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
    userProfile: {},
    profileVaults: {},
    keeps: [],
    keep: {},
    vaults: [],
    vault: {},
    vaultKeeps: [],
  },
  mutations: {
    updateUser(state, payload) {
      state.user = payload
    },
    publicUser(state, payload) {
      state.userProfile = payload
    },
    setKeeps(state, payload) {
      state.keeps = payload
    },
    setKeep(state, payload) {
      state.keep = payload
    },
    setVaults(state, payload) {
      state.vaults = payload
    },
    setProfileVaults(state, payload){
      state.profileVaults = payload
    },
    setVault(state, payload) {
      state.vault = payload
    },
    setVaultKeeps(state, payload) {
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
    getKeep({ commit, dispatch, state }, payload) {
      myDB.get('keeps/' + payload)
        .then(res => {
          commit('setKeep', res.data)
          dispatch('getProfileUser', res.data.userId)
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
    updateKeep({ commit, dispatch, state }, payload) {
      myDB.put('keeps/' + payload.id, payload)
        .then(res => {
          dispatch('getKeeps')
        })
        .catch(err => {
          console.log(err)
        })
    },
    removeKeep({ commit, dispatch, state }, payload) {
      myDB.delete('keeps/' + payload.id)
        .then(res => {
          router.push({ name: 'Profile', params: { profileId: state.user.id } })
        })
        .catch(err => {
          console.log(err)
        })
    },

    //Vault Actions
    getVaults({ commit, dispatch, state }, payload) {
      myDB.get('vaults/user/' + payload, payload)
        .then(res => {
          commit('setVaults', res.data)
        })
        .catch(err => {
          console.log(err)
        })
    },
    getProfileUserVaults({ commit, dispatch, state }, payload) {
      myDB.get('vaults/user/' + payload, payload)
        .then(res => {
          commit('setProfileVaults', res.data)
        })
        .catch(err => {
          console.log(err)
        })
    },


    createVault({ commit, dispatch, state }, payload) {
      myDB.post('vaults', payload)
        .then(res => {
          dispatch('getVaults', res.data.userId)
        })
        .catch(err => {
          console.log(err)
        })
    },
    getVault({ commit, dispatch, state }, payload) {
      myDB.get('vaults/' + payload)
        .then(res => {
          commit('setVault', res.data)
          dispatch('getVaultKeeps', res.data.id)
        })
        .catch(err => {
          console.log(err)
        })
    },

    createVaultKeep({ commit, dispatch, state }, payload) {
      myDB.post('vaultkeeps', { userId: state.user.id, keepId: payload.keepId, vaultId: payload.vaultId.vault })
        .then(res => {
          // dispatch('getVaultKeeps', res.data.vaultId)
        })
        .catch(err => {
          console.log(err)
        })
    },
    addToVault({ commit, dispatch, state }, payload) {
      myDB.post('vaultkeeps', { userId: payload.user.id, keepId: payload.keep.id, vaultId: payload.vault.id })
        .then(res => {
          payload.keep.saves += 1
          dispatch('updateKeep', payload.keep)
        })
        .catch(err => {
          console.log(err)
        })
    },

    getVaultKeeps({ commit, dispatch, state }, payload) {
      myDB.get('vaultkeeps/' + payload, payload)
        .then(res => {
          commit('setVaultKeeps', res.data)
        })
        .catch(err => {
          console.log(err)
        })
    },


    getProfileUser({ commit, dispatch, state }, payload) {
      auth.get('' + payload, payload)
        .then(res => {
          commit('publicUser', res.data)
        })
        .catch(err => {
          console.log(err)
        })
    },


    //User Auth Methods
    createUser({ commit, dispatch, state }, payload) {
      auth.post('register', payload)
        .then(res => {
          delete res.data.username
          delete res.data.id
          res.data.password = payload.password
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
          // router.push({ name: 'Profile', params: { profileId: state.user.id } })
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
    authenticate({ commit, dispatch, state }, payload) {
      auth.get('authenticate', payload).then(res => {
        commit('updateUser', res.data)
        if (res.data == "") {
          router.push({ name: 'Home' })
        }
        else {
          dispatch("getVaults", res.data.id)
        }
      })
        .catch(err => {
          console.error(err);
          router.push({ name: 'Home' })
        })
    },
  }
})