$(document).ready(function () {

    $("#addTagSpan").on("click", tag.inputForTag);
    $("#addTag").on("click", function () {
        var input = $(".tagInput").val();
        if (input == "" || input.length < 3) {
            alert("Tag must contain 3 to 50 letters!");
            return;
        }
        var regex = /^([a-zA-Z]+ ?)+[a-zA-Z]$/;
        if (regex.test(input)) {
            $.find(".tag").forEach(function (element, index, array) {
                if(element.innerText == input) input = "";
            })
            if (input == "") {
                tag.inputForTag();
                return;
            }
            var productID = $("#product-id").val();
            tag.addTag(input, productID);
        } else {
            alert("Invalid tag name!");
        }
    });
    $("div .tags").on("click", ".removeTag", function () {
        if (confirm("You are going to delete this tag.")) {
            tag.removeTag($(this).attr("data-tagID"), $("#product-id").val());
            $(this).remove();
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
                $(".tags").append("<a class='tag' href='/Product/Tag?name=" + value.toLowerCase() + "' data-tagID=" + data.tagID + ">" + value.toLowerCase() + "</a>")
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