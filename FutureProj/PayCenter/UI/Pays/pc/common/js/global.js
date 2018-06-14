//增加全局配置网址
; (function ($) {
    var host = location.host;
    var defaults = {
        api:'',
    };
    if (host.indexOf('test') > -1 || host.indexOf('localhost') > -1 || host.indexOf('192.168') > -1) {
        defaults = {
            api:'',
        }
    }
    $.config = defaults;
})(jQuery);
//判断手机版跳转
;(function ($) {
    var browser = {
        versions: function () {
            var u = navigator.userAgent, app = navigator.appVersion;
            return {//移动终端浏览器版本信息   
                trident: u.indexOf('Trident') > -1, //IE内核  
                presto: u.indexOf('Presto') > -1, //opera内核  
                webKit: u.indexOf('AppleWebKit') > -1, //苹果、谷歌内核  
                gecko: u.indexOf('Gecko') > -1 && u.indexOf('KHTML') == -1, //火狐内核  
                mobile: !!u.match(/AppleWebKit.*Mobile.*/), //是否为移动终端  
                ios: !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/), //ios终端  
                android: u.indexOf('Android') > -1 || u.indexOf('Linux') > -1, //android终端或者uc浏览器  
                iPhone: u.indexOf('iPhone') > -1, //是否为iPhone或者QQHD浏览器  
                iPad: u.indexOf('iPad') > -1, //是否iPad    
                webApp: u.indexOf('Safari') == -1 //是否web应该程序，没有头部与底部  
            };
        }(),
        language: (navigator.browserLanguage || navigator.language).toLowerCase()
    }
    if (browser.versions.mobile || browser.versions.ios || browser.versions.android ||
      browser.versions.iPhone || browser.versions.iPad) {
        if(location.href.indexOf('desktop')==-1){
            location.href = location.href.replace(/pc/,'wap');
        }
    }
})(jQuery);
/*
* 新版设计的弹窗 beta
* 用法 new w_layer(message:'弹出的内容');
*/
window.w_layer_obj={
    v:'1.0',
    index:0
};
function  w_layer(config){
    var defaults = {
        title:'',
        message: '',
        type: 'alert', //目前支持弹窗，alert,confirm
        zIndex: 10000,
        close: true, //关闭按钮是否显示，默认显示
        btn_show:true, //按钮是否显示，默认显示
        icon: 0, //内容上面的图标，默认无，1为打勾图标
        success: function () { }, //成功回调，alert默认返回成功回调
        cancel:function(){}, //取消回调
        button:['确 定','取 消'], //按钮
        index: 0,//当前第几层
        cssClass: '', //需要自定义的样式
        closeCb:function(){} //关闭回调
    };
    this.options=$.extend({},defaults,config);
    this.options.zIndex+= w_layer_obj.index;
    this.options.index=w_layer_obj.index;
    this.build();
    w_layer_obj.index++;
}
w_layer.prototype={
   //构建html
   build:function(){
        var arr=[];
        var _this=this;
        arr.push('<div class="w-mask" id="w-mask-'+w_layer_obj.index+'" style="z-index:'+(_this.options.zIndex-1)+'"></div>');
        arr.push('<div class="w-layer  w-layer-anim  ' + _this.options.cssClass + '" id="w-layer-' + w_layer_obj.index + '">');
        arr.push('<div class="w-layer-body  w-layer-'+_this.options.type+'">')
        if(!_this.options.title && !_this.options.icon){
            //弹出框无标题且无图片设置默认padding-top
            arr.push('<div class="w-layer-content w-layer-alert-singel">');
        }else{
            arr.push('<div class="w-layer-content">');
        } 
        if(_this.options.title){
            arr.push('<div class="w-layer-title">'+_this.options.title+'</div>');
        }
        if(_this.options.icon){
           arr.push('<i class="w-layer-icon w-layer-icon-'+_this.options.icon+'"></i>'); 
        }
        arr.push('<div class="w-layer-content-wrap">'+_this.options.message+'</div></div>');
        arr.push('<div class="w-layer-option">');
        if(_this.options.close){
            arr.push('<a class="w-layer-close" href="javascript:;"></a>');
        }
        arr.push('</div>');
        if(_this.options.btn_show){
            arr.push('<div class="w-layer-btn">');
            arr.push('<a class="btn btn-success" href="javascript:;">'+_this.options.button[0]+'</a>'); 
            if(_this.options.type=='confirm'){
                arr.push('<a class="btn btn-cancel" href="javascript:;">'+_this.options.button[1]+'</a>'); 
            }  
            arr.push('</div>');
        }
        _this.layer=$('<div>'+arr.join("")+'</div>').appendTo("body");
        _this.show();    
    },
    //显示
    show:function(){
        var _this=this;
        var w_layer=$("#w-layer-"+_this.options.index);
        var $win=$(window);
        var calcPosition=function(){
            var w=w_layer.width(),h=w_layer.height();
            var left = (parseInt($win.width()-w) / 2);
            var top =  (parseInt($win.height()-h) / 2);
            w_layer.css({'z-index':(_this.options.zIndex),'left':left+'px','top':top+'px'});
        };
        calcPosition();
        $win.resize(function(){
            calcPosition();
        });
        _this._bindEvent();
        //增加多语言自动切换
        if (typeof TWLanguage != 'undefined') TWLanguage.translateBody(w_layer[0]);
    },
    //绑定弹窗事件
    _bindEvent:function(){
        var _this=this;
        _this.layer.find(".btn-success").on('click',function(){
            if ($.isFunction(_this.options.success)) {
               if(_this.options.success(_this)==false){
                 return false;
               }
            }
            _this.close();
        });
        _this.layer.find(".btn-cancel").on('click',function(){
            if ($.isFunction(_this.options.cancel)) {
                if(_this.options.cancel(_this)==false){
                    return false;
                }
            }
            _this.close();
        });
        _this.layer.find(".w-layer-close").on('click',function(){
            _this.close();
            _this.options.closeCb && _this.options.closeCb(_this);
        });
    },
    close:function(){
        var _this=this;
        _this.layer.fadeOut('fast',function(){
            _this.layer.remove(); 
        })
    },
    closeAll:function(){
       for(var i=0;i<w_layer_obj.index;i++){
            $("#w-layer-"+i).fadeOut('fast',function(){
                $(this).remove();
            });
            $("#w-mask-"+i).fadeOut('fast',function(){
                $(this).remove();
            });
       }   
    }
}



//需加载jquery.form.js文件
function w_upload($d,$form,opts){
	//通过上传文件框触发
	$d.on('change',function(){
		var hash=['gif','jpg','jpeg','png'];
		var filename=$(this).val();
		var ext=filename.substring(filename.lastIndexOf(".")+1).toLowerCase();
		if($.inArray(ext,hash)=='-1' && ext){
			new w_layer({message:"图片格式不对"});
			return;
		}
		opts = opts || {};
	    opts.beforeSend = opts.beforeSend || function () {return true; };
	    opts.type = opts.type || 'post';
	    opts.dataType = opts.dataType || 'json';
	    if(opts.url){
	    	opts.url=opts.url;
	    }else{
		    var action=$form.attr('action');
		    opts.url=action || window.location.href || '';
	    }
	    opts.data = opts.data || {};
	    opts.error = opts.error || function (XmlHttpRequest, textStatus, errorThrown) { if (XmlHttpRequest.responseText != "") new w_layer({message:opts.errorMsg});}
	    opts.fail = opts.fail;
	    opts.uploadProgress = opts.uploadProgress;
	    opts.errorMsg = opts.errorMsg || '网络忙，请稍后再试';
	    opts.success = opts.success || function () { return true; };
	    opts.complete = opts.complete || function () {return true; };
	    opts.timeout=opts.timeout || 10000; 
		$form.ajaxSubmit({
			url:opts.url,
			type:opts.type,
			data:opts.data,
			dataType:opts.dataType, 
			beforeSend:opts.beforeSend,
			complete:opts.complete,
			error:opts.error,
			uploadProgress:opts.uploadProgress,
			success: function (data,status,xhr) {
				if(data.IsSuccess==true){
		    		opts.success(data,status,xhr);
		    	}else{
		    		if(opts.fail){
		    			opts.fail(data,status,xhr);
		    		}else{
		    			if(data.Message) new w_layer({message:data.Message});
		    		}
		    	}				
			}
		});
	});
}

//倒计时相关
function w_alarm(config){
    //默认调用的 end为字符串【2016/11/14 00:00:00 2016-11-14 00:00:00】
    var defaults={
        end:+new Date(),
        timestamp:false, //如果此参数为true则代表是时间戳，不自动转换
        callback:function(){}
    }
    //设置开始结束格式
    if(!config['timestamp']){
        if(config['end']) config['end']= +new Date(config['end'].replace(/-/g,'/'));        
    }
    this.options=$.extend({},defaults,config);
    this.init();    
}
w_alarm.prototype={
    init:function(){
        this.countDown();
    },
    countDown: function () {
        var self = this;
        var timer = setInterval(function () {
            var msg = self.getTimeLeft();
            if (msg == false) {
                clearInterval(timer);
            }
            if (typeof self.options.callback != 'undefined') self.options.callback(msg);
        }, 1000);
    },
    getTimeLeft:function(){
        //获取与当前时间的时间差
        var self=this;
        var hottime = self.options.end - (+new Date());
        if (hottime <= 0) return false;
        d = Math.floor(hottime / 1000 / 60 / 60 / 24);
        h = Math.floor(hottime / 1000 / 60 / 60 % 24);
        m = Math.floor(hottime / 1000 / 60 % 60);
        s = Math.floor(hottime / 1000 % 60);
        if (d < 10) d = "0" + d;
        if (h < 10) h = "0" + h;
        if (m < 10) m = "0" + m;
        if (s < 10) s = "0" + s;
        return [d,h,m,s];
    }

}

//表格排序
;(function ($) {
    $.fn.tableSort = function (callback) {
        var table=$(this);
        table.find("th.sortable").on('click',function(){
            var rows=table.find('tbody tr').toArray().sort(comparer($(this).index()));
            this.asc= !this.asc;
            if (!this.asc){rows = rows.reverse();}
            table.find("th.sortable").removeClass('desc asc');
            if(this.asc){ 
                $(this).addClass('desc');
            }else{
                $(this).addClass('asc');
            }
            table.find('tbody').empty().html(rows);
            table.find('tbody tr').each(function(i,d){
                if(i%2==1){
                    $(this).addClass('even');
                }else{
                    $(this).removeClass('even');
                }
            });
            if(typeof callback == 'function') callback(rows);
        });
        function comparer(index) {
            return function(a, b) {
                var valA = getCellValue(a, index), valB = getCellValue(b, index);
                return $.isNumeric(valA) && $.isNumeric(valB) ?
                    valA - valB : valA.localeCompare(valB);
            };
        }
        function getCellValue(row, index){
            return $(row).children('tbody td').eq(index).text();
        }
    };
})(jQuery);

/*cookie*/
;(function ($) { 
    var pluses = /\+/g;

	function encode(s) {
		return config.raw ? s : encodeURIComponent(s);
	}

	function decode(s) {
		return config.raw ? s : decodeURIComponent(s);
	}

	function stringifyCookieValue(value) {
		return encode(config.json ? JSON.stringify(value) : String(value));
	}

	function parseCookieValue(s) {
		if (s.indexOf('"') === 0) {
			// This is a quoted cookie as according to RFC2068, unescape...
			s = s.slice(1, -1).replace(/\\"/g, '"').replace(/\\\\/g, '\\');
		}

		try {
			s = decodeURIComponent(s.replace(pluses, ' '));
			return config.json ? JSON.parse(s) : s;
		} catch(e) {}
	}

	function read(s, converter) {
		var value = config.raw ? s : parseCookieValue(s);
		return $.isFunction(converter) ? converter(value) : value;
	}

	var config = $.cookie = function (key, value, options) {

		// Write

		if (arguments.length > 1 && !$.isFunction(value)) {
			options = $.extend({}, config.defaults, options);

			if (typeof options.expires === 'number') {
				var days = options.expires, t = options.expires = new Date();
				t.setMilliseconds(t.getMilliseconds() + days * 864e+5);
			}

			return (document.cookie = [
				encode(key), '=', stringifyCookieValue(value),
				options.expires ? '; expires=' + options.expires.toUTCString() : '', // use expires attribute, max-age is not supported by IE
				options.path    ? '; path=' + options.path : '',
				options.domain  ? '; domain=' + options.domain : '',
				options.secure  ? '; secure' : ''
			].join(''));
		}

		// Read
		var result = key ? undefined : {},
			cookies = document.cookie ? document.cookie.split('; ') : [],
			i = 0,
			l = cookies.length;

		for (; i < l; i++) {
			var parts = cookies[i].split('='),
				name = decode(parts.shift()),
				cookie = parts.join('=');

			if (key === name) {
				result = read(cookie, value);
				break;
			}
			if (!key && (cookie = read(cookie)) !== undefined) {
				result[name] = cookie;
			}
		}

		return result;
	};

	config.defaults = {};

	$.removeCookie = function (key, options) {
		$.cookie(key, '', $.extend({}, options, { expires: -1 }));
		return !$.cookie(key);
	};

})(jQuery);

//解决 IE8 不支持console
window.console = window.console || (function(){  
    var c = {}; c.log = c.warn = c.debug = c.info = c.error = c.time = c.dir = c.profile  
    = c.clear = c.exception = c.trace = c.assert = function(){};  
    return c;  
})();

//返回系统信息
; (function ($) {
    $.devices = function () {
        var u = navigator.userAgent.toLowerCase(),
            platform = navigator.platform;

        var devices = {};
        //获取系统信息
        var systemInfo = function () {
            var result = {
                isWin: (platform == "Win32") || (platform == "Windows") || (platform == "Win64"), //IE内核  
                isMac: (platform == "Mac68K") || (platform == "MacPPC") || (platform == "Macintosh") || (platform == "MacIntel"), //opera内核  
                isLinux: (String(platform).indexOf("Linux") > -1), //火狐内核  
                isIos: !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/i), //ios终端  
                isAndroid: u.indexOf('android') > -1 || u.indexOf('linux') > -1,
                isWx: !!u.match(/MicroMessenger/i),
                isApp: !!u.match(/XinshengView/i)
            };
            if ((navigator.platform == "X11") && !result.isWin && !result.isMac) {
                result.isUnix = true;
            } else {
                result.isUnix = false;
            }
            return result;
        }();

        var arr = [
            { type: 'isWin', value: 'Windows' },
            { type: 'isMac', value: 'MacOS' },
            { type: 'isIos', value: 'IOS' },
            { type: 'isAndroid', value: 'Android' },
            { type: 'isLinux', value: 'Linux' },
            { type: 'isUnix', value: 'Unix' },
        ];
        devices.systemInfo = 'Others';
        $.each(arr, function (i, d) {
            if (systemInfo[d.type]) {
                devices.systemInfo = d.value;
                return false;
            }
        });

        //获取设备信息
        if (systemInfo.isWin) {
            //windows系统判断
            var os = {
                '5.0': 'Windows 2000',
                '5.2': 'Windows 2003',
                '5.1': 'Windows XP',
                '6.0': 'Windows Vista',
                '6.1': 'Windows 7',
                '6.2': 'Windows 8',
                '10.0': 'Windows 10'
            };
            if (u.match(/windows\s*nt\s*([0-9.]+)/)) {
                if (os[RegExp.$1]) {
                    devices.softInfo = os[RegExp.$1];
                } 
            }
            if (!devices.softInfo) devices.softInfo = 'Windows设备';
        } else if (systemInfo.isMac) {
            devices.softInfo = 'MacOS设备';
        } else if (systemInfo.isApp) {
            devices.softInfo = '未知';
        } else if (systemInfo.isIos) {
            devices.softInfo = 'IOS设备';
        } else if (systemInfo.isAndroid) {
            devices.softInfo = 'Android设备';
        } else {
            devices.softInfo = 'Others';
        }

        //获取浏览器版本
        if (systemInfo.isWx) {
            devices.browserInfo = "微信";
        } else if (u.indexOf("msie") >= 0) {
            var ver = u.match(/msie ([\d.]+)/)[1];
            devices.browserInfo = "IE " + ver;
        } else if (u.indexOf("edge") >= 0) {
            var ver = u.match(/edge\/([\d.]+)/)[1];
            devices.browserInfo = "IE edge" + ver;
        } else if (u.indexOf("trident") >= 0 && u.indexOf('rv') >= 0) {
            var ver = u.match(/.*rv:([\w.]+)/)[1];
            devices.browserInfo = "IE " + ver;
        } else if (u.indexOf("firefox") >= 0) {
            //firefox
            var ver = u.match(/firefox\/([\d.]+)/)[1];
            devices.browserInfo = "Firefox " + ver;
        } else if (u.indexOf("chrome") >= 0) {
            //chrome
            var ver = u.match(/chrome\/([\d.]+)/)[1];
            devices.browserInfo = "Chrome " + ver;
        } else if (u.indexOf("crios") >= 0) {
            //crios
            var ver = u.match(/crios\/([\d.]+)/)[1];
            devices.browserInfo = "CriOS " + ver;
        } else if (u.indexOf("opera") >= 0) {
            //Opera
            var ver = u.match(/opera.([\d.]+)/)[1];
            devices.browserInfo = "Opera " + ver;
        } else if (u.indexOf("safari") >= 0) {
            //Safari
            var ver = u.match(/version\/([\d.]+)/)[1];
            devices.browserInfo = "Safari " + ver;
        } else {
            devices.browserInfo = '未知';
        }

        //APP下判断
        if (systemInfo.isApp) {

            devices.deviceInfo = $("body").attr('data-devices') || '{}';
            if (devices.deviceInfo == '{}' || !devices.deviceInfo) {
                devices.browserInfo = '未知';
                devices.softInfo = '未知';
                devices.deviceInfo = u;
            } else {
                var info = $.parseJSON(devices.deviceInfo);
                devices.softInfo = info.deviceModel;
                devices.browserInfo = 'APP ' + info.appVersion;
            }

        } else {
            devices.deviceInfo = u;
        }
        return devices;
    }
})(jQuery);

//常用函数类
; (function ($) {
    //获取时间戳
    $.getLocalTime = function (at, fmt) {
        if (!at) return '-';
        if (typeof at == 'string') {
            at = new Date(at.replace(/-/g, '/'));
        }
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
    };
    //获取url参数
    $.getParams = function (type) {
        var result = {};
        var params = (window.location.search.split('?')[1] || '').split('&');
        for (var param in params) {
            if (params.hasOwnProperty(param)) {
                paramParts = params[param].split('=');
                result[paramParts[0]] = decodeURIComponent(paramParts[1] || "");
            }
        }
        if (type) {
            return result[type];
        }
        return result;
    };
    //简化ajax方法
    $.ajaxF = function (options) {
        var defaults = {
            type: 'post',
            url: window.location.href,
            dataType: 'json',
            timeout: 8000,
            beforeSend: function () {
                return true;
            },
            complete: function () {
                return true;
            },
            success: function () {
                return true;
            },
            error: function (data) {
                new w_layer({message:"网络出错，请重试！"});
            },
            fail: function () {
                //返回错误回调函数
                return true;
            }
        };
        var callback = {};
        if (typeof options.success == 'function') {
            callback.success = function (data) {
                if (data.ok != 1||data.code !=0) {
                    if (typeof options.fail == 'function') {
                        options.fail(data);
                    } else {
                        new w_layer({message:data.msg});
                    }
                    return true;
                }
                return options.success(data);
            }
        };
        //配置loading
        if(options.loading){
            var overlay=$.overlay(options.loading);
                callback.beforeSend=function(){
                    overlay.show();
                };
                callback.complete=function(){
                    overlay.remove();
                }
        }
        return $.ajax($.extend({}, defaults, options, callback));
    };
    $.overlay=function(dom){
        if(typeof dom==='string'){
            $dom=$(dom);
            $dom.css({position:'relative'})
        };
        $dom.append('<div class="overlay" style="display:none;"><i class="fa fa-refresh fa-spin"></i> </div>');
        overlay=$dom.find(".overlay");
        if(dom=='body'){
            $(".overlay").css({"position":"fixed"});
        };
        return overlay;
    };

})(jQuery);
