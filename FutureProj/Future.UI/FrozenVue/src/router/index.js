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
      path: '/home',
      name: 'home',
      component: App.components.home
    },
    {
      path:'/task',
      name:'task',
      component: App.components.task
    },
    {
      path: '/news',
      name: 'news',
      component: App.components.news
    },
    {
      path: '/cangdan',
      name: 'cangdan',
      component: App.components.cangdan
    },
    {
      path: '/tongji',
      name: 'tongji',
      component: App.components.tongji
    },
    {
      path: '/moment',
      name: 'moment',
      component: App.components.moment
    },
    {
      path: '/ai',
      name: 'ai',
      component: App.components.ai
    },
    {
      path: '/ai/detial/:id',
      name: 'aidetial',
      component: App.components.aidetial
    },
    {
      path:'*',
      redirect:'/'
    }
  ]
})
