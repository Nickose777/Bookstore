function editBook(encryptedId) {
    $.ajax({
        url: "/Book/Get/" + encryptedId,
        dataType: "html",
        type: "GET",
        success: function (html) {
            $("#modalBody").html(html);
            $("#modal").modal();
        },
        error: function (error) {
            alert(error.statusText + " (" + error.status + ")");
        }
    });
}