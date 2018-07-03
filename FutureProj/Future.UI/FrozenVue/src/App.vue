<template>
  <div>
    <router-view/>
    <div class="footer ui-footer">
      <ul class="ui-tiled linklist">
        <li class="ui-border-r">资讯</li>
        <li class="ui-border-r">仓单</li>
        <li class="ui-border-r">统计局</li>
        <li class="ui-border-r">圈子</li>
        <li>AI量化</li>
      </ul>
    </div>
  </div>
</template>

<script>
export default {
data(){
  return{
    flinklist:[]
  }
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
