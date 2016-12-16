var formValidate = (function() {
    'use strict';

    var _forms = $('form:not([novalidate])');
    var _body = $('body');
    var _win = $(window);

    var _init = function() {
        _setValidation();
    };

    var _setValidation = function() {
        _forms.each(function(key, form) {
            _setFormElements(form);
            _setValidityTrigger(form);
            //_setCustomError(form);
            // _changeSubmit();
            _onSubmit(form);
        });
    };

    var _setFormElements = function(form) {
        form._validate = { inputs: [], isValid: false };

        form._validate.inputs = $(form)
            .find('input:not([type=hidden]),textarea,select')
            .map(function(key, element) {
                _setDefaultPattern(element);
                return (!element.formNoValidate && element.required && element.willValidate) ? element : null;
            });

        console.log(form._validate.inputs);
    };

    var _setDefaultPattern = function(element) {
        if (element.type === 'email' && !element.pattern) element.pattern = '^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$';
        if (element.type === 'url' && !element.pattern) element.pattern = '[a-z][\-\.+a-z]*';
    };

    var _setValidityTrigger = function(form) {
        $(form._validate.inputs).on('blur', _checkCustomError).on('keyup', _checkCustomError);
    };

    var _getTypeTranslation = function(type) {
        var text;

        switch (type) {
            case "email":
                text = Translate.FormInputTypeEmail;
                break;
            case "tel":
                text = Translate.FormInputTypeTel;
                break;
            case "url":
                text = Translate.FormInputTypeUrl;
                break;
            case "date":
                text = Translate.FormInputTypeDate;
                break;
            case "time":
                text = Translate.FormInputTypeTime;
                break;
            case "number":
                text = Translate.FormInputTypeNumber;
                break;
            default:
                text = type;
        }

        return text;
    };

    var _checkCustomError = function(e) {
        var errorMessage = '';
        var lngt = 0;
        var element = e.target || e.srcElement;
        var title = element.title.capitalize();
        var type = _getTypeTranslation(element.type);

        if (!element.validity.valid) {
            if (element.validity.badInput) {
                errorMessage += "\r\n" + Translate.FormInputErrorBadInput.format(title);
                lngt++;
            }
            if (element.validity.patternMismatch) {
                errorMessage += "\r\n" + Translate.FormInputErrorPatternMismatch.format(title, type);
                lngt++;
            }
            if (element.validity.rangeOverflow) {
                errorMessage += "\r\n" + Translate.FormInputErrorRangeOverflow.format(title);
                lngt++;
            }
            if (element.validity.rangeUnderflow) {
                errorMessage += "\r\n" + Translate.FormInputErrorRangeUnderflow.format(title);
                lngt++;
            }
            if (element.validity.stepMismatch) {
                errorMessage += "\r\n" + Translate.FormInputErrorStepMismatch.format(title);
                lngt++;
            }
            if (element.validity.tooLong) {
                errorMessage += "\r\n" + Translate.FormInputErrorTooLong.format(title);
                lngt++;
            }
            if (element.validity.tooShort) {
                errorMessage += "\r\n" + Translate.FormInputErrorTooShort.format(title);
                lngt++;
            }
            if (element.validity.typeMismatch) {
                errorMessage += "\r\n" + Translate.FormInputErrorTypeMismatch.format(title, type);
                lngt++;
            }
            if (element.validity.valueMissing) {
                errorMessage += "\r\n" + Translate.FormInputErrorValueMissing.format(title);
                lngt++;
            }

            if (lngt > 1) errorMessage = errorMessage.replace(/\r\n/ig, "\r\n-");

            element.setCustomValidity(errorMessage.trim());
        }
    };

    var _checkFormSubmit = function(e) {
        if (!this.checkValidity()) {
            $(this._validate.inputs)
                .each(function(index, item) {
                    if (!item.validity.valid) _checkCustomError({ target: item });
                });
        }
    };

    var _onSubmit = function(form) {
        var button = $(form).offsetParent().find("button[type=submit]:not([disabled])");

        // trigger ajax sumbit when we use a script (bookmark) to login
        form.submit = function() { $(this).trigger('submit'); };

        button.on('click', _checkFormSubmit.bind(form));

        // Change submit button style
        $(form).submit(function(e) {
            // Use parent for standard modal buttons that are outside the form.
            if (form.checkValidity()) button.addClass("submit").attr("disabled", "true");
        });
    };

    return {
        init: function() {
            _init();
        }
    }
})();

$(document).ready(function() {
    formValidate.init();
});