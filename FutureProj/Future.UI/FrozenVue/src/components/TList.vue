<template>
<div>
    <toFHeader></toFHeader>
    <div id="ntabcontent" class="ui-tab">
    <ul class="ui-tab-nav ui-border-b">
      <li class="current"><span>最新发布</span></li>
        <li data-type="dce"><span>大商所</span></li>
        <li data-type="czce"><span>郑商所</span></li>
        <li data-type="shfe"><span>上商所</span></li>
    </ul>
    <ul class="ui-tab-content contentb" style="width:400%;">
        <li></li>
        <li></li>
        <li></li>
        <li></li>
    </ul>                  
    </div>
</div>
</template>

<script>
import FHeader from '@/components/FHeader'
    export default {
          components:{
      'toFHeader':FHeader
  },
  data(){
      return {
          msg:"this is my first vue"
      }

  },
  mounted(){
  }
    }
</script>

<style scoped>

</style>