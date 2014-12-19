$(document).ready(function () {

    $.getJSON('http://localhost:38006/api/Comments',
             function (Data) {
                 $.each(Data, function (key, val) {
                     $("#comments").html("<div>" + val.text + "</div>");
                 });
             });

    //function GetCommentsByProduct() {
    //    jQuery.support.cors = true;
    //    $.ajax({
    //        url: 'http://localhost:38006/api/Comments',
    //        type: 'GET',
    //        dataType: 'json',
    //        success: function (data) {
    //            WriteResponse(data);   
    //        },
    //        error: function (x, y, z) {
    //            alert(x + '\n' + y + '\n' + z);
    //        }
    //    });
    //};

    //function WriteResponse(comments) {
    //    if (comments !== null) {
    //        var strResult = "<div class='media-body'><h4 class='media-heading'><span class='info'>";
    //        $.each(comments, function (index, comments) {
    //            strResult += comments.timeStamp + "</span></h4>" + "<p class='comment-text'>" + comments.text + "</p><hr /></div>"
    //        });
    //        $("#comments").html(strResult);
    //    }
    //    else {
    //        $("#comments").html("No comments to display");
    //    }
    //}

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
            success: function (data) { },
            error: function () { alert('error'); }
        });
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
            products = $('.product');
            window.location.hash = options.url;
        });
        return false;
    }

    $(".body-content").on("click", ".ajax-pagination a", getPage);

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
});