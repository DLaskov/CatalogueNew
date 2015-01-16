$(document).ready(function () {
    wishlistButtonBehavior.productDetails();
    wishlistButtonBehavior.wishlistPage();
});

var wishlistButtonBehavior = (function () {

    var wishlistButton = $('#add-wishlist');
    var wishlist = $('#has-wishlist');
    var wishlistID = wishlist.val();
    var wishlistRemove = $('#documentsList');

    function inProductDetails() {

        wishlistButton.click(function () {
            if (wishlistID == 0) {
                wishlistButton.attr("disabled", "disabled");
                wishlistButton.val('Loading...');
                $.post("AddToWishlist", { data: $("#product-id").val() }, function (data, textStatus, jqXHR) {
                    wishlistID = data.Message;
                    wishlistButton.removeAttr("disabled");
                    wishlistButton.val("Remove From Wishlist");
                    wishlistButton.addClass("btn btn-sm btn-danger");
                });
            }
            else {
                wishlistButton.attr("disabled", "disabled");
                wishlistButton.val('Loading...');
                $.post("RemoveFromWishlist", { data: wishlistID }, function (data, textStatus, jqXHR) {
                    wishlist.val('0');
                    wishlistID = '0';
                    wishlistButton.removeAttr("disabled");
                    wishlistButton.val('Add To Wishlist');
                    wishlistButton.removeClass("btn btn-sm btn-danger")
                    wishlistButton.removeClass("btn btn-sm btn-warning")
                    wishlistButton.addClass("btn btn-sm btn-success");
                });

            }
        });

        if (wishlistID !== '0') {
            wishlistButton.val("In Your Wishlist");
            wishlistButton.addClass("btn btn-sm btn-warning")
        }

        wishlistButton.mouseenter(function () {
            if (wishlistID !== '0') {
                wishlistButton.val("Remove From Wishlist");
                wishlistButton.removeClass("btn btn-sm btn-warning");
                wishlistButton.removeClass("btn btn-sm btn-success");
                wishlistButton.addClass("btn btn-sm btn-danger")
            }
        });

        wishlistButton.mouseleave(function () {
            if (wishlistID !== '0') {
                wishlistButton.val("In Your Wishlist");
                wishlistButton.removeClass("btn btn-sm btn-danger");
                wishlistButton.addClass("btn btn-sm btn-warning");
            }
        });
    }   

    function inWishlist() {

        $(document).on('click', '.wish',
            function (event) {
            $("#load").removeAttr('style');
            var name = $(this).closest('.wish').val();
            var currentPage = $("#current-page").val();
            $.post("Wishlist/RemoveFromWishlist", { data: name, page: currentPage },
                function (result) {

                    var a = $(".ajax-pagination a");
                    var currentPage = result.Page;
                    var options =
                        {
                            url: "?page=" + currentPage,
                            data: $("form").serialize(),
                            type: "get"
                        };

                    $.ajax(options).done(function (data) {
                        var target = a.parents(".ajax-pagination").attr("data-devtest-target");
                        $(target).replaceWith(data);
                        products = $('.product');
                        history.pushState(null, null, options.url);
                    });
                    $("#load").attr('style', 'display: none');
                });
        });
    }

    return {
        productDetails: inProductDetails,
        wishlistPage: inWishlist
    }

})();



