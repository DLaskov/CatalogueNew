$(document).ready(function () {

    getCommentsByProduct();

    function renderBodyComment(indexParam, parentId) {
        'use strict'
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
        var replay = '<div class="panel-body replay"><form id="form-comment">';
        replay += '<textarea id="comment-replay" class="form-control counted" name="comment" placeholder="Add comment about the product:" rows="3" ></textarea>';
        replay += '<h6 class="pull-right" id="counter-replay-' + index + '">1000 characters remaining</h6>';
        replay += '<button id="submit-replay" class="btn btn-info">Submit replay</button> ';
        replay += '<button id="cancel-replay" class="btn btn-default">Cancel</button></form></div>';

        innerHtml += edit;
        innerHtml += '<h4 class="media-heading"><span class="info"></span><small></small></h4><p class="text-info"></p>';

        if (isAuth === 'true') {
            innerHtml += '<button class="btn btn-info" id="replay">Replay</button> ';

            if (hasRole === 'true') {
                innerHtml += '<button class="btn btn-default" id="edit">Edit</button> ';
                innerHtml += '<button class="btn btn-danger" id="delete">Delete</button>';
            }

            innerHtml += replay;
        }
        innerHtml += '</div>';

        return innerHtml;
    }

    function getCommentsByProduct() {
        'use strict'
        $.ajax({
            url: 'http://localhost:38006/api/Comments?productId=' + $("#product-id").val(),
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                $("#comments .media-body").remove();
                $("#comments hr").remove();

                $.each(data, function (index, comments) {
                    var innerHtml = renderBodyComment(index);
                    $("#comments").append(innerHtml);

                    $.each(comments, function (key, element) {
                        if (key === 'Text') {
                            $("#" + index).find("p").html(element);
                        }
                        else if (key === 'TimeStamp') {
                            var temp = convertUTCDateToLocalDate(new Date(element));
                            var date = moment.utc(temp).format('MMMM Do YYYY, h:mm:ss a');
                            $("#" + index).find("small").html(" &nbsp;" + date);
                        }
                        else if (key === 'Users') {
                            $.each(element, function (key, element) {
                                if (key === 'UserName') {
                                    $("#" + index).find("h4").prepend(element);
                                }
                            });
                        }
                        else if (key === 'CommentID') {
                            $("#" + index).append('<input type="hidden" name="comment-id" value=' + element + ' />');
                            getCommentsByParent(element);
                        }
                        else if (key === 'ParentCommentID') {
                            $("#" + index).append('<input type="hidden" name="parent-comment-id" value=' + element + ' />');
                        }
                    });
                });
            },
            error: function (x, y, z) {
            }
        });
    }

    function getCommentsByParent(parentId) {
        'use strict'
        $.ajax({
            url: 'http://localhost:38006/api/Comments?parentId=' + parentId,
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                var strParentId = parentId.toString();

                $.each(data, function (index, comments) {
                    var innerHtml = renderBodyComment(index, strParentId);
                    var commentId = $('input[name="comment-id"][value="' + parentId + '"]');

                    if (commentId.val() === strParentId) {
                        $(commentId.closest('.media-body').append(innerHtml));
                    }

                    $.each(comments, function (key, element) {
                        var innerHtmlId = parentId + '-' + index;
                        if (key === 'Text') {
                            $("#" + innerHtmlId).find("p").html(element);
                        }
                        else if (key === 'TimeStamp') {
                            var temp = convertUTCDateToLocalDate(new Date(element));
                            var date = moment.utc(temp).format('MMMM Do YYYY, h:mm:ss a');
                            $("#" + innerHtmlId).find("small").html(" &nbsp;" + date);
                        }
                        else if (key === 'Users') {
                            $.each(element, function (key, element) {
                                if (key === 'UserName') {
                                    $("#" + innerHtmlId).find("h4").prepend(element);
                                }
                            });
                        }
                        else if (key === 'CommentID') {
                            $("#" + innerHtmlId).append('<input type="hidden" name="comment-id" value=' + element + ' />');
                            getCommentsByParent(element);
                        }
                        else if (key === 'ParentCommentID') {
                            $("#" + innerHtmlId).append('<input type="hidden" name="parent-comment-id" value=' + element + ' />');
                        }
                    });
                });
            },
            error: function (x, y, z) {
            }
        });
    }

    $("#comments").on("click", "#submit-replay", function (e) {
        e.preventDefault();
        var _this = $(this);

        var comment = {
            text: _this.closest('.media-body').find("#comment-replay").val(),
            userId: $("#user-id").val(),
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

    $("#comments").on("click", "#replay", function (e) {
        e.preventDefault();

        var mediaBody = $(this).closest('.media-body');
        var classReplay = mediaBody.find('.replay').first();
        var counterId = mediaBody.attr('id');

        classReplay.fadeToggle();
        classReplay.find("#comment-replay").charCounter(1000, { container: "#counter-replay-" + counterId });
    });

    $("#comments").on("click", "#cancel-replay", function (e) {
        e.preventDefault();
        $(this).closest('.replay').fadeOut();
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
        'use strict'
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
            var src = $(this).attr("src");
            var id = src.split("=")[1];
            $.post("../RemoveImageById", { value: id });
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

    $('#stars').on('starrr:change', function (e, value) {
        //$('#count').html(value);
        var rating = {
            value: value,
            userID: $("#user-id").val(),
            productId: $("#product-id").val()
        }

        $.ajax({
            url: 'http://localhost:38006/api/Rating',
            type: 'POST',
            data: JSON.stringify(rating),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $.each(data, function (key, element) {
                    if (key === 'UserID') {
                        getRatingByUser(element);
                    }
                });
            },
            error: function () { }
        });
    });

    function getRatingByUser(userID) {
        'use strict'


        $.ajax({
            url: 'http://localhost:38006/api/Rating?userID=' + userID + '&productID=' + $("#product-id").val(),
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                $.each(data, function (key, element) {
                    if (key === 'Value') {
                        $('#count').html(element);
                    }
                });
                $('#stars').hide();
                $('#rating').show();
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

// Starrr plugin (https://github.com/dobtco/starrr)
var __slice = [].slice;

(function ($, window) {
    var Starrr;

    Starrr = (function () {
        Starrr.prototype.defaults = {
            rating: void 0,
            numStars: 5,
            change: function (e, value) { }
        };

        function Starrr($el, options) {
            var i, _, _ref,
              _this = this;

            this.options = $.extend({}, this.defaults, options);
            this.$el = $el;
            _ref = this.defaults;
            for (i in _ref) {
                _ = _ref[i];
                if (this.$el.data(i) != null) {
                    this.options[i] = this.$el.data(i);
                }
            }
            this.createStars();
            this.syncRating();
            this.$el.on('mouseover.starrr', 'span', function (e) {
                return _this.syncRating(_this.$el.find('span').index(e.currentTarget) + 1);
            });
            this.$el.on('mouseout.starrr', function () {
                return _this.syncRating();
            });
            this.$el.on('click.starrr', 'span', function (e) {
                return _this.setRating(_this.$el.find('span').index(e.currentTarget) + 1);
            });
            this.$el.on('starrr:change', this.options.change);
        }

        Starrr.prototype.createStars = function () {
            var _i, _ref, _results;

            _results = [];
            for (_i = 1, _ref = this.options.numStars; 1 <= _ref ? _i <= _ref : _i >= _ref; 1 <= _ref ? _i++ : _i--) {
                _results.push(this.$el.append("<span class='glyphicon .glyphicon-star-empty'></span>"));
            }
            return _results;
        };

        Starrr.prototype.setRating = function (rating) {
            if (this.options.rating === rating) {
                rating = void 0;
            }
            this.options.rating = rating;
            this.syncRating();
            return this.$el.trigger('starrr:change', rating);
        };

        Starrr.prototype.syncRating = function (rating) {
            var i, _i, _j, _ref;

            rating || (rating = this.options.rating);
            if (rating) {
                for (i = _i = 0, _ref = rating - 1; 0 <= _ref ? _i <= _ref : _i >= _ref; i = 0 <= _ref ? ++_i : --_i) {
                    this.$el.find('span').eq(i).removeClass('glyphicon-star-empty').addClass('glyphicon-star');
                }
            }
            if (rating && rating < 5) {
                for (i = _j = rating; rating <= 4 ? _j <= 4 : _j >= 4; i = rating <= 4 ? ++_j : --_j) {
                    this.$el.find('span').eq(i).removeClass('glyphicon-star').addClass('glyphicon-star-empty');
                }
            }
            if (!rating) {
                return this.$el.find('span').removeClass('glyphicon-star').addClass('glyphicon-star-empty');
            }
        };

        return Starrr;

    })();
    return $.fn.extend({
        starrr: function () {
            var args, option;

            option = arguments[0], args = 2 <= arguments.length ? __slice.call(arguments, 1) : [];
            return this.each(function () {
                var data;

                data = $(this).data('star-rating');
                if (!data) {
                    $(this).data('star-rating', (data = new Starrr($(this), option)));
                }
                if (typeof option === 'string') {
                    return data[option].apply(data, args);
                }
            });
        }
    });
})(window.jQuery, window);

$(function () {
    return $(".starrr").starrr();
});
