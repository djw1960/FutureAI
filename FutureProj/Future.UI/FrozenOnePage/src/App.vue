<template>
  <div>
    <router-view/>
  </div>
</template>

<script>
import login from '@/components/Share/login'
import signup from '@/components/Share/signup'
import moment from '@/components/M/moment'
import home from '@/components/M/home'
import ai from '@/components/AI/index.vue'
import master from '@/components/Share/master.vue'
export default {
data(){
  return{
  }
},
components:{
  login:login,
  signup:signup,
  moment:moment,
  home:home,
  ai:ai,
  master:master
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
