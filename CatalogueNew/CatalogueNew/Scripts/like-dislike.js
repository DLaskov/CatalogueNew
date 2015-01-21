$(document).ready(function () {
    'use strict'
    likeRevealingModule.likesDislikes();
});

var likeRevealingModule = (function () {
    var $productID = $("#product-id").val();
    var isAuth = $("[name='is-auth']").val();

    $('#like').click(function (e) {
        e.preventDefault();

        if (isAuth === 'false') {
            alert('Log in please!');
            return false;
        }

        var _this = $(this);
        var like = {
            isLike: true,
            productID: $productID
        }

        $.ajax({
            url: '/api/LikeDislike',
            type: 'POST',
            data: JSON.stringify(like),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                getLikesDislikes();
            },
            error: function () { }
        });
    });

    $('#dislike').click(function (e) {
        e.preventDefault();

        if (isAuth === 'false') {
            alert('Log in please!');
            return false;
        }

        var _this = $(this);
        var dislike = {
            isLike: false,
            productID: $productID
        }

        $.ajax({
            url: '/api/LikeDislike',
            type: 'POST',
            data: JSON.stringify(dislike),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                getLikesDislikes();
            },
            error: function () { }
        });
    });

    function getLikesDislikes() {
        $.ajax({
            url: '/api/LikeDislike?&productID=' + $productID,
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                buttonKind(data);
            }
        });
    }

    function buttonKind(data) {
        $('#like').text(' Like | ' + data.LikesCount);
        $('#dislike').text(' Dislike | ' + data.DislikesCount);

        if (data.IsLike !== null) {
            if (data.IsLike === true) {
                $('#dislike').removeClass('btn-danger');
                $('#like').addClass('btn-info');
            }
            else {
                $('#like').removeClass('btn-info');
                $('#dislike').addClass('btn-danger');
            }
        }
    }

    return {
        productID: $productID,
        likesDislikes: getLikesDislikes
    }
})();