NPM安装教程：https://www.cnblogs.com/goldlong/p/8027997.html

echo %PATH%
node -v
npm -v
npm config set prefix "D:\nodejs\node_global"
npm config set cache "D:\nodejs\node_cache"

npm list -global
npm config set registry=http://registry.npm.taobao.org
npm config list
npm config get registry

//检查npm和仓储地址是否联通，联通将可以获取到信息
npm info vue
//安装最新版npm
npm install npm -g

注意，此时，默认的模块D:\nodejs\node_modules 目录
将会改变为D:\nodejs\node_global\node_modules 目录，
如果直接运行npm install等命令会报错的。
我们需要做1件事情：
1、增加环境变量NODE_PATH 内容是：D:\nodejs\node_global\node_modules

（注意，一下操作需要重新打开CMD让上面的环境变量生效）
一、测试NPM安装vue.js
命令：npm install vue -g
这里的-g是指安装到global全局目录去

二、测试NPM安装vue-router
命令：npm install vue-router -g

三、运行npm install vue-cli -g安装vue脚手架

四、此时直接执行vue命令会提示错误，需要对path环境变量添加D:\nodejs\node_global
win10以下版本的，横向显示PATH的，注意添加到最后时，不要有分号【;】

五、重新打开CMD，并且测试vue是否使用正常 vue -V

注意，vue-cli工具是内置了模板包括 webpack 和 webpack-simple,前者是比较复杂专业的项目，
他的配置并不全放在根目录下的 webpack.config.js 中。

vuw init webpack vue01 创建第一个项目
cd vue01
npm install
npm run dev

npm run build
生成静态文件，打开dist文件夹下新生成的index.html文件



VSCODE HTML TAB快捷键 在vue文件不起作用问题
文件-首选项-设置  新增用户设置
    "emmet.triggerExpansionOnTab": true,
    "emmet.showAbbreviationSuggestions": true,
    "emmet.showExpandedAbbreviation": "always",
    "emmet.includeLanguages": {
        "vue-html": "html",
        "vue": "html"
    }


VUE:http://www.runoob.com/vue2/vue-install.html


http://doc.vue-js.com/v2/api/#mounted

<template>
<div>
    <h1>{{msg}}</h1>
  <div id='maincontent' style="width:400px;height:300px">

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


-------------------------------------------------------ELECTRON---------------------------------------------
