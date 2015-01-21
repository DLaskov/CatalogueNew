$(document).ready(function () {
    'use strict'
    var productID = ratingRevealingModule.productID;
    ratingRevealingModule.productRating(productID);
});

var ratingRevealingModule = (function () {
    'use strict'
    var $isAuth = $("[name='is-auth']").val();
    var $productID = $("#product-id").val();
    var $inputRating = $("#input-rating");

    if ($isAuth == 'false') {
        $inputRating.rating('refresh', { disabled: true });
    }

    $inputRating.on('rating.change', function (event, value, caption) {
        var rating = {
            value: value,
            productId: $productID
        }

        $.ajax({
            url: '/api/Rating',
            type: 'POST',
            data: JSON.stringify(rating),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                getProductRating(rating.productId);
            },
            error: function () { }
        });
    });

    function getProductRating(productID) {
        $.ajax({
            url: '/api/Rating?&productID=' + productID,
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                $inputRating.rating('update', data.TotalRating);
                if (data.UserRating !== 0) {
                    $inputRating.rating('refresh', { disabled: true });
                    $('#rating-id').append(' You gave a rating of ' + data.UserRating + ' star(s)');
                }
            }
        });
    };

    return {
        productRating: getProductRating,
        productID: $productID
    }
})();
