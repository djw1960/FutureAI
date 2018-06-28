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
// Zepto.cookie plugin
// 
// Copyright (c) 2010, 2012 
// @author Klaus Hartl (stilbuero.de)
// @author Daniel Lacy (daniellacy.com)
// 
// Dual licensed under the MIT and GPL licenses:
// http://www.opensource.org/licenses/mit-license.php
// http://www.gnu.org/licenses/gpl.html
; (function ($) {
    $.extend($.fn, {
        cookie: function (key, value, options) {
            var days, time, result, decode

            // A key and value were given. Set cookie.
            if (arguments.length > 1 && String(value) !== "[object Object]") {
                // Enforce object
                options = $.extend({}, options)

                if (value === null || value === undefined) options.expires = -1

                if (typeof options.expires === 'number') {
                    days = (options.expires * 24 * 60 * 60 * 1000)
                    time = options.expires = new Date()

                    time.setTime(time.getTime() + days)
                }

                value = String(value)

                return (document.cookie = [
                    encodeURIComponent(key), '=',
                    options.raw ? value : encodeURIComponent(value),
                    options.expires ? '; expires=' + options.expires.toUTCString() : '',
                    options.path ? '; path=' + options.path : '',
                    options.domain ? '; domain=' + options.domain : '',
                    options.secure ? '; secure' : ''
                ].join(''))
            }

            // Key and possibly options given, get cookie
            options = value || {}

            decode = options.raw ? function (s) { return s } : decodeURIComponent

            return (result = new RegExp('(?:^|; )' + encodeURIComponent(key) + '=([^;]*)').exec(document.cookie)) ? decode(result[1]) : null
        }

    })
})(window.Zepto)
function Base64() {

    // private property
    _keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";

    // public method for encoding
    this.encode = function (input) {
        var output = "";
        var chr1, chr2, chr3, enc1, enc2, enc3, enc4;
        var i = 0;
        input = _utf8_encode(input);
        while (i < input.length) {
            chr1 = input.charCodeAt(i++);
            chr2 = input.charCodeAt(i++);
            chr3 = input.charCodeAt(i++);
            enc1 = chr1 >> 2;
            enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
            enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
            enc4 = chr3 & 63;
            if (isNaN(chr2)) {
                enc3 = enc4 = 64;
            } else if (isNaN(chr3)) {
                enc4 = 64;
            }
            output = output +
                _keyStr.charAt(enc1) + _keyStr.charAt(enc2) +
                _keyStr.charAt(enc3) + _keyStr.charAt(enc4);
        }
        return output;
    }

    // public method for decoding
    this.decode = function (input) {
        var output = "";
        var chr1, chr2, chr3;
        var enc1, enc2, enc3, enc4;
        var i = 0;
        input = input.replace(/[^A-Za-z0-9\+\/\=]/g, "");
        while (i < input.length) {
            enc1 = _keyStr.indexOf(input.charAt(i++));
            enc2 = _keyStr.indexOf(input.charAt(i++));
            enc3 = _keyStr.indexOf(input.charAt(i++));
            enc4 = _keyStr.indexOf(input.charAt(i++));
            chr1 = (enc1 << 2) | (enc2 >> 4);
            chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
            chr3 = ((enc3 & 3) << 6) | enc4;
            output = output + String.fromCharCode(chr1);
            if (enc3 != 64) {
                output = output + String.fromCharCode(chr2);
            }
            if (enc4 != 64) {
                output = output + String.fromCharCode(chr3);
            }
        }
        output = _utf8_decode(output);
        return output;
    }

    // private method for UTF-8 encoding
    _utf8_encode = function (string) {
        string = string.replace(/\r\n/g, "\n");
        var utftext = "";
        for (var n = 0; n < string.length; n++) {
            var c = string.charCodeAt(n);
            if (c < 128) {
                utftext += String.fromCharCode(c);
            } else if ((c > 127) && (c < 2048)) {
                utftext += String.fromCharCode((c >> 6) | 192);
                utftext += String.fromCharCode((c & 63) | 128);
            } else {
                utftext += String.fromCharCode((c >> 12) | 224);
                utftext += String.fromCharCode(((c >> 6) & 63) | 128);
                utftext += String.fromCharCode((c & 63) | 128);
            }

        }
        return utftext;
    }

    // private method for UTF-8 decoding
    _utf8_decode = function (utftext) {
        var string = "";
        var i = 0;
        var c = c1 = c2 = 0;
        while (i < utftext.length) {
            c = utftext.charCodeAt(i);
            if (c < 128) {
                string += String.fromCharCode(c);
                i++;
            } else if ((c > 191) && (c < 224)) {
                c2 = utftext.charCodeAt(i + 1);
                string += String.fromCharCode(((c & 31) << 6) | (c2 & 63));
                i += 2;
            } else {
                c2 = utftext.charCodeAt(i + 1);
                c3 = utftext.charCodeAt(i + 2);
                string += String.fromCharCode(((c & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63));
                i += 3;
            }
        }
        return string;
    }
}