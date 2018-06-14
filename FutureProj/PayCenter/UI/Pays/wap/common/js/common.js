//rem
; (function (doc, win) {
    var html = doc.documentElement, //html
        resizeEvt = 'orientationchange' in window ? 'orientationchange' : 'resize',
        recalc = function () {
            var hWidth = html.getBoundingClientRect().width;
            if (!hWidth) return;
            hWidth = Math.min(540, hWidth);
            html.style.fontSize = (hWidth / 7.5) + 'px';
        };
    if (!doc.addEventListener) return;
    win.addEventListener(resizeEvt, recalc, false);
    recalc();
})(document, window);
//live800
;(function ($) {
    $(document).on('click', '[data-live800="1"]', function () {
        var url = 'https://chat32.live800.com/live800/chatClient/chatbox.jsp?companyID=215184&configID=35519&jid=5377035377';
        ;(typeof analysesData == 'function') && analysesData('2', '2', '');
        if ($(this)[0].tagName.toLowerCase() == 'a') {
            $(this).attr({ 'href': url, 'target': '_blank' });
        } else {
            window.location.href = url;
        }
    });
    $(document).on('click', '[data-qq="1"]', function () {
        var url = 'http://wpa.b.qq.com/cgi/wpa.php?ln=2&uin=4006668236';
        (typeof analysesData == 'function') && analysesData('2', '1', '');
        if ($(this)[0].tagName.toLowerCase() == 'a') {
            $(this).attr({ 'href': url, 'target': '_blank' });
        } else {
            window.location.href = url;
        }
    });
})(jQuery);
