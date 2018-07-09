import Vue from 'vue'
import Router from 'vue-router'
import News from '@/components/News'
import CList from '@/components/CList'
import TList from '@/components/TList'
import FIndex from '@/components/FIndex'
import AIndex from '@/components/AIndex'
import NewsInfo from '@/components/NewsInfo'

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
      path:'/n',
      name: 'NList',
      component: News,
      children:[
        {
          // 当 /user/:id/profile 匹配成功，
          // UserProfile 会被渲染在 User 的 <router-view> 中
          path: 'd',
          name:'NewsInfo',
          component: NewsInfo
        }
      ]
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
