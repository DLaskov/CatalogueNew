var wishlistButton = $('#add-wishlist');
var wishlist = $('#has-wishlist');
var wishlistID = wishlist.val();
var wishlistRemove = $('#documentsList');

$(document).ready(function () {

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
            wishlistButton.toggleClass("btn btn-sm btn-danger")
        }
    });

    wishlistButton.mouseleave(function () {
        if (wishlistID !== '0') {
            wishlistButton.val("In Your Wishlist");
            wishlistButton.removeClass("btn btn-sm btn-danger");
            wishlistButton.addClass("btn btn-sm btn-warning");
        }
    });

    wishlistRemove.on('click', '.wish',
        function (event) {
        $("#load").removeAttr('style');
        var name = $(event.target).closest('button').val();
        $.post("RemoveFromWishlist", { data: name },
            function (result) {
                wishlistRemove.load('Index', { page: getUrlParameter('page') });
                $("#load").attr('style', 'display: none');
            });
       
    });

    function getUrlParameter(sParam) {
        var sPageURL = window.location.search.substring(2);
        var sURLVariables = sPageURL.split('?');
        for (var i = 0; i < sURLVariables.length; i++) {
            var sParameterName = sURLVariables[i].split('=');
            if (sParameterName[0] == sParam) {
                return sParameterName[1];
            }
        }
    }

});
