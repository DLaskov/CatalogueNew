$(document).ready(function () {
    'use strict'

    var products = $(".product");

    $(".categories-dd").change(function () {
        var value = $(this).val();
        products.hide();

        products.each(function (index) {

            if (!value) {
                products.show();

                return false;
            }

            if (value === $(this).attr("id")) {
                $(this).show();
            }

        });
    });
});