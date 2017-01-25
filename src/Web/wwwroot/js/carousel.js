/*TODO:
- Mousewheel implementation
- visible slides
- multiple effects / transitions
- debug modus
*/

var carousel = (function () {
	'use strict';

	var _element;
	var _timeout;
	var _currentSlide = 0;

	var _settings = {
		slides: [],
		effect: 'scroll',
		transition: 'sinoidal',
		timeout: 6,
		visibleSlides: 1,
		auto: true,
		circular: false,
		wheel: true,
		controls: true,
		debug: false
	};

	var _init = function (element, options) {
		_settings = $.extend({}, _settings, options);

		_element = $(element);

		_setAdjustments();
		_setControls();
		_setSlides();

		if (_settings.auto) _start();
	};

	var _setAdjustments = function () {
		if (_settings.effect === 'scroll') _settings.circular = true;
	};

	var _setControls = function () {
		if (!_settings.controls) return;

		var dots = $('<nav class="dots">');

		_settings.slides.forEach(function (item, key) {
			var dot = $('<a>').attr({
				'href': item.url,
				'target': item.target,
				'title': item.title,
				'slide': key
			}).addClass(key === 0 ? 'active' : '').click(function (e) {
				e.preventDefault();
				_openSlide(key);
			});
			dots.append(dot);
		});

		var arrowPrev = $('<a>').attr({
			'href': '#'
		}).addClass('arrow prev').click(function (e) {
			e.preventDefault();
			var key = (_currentSlide === 0) ? _settings.slides.length - 1 : _currentSlide - 1;
			_openSlide(key);
		});

		var arrowNext = $('<a>').attr({
			'href': '#'
		}).addClass('arrow next').click(function (e) {
			e.preventDefault();
			var key = (_settings.slides.length - 1 === _currentSlide) ? 0 : _currentSlide + 1;
			_openSlide(key);
		});

		_element.append(arrowPrev);
		_element.append(dots);
		_element.append(arrowNext);

		// Stop automatic slider if you enter your mouse cursor
		_element.mouseenter(_clearTimer).mouseleave(_setTimer);
	};

	var _createSlide = function (item, key, slideWidth) {
		var title = $('<h1>').html(item.title);
		var text = $('<p>').html(item.text);
		var content = $('<div>').addClass('content-text');
		var slide = $('<div>').attr({
			'slide': key
		});

		if (item.href !== '') {
			slide = $('<a>').attr({
				'href': item.url,
				'target': item.target || '',
				'slide': key
			});
		}

		content.append(title).append(text);
		slide.append(content);
		return slide.css({
			'backgroundImage': 'url("' + item.bg + '")',
			'width': slideWidth + '%'
		}).addClass('slide').addClass(key === 0 ? 'active' : '');
	};

	var _setSlides = function () {
		// For a nice clean scroll effect we need to have a duplicate first slide at the end of all slides
		//var totalSlides = (_settings.effect === 'scroll') ? _settings.slides.length + 1 : _settings.slides.length;
		var totalSlides = _settings.slides.length;
		var slideWidth = 100 / totalSlides;
		var wrapper = _element.find('.wrapper').css({
			'width': totalSlides + '00%'
		});

		_settings.slides.forEach(function (item, key) {
			wrapper.append(_createSlide(item, key, slideWidth));
		});

		// For a nice clean scroll effect we need to have a duplicate first slide at the end of all slides
		/*
		if (_settings.effect === 'scroll') {
		    wrapper.append(_createSlide(_settings.slides[0], 0, slideWidth));
		}*/
	};

	var _openSlide = function (key) {

		_clearTimer();

		_element.find('.active').removeClass('active');
		_element.find('[slide=' + key + ']').addClass('active');

		_element.find('.wrapper').css({
			'left': '-' + key + '00%'
		});

		_currentSlide = key;

		_setTimer();
	};
	var _clearTimer = function () {
		window.clearTimeout(_timeout);
	};

	var _setTimer = function () {
		_clearTimer();
		if (_settings.timeout > 0 && _settings.auto) _timeout = window.setTimeout(_openSlideTimer, _settings.timeout * 1000);
	};

	var _openSlideTimer = function () {
		var key = (_settings.slides.length - 1 === _currentSlide) ? 0 : _currentSlide + 1;
		_openSlide(key);
	};

	var _start = function () {
		_setTimer();
	};

	return {
		init: function (element) {
			_init(element, element.settings);
		}
	}
})();

$(document).ready(function () {
	$('.content-carousel').each(function (index, item) {
		carousel.init(item);
	});
});