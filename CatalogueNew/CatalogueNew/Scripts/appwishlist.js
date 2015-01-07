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
        }
    });

    if (wishlistID !== '0') {
        wishlistButton.val("In Your Wishlist");
    }

    wishlistButton.mouseenter(function () {
        if (wishlistID !== '0') {
            wishlistButton.val("Remove From Wishlist");
        }
    });

    wishlistButton.mouseleave(function () {
        if (wishlistID !== '0') {
            wishlistButton.val("In Your Wishlist");
        }
    });   
});
