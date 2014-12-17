$(document).ready(function () {

    var products = $(".product");

    $(".categories-dd").change(function () {
        var value = $(this).val();
        products.hide();

        products.each(function (index) {

            if (!value) {
                products.show();

                return false;
            }

            if (value === $(this).attr("id")) {
                $(this).show();
            }

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
        });
        return false;
    }

    $(".body-content").on("click", ".ajax-pagination a", getPage);

    Dropzone.options.dropzoneForm = { // The camelized version of the ID of the form element

        // The configuration we've talked about above
        autoProcessQueue: true,
        uploadMultiple: true,
        parallelUploads: 100,
        maxFiles: 4,

        // The setting up of the dropzone
        init: function () {
            var myDropzone = this;

            // Listen to the sendingmultiple event. In this case, it's the sendingmultiple event instead
            // of the sending event because uploadMultiple is set to true.
            this.on("sendingmultiple", function () {
                // Gets triggered when the form is actually being sent.
                // Hide the success button or the complete form.
            });
            this.on("successmultiple", function (files, response) {
                // Gets triggered when the files have successfully been sent.
                // Redirect user or notify of success.
            });
            this.on("errormultiple", function (files, response) {
                // Gets triggered when there was an error sending the files.
                // Maybe show form again, and notify user of error
                alert('Maximum files: 4');
            });
            this.on("success", function (file, data) {
                if (this.getUploadingFiles().length === 0 && this.getQueuedFiles().length === 0) {
                    $("div .form-horizontal").prepend(data);
                }
            });
        }
    }
});