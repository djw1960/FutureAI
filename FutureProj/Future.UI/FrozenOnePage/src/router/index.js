import Vue from 'vue'
import Router from 'vue-router'
import App from '@/App.vue'

Vue.use(Router)

export default new Router({
  mode: 'history',
  routes: [
    {
      path:'/',
      name: 'login',
      component: App.components.login
    },
    {
      path:'/signup',
      name: 'signup',
      component: App.components.signup
    },
    {
      path: '/home',
      name: 'home',
      component: App.components.home
    },
    {
      path: '/ai',
      name: 'ai',
      component: App.components.ai
    },
    {
      path: '/master',
      name: 'master', 
      component: App.components.master
    },
    {
      path:'*',
      redirect:'/'
    }
  ]
})
