$(document).ready(function () {
    'use strict'
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
    if ($('div .details').is('.details')) {
        var isAuth = $("[name='is-auth']").val();

        if (isAuth == 'false') {
            $('#input-rating').data('readonly', 'true');
        }

        getCommentsByProduct();
    }
    $('.bxslider').bxSlider({
        pagerCustom: '#bx-pager',
        height: 265,
        adaptiveHeight: true
    });
    function getCommentsByProduct() {
        var productId = $("#product-id").val();

        $.ajax({
            url: 'http://localhost:38006/api/Comments?productId=' + productId,
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                $("#comments .media-body").remove();
                $("#comments hr").remove();
                renderComments(data)
            }
        });
    }

    function renderComments(data, parentId) {
        $.each(data, function (index, comment) {

            if (comment.ParentComment.ParentCommentID == null) {
                var innerHtml = renderBodyComment(index);
                $("#comments").append(innerHtml);

                renderData(index, comment);
            }
            else {
                var innerHtml = renderBodyComment(index, parentId);
                var commentId = $('input[name="comment-id"][value="' + parentId + '"]');
                var commentIdVal = +commentId.val();
                var innerHtmlId = parentId + '-' + index;

                if (commentIdVal === parentId) {
                    $(commentId.closest('.media-body').append(innerHtml));
                }

                renderData(innerHtmlId, comment);
            }

            if (comment.ChildrenComments.length > 0) {
                renderComments(comment.ChildrenComments, comment.ParentComment.CommentID)
            }

        });
    }

    function renderData(index, comment) {
        var timeStamp = convertUTCDateToLocalDate(new Date(comment.ParentComment.TimeStamp));
        var date = moment.utc(timeStamp).format('MMMM Do YYYY, h:mm:ss a');

        $("#" + index).find("p").text(comment.ParentComment.Text).html();
        $("#" + index).find("small").html(" &nbsp;" + date);
        $("#" + index).find("h4").prepend(comment.ParentComment.Users.UserName);
        $("#" + index).append('<input type="hidden" name="comment-id" value=' + comment.ParentComment.CommentID + ' />');
        $("#" + index).append('<input type="hidden" name="parent-comment-id" value=' + comment.ParentComment.ParentCommentID + ' />');
    }

    function renderBodyComment(indexParam, parentId) {
        var index = indexParam;
        var divClass = 'comment';

        if (parentId !== undefined) {
            index = parentId + '-' + indexParam;
            divClass = 'child-comment';
        }

        var hasRole = $("[name='has-role']").val();
        var isAuth = $("[name='is-auth']").val();
        var innerHtml = '<hr /><div class="media-body ' + divClass + '" id=' + index + '>';
        var edit = '<div class="panel-body edit"><form id="form-comment">';
        edit += '<textarea id="comment-edit" class="form-control counted" name="comment" placeholder="Add comment about the product:" rows="3" ></textarea>';
        edit += '<h6 class="pull-right" id="counter-edit-' + index + '">1000 characters remaining</h6>';
        edit += '<button id="edit-comment" class="btn btn-info">Edit comment</button> ';
        edit += '<button id="cancel-edit" class="btn btn-default">Cancel</button></form></div>';
        var reply = '<div class="panel-body reply"><form id="form-comment">';
        reply += '<textarea id="comment-reply" class="form-control counted" name="comment" placeholder="Add comment about the product:" rows="3" ></textarea>';
        reply += '<h6 class="pull-right" id="counter-reply-' + index + '">1000 characters remaining</h6>';
        reply += '<button id="submit-reply" class="btn btn-info">Submit reply</button> ';
        reply += '<button id="cancel-reply" class="btn btn-default">Cancel</button></form></div>';

        innerHtml += edit;
        innerHtml += '<h4 class="media-heading"><span class="info"></span><small></small></h4><p class="text-info"></p>';

        if (isAuth === 'true') {
            innerHtml += '<button class="btn btn-info" id="reply">Reply</button> ';

            if (hasRole === 'true') {
                innerHtml += '<button class="btn btn-default" id="edit">Edit</button> ';
                innerHtml += '<button class="btn btn-danger" id="delete">Delete</button>';
            }

            innerHtml += reply;
        }
        innerHtml += '</div>';

        return innerHtml;
    }

    $("#comments").on("click", "#submit-reply", function (e) {
        e.preventDefault();
        var _this = $(this);

        var comment = {
            text: _this.closest('.media-body').find("#comment-reply").val(),
            productId: $("#product-id").val(),
            parentCommentId: _this.closest('.media-body').find('[name="comment-id"]').val()
        };

        var commentText = comment.text;

        if (commentText === '') {
            return false;
        }

        $.ajax({
            url: 'http://localhost:38006/api/Comments',
            type: 'POST',
            data: JSON.stringify(comment),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                getCommentsByProduct();
            },
            error: function () { }
        });
    });

    $("#comments").on("click", "#reply", function (e) {
        e.preventDefault();

        var mediaBody = $(this).closest('.media-body');
        var classReply = mediaBody.find('.reply').first();
        var counterId = mediaBody.attr('id');

        classReply.fadeToggle();
        classReply.find("#comment-reply").charCounter(1000, { container: "#counter-reply-" + counterId });
    });

    $("#comments").on("click", "#cancel-reply", function (e) {
        e.preventDefault();
        $(this).closest('.reply').fadeOut();
    });

    $("#comments").on("click", "#edit", function (e) {
        e.preventDefault();

        var mediaBody = $(this).closest('.media-body');
        var classEdit = mediaBody.find('.edit').first();
        var counterId = mediaBody.attr('id');
        var text = mediaBody.find('p').first().text();

        mediaBody.find('#comment-edit').first().text(text);
        mediaBody.find('.edit').first().fadeToggle();
        classEdit.find("#comment-edit").charCounter(1000, { container: "#counter-edit-" + counterId });
    });

    $("#comments").on("click", "#cancel-edit", function (e) {
        e.preventDefault();
        $(this).closest('.media-body').find('.edit').first().fadeOut();
    });

    $("#comments").on("click", "#edit-comment", function (e) {
        e.preventDefault();
        var _this = $(this);

        var comment = {
            commentID: _this.closest('.media-body').find('[name="comment-id"]').val(),
            text: _this.closest('.edit').find("#comment-edit").val(),
            userId: $("#user-id").val(),
            productId: $("#product-id").val(),
            parentCommentId: _this.closest('.media-body').find('[name="parent-comment-id"]').val()
        };

        var commentText = comment.text;

        if (commentText === '') {
            return false;
        }

        $.ajax({
            url: 'http://localhost:38006/api/Comments',
            type: 'PUT',
            data: JSON.stringify(comment),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                getCommentsByProduct();
            },
            error: function () { }
        });
    });

    $("#comments").on("click", "#delete", function (e) {
        e.preventDefault();
        var commentId = $(this).parent().find("input").val();

        $.ajax({
            url: 'http://localhost:38006/api/Comments?commentId=' + commentId,
            type: 'DELETE',
            dataType: 'json',
            success: function () {
                getCommentsByProduct();
            },
            error: function () { }
        });
    });

    $("#submit-comment").click(function (e) {
        e.preventDefault();
        var comment = {
            text: $("#comment").val(),
            userId: $("#user-id").val(),
            productId: $("#product-id").val()
        };

        $.ajax({
            url: 'http://localhost:38006/api/Comments',
            type: 'POST',
            data: JSON.stringify(comment),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                getCommentsByProduct();
            },
            error: function () { }
        });
    });

    function convertUTCDateToLocalDate(date) {
        var newDate = new Date(date.getTime() + date.getTimezoneOffset() * 60 * 1000);

        var offset = date.getTimezoneOffset() / 60;
        var hours = date.getHours();

        newDate.setHours(hours - offset);

        return newDate;
    }

    function getTimezoneName() {
        timezone = jstz.determine();
        return timezone.name();
    }

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
            window.location.hash = options.url;
        });
        return false;
    }
    $(".img-preview").on("click", function () {
        if (confirm("You are going to delete this image.")) {
            var src = $("img", this).attr("src");
            var id = src.split("=")[1];
            $.post("RemoveImageById", { value: id });
            $(this).remove();
        }
    }
    );

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

    $("#comment").charCounter(1000, { container: "#counter" });

    $('#input-rating').on('rating.change', function (event, value, caption) {

        var rating = {
            value: value,
            productId: $("#product-id").val()
        }

        $.ajax({
            url: 'http://localhost:38006/api/Rating',
            type: 'POST',
            data: JSON.stringify(rating),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                getProductRating(rating.productId);
            },
            error: function () { }
        });
    });

    if ($('div').is('.details')) {
        var productID = $("#product-id").val();
        getProductRating(productID)
    }

    function getProductRating(productID) {

        $.ajax({
            url: 'http://localhost:38006/api/Rating?&productID=' + productID,
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                $('#input-rating').rating('update', data);
            }
        });
    };
});

(function ($) {
    /**
     * attaches a character counter to each textarea element in the jQuery object
     * usage: $("#myTextArea").charCounter(max, settings);
     */

    $.fn.charCounter = function (max, settings) {
        max = max || 100;
        settings = $.extend({
            container: "<span></span>",
            classname: "charcounter",
            format: "(%1 characters remaining)",
            pulse: true,
            delay: 0
        }, settings);
        var p, timeout;

        function count(el, container) {
            el = $(el);
            if (el.val().length > max) {
                el.val(el.val().substring(0, max));
                if (settings.pulse && !p) {
                    pulse(container, true);
                };
            };
            if (settings.delay > 0) {
                if (timeout) {
                    window.clearTimeout(timeout);
                }
                timeout = window.setTimeout(function () {
                    container.html(settings.format.replace(/%1/, (max - el.val().length)));
                }, settings.delay);
            } else {
                container.html(settings.format.replace(/%1/, (max - el.val().length)));
            }
        };

        function pulse(el, again) {
            if (p) {
                window.clearTimeout(p);
                p = null;
            };
            el.animate({ opacity: 0.1 }, 100, function () {
                $(this).animate({ opacity: 1.0 }, 100);
            });
            if (again) {
                p = window.setTimeout(function () { pulse(el) }, 200);
            };
        };

        return this.each(function () {
            var container;
            if (!settings.container.match(/^<.+>$/)) {
                // use existing element to hold counter message
                container = $(settings.container);
            } else {
                // append element to hold counter message (clean up old element first)
                $(this).next("." + settings.classname).remove();
                container = $(settings.container)
                                .insertAfter(this)
                                .addClass(settings.classname);
            }
            $(this)
                .unbind(".charCounter")
                .bind("keydown.charCounter", function () { count(this, container); })
                .bind("keypress.charCounter", function () { count(this, container); })
                .bind("keyup.charCounter", function () { count(this, container); })
                .bind("focus.charCounter", function () { count(this, container); })
                .bind("mouseover.charCounter", function () { count(this, container); })
                .bind("mouseout.charCounter", function () { count(this, container); })
                .bind("paste.charCounter", function () {
                    var me = this;
                    setTimeout(function () { count(me, container); }, 10);
                });
            if (this.addEventListener) {
                this.addEventListener('input', function () { count(this, container); }, false);
            };
            count(this, container);
        });
    };

})(jQuery);
