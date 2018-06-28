﻿(function (doc, win) {
    var future = {
        data: {
            urls: "http://www.doapi.info/api/futurecenter",
            type: $.getParams("type") ||'',
            cate: $.getParams("cate")||'n',
            code: $.getParams("code") || '',
            id: $.getParams("id") || '',
            page: 0,
            number:3,
        },
        commonobj: {
            utoken: '',
            uname: '',
            ulogin:''
        },
        init: function () {
            var self = this;
            self.bindevent();
        },
        ajax: null,
        bindevent: function () {
            var self = this;
            var r = self.getCookies();
            if (r&&self.commonobj.uname.length>0) {
                $(".header .userimg").hide();
                $(".header .username .ui-btn-lg").text(self.commonobj.uname);
                $(".header .username").show();
            } else {
                $(".header .username").hide()
                $(".header .userimg").show();
            }
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
            //根据月份查询
            $('#monthlist>div').on('click', function () {
                $(this).parent().find('div').removeClass('current');
                $(this).addClass('current');
                var page = $('#monthlist').data('page');
                var month = $(this).data('month');
                self.data.number = month;
                switch (page) {
                    case 't':
                        self.loadtjlist();
                        break;
                    case 'c':
                        self.loadcdlist();
                        break;
                    case 'tc':
                        self.loadtjcompare();
                        break;
                    default:
                }
                
            });
            $("#ntabcontent>.ui-tab-content ul>li").live("click", function () {
                var url = $(this).data('href');
                if (url) {
                    location.href = url;
                }
            });
            $(".ui-tab-content>li tbody>tr").live("click", function () {
                var url = $(this).data('href');
                if (url) {
                    location.href = url;
                }

            });
            $("#cdcontent>tbody>tr").live("click", function () {
                var url = $(this).data('href');
                if (url) {
                    location.href = url;
                }
            });
            $("#tjcatelist label").live("click", function () {
                if ($(this).hasClass('labcurrent')) {
                    $(this).removeClass('labcurrent');
                    return;
                }
                var sel = $(this).parent().find('label.labcurrent');
                if (sel.length >= 2) {
                    $.toast("最多只能选择两个品种");
                    return;
                }
                $(this).addClass('labcurrent');
            });
            $(".ui-user").on("click", function () {
                $('.ui-actionsheet').addClass('show');
            });
            $("#cancel").live('click',function () {
                $(".ui-actionsheet").removeClass("show");
            });
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
                            lihtml += '<li data-href="./n/d.html?id=' + item.ID + '"><div class="ui-list-img-square nimg"><span style="background-image:url(http://placeholder.qiniudn.com/140x140)"></span></div><div class=\"ui-list-info ui-border-t\"><h4 class=\"ui-nowrap\">' + item.NewsTitle + '</h4><p class=\"ui-nowrap\">来源：' + item.NSource + '  日期：' + item.PublishDate + '</p></div></li>';
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
                    $.overlay("body").remove();
                }
            })
        },
        loadcd: function (ethisobj) {
            var self = this;
            self.data.type = $(ethisobj).data('type');
            var params = {
                no: 1020,
                type: self.data.type,
                cate: self.data.cate,
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
                        var lihtml = '<table class="ui-table ui-border-tb"><thead><tr><th>品种名称</th><th>价格</th><th>涨跌</th><th>涨跌幅</th></tr></thead><tbody>';


                        for (var i = 0; i < list.length; i++) {
                            var item = list[i];
                            lihtml += '<tr data-href="d.html?type=' + self.data.type + '&cate=m&code=' + item.CateCode + '"><td class="fcol" >' + item.CateName + '</td><td>' + item.TDSum + '</td><td>' + item.Change + '</td><td>' + self.numFormat(item.Change / item.YTDSum,2) + '%</td></tr>';
                        }
                        lihtml += '</tbody></table>';
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
        loadcdlist: function () {
            var self = this;
            var params = {
                no: 1020,
                type: self.data.type,
                cate: self.data.cate,
                code: self.data.code,
                number: self.data.number
            };
            $.ajax({
                type: 'POST',
                url: self.data.urls,
                data: params,
                dataType: 'json',
                timeout: 9000,
                success: function (data) {
                    if (data.code == 0) {
                        var dlist = data.data;
                        var dt = [];
                        var da = [];
                        var model = dlist[0];
                        for (var i = 0; i < dlist.length; i++) {
                            var item = dlist[i];
                            dt.push(item.Date);
                            da.push(item.TDSum);
                        }
                        self.InitEChartOneTable(model.CateName, dt, da);
                    }
                    else
                    {
                        alert(data.msg);
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
        loadtj: function () {
            var self = this;
            var params = {
                no: 1030,
                cate: self.data.cate,
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
                        var lihtml = '';
                        for (var i = 0; i < list.length; i++) {
                            var item = list[i];
                            lihtml += '<tr data-href="d.html?cate=m&code=' + item.PCode + '"><td class="fcol" >' + self.namesubstring(item.PName) + '</td><td>' + item.Price + '</td><td>' + item.RisePrice + '</td><td>' + item.RiseRange + '%</td></tr>';
                        }
                        $('#cdcontent>tbody').html(lihtml);
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
        loadtjlist: function () {
            var self = this;
            var params = {
                no: 1030,
                cate: self.data.cate,
                code: self.data.code,
                number: self.data.number
            };
            $.ajax({
                type: 'POST',
                url: self.data.urls,
                data: params,
                dataType: 'json',
                timeout: 9000,
                success: function (data) {
                    if (data.code == 0) {
                        var dlist = data.data;
                        var dt = [];
                        var da = [];
                        var model = dlist[0];
                        for (var i = 0; i < dlist.length; i++) {
                            var item = dlist[i];
                            dt.push(item.DateTime);
                            da.push(item.Price);
                        }
                        self.InitEChartOneTable(model.PName, dt, da);
                    }
                    else {
                        alert(data.msg);
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
        loadtjcate: function () {
            var self = this;
            var params = {
                no: 1030,
                cate:'n',
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
                        var lihtml = '';
                        for (var i = 0; i < list.length; i++) {
                            var item = list[i];
                            lihtml += '<label data-code=' + item.PCode + ' class="ui-label">' + item.PName + '</label>';
                        }
                        $('#tjcatelist').html(lihtml);
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
        loadtjcompare: function () {
            var self = this;
            var params = {
                no: 1031,
                cate: 'm',
                code: $.getParams('cs'),
                number: self.data.number
            };
            $.ajax({
                type: 'POST',
                url: self.data.urls,
                data: params,
                dataType: 'json',
                timeout: 9000,
                success: function (data) {
                    if (data.code == 0 && data.data) {
                        var dlist1 = data.data.list1;
                        var dlist2 = data.data.list2;
                        var dt = [];
                        var da1 = [];
                        var da2 = [];
                        var model1 = dlist1[0];
                        var model2 = dlist2[0];
                        for (var i = 0; i < dlist1.length; i++) {
                            dt.push(dlist1[i].DateTime);
                            da1.push(dlist1[i].Price);
                            da2.push(dlist2[i].Price);
                        }
                        self.InitEChartMoreTable(dt, da1,da2);
                    }
                    else {
                        alert(data.msg);
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
        signin: function () {
            var self = this;
            var encoder = new Base64();
            var params = {
                no: 1000,
                account: encoder.encode('account'),
                pwd: encoder.encode('pwd')
            };

            $.ajax({
                type: 'POST',
                url: self.data.urls,
                data: params,
                dataType: 'json',
                timeout: 9000,
                success: function (data) {
                    if (data.code == 0) {
                        self.commonobj.account = data.data.account;
                        self.commonobj.uname = data.data.username;
                        self.commonobj.ulogin = data.data.token;
                        self.setCookies();
                    } else {
                        $.toast(data.msg);
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
        signout: function () {
        },
        InitEChartOneTable: function (title,date,data) {
            var myChart = echarts.init(document.getElementById('main'), null, { renderer: 'svg' });
            option = {
                tooltip: {
                    trigger: 'axis',
                    position: function (pt) {
                        return [pt[0], '10%'];
                    }
                },
                title: {
                    left: 'center',
                    text: title + '趋势面积图',
                },
                xAxis: {
                    type: 'category',
                    boundaryGap: false,
                    data: date
                },
                yAxis: {
                    type: 'value',
                    boundaryGap: [0, '100%']
                },
                series: [
                    {
                        name: title,
                        type: 'line',
                        smooth: true,
                        symbol: 'none',
                        sampling: 'average',
                        itemStyle: {
                            normal: {
                                color: 'rgb(255, 70, 131)'
                            }
                        },
                        areaStyle: {
                            normal: {
                                color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                                    offset: 0,
                                    color: 'rgb(255, 158, 68)'
                                }, {
                                    offset: 1,
                                    color: 'rgb(255, 70, 131)'
                                }])
                            }
                        },
                        data: data
                    }
                ]
            };
            // 使用刚指定的配置项和数据显示图表。
            myChart.setOption(option);
        },
        InitEChartMoreTable: function (date, data1, data2) {
            var myChart = echarts.init(document.getElementById('main'), null, { renderer: 'svg' });
            option = {
                tooltip: {
                    trigger: 'axis',
                    position: function (pt) {
                        return [pt[0], '10%'];
                    }
                },
                title: {
                    left: 'center',
                    text:'多品种趋势面积图',
                },
                xAxis: {
                    type: 'category',
                    boundaryGap: false,
                    data: date
                },
                yAxis: {
                    type: 'value',
                    boundaryGap: [0, '100%']
                },
                series: [
                    {
                        name: data1[0].PName,
                        type: 'line',
                        smooth: true,
                        symbol: 'none',
                        sampling: 'average',
                        itemStyle: {
                            normal: {
                                color: 'rgb(255, 70, 131)'
                            }
                        },
                        areaStyle: {
                            normal: {
                                color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                                    offset: 0,
                                    color: 'rgb(255, 158, 68)'
                                }, {
                                    offset: 1,
                                    color: 'rgb(255, 70, 131)'
                                }])
                            }
                        },
                        data: data1
                    }, {
                        name: data2[0].PName,
                        type: 'line',
                        smooth: true,
                        symbol: 'none',
                        sampling: 'average',
                        itemStyle: {
                            normal: {
                                color: 'rgb(255, 70, 131)'
                            }
                        },
                        areaStyle: {
                            normal: {
                                color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                                    offset: 0,
                                    color: 'rgb(255, 158, 68)'
                                }, {
                                    offset: 1,
                                    color: 'rgb(255, 70, 131)'
                                }])
                            }
                        },
                        data: data2
                    }
                ]
            };
            // 使用刚指定的配置项和数据显示图表。
            myChart.setOption(option);
        },
        namesubstring: function (str) {
            return str.substring(0, str.indexOf('（') < 1 ? 2 : str.indexOf('（')) 
        },
        numFormat: function (num, n) {
            if (isNaN(num)) {
                return '0.00';
            }
            var result = num.toFixed(n);
            return result;
        },
        setCookies: function () {
            var self = this;
            $.fn.cookie('commonobj', JSON.stringify(self.commonobj));
        },
        getCookies: function () {
            var self = this;
            var data = $.fn.cookie("commonobj");
            if (data) {
                self.commonobj = data;
                return true;
            } else {
                return false;
            }
        },
    };
    $.fn.future = future;
    future.init();
})(document, window);