import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/components/Home'
import Profile from '@/components/Profile'
import Vault from '@/components/Vault'


Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Home',
      component: Home
    },
    {
      path: '/profile/:profileId',
      name: 'Profile',
      component: Profile
    },
    {
      path: '/vault/:vaultId',
      name: 'Vault',
      component: Vault
    }
  ]
})
