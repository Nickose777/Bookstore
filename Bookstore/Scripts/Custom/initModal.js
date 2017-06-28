$(document).ready(function () {
    $("#modalBody").on("submit", "#form-edit-book", function (e) {
        e.preventDefault();

        var form = $("#form-edit-book");
        $.ajax({
            url: form.attr("action"),
            method: form.attr("method"),
            data: form.serialize(),
            dataType: "html",
            success: function (html) {
                $("#modalBody").html(html);
            }
        });
    });

    $(document).on("hide.bs.modal", "#modal", function () {
        location.reload(true);
    });
});