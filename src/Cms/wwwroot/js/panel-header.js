
var panelHeader = (function () {
	'use strict';

	var _element = $('#backToTop');
	var _body = $('body');
	var _win = $(window);

	var _init = function () {
		_setEvent();
	};

	var _setEvent = function () {
		//_element.click(_scrollToTop);
	};

	return {
		init: function () {
			_init();
		}
	}
})();

$(document).ready(function () {
	panelHeader.init();
});