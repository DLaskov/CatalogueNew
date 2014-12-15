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

    var getPage = function () {
        var a = $(this);

        var options =
            {
                url: a.attr("href"),
                data: $("form").serialize(),
                type: "get"
            };

        $.ajax(options).done(function (data) {
            var target = a.parents(".ajax-pagination").attr("data-devtest-target");
            $(target).replaceWith(data);
            products = $('.product');
        });
        return false;
    }

    $(".body-content").on("click", ".ajax-pagination a", getPage);
});