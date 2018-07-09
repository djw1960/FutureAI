<template>
<div>
    <router-view></router-view>
    <toFHeader></toFHeader>
    <div id="ntabcontent" class="ui-tab">
    <ul class="ui-tab-nav ui-border-b">
      <li class="current"><span>最新发布</span></li>
        <li data-type="dce"><span>大商所</span></li>
        <li data-type="czce"><span>郑商所</span></li>
        <li data-type="shfe"><span>上商所</span></li>
    </ul>
    <ul class="ui-tab-content contentb" style="width:400%;">
        <li>
            <Newsli></Newsli>
        </li>
        <li></li>
        <li></li>
        <li></li>
    </ul>                  
    </div>
</div>

</template>

<script>
import echarts from 'echarts'
import FHeader from '@/components/FHeader'
import NewsLi from '@/components/NewsLi'
import {getdataPost} from '@/api/ApiList.js'
export default {
  components:{
      'toFHeader':FHeader,
      'Newsli':NewsLi
  },
  data(){
      return {
          msg:"this is my first vue"
      }

  },
  methods:{
      loadNews(){
          var params = {
                no: 1010,
                page: 1,
                type: '',
            };
            getdataPost(params).then(function (response) {
                console.log(response);
            }).catch(function (error) {
                console.log(error);
            });
      },

  },
  created() {
      this.loadNews();
  },

}
</script>
<style>

</style>
