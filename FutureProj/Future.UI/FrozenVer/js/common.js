; (function ($) {
    //获取时间戳
    $.getLocalTime = function (at, fmt) {
        if (!at) return '-';
        if (typeof at == 'string') {
            at = new Date(at.replace(/-/g, '/'));
        } else {
            at = new Date(at);
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
    //字符串操作
    $.str_length = function (str) {
        var realLength = 0, len = str.length, charCode = -1;
        for (var i = 0; i < len; i++) {
            charCode = str.charCodeAt(i);
            if (charCode >= 0 && charCode <= 128) realLength += 1;
            else realLength += 2;
        }
        return realLength;
    }
    $.str_cut = function (str, len, left) {
        var newLength = 0;
        var newStr = "";
        var chineseRegex = /[^\x00-\xff]/g;
        var singleChar = "";
        var strLength = str.replace(chineseRegex, "**").length;
        left = left || '...';
        for (var i = 0; i < strLength; i++) {
            singleChar = str.charAt(i).toString();
            if (singleChar.match(chineseRegex) != null) {
                newLength += 2;
            }
            else {
                newLength++;
            }
            if (newLength > len) {
                break;
            }
            newStr += singleChar;
        }

        if (strLength > len) {
            newStr += left;
        }
        return newStr;
    }
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
            url: options.url,
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
                $.toast("网络出错，请重试！");
            },
            fail: function () {
                //返回错误回调函数
                return true;
            }
        };
        var callback = {};
        if (typeof options.success == 'function') {
            callback.success = function (data) {
                if (data.ok != 1 || data.code != 0) {
                    if (typeof options.fail == 'function') {
                        options.fail(data);
                    } else {
                        $.toast(data.msg);
                    }
                    return true;
                }
                return options.success(data);
            }
        };
        //配置loading
        if (options.loading) {
            var overlay = $.overlay(options.loading);
            callback.beforeSend = function () {
                overlay.show();
            };
            callback.complete = function () {
                overlay.remove();
            }
        }
        return $.ajax($.extend({}, defaults, options, callback));
    };
    $.overlay = function (dom) {
        if (typeof dom === 'string') {
            $dom = $(dom);
            $dom.css({ position: 'relative' })
        };
        $dom.append('<div class="ui-loading-block overlay"><div class="ui-loading-cnt overlayload" ><i class="ui-loading-bright"></i><p>正在加载中...</p></div></div>');
        overlay = $dom.find(".overlay");
        if (dom == 'body') {
            $(".overlay").css({ "position": "fixed" });
            $dom.css({ position: '' })
        };
        return overlay;
    };
    $.toast = function (msg) {
        //var msgm = '<div class="ui-tooltips ui-tooltips-warn"><div class="ui-tooltips-cnt ui-border-b"><i></i>' + msg + '<a class="ui-icon-close"></a></div></div>';
        var msgm2 = '<div class="ui-tooltips ui-tooltips-warn ui-tooltips-hignlight"><div class="ui-tooltips-cnt ui-border-b" ><i></i>' + msg + '<a class="ui-icon-close"></a></div ></div >'
        if ($('body').find('div.ui-tooltips.ui-tooltips-warn')) {
            $('body').find('div.ui-tooltips.ui-tooltips-warn').remove();
        }
        $('body').append(msgm2);
        $('body').find('div.ui-tooltips.ui-tooltips-warn').css({ "position": "fixed"});
    };
    $(document).on("click", "a.ui-icon-close", function () {
        $(this).parent().parent().remove()
    });
    //$('a.ui-icon-close').live('click', function () {
    //    $(this).parent('div.ui-tooltips.ui-tooltips-warn').remove();
    //});
})(window.Zepto);