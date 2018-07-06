<template>
  <div>
    <router-view/>
    <div class="footer ui-footer">
      <ul class="ui-tiled linklist">
        <li class="ui-border-r"><router-link to="/">资讯</router-link></li>
        <li class="ui-border-r"><router-link to="/c">仓单</router-link></li>
        <li class="ui-border-r"><router-link to="/t">统计局</router-link></li>
        <li class="ui-border-r"><router-link to="/f">圈子</router-link></li>
        <li ><router-link to="/a">AI量化</router-link></li>
      </ul>
    </div>
  </div>
</template>

<script>
export default {
data(){
  return{
  }
},
methods:{
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
