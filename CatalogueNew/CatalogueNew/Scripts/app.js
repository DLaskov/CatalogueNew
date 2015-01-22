$(document).ready(function () {

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
            var array = options.url.split('/');
            var lastsegment = array.pop();
            history.pushState(null, null, lastsegment);
        });
        return false;
    };

    $(".body-content").on("click", ".ajax-pagination a", getPage);

    $(function () {
        $('.navbar-brand').data('size', 'big');
    });

    $(window).scroll(function () {
        if ($(document).scrollTop() > 0) {
            if ($('.navbar-brand').data('size') == 'big') {
                $('.navbar-brand').data('size', 'small');
                $('.navbar-brand').stop().animate({
                    height: '0px',
                    padding: '0'
                }, 600);
                $('.navbar').stop().animate({
                    height: '50px'
                }, 600);
                $('.navbar ul').stop().animate({
                    marginTop: '0'
                }, 600);
                $('.navbar-right').stop().animate({
                    marginTop: '0'
                }, 600);
                $('.body-content').stop().animate({
                    marginTop: '40px'
                }, 600);
                $('.dropdown-menu ul').removeAttr('style');


                $(function () {
                    $('.navbar-nav a').hover(function () {
                        $(this).css('height', '5%');
                    },
                    function () {
                        $(this).css('height', '5%');
                    });
                });
            }
        }
        else {
            if ($('.navbar-brand').data('size') == 'small' && $(window).width() > 768) {
                $('.navbar-brand').data('size', 'big');
                $('.navbar-brand').stop().animate({
                    height: '72px'
                }, 600);
                $('.navbar').stop().animate({
                    height: '150px'
                }, 600);
                $('.navbar ul').stop().animate({
                    marginTop: '7%'
                }, 600);
                $('.navbar-right').stop().animate({
                    marginTop: '7%'
                }, 600);
                $('.dropdown-menu').stop().animate({
                    marginTop: '0'
                }, 0);
                $('.body-content').stop().animate({
                    marginTop: '120px'
                }, 600);
                $(function () {
                    $('.main-parent').hover(function () {
                        $(this).css('height', '65px');
                    },
                    function () {
                        $(this).css('height', '65px');
                    });
                });
            }
        }
    });

});
