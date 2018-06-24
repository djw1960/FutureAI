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
    $('#ntabcontent>.ui-tab-nav').eq(0).find('li').on('click', function () {
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

    //tab切换
    $('#cdtabcontent>.ui-tab-nav').eq(0).find('li').on('click', function () {
        $(this).parent().find('li').removeClass('current');
        $(this).addClass('current');
        //加载tab数据-切换tabContent
        var lihtml = '<ul class="ui-list ui-border - tb ">';
        lihtml += '<li class="ui-border-t" style= "margin-left:0px;" >';
        lihtml += '<ul class="ui-row contenthr"><li class="ui-col ui-col-25 ui-border-r contentdh">名称</li><li class="ui-col ui-col-25 contentdd">价格</li>';
        lihtml += '<li class="ui-col ui-col-25 contentdd">涨跌额</li><li class="ui-col ui-col-25 contentdd">涨跌幅</li></ul></li >';
        lihtml += '<li class="ui-border-t" style="margin-left:0px;"><ul class="ui-row contentdr"><li class="ui-col ui-col-25 ui-border-r contentdh">螺纹钢</li><li class="ui-col ui-col-25 contentdd">4238</li><li class="ui-col ui-col-25 contentdd">+79</li> <li class="ui-col ui-col-25 contentdd">+0.6%</li></ul> </li></ul>';           
                         
        $('.ui-tab-content>li').eq($(this).index()).html(lihtml);
        $('.ui-tab-content').eq(0).css({
            'transform': 'translate3d(-' + ($(this).index() * $('.ui-tab-content li').offset().width) + 'px,0,0)',
            'transition': 'transform 0.5s linear'
        })
    });
})(document, window);