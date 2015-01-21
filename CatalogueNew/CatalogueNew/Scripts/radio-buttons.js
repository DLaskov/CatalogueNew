$(document).ready(function () {
    'use strict'
    radioButtonsModule.setGenderValue();
})

var radioButtonsModule = (function () {
    var $female = $('#female');
    var $male = $('#male');

    function setGenderValue() {
        if ($female.hasClass('active')) {
            $('#gender').val('female');
        }

        if ($male.hasClass('active')) {
            $('#gender').val('male');
        }
    }

    $('#radioBtn a').on('click', function () {
        var $this = $(this);
        var sel = $this.data('title');
        var tog = $this.data('toggle');
        $('#' + tog).prop('value', sel);

        $('a[data-toggle="' + tog + '"]').not('[data-title="' + sel + '"]').removeClass('active').addClass('notActive');
        $('a[data-toggle="' + tog + '"][data-title="' + sel + '"]').removeClass('notActive').addClass('active');

        if ($this.hasClass('active')) {
            if ($this.attr('id') == 'female') {
                $this.addClass('btn-danger');
                $male.removeClass('btn-primary').addClass('btn-default');
            }

            if ($this.attr('id') == 'male') {
                $this.addClass('btn-primary');
                $female.removeClass('btn-danger').addClass('btn-default');
            }
        }
    })

    return {
        setGenderValue: setGenderValue
    }
})();