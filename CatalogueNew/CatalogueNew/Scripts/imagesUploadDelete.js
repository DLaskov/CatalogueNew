$(".img-preview").on("click", function () {
    if (confirm("You are going to delete this image.")) {
        var src = $("img", this).attr("src");
        var id = src.split("=")[1];
        $.post("../RemoveImageById", { value: id });
        $(this).remove();
    }
});

//Dropzone initialize

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