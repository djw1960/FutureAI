<template>
  <div>
    <router-view/>
  </div>
</template>

<script>
import login from '@/components/Share/login'
import home from '@/components/M/home'
import task from '@/components/M/task'
import news from '@/components/M/news'
import cangdan from '@/components/M/cangdan'
import tongji from '@/components/M/tongji'
import moment from '@/components/M/moment'
import ai from '@/components/AI/index.vue'
import aiedit from '@/components/AI/edit.vue'
import aiAdd from '@/components/AI/add.vue'
export default {
data(){
  return{
  }
},
components:{
  login:login,
  home:home,
  task:task,
  news:news,
  cangdan:cangdan,
  tongji:tongji,
  moment:moment,
  ai:ai,
  aiedit:aiedit,
  aiAdd:aiAdd,
},
methods:{
},
created(){
}
}
</script>

<style>
@import url('./assets/css/frozenui.css');
@import url('./assets/css/style.css');
.width100{
  width: 100%;
}
</style>
