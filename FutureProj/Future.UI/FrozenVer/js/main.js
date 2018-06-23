(function (doc, win) {
    $('#list td,#list li,.linklist li').click(function () {
        location.href = $(this).data('href');
    });

    $('.ui-header .ui-btn').click(function () {
        location.href = 'index.html';
    });

    $("#btn1").click(function () {
        $('.ui-actionsheet').addClass('show');
    });

    $("#cancel").click(function () {
        $(".ui-actionsheet").removeClass("show");
    });
    //tab切换
    $('.ui-tab-nav').eq(0).find('li').on('click', function () {
        $(this).parent().find('li').removeClass('current');
        $(this).addClass('current');
        //加载tab数据-切换tabContent
        var lihtml = "<ul class=\"ui-list ui-border-tb \"><li><div class=\"ui-list-img-square\"><span style=\"background-image:url(http://placeholder.qiniudn.com/188x188)\" ></span> </div><div class=\"ui-list-info ui-border-t\"><h4 class=\"ui-nowrap\">这是标题，加ui-nowrap可以超出长度截断</h4><p class=\"ui-nowrap\">这是内容，加ui-nowrap可以超出长度截断</p></div></li></ul>";
        $('.ui-tab-content>li').eq($(this).index()).html(lihtml);
        $('.ui-tab-content').eq(0).css({
            'transform': 'translate3d(-' + ($(this).index() * $('.ui-tab-content li').offset().width) + 'px,0,0)',
            'transition': 'transform 0.5s linear'
        })
    });
})(document, window);