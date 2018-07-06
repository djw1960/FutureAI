import Vue from 'vue'
import Router from 'vue-router'
import News from '@/components/News'
import CList from '@/components/CList'
import TList from '@/components/TList'
import FIndex from '@/components/FIndex'
import AIndex from '@/components/AIndex'

Vue.use(Router)

export default new Router({
  mode: 'history',
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
    },
    {
      path:'/f',
      name:'FIndex',
      component:FIndex
    },
    {
      path:'/a',
      name:'AIndex',
      component:AIndex
    }
  ]
})
