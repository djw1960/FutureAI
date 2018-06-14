//配置文件
; (function ($) {
  var host = location.host;
  var defaults = {
      url: 'https://m.xs9999.com',
      account: 'http://maccount.xs9999.com',
      uc: 'http://muc.xs9999.com',
      deposit: 'http://mdeposit.xs9999.com',
      api: 'https://crmapi.xs9999.com',
      dataCenter: 'https://crmapi.xs9999.com/Crawl/'
  };
  if (host.indexOf('test') > -1 || host.indexOf('localhost') > -1) {
      defaults = {
          url: 'http://testm.xs9999.com/',
          account: 'http://testmaccount.xs9999.com',
          uc: 'http://testmuc.xs9999.com',
          deposit: 'http://testmdeposit.xs9999.com',
          api: 'http://testapi.xs9999.com',
          dataCenter: 'http://testapi.xs9999.com/Crawl/'
      }
  }
  $.config = defaults;
})(jQuery);

//设备判断
; (function ($) {
  var device = {};
  var ua = navigator.userAgent;

  var android = ua.match(/(Android);?[\s\/]+([\d.]+)?/);
  var ipad = ua.match(/(iPad).*OS\s([\d_]+)/);
  var ipod = ua.match(/(iPod)(.*OS\s([\d_]+))?/);
  var iphone = !ipad && ua.match(/(iPhone\sOS)\s([\d_]+)/);

  device.ios = device.android = device.iphone = device.ipad = device.androidChrome = false;

  // Android
  if (android) {
      device.os = 'android';
      device.osVersion = android[2];
      device.android = true;
      device.androidChrome = ua.toLowerCase().indexOf('chrome') >= 0;
  }
  if (ipad || iphone || ipod) {
      device.os = 'ios';
      device.ios = true;
  }
  // iOS
  if (iphone && !ipod) {
      device.osVersion = iphone[2].replace(/_/g, '.');
      device.iphone = true;
  }
  if (ipad) {
      device.osVersion = ipad[2].replace(/_/g, '.');
      device.ipad = true;
  }
  if (ipod) {
      device.osVersion = ipod[3] ? ipod[3].replace(/_/g, '.') : null;
      device.iphone = true;
  }
  // iOS 8+ changed UA
  if (device.ios && device.osVersion && ua.indexOf('Version/') >= 0) {
      if (device.osVersion.split('.')[0] === '10') {
          device.osVersion = ua.toLowerCase().split('version/')[1].split(' ')[0];
      }
  }

  // Webview
  device.webView = (iphone || ipad || ipod) && ua.match(/.*AppleWebKit(?!.*Safari)/i);

  // Minimal UI
  if (device.os && device.os === 'ios') {
      var osVersionArr = device.osVersion.split('.');
      device.minimalUi = !device.webView &&
          (ipod || iphone) &&
          (osVersionArr[0] * 1 === 7 ? osVersionArr[1] * 1 >= 1 : osVersionArr[0] * 1 > 7) &&
          $('meta[name="viewport"]').length > 0 && $('meta[name="viewport"]').attr('content').indexOf('minimal-ui') >= 0;
  }

  // Check for status bar and fullscreen app mode
  var windowWidth = $(window).width();
  var windowHeight = $(window).height();
  device.statusBar = false;
  if (device.webView && (windowWidth * windowHeight === screen.width * screen.height)) {
      device.statusBar = true;
  }
  else {
      device.statusBar = false;
  }

  // Classes
  var classNames = [];

  // Pixel Ratio
  device.pixelRatio = window.devicePixelRatio || 1;
  classNames.push('pixel-ratio-' + Math.floor(device.pixelRatio));
  if (device.pixelRatio >= 2) {
      classNames.push('retina');
  }

  // OS classes
  if (device.os) {
      classNames.push(device.os, device.os + '-' + device.osVersion.split('.')[0], device.os + '-' + device.osVersion.replace(/\./g, '-'));
      if (device.os === 'ios') {
          var major = parseInt(device.osVersion.split('.')[0], 10);
          for (var i = major - 1; i >= 6; i--) {
              classNames.push('ios-gt-' + i);
          }
      }

  }
  // Status bar classes
  if (device.statusBar) {
      classNames.push('with-statusbar-overlay');
  }
  else {
      $('html').removeClass('with-statusbar-overlay');
  }

  // Add html classes
  if (classNames.length > 0) $('html').addClass(classNames.join(' '));

  // wechat..
  device.isWeixin = /MicroMessenger/i.test(ua);
  // app
  device.isApp = /XinshengView/i.test(ua);

  $.device = device;

})(jQuery);

//相关拓展
; (function ($) {
  $.fn.transitionEnd = function (callback) {
      var events = ['webkitTransitionEnd', 'transitionend', 'oTransitionEnd', 'MSTransitionEnd', 'msTransitionEnd'],
          i, dom = this;

      function fireCallBack(e) {
          /*jshint validthis:true */
          if (e.target !== this) return;
          callback.call(this, e);
          for (i = 0; i < events.length; i++) {
              dom.off(events[i], fireCallBack);
          }
      }
      if (callback) {
          for (i = 0; i < events.length; i++) {
              dom.on(events[i], fireCallBack);
          }
      }
      return this;
  };

  $.fn.animationEnd = function (callback) {
      var events = ['webkitAnimationEnd', 'OAnimationEnd', 'MSAnimationEnd', 'animationend'],
          i, dom = this;

      function fireCallBack(e) {
          callback(e);
          for (i = 0; i < events.length; i++) {
              dom.off(events[i], fireCallBack);
          }
      }
      if (callback) {
          for (i = 0; i < events.length; i++) {
              dom.on(events[i], fireCallBack);
          }
      }
      return this;
  };
})(jQuery);

//cookie操作
; (function ($) {
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
      } catch (e) { }
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
      options.path ? '; path=' + options.path : '',
      options.domain ? '; domain=' + options.domain : '',
      options.secure ? '; secure' : ''
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

//弹窗类
//弹窗
; (function ($) {
  var _modalTemplateTempDiv = document.createElement('div');

  $.modalStack = [];

  $.modalStackClearQueue = function () {
      if ($.modalStack.length) {
          ($.modalStack.shift())();
      }
  };
  $.modal = function (params) {
      params = params || {};
      var modalHTML = '';
      var buttonsHTML = '';
      if (params.buttons && params.buttons.length > 0) {
          for (var i = 0; i < params.buttons.length; i++) {
              buttonsHTML += '<span class="modal-button' + (params.buttons[i].bold ? ' modal-button-bold' : '') + '' + (params.buttons[i].cssClass ? ' ' + params.buttons[i].cssClass : '') + '">' + params.buttons[i].text + '</span>';
          }
      }
      var extraClass = params.extraClass || '';
      var titleHTML = params.title ? '<div class="modal-title">' + params.title + '</div>' : '';
      var closeHTML = params.close ? '<div class="modal-close"><a href="javascript:"></a></div>' : '';
      var textHTML = params.text ? '<div class="modal-text"><div class="modal-overflow">' + params.text + '</div></div>' : '';
      var afterTextHTML = params.afterText ? params.afterText : '';
      var noButtons = !params.buttons || params.buttons.length === 0 ? 'modal-no-buttons' : '';
      var verticalButtons = params.verticalButtons ? 'modal-buttons-vertical' : '';
      modalHTML = '<div class="modal ' + extraClass + ' ' + noButtons + '"><div class="modal-inner">' + (closeHTML + titleHTML + textHTML + afterTextHTML) + '</div><div class="modal-buttons ' + verticalButtons + '">' + buttonsHTML + '</div></div>';

      _modalTemplateTempDiv.innerHTML = modalHTML;

      var modal = $(_modalTemplateTempDiv).children();

      $(defaults.modalContainer).append(modal[0]);

      // Add events on buttons
      modal.find('.modal-button').each(function (index, el) {
          $(el).on('click', function (e) {
              if (params.buttons[index].close !== false) $.closeModal(modal);
              if (params.buttons[index].onClick) params.buttons[index].onClick(modal, e);
              if (params.onClick) params.onClick(modal, index);
          });
      });
      //绑定close事件
      if (closeHTML) {
          modal.find('.modal-close a').on('click', function () {
              $.closeModal(modal);
          });
      }
      $.openModal(modal);
      return modal[0];
  };
  $.alert = function (text, title, callbackOk) {
      if (typeof title === 'function') {
          callbackOk = arguments[1];
          title = undefined;
      }
      return $.modal({
          text: text || '',
          title: typeof title === 'undefined' ? defaults.modalTitle : title,
          buttons: [{ text: defaults.modalButtonOk, bold: true, onClick: callbackOk, cssClass: 'modal-button-primary' }]
      });
  };
  $.confirm = function (text, title, callbackOk, callbackCancel) {
      if (typeof title === 'function') {
          callbackCancel = arguments[2];
          callbackOk = arguments[1];
          title = undefined;
      }
      return $.modal({
          text: text || '',
          title: typeof title === 'undefined' ? defaults.modalTitle : title,
          buttons: [
              { text: defaults.modalButtonCancel, onClick: callbackCancel },
              { text: defaults.modalButtonOk, bold: true, onClick: callbackOk, cssClass: 'modal-button-primary' }
          ]
      });
  };
  $.prompt = function (text, title, callbackOk, callbackCancel) {
      if (typeof title === 'function') {
          callbackCancel = arguments[2];
          callbackOk = arguments[1];
          title = undefined;
      }
      return $.modal({
          text: text || '',
          title: typeof title === 'undefined' ? defaults.modalTitle : title,
          afterText: '<input type="text" class="modal-text-input">',
          buttons: [
              {
                  text: defaults.modalButtonCancel
              },
              {
                  text: defaults.modalButtonOk,
                  bold: true,
                  cssClass: 'modal-button-primary'
              }
          ],
          onClick: function (modal, index) {
              if (index === 0 && callbackCancel) callbackCancel($(modal).find('.modal-text-input').val());
              if (index === 1 && callbackOk) callbackOk($(modal).find('.modal-text-input').val());
          }
      });
  };
  $.modalLogin = function (text, title, callbackOk, callbackCancel) {
      if (typeof title === 'function') {
          callbackCancel = arguments[2];
          callbackOk = arguments[1];
          title = undefined;
      }
      return $.modal({
          text: text || '',
          title: typeof title === 'undefined' ? defaults.modalTitle : title,
          afterText: '<input type="text" name="modal-username" placeholder="' + defaults.modalUsernamePlaceholder + '" class="modal-text-input modal-text-input-double"><input type="password" name="modal-password" placeholder="' + defaults.modalPasswordPlaceholder + '" class="modal-text-input modal-text-input-double">',
          buttons: [
              {
                  text: defaults.modalButtonCancel
              },
              {
                  text: defaults.modalButtonOk,
                  bold: true,
                  cssClass: 'modal-button-primary'
              }
          ],
          onClick: function (modal, index) {
              var username = $(modal).find('.modal-text-input[name="modal-username"]').val();
              var password = $(modal).find('.modal-text-input[name="modal-password"]').val();
              if (index === 0 && callbackCancel) callbackCancel(username, password);
              if (index === 1 && callbackOk) callbackOk(username, password);
          }
      });
  };
  $.modalPassword = function (text, title, callbackOk, callbackCancel) {
      if (typeof title === 'function') {
          callbackCancel = arguments[2];
          callbackOk = arguments[1];
          title = undefined;
      }
      return $.modal({
          text: text || '',
          title: typeof title === 'undefined' ? defaults.modalTitle : title,
          afterText: '<input type="password" name="modal-password" placeholder="' + defaults.modalPasswordPlaceholder + '" class="modal-text-input">',
          buttons: [
              {
                  text: defaults.modalButtonCancel
              },
              {
                  text: defaults.modalButtonOk,
                  bold: true,
                  cssClass: 'modal-button-primary'
              }
          ],
          onClick: function (modal, index) {
              var password = $(modal).find('.modal-text-input[name="modal-password"]').val();
              if (index === 0 && callbackCancel) callbackCancel(password);
              if (index === 1 && callbackOk) callbackOk(password);
          }
      });
  };
  $.showPreloader = function (title) {
      $.hidePreloader();
      $.showPreloader.preloaderModal = $.modal({
          title: title || defaults.modalPreloaderTitle,
          text: '<div class="preloader"></div>'
      });

      return $.showPreloader.preloaderModal;
  };
  $.hidePreloader = function () {
      $.showPreloader.preloaderModal && $.closeModal($.showPreloader.preloaderModal);
  };
  $.showIndicator = function () {
      if ($('.preloader-indicator-modal')[0]) return;
      $(defaults.modalContainer).append('<div class="preloader-indicator-overlay"></div><div class="preloader-indicator-modal"><span class="preloader preloader-white"></span></div>');
  };
  $.hideIndicator = function () {
      $('.preloader-indicator-overlay, .preloader-indicator-modal').remove();
  };
  // Action Sheet
  $.actions = function (params) {
      var modal, groupSelector, buttonSelector;
      params = params || [];

      if (params.length > 0 && !$.isArray(params[0])) {
          params = [params];
      }
      var modalHTML;
      var buttonsHTML = '';
      for (var i = 0; i < params.length; i++) {
          for (var j = 0; j < params[i].length; j++) {
              if (j === 0) buttonsHTML += '<div class="actions-modal-group">';
              var button = params[i][j];
              var buttonClass = button.label ? 'actions-modal-label' : 'actions-modal-button';
              if (button.bold) buttonClass += ' actions-modal-button-bold';
              if (button.color) buttonClass += ' color-' + button.color;
              if (button.bg) buttonClass += ' bg-' + button.bg;
              if (button.disabled) buttonClass += ' disabled';
              buttonsHTML += '<span class="' + buttonClass + '">' + button.text + '</span>';
              if (j === params[i].length - 1) buttonsHTML += '</div>';
          }
      }
      modalHTML = '<div class="actions-modal">' + buttonsHTML + '</div>';
      _modalTemplateTempDiv.innerHTML = modalHTML;
      modal = $(_modalTemplateTempDiv).children();
      $(defaults.modalContainer).append(modal[0]);
      groupSelector = '.actions-modal-group';
      buttonSelector = '.actions-modal-button';

      var groups = modal.find(groupSelector);
      groups.each(function (index, el) {
          var groupIndex = index;
          $(el).children().each(function (index, el) {
              var buttonIndex = index;
              var buttonParams = params[groupIndex][buttonIndex];
              var clickTarget;
              if ($(el).is(buttonSelector)) clickTarget = $(el);
              // if (toPopover && $(el).find(buttonSelector).length > 0) clickTarget = $(el).find(buttonSelector);

              if (clickTarget) {
                  clickTarget.on('click', function (e) {
                      if (buttonParams.close !== false) $.closeModal(modal);
                      if (buttonParams.onClick) buttonParams.onClick(modal, e);
                  });
              }
          });
      });
      $.openModal(modal);
      return modal[0];
  };
  $.popup = function (modal, removeOnClose) {
      if (typeof removeOnClose === 'undefined') removeOnClose = true;
      if (typeof modal === 'string' && modal.indexOf('<') >= 0) {
          var _modal = document.createElement('div');
          _modal.innerHTML = modal.trim();
          if (_modal.childNodes.length > 0) {
              modal = _modal.childNodes[0];
              if (removeOnClose) modal.classList.add('remove-on-close');
              $(defaults.modalContainer).append(modal);
          }
          else return false; //nothing found
      }
      modal = $(modal);
      if (modal.length === 0) return false;
      modal.show();
      //modal.find(".content").scroller("refresh");
      if (modal.find('.' + defaults.viewClass).length > 0) {
          $.sizeNavbars(modal.find('.' + defaults.viewClass)[0]);
      }
      $.openModal(modal);

      return modal[0];
  };
  $.pickerModal = function (pickerModal, removeOnClose) {
      if (typeof removeOnClose === 'undefined') removeOnClose = true;
      if (typeof pickerModal === 'string' && pickerModal.indexOf('<') >= 0) {
          pickerModal = $(pickerModal);
          if (pickerModal.length > 0) {
              if (removeOnClose) pickerModal.addClass('remove-on-close');
              $(defaults.modalContainer).append(pickerModal[0]);
          }
          else return false; //nothing found
      }
      pickerModal = $(pickerModal);
      if (pickerModal.length === 0) return false;
      pickerModal.show();
      $.openModal(pickerModal);
      return pickerModal[0];
  };
  $.loginScreen = function (modal) {
      if (!modal) modal = '.login-screen';
      modal = $(modal);
      if (modal.length === 0) return false;
      modal.show();
      if (modal.find('.' + defaults.viewClass).length > 0) {
          $.sizeNavbars(modal.find('.' + defaults.viewClass)[0]);
      }
      $.openModal(modal);
      return modal[0];
  };
  //显示一个消息，会在2秒钟后自动消失
  $.toast = function (msg, duration, extraclass,toastCb) {
      var $toast = $('<div class="modal toast ' + (extraclass || '') + '">' + msg + '</div>').appendTo(document.body);
      if (typeof (duration) === 'function') toastCb = duration;
      if (typeof (extraclass) === 'function') toastCb = extraclass;
      $.openModal($toast, function () {
          setTimeout(function () {
              $.closeModal($toast, toastCb);
          }, duration || 2000);
      });
  };
  $.openModal = function (modal, cb) {
      modal = $(modal);
      var isModal = modal.hasClass('modal'),
          isNotToast = !modal.hasClass('toast');

      var isPopup = modal.hasClass('popup');
      var isLoginScreen = modal.hasClass('login-screen');
      var isPickerModal = modal.hasClass('picker-modal');
      var isToast = modal.hasClass('toast');
      if (isModal) {
          modal.show();
          modal.css({
              marginTop: -Math.round(modal.outerHeight() / 2) + 'px'
          });
      }
      if (isToast) {
          modal.css({
              marginLeft: -Math.round(modal.outerWidth() / 2) + 'px'
          });
      }

      var overlay;
      if (!isLoginScreen && !isPickerModal && !isToast) {
          if ($('.modal-overlay').length === 0 && !isPopup) {
              $(defaults.modalContainer).append('<div class="modal-overlay"></div>');
          }
          if ($('.popup-overlay').length === 0 && isPopup) {
              $(defaults.modalContainer).append('<div class="popup-overlay"></div>');
          }
          overlay = isPopup ? $('.popup-overlay') : $('.modal-overlay');
      }

      //Make sure that styles are applied, trigger relayout;
      var clientLeft = modal[0].clientLeft;

      // Trugger open event
      modal.trigger('open');

      // Picker modal body class
      if (isPickerModal) {
          $(defaults.modalContainer).addClass('with-picker-modal');
      }

      // Classes for transition in
      if (!isLoginScreen && !isPickerModal && !isToast) overlay.addClass('modal-overlay-visible');
      modal.removeClass('modal-out').addClass('modal-in').transitionEnd(function (e) {
          if (modal.hasClass('modal-out')) modal.trigger('closed');
          else modal.trigger('opened');
      });
      // excute callback
      if (typeof cb === 'function') {
          cb.call(this);
      }
      return true;
  };
  $.closeModal = function (modal,cb) {
      modal = $(modal || '.modal-in');
      if (typeof modal !== 'undefined' && modal.length === 0) {
          return;
      }
      var isModal = modal.hasClass('modal'),
          isPopup = modal.hasClass('popup'),
          isToast = modal.hasClass('toast'),
          isLoginScreen = modal.hasClass('login-screen'),
          isPickerModal = modal.hasClass('picker-modal'),
          removeOnClose = modal.hasClass('remove-on-close'),
          overlay = isPopup ? $('.popup-overlay') : $('.modal-overlay');
      if (isPopup) {
          if (modal.length === $('.popup.modal-in').length) {
              overlay.removeClass('modal-overlay-visible');
          }
      } else if (isModal) {
          if (modal.length === $('.modal.modal-in').length) {
              overlay.removeClass('modal-overlay-visible');
          }
      } else if (!(isPickerModal || isToast)) {
          overlay.removeClass('modal-overlay-visible');
      }

      modal.trigger('close');

      // Picker modal body class
      if (isPickerModal) {
          $(defaults.modalContainer).removeClass('with-picker-modal');
          $(defaults.modalContainer).addClass('picker-modal-closing');
      }

      modal.removeClass('modal-in').addClass('modal-out').transitionEnd(function (e) {
          if (modal.hasClass('modal-out')) modal.trigger('closed');
          else modal.trigger('opened');

          if (isPickerModal) {
              $(defaults.modalContainer).removeClass('picker-modal-closing');
          }
          if (isPopup || isLoginScreen || isPickerModal) {
              modal.removeClass('modal-out').hide();
              if (removeOnClose && modal.length > 0) {
                  modal.remove();
              }
          }
          else {
              modal.remove();
          }
      })
      if (isModal && defaults.modalStack) {
          $.modalStackClearQueue();
      }
      if (typeof cb === 'function') {
          cb.call(this);
      }
      return true;
    };
    $.fn.dataset = function () {
        var el = this[0];
        if (el) {
            var dataset = {};
            if (el.dataset) {

                for (var dataKey in el.dataset) { // jshint ignore:line
                    dataset[dataKey] = el.dataset[dataKey];
                }
            } else {
                for (var i = 0; i < el.attributes.length; i++) {
                    var attr = el.attributes[i];
                    if (attr.name.indexOf('data-') >= 0) {
                        dataset[$.toCamelCase(attr.name.split('data-')[1])] = attr.value;
                    }
                }
            }
            for (var key in dataset) {
                if (dataset[key] === 'false') dataset[key] = false;
                else if (dataset[key] === 'true') dataset[key] = true;
                else if (parseFloat(dataset[key]) === dataset[key] * 1) dataset[key] = dataset[key] * 1;
            }
            return dataset;
        } else return undefined;
    };
  function handleClicks(e) {
      /*jshint validthis:true */
      var clicked = $(this);
      var url = clicked.attr('href');


      //Collect Clicked data- attributes
      var clickedData = clicked.dataset();

      // Popup
      var popup;
      if (clicked.hasClass('open-popup')) {
          if (clickedData.popup) {
              popup = clickedData.popup;
          }
          else popup = '.popup';
          $.popup(popup);
      }
      if (clicked.hasClass('close-popup')) {
          if (clickedData.popup) {
              popup = clickedData.popup;
          }
          else popup = '.popup.modal-in';
          $.closeModal(popup);
      }

      // Close Modal
      if (clicked.hasClass('modal-overlay')) {
          if ($('.modal.modal-in').length > 0 && defaults.modalCloseByOutside)
              $.closeModal('.modal.modal-in');
          if ($('.actions-modal.modal-in').length > 0 && defaults.actionsCloseByOutside)
              $.closeModal('.actions-modal.modal-in');

      }
      if (clicked.hasClass('popup-overlay')) {
          if ($('.popup.modal-in').length > 0 && defaults.popupCloseByOutside)
              $.closeModal('.popup.modal-in');
      }
  }
  $(document).on('click', ' .modal-overlay, .popup-overlay, .close-popup, .open-popup, .close-picker', handleClicks);
  var defaults = $.modal.prototype.defaults = {
      modalStack: true,
      modalButtonOk: '确定',
      modalButtonCancel: '取消',
      modalPreloaderTitle: '加载中',
      modalUsernamePlaceholder: '账户',
      modalPasswordPlaceholder:'密码',
      modalContainer: document.body ? document.body : 'body'
  };
})(jQuery);

/*
* ga追踪码
* 页面调用前需要设置pageName $.xs_ga.setPageName('name');
* 调用方法$.xs_ga.send(name,position,index);
*/
; (function () {
  //定义ga调用方式
  var XS = function () {
      this.init();
  };
  XS.prototype = {
      setPageName: function (name) {
          this.page_name = name;
          return name;
      },
      send: function (name, position, index) {
          if (typeof ga != 'undefined' && this.page_name) ga('send', 'event', this.page_name, name, position, (index || '1'));
      },
      init: function () {
          var doc = $(document), self = this;
          //有data-ga属性赋予click事件
          doc.on('click', '[data-ga]', function () {
              var arg = $(this).attr('data-ga').split(',');
              if (arg.length < 2) return;
              self.send.apply(self, arg);
          });
          //有data-ga-c属性为common事件,默认page_name为lp_xs9999
          doc.on('click', '[data-ga-c]', function () {
              var arg = $(this).attr('data-ga-c').split(',');
              if (arg.length < 2) return;
              self.send.apply({ page_name: 'lp_xs9999' }, arg);
          });
      }
  };
  $(function () {
      $.xs_ga = new XS();
  });
})(jQuery);

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
            if (data.ok != 1||data.code !=0) {
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
        $dom.css({position:''})
    };
    return overlay;
};

})(jQuery);