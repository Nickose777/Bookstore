function editBook(encryptedId) {
    $.ajax({
        url: "/Book/Get/" + encryptedId,
        dataType: "html",
        type: "GET",
        success: function (data) {
            $("#modalContainer").html(data);
            $("#modal").modal();
        },
        error: function (error) {
            alert(error.statusText + " (" + error.status + ")");
        }
    });
}