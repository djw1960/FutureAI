import Vue from 'vue'
import Router from 'vue-router'
import login from '@/components/Share/login'
import home from '@/components/M/home'

Vue.use(Router)

export default new Router({
  mode: 'history',
  routes: [
    {
      path:'/',
      name: 'login',
      component: login
    },
    {
      path: '/home',
      name: 'home',
      component: home
    },
    {
      path:'*',
      redirect:'/'
    }
  ]
})
