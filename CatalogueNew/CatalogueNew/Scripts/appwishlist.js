var wishlistButton = $('#add-wishlist');
var wishlist = $('#has-wishlist');
var wishlistID = wishlist.val();

$(document).ready(function () {

    wishlistButton.click(function () {

        if (wishlistID == 0) {
            $.post("AddToWishlist", { data: $("#product-id").val() }, function (data, textStatus, jqXHR) { wishlistID = data.Message; });

            wishlistButton.val("Added");        
        }
        else {
            $.post("RemoveFromWishlist", { data: wishlistID });
            wishlist.val('0');
            wishlistID = '0';
            wishlistButton.val('+ Add To Wishlist');
            wishlistButton.removeClass("btn btn-sm btn-danger")
            wishlistButton.removeClass("btn btn-sm btn-warning")
            wishlistButton.addClass("btn btn-sm btn-success");
        }
    });

    if (wishlistID !== '0') {
        wishlistButton.val("In Your Wishlist");
        wishlistButton.addClass("btn btn-sm btn-warning")
    }

    wishlistButton.mouseenter(function () {
        if (wishlistID !== '0') {
            wishlistButton.val("Remove From Wishlist");
            wishlistButton.addClass("btn btn-sm btn-danger");
        }
    });

    wishlistButton.mouseleave(function () {
        if (wishlistID !== '0') {
            wishlistButton.val("In Your Wishlist");
            wishlistButton.removeClass("btn btn-sm btn-danger");
            wishlistButton.addClass("btn btn-sm btn-warning");
        }
    });   
});
