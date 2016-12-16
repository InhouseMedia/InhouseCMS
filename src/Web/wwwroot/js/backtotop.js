var backToTop = (function() {
    'use strict';

    var _element = $('#backToTop');
    var _body = $('body');
    var _win = $(window);

    var _init = function() {
        _setEvent();
        _triggerButton();
    };

    var _setEvent = function() {
        _element.click(_scrollToTop);
    };

    var _scrollToTop = function(e) {
        e.preventDefault();
        _body.animate({ scrollTop: '0' }, 250);
    };

    var _triggerButton = function() {
        var viewportHeight = $(window).height();

        _win.scroll(function() {
            var scrollTop = _win.scrollTop();
            var isActive = _element.hasClass('active');

            if (scrollTop >= viewportHeight && !isActive) {
                _element.addClass('active');
            } else if (scrollTop < viewportHeight && isActive) {
                _element.removeClass('active');
            }
        });
    };

    return {
        init: function() {
            _init();
        }
    }
})();

$(document).ready(function() {
    backToTop.init();
});