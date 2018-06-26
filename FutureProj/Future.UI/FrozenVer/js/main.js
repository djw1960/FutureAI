(function (doc, win) {
    var future = {
        data: {
            urls: "http://www.doapi.info/api/futurecenter",
            type: '',
            cate: $.getParams("cate")||'n',
            code: $.getParams("code") || '',
            id: $.getParams("id") || '',
            page: 0,
        },
        init: function () {
            var self = this;
            self.bindevent();
        },
        ajax: null,
        bindevent: function () {
            var self = this;
            $('#list td,#list li,.linklist li').click(function () {
                location.href = $(this).data('href');
            });
            //tab切换
            $('#ntabcontent>.ui-tab-nav').eq(0).find('li').on('click', function () {
                $(this).parent().find('li').removeClass('current');
                $(this).addClass('current');
                //加载tab数据-切换tabContent
                self.loadnews(this);            
            });
            //tab切换
            $('#cdtabcontent>.ui-tab-nav').eq(0).find('li').on('click', function () {
                $(this).parent().find('li').removeClass('current');
                $(this).addClass('current');
                //加载tab数据-切换tabContent
                self.loadcd(this);
            });
            $("#ntabcontent>.ui-tab-content ul>li").live("click", function () {
                location.href = $(this).data('href');
            })
        },
        turncontent: function (conthtml) {

        },
        loadnews: function (ethisobj) {
            var self = this;
            self.data.type = $(ethisobj).data('type');
            var params = {
                no: 1010,
                page: self.data.page,
                type: self.data.type,
            };

            $.ajax({
                type: 'POST',
                url: self.data.urls,
                data: params,
                dataType: 'json',
                timeout: 9000,
                success: function (data) {
                    if (data.code == 0) {
                        var list = data.data.list;
                        var lihtml = '<ul class=\"ui-list ui-border-tb \">';
                        for (var i = 0; i < list.length; i++) {
                            var item = list[i];
                            lihtml += '<li data-href="./n/d.html?id=' + item.ID + '"><div class="ui-list-img-square nimg"><span style="background-image:url(http://placeholder.qiniudn.com/140x140)"></span></div><div class=\"ui-list-info ui-border-t\"><h4 class=\"ui-nowrap\">' + item.NewsTitle + '</h4><p class=\"ui-nowrap\">来源：' + item.NSource + '日期：' + item.PublishDate + '</p></div></li>';
                        }
                        lihtml += '</ul">';
                        $('.ui-tab-content>li').eq($(ethisobj).index()).html(lihtml);
                        $('.ui-tab-content').eq(0).css({
                            'transform': 'translate3d(-' + ($(ethisobj).index() * $('.ui-tab-content li').offset().width) + 'px,0,0)',
                            'transition': 'transform 0.5s linear'
                        })
                    }
                },
                error: function (xhr, type) {
                    alert('Ajax error!')
                },
                beforeSend: function () {
                    $.overlay("body").show();
                },
                complete: function () {
                    $.overlay("body").hide();
                    $.overlay("body").remove();
                }
            })
        },
        loadnewsinfo: function () {
            var self = this;
            var params = {
                no: 1011,
                id: self.data.id,
            };

            $.ajax({
                type: 'POST',
                url: self.data.urls,
                data: params,
                dataType: 'json',
                timeout: 9000,
                success: function (data) {
                    if (data.code == 0) {
                        var item = data.data;
                        $('#newsheader span').eq(0).text(item.NewsTitle);
                        $('#newsheader span').eq(1).text('来源：' + item.NSource + '  日期：' + item.PublishDate);
                        $("#newsContent .ui-whitespace").html(item.NewContent);
                    }
                },
                error: function (xhr, type) {
                    alert('Ajax error!')
                },
                beforeSend: function () {
                    $.overlay("body").show();
                },
                complete: function () {
                    $.overlay("body").hide();
                }
            })
        },
        loadcd: function (ethisObj) {
            var self = this;
            var lihtml = '<ul class="ui-list ui-border - tb ">';
            lihtml += '<li class="ui-border-t" style= "margin-left:0px;" >';
            lihtml += '<ul class="ui-row contenthr"><li class="ui-col ui-col-25 ui-border-r contentdh">名称</li><li class="ui-col ui-col-25 contentdd">价格</li>';
            lihtml += '<li class="ui-col ui-col-25 contentdd">涨跌额</li><li class="ui-col ui-col-25 contentdd">涨跌幅</li></ul></li >';
            lihtml += '<li class="ui-border-t" style="margin-left:0px;"><ul class="ui-row contentdr"><li class="ui-col ui-col-25 ui-border-r contentdh">螺纹钢</li><li class="ui-col ui-col-25 contentdd">4238</li><li class="ui-col ui-col-25 contentdd">+79</li> <li class="ui-col ui-col-25 contentdd">+0.6%</li></ul> </li></ul>';

            $('.ui-tab-content>li').eq($(ethisObj).index()).html(lihtml);
            $('.ui-tab-content').eq(0).css({
                'transform': 'translate3d(-' + ($(ethisObj).index() * $('.ui-tab-content li').offset().width) + 'px,0,0)',
                'transition': 'transform 0.5s linear'
            })
        }
    };
    $.fn.future = future;
    future.init();
})(document, window);