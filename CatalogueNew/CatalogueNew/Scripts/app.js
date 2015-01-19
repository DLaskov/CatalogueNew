$(document).ready(function () {

    $("#registerForm").submit(function (event) {
        if ($("#Password").val() != $("#passwordConfirm").val()) {
            $("#passwordConfirm").after("<p style=\"color: red\">Password doesn't match!</p>");
            event.preventDefault();
        }
        if ($("#UserName").val() == $("#Password").val()) {
            $("#Password").after("<p style=\"color: red\">Password must be different from username!</p>");
            event.preventDefault();
        }
        return;
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
            var array = options.url.split('/');
            var lastsegment = array[array.length - 1];
            history.pushState(null, null, "/" + lastsegment);
        });
        return false;
    }

    $(".img-preview").on("click", function () {
        if (confirm("You are going to delete this image.")) {
            var src = $("img", this).attr("src");
            var id = src.split("=")[1];
            $.post("../RemoveImageById", { value: id });
            $(this).remove();
        }
    });

    $(".body-content").on("click", ".ajax-pagination a", getPage);

    function confirmProductDelete() {
        if (!confirm("You are going to delete this product.")) {
            $(this).preventDefault();
        }
    };

    Dropzone.options.dropzoneForm = {

        autoProcessQueue: true,
        uploadMultiple: false,
        parallelUploads: 100,
        maxFiles: 4,

        init: function () {
            var myDropzone = this;
            this.on("maxfilesexceeded", function (data) {
                var res = eval('(' + data.xhr.responseText + ')');

            });

            this.on("addedfile", function (file) {
                var removeButton = Dropzone.createElement("<button>Remove file</button>");
                var _this = this;

                removeButton.addEventListener("click", function (e) {

                    e.preventDefault();
                    e.stopPropagation();

                    _this.removeFile(file);

                    $.post("RemoveImage", { value: document.getElementById(file.UniqueName).value });
                    document.getElementById(file.UniqueName).remove();
                });

                file.previewElement.appendChild(removeButton);
            });

            this.on("sendingmultiple", function () {
            });
            this.on("successmultiple", function (files, response) {
            });
            this.on("errormultiple", function (files, response) {
            });
            this.on("success", function (file, data) {
                file.UniqueName = data.UniqueName;
                var innerHtml = "<input type='hidden' name='FileAttributesCollection' id='" + data.UniqueName +
                    "' value='" + data.UniqueName + "\\" + data.ImgName + "\\" + data.MimeType + "' />"
                $("div .form-horizontal").prepend(innerHtml);
            });
            }
            }

    $("#addTagSpan").on("click", tag.inputForTag);
    $("#addTag").on("click", function () {
        var input = $(".tagInput").val();
        if (input != "") {
            var productID = $("#product-id").val();
            tag.addTag(input, productID);
        }
        });
    $(".removeTag").on("click", function () {
        if (confirm("You are going to delete this tag.")) {
            tag.removeTag($(this).attr("data-tagID"), $("#product-id").val());
            $(this).remove();
        }
    });

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
            $('.dropdown-menu ul').removeAttr('style');
        }
    }
    else {
        if ($('.navbar-brand').data('size') == 'small') {
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
        }
    }
});

});

var tag = (function () {
    function toggleInputForTag() {
        $(".addTagDiv").fadeToggle("slow", function () {
            if ($(".addTagDiv").is(':hidden')) {
                $("#addTagSpan").removeClass("glyphicon-remove");
                $("#addTagSpan").css("color", "#449d44");
                $("#addTagSpan").addClass("glyphicon-plus");
                $(".tagInput").val("");

            } else {
                $("#addTagSpan").removeClass("glyphicon-plus");
                $("#addTagSpan").css("color", "#d9534f");
                $("#addTagSpan").addClass("glyphicon-remove");
            }
        })
    };

    function postTag(value, id) {
        $.post(
            "AddTag",
            { tagName: value, productID: id },
            function (data) {
                toggleInputForTag();
                $(".tags").append("<a href='/Product/Tag?name=" + value.toLowerCase() + "' data-tagID=" + data.tagID + ">" + value.toLowerCase() + "</a>")
                if (data.isInRole == "yes") {
                    $(".tags").append("<span class='glyphicon glyphicon-remove-circle removeTag' data-tagID=" + data.tagID + "></span>")
                }
            }
            );
    };

    function removeTag(tagID, productID) {
        $.post(
            "RemoveTag",
            {
                tagID: tagID,
                productID: productID
            },
            function () {
                $("a[data-tagID=" + tagID + "]").remove();
            }
            );
    }
    return {
        inputForTag: toggleInputForTag,
        addTag: postTag,
        removeTag: removeTag
    }
})();
