import Vue from 'vue'
import Router from 'vue-router'
import login from '@/components/Share/login'
import home from '@/components/M/home'
import task from '@/components/M/task'

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
      path:'/task',
      name:'task',
      component:task
    },
    {
      path:'*',
      redirect:'/'
    }
  ]
})
