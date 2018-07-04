import Vue from 'vue'
import Router from 'vue-router'
import News from '@/components/News'
import CList from '@/components/CList'
import TList from '@/components/TList'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path:'/',
      name: 'News',
      component: News
    },
    {
      path:'/c',
      name:'CList',
      component: CList
    },
    {
      path: '/t',
      name: 'TList',
      component: TList
    }
  ]
})
