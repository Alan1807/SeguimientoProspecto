function validaRechazo() {
    debugger;
    var rechazo = $('#txtObservacionesRechazo').val();

    if (rechazo == "") {
        showNotification("Faltan observaciones", "Para rechazar es necesario capturar observaciones de rechazo", "warning");
        return false;
    }

    hideShowLoader(true);
    return true;
}