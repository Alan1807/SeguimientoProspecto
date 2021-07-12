function hideShowLoader(show) {
    if (show)
        $('#divLoader').css('display', 'block');
    else
        $('#divLoader').css('display', 'none');
}

function showNotification(Titulo, Descripcion, Tipo) {
    $(".notification").removeClass("alert-success");
    $(".notification").removeClass("alert-danger");
    $(".notification").removeClass("alert-warning");

    $('#hTituloNoti').text(Titulo);
    $('#pDescNoti').html(Descripcion);

    switch (Tipo) {
        case "success":
            $(".notification").addClass("alert-success");
            break;
        case "warning":
            $(".notification").addClass("alert-warning");
            break;
        case "danger":
            $(".notification").addClass("alert-danger");
            break;
    }

    $(".notification").removeClass("notification-hide");
    $(".notification").addClass("notification-show");
    $(".notification").removeClass("d-none");

    setTimeout(function () {
        $(".notification").addClass("notification-hide");
        $(".notification").removeClass("notification-show");
        $(".notification").addClass("d-none");
    }, 5000);
}