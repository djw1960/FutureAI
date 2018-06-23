(function (doc, win) {
    $('#list td,#list li,.linklist li').click(function(){
          location.href= $(this).data('href');
      });

      $('.ui-header .ui-btn').click(function(){
          location.href= 'index.html';
      });

      $("#btn1").click(function(){
    		$('.ui-actionsheet').addClass('show');
    	});

    	$("#cancel").click(function(){
    		$(".ui-actionsheet").removeClass("show");
      });
    //tab«–ªª
        $('.ui-tab-nav').eq(0).find('li').on('click', function () {
            $(this).parent().find('li').removeClass('current');
            $(this).addClass('current');
            $('.ui-tab-content').eq(0).css({
                'transform': 'translate3d(-' + ($(this).index() * $('.ui-tab-content li').offset().width) + 'px,0,0)',
                'transition': 'transform 0.5s linear'
            })
        });
})(document, window);
