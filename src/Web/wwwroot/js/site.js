var common = (function () {
    'use strict';

    var _header = $('header');
    var _headerHeight = _header.outerHeight();
    var _body = $('body');
    var _win = $(window);
    var _prev = 0;

    var _init = function () {
        _win.resize(_pageFill);
        _win.scroll(_fixedHeader);
        _pageFill();
    };

    var _pageFill = function() {
        var isMaxFill = _body.hasClass('fill');
        var maxFill = (isMaxFill) ?
            _win.outerHeight() > (_body.get(0).offsetHeight + _body.find('footer').outerHeight()) :
            _win.outerHeight() > _body.get(0).offsetHeight;

        if (maxFill) {
            if (!isMaxFill) _body.addClass('fill');
        } else {
            if (isMaxFill) _body.removeClass('fill');
        }
    };

    var _fixedHeader = function() {
        var scrollTop = _win.scrollTop();
        var isFixedHeader = _header.hasClass('fixed');
        var current = parseInt(_header.css('top'));
        var delta = scrollTop - _prev;

        _prev = scrollTop;

        if (delta > 0) { // Scroll down
            if (isFixedHeader) _header.removeClass('fixed');
            if (current > -_headerHeight) _header.css('top', Math.max(current - delta, -_headerHeight) + 'px');
        } else { // Scroll up
            if (!isFixedHeader) _header.addClass('fixed');
            if (current < 0) _header.css('top', Math.min(current - delta, 0) + 'px');
        }
    };

    return {
        init: function () {
            _init();
        }
    }
})();

$(document).ready(function () {
    common.init();
});