<template>
<div>
    <h1>{{msg}}</h1>
  <div id='maincontent'>

  </div>
</div>

</template>

<script>
import echarts from 'echarts'
export default {
  name: 'Menus',
  data(){
      return {
          msg:"this is my first vue"
      }

  },
  mounted(){
      // 基于准备好的dom，初始化echarts实例
var myChart = echarts.init(document.getElementById('maincontent'));
console.log(myChart);
// 绘制图表
myChart.setOption({
    title: {
        text: 'ECharts 入门示例'
    },
    tooltip: {},
    xAxis: {
        data: ['衬衫', '羊毛衫', '雪纺衫', '裤子', '高跟鞋', '袜子']
    },
    yAxis: {},
    series: [{
        name: '销量',
        type: 'bar',
        data: [5, 20, 36, 10, 10, 20]
    }]
});
  }
}
</script>
<style>
#maincontent{
    width:400px;height:300px
}
</style>
