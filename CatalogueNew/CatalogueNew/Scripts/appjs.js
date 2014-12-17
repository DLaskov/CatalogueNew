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

    //Dropzone.options.dropzoneForm = { // The camelized version of the ID of the form element

    //    // The configuration we've talked about above
    //    autoProcessQueue: true,
    //    uploadMultiple: true,
    //    parallelUploads: 100,
    //    maxFiles: 100,

    //    // The setting up of the dropzone
    //    init: function () {
    //        var myDropzone = this;

    //        // First change the button to actually tell Dropzone to process the queue.
    //        //this.element.querySelector("button[type=submit]").addEventListener("click", function (e) {
    //        //    // Make sure that the form isn't actually being sent.
    //        //    e.preventDefault();
    //        //    e.stopPropagation();
    //        //    myDropzone.processQueue();
    //        //});

    //        // Listen to the sendingmultiple event. In this case, it's the sendingmultiple event instead
    //        // of the sending event because uploadMultiple is set to true.
    //        this.on("sendingmultiple", function () {
    //            // Gets triggered when the form is actually being sent.
    //            // Hide the success button or the complete form.
    //        });
    //        this.on("successmultiple", function (files, response) {
    //            // Gets triggered when the files have successfully been sent.
    //            // Redirect user or notify of success.
    //        });
    //        this.on("errormultiple", function (files, response) {
    //            // Gets triggered when there was an error sending the files.
    //            // Maybe show form again, and notify user of error
    //            alert('errormultiple');
    //        });
    //        this.on("success", function (file, data) {
    //            alert(data);
    //        });
    //    }
    //}

    ////File Upload response from the server
    //Dropzone.options.dropzoneForm = {
    //    autoProcessQueue: false,
    //    maxFiles: 6,
    //    init: function () {
    //        this.on("maxfilesexceeded", function (data) {
    //            var res = eval('(' + data.xhr.responseText + ')');

    //        });
    //        this.on("addedfile", function (file) {

    //            // Create the remove button
    //            var removeButton = Dropzone.createElement("<button>Remove file</button>");

    //            // Capture the Dropzone instance as closure.
    //            var _this = this;

    //            // Listen to the click event
    //            removeButton.addEventListener("click", function (e) {
    //                // Make sure the button click doesn't submit the form:
    //                e.preventDefault();
    //                e.stopPropagation();
    //                // Remove the file preview.
    //                _this.removeFile(file);
    //                // If you want to the delete the file on the server as well,
    //                // you can do the AJAX request here.
    //            });

    //            var submitButton = document.querySelector("#submit-all")
    //            myDropzone = this; // closure

    //            submitButton.addEventListener("click", function () {
    //                myDropzone.processQueue(); // Tell Dropzone to process all queued files.
    //            });

    //            // You might want to show the submit button only when
    //            // files are dropped here:
    //            this.on("addedfile", function () {
    //                // Show submit button here and/or inform user to click it.
    //            });
    //            // Add the button to the file preview element.
    //            file.previewElement.appendChild(removeButton);
    //        });
    //    }
    //};
});