/*!art-template - Template Engine | http://aui.github.com/artTemplate/*/
!function(){
  template.helper('defaultVal', function (name,value) {
    if(!value) value='';
    var result=(name)?name:value;
    return result;
  });
  template.helper('toFixed', function (value,num) {
    num=num || 2;
    if(value==null || value=='') value=0;
    return value.toFixed(num);
  });
  function getLocalTime(at,fmt) {  
    if(!at) return '-';
    var at=new Date(at);
    var o = {
      "M+": at.getMonth() + 1, //月份 
      "d+": at.getDate(), //日 
      "h+": at.getHours(), //小时 
      "m+": at.getMinutes(), //分 
      "s+": at.getSeconds(), //秒 
      "q+": Math.floor((at.getMonth() + 3) / 3), //季度 
      "S": at.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (at.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
    if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;   
  }
  template.helper('getLocalTime', function (at,fmt) {
    return getLocalTime(at,fmt);
  });
  // 重写获取方法，如果有缓存则读缓存
  template.get = function (filename) {
    var cache;   
    if (template.cache[filename]) {
        // 使用内存缓存
        cache = template.cache[filename];
    } else if (typeof document === 'object') {
        // 加载模板并编译
        var elem = document.getElementById(filename);
        
        if (elem) {
            var source = (elem.value || elem.innerHTML)
            .replace(/^\s*|\s*$/g, '');
            cache = template.compile(source, {
                filename: filename
            });
        }
    }
    return cache;
  };
    //获取倒计时的月，天，时，分 ，秒
  function getTimeLeft(endtime,fmt) {
      var enddate = +new Date(endtime.replace(/-/g, '/'));
      //获取与当前时间的时间差
      var currentdate = +new Date();
      var hottime = enddate - currentdate;
      if (hottime <= 0) return false;
      month = enddate.getMonth()+1 - currentdate.getMonth()+1;
      d = Math.floor(hottime / 1000 / 60 / 60 / 24);
      h = Math.floor(hottime / 1000 / 60 / 60 % 24);
      m = Math.floor(hottime / 1000 / 60 % 60);
      s = Math.floor(hottime / 1000 % 60);
      month = month > 0 ? month : 1;
      if (d < 10) d = "0" + d;
      if (h < 10) h = "0" + h;
      if (m < 10) m = "0" + m;
      if (s < 10) s = "0" + s;
      var obj = { "month": month, "d": d, "h": d, "m": m, "s": s };
      return obj[fmt];
  }
  template.helper('getTimeLeft', function (endtime,fmt) {
      return getTimeLeft(endtime, fmt);
  });
  function getActivityDataType(type) {
      var result= "";
      switch (type) {
          case 1:
              result = "客服添加";
              break;
          case 2:
              result = "本人申请";
              break;
              default:
      }
      return result;
  }
  template.helper('getActivityDataType', function (type) {
      return getActivityDataType(type);
  });
}();