﻿var language = (function () {
	'use strict';

	var _element = $('.box-locale form input[type=radio]');

	var _setEvent = function () {
		_element.change(_submitLanguage);
	};

	var _submitLanguage = function (e) {
		e.preventDefault();
		e.target.form.submit();
	};

	return {
		init: function () {
			_setEvent();
		}
	}
})();

$(document).ready(function () {
	language.init();
});