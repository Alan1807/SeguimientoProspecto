$(document).ready(function () {
    inicializarEventos();
});

var uploadedFiles = 1;

// INICIALIZA LOS EVENTOS
function inicializarEventos() {
    $('#filupDocumento_ProgressBar').on('DOMSubtreeModified', function () {
        var textoStatusFiles = $('#filupDocumento_ProgressBar').text();
        var porcentaje = textoStatusFiles.slice(9, -2);

        setProgress(porcentaje);
    });

    $('.nombres').each(function () {

        var ctrl = $(this);

        ctrl.on('keypress', function (e) {
            if ((e.which >= 65 && e.which <= 90) || (e.which >= 97 && e.which <= 122) || e.which <= 32)
                return true
            else
                return false
        })

    });

    $('.numeros').each(function () {

        var ctrl = $(this);

        ctrl.on('keypress', function (e) {
            if (e.which >= 48 && e.which <= 57)
                return true
            else
                return false
        })

    });

    $('.telefono').on('keypress', function (e) {
        if ((e.which >= 48 && e.which <= 57) || e.which <= 40 || e.which <= 41 || e.which <= 43)
            return true
        else
            return false
    });

    $('.rfc').on('keypress', function (e) {
        if ((e.which >= 65 && e.which <= 90) || (e.which >= 97 && e.which <= 122) || (e.which >= 48 && e.which <= 57))
            return true
        else
            return false
    });

    $('.filename').on('keypress', function (e) {
        if ((e.which >= 65 && e.which <= 90) || (e.which >= 97 && e.which <= 122) || (e.which >= 48 && e.which <= 57) || e.which == 95 || e.which <= 32)
            return true
        else
            return false
    });
}

// ABRE LA VENTANA PARA SELECCIONAR ARCHIVO
function btnSeleccionaArchivo_OnClick() {
    if ($('#txtNombreDocumento').val() == "") {
        showNotification("Especifique un nombre", "No se ha especificado un nombre para el documento", "warning");
        return;
    }

    $('#filupDocumento_Html5InputFile').click();
}

// LIMPIA ARCHIVOS DEL FILEUPLOAD
function btnLimpiarArchivos_OnClick() {
    $(".removeButton").each(function (index, element) {
        uploadedFiles = 1;
        $(element).click();
    });
}

// INICIO DE LA CARGA DEL ARCHIVO
function filupDocumento_OnClientUploadStart() {
    uploadedFiles = 1;
    var fileName = "Porfavor espere";
    $('#pNombreArchivo').html(fileName);
    $('#hdnNombreArchivo').val(fileName);
    $('.progress').removeClass('d-none');
    $('#fileIcon').removeClass('text-success');
    $('#fileIcon').removeClass('text-danger');
    $('#fileIcon').addClass('text-muted');

    //Estatus del archivo    
    $('#pEstatusArchivo').html('<span class="fa fa-refresh fa-refresh-rotate"></span> Leyendo archivo... <span class="badge badge-danger d-none" id="idBgUploadedFiles"><span>');
    $('#pEstatusArchivo').removeClass('text-success');
    $('#pEstatusArchivo').removeClass('text-danger');
    $('#pEstatusArchivo').addClass('text-muted');
}

// FIN DE LA CARGA DEL ARCHIVO
function filupDocumento_OnClientUploadCompleteAll() {
    var fileupload = document.getElementById("filupDocumento");
    var nfile = fileupload.control._filesInQueue[0];
    var fileName = "Completado";
    $('#pNombreArchivo').html(fileName);
    $('#hdnNombreArchivo').val(fileName);

    $('.progress-bar').css('width', '100%');
    $('#fileIcon').removeClass('text-muted');
    $('#fileIcon').addClass('text-success');
    $('.progress-bar').removeClass('bg-info');
    $('.progress-bar').removeClass('bg-danger');
    $('.progress-bar').addClass('bg-success');

    //Estatus del archivo
    $('#pEstatusArchivo').html('<span class="fa fa-check"></span> Archivo leído con éxito'); //uploadedFiles
    $('#pEstatusArchivo').removeClass('text-muted');
    $('#pEstatusArchivo').removeClass('text-danger');
    $('#pEstatusArchivo').addClass('text-success');

    setTimeout(function () {
        $('.progress').addClass('progress-bar-hide');
        setTimeout(function () {
            $('.progress').addClass('d-none');
            $('.progress-bar').css('width', '0%');
            $('.progress').removeClass('progress-bar-hide');
            $('.progress-bar').removeClass('bg-success');
            $('.progress-bar').removeClass('bg-danger');
            $('.progress-bar').addClass('bg-info');
        }, 500);
    }, 800);

    uploadedFiles = 1;
}

// ERROR AL CARGAR EL ARCHIVO
function filupDocumento_OnClientUploadError() {
    $('#filupDocumento_QueueContainer').empty();
    $('#fileIcon').removeClass('text-muted');
    $('#fileIcon').addClass('text-danger');
    $('.progress-bar').removeClass('bg-info');
    $('.progress-bar').removeClass('bg-success');
    $('.progress-bar').addClass('bg-danger');

    //Estatus del archivo
    $('#pEstatusArchivo').html('<span class="fa fa-times"></span> Error al leer el archivo');
    $('#pEstatusArchivo').removeClass('text-success');
    $('#pEstatusArchivo').removeClass('text-muted');
    $('#pEstatusArchivo').addClass('text-danger');

    setTimeout(function () {
        $('.progress').addClass('progress-bar-hide');
        setTimeout(function () {
            $('.progress').addClass('d-none');
            $('.progress-bar').css('width', '0%');
            $('.progress').removeClass('progress-bar-hide');
            $('.progress-bar').removeClass('bg-success');
            $('.progress-bar').removeClass('bg-danger');
            $('.progress-bar').addClass('bg-info');
        }, 500);
    }, 800);
}

// VALIDA EXTENSION DE ARCHIVO
function validaExtension() {
    var fileupload = document.getElementById("filupDocumento");
    var nfile = fileupload.control._filesInQueue[0]._fileName;
    var botones = [];

    nfile = nfile.replace(/%20/g, "");
    var ext = nfile.match(/\.([^\.]+)$/)[1];

    if (!(ext == "pdf" || ext == "jpg" || ext == "png")) {
        var btnUpdate = document.getElementsByClassName("ajax__fileupload_uploadbutton");
        botones = btnUpdate;

        showNotification("Extensión inválida", "La extensión del archivo que está intentando cargar no es admitida", "warning");

        var btnRemove = document.getElementsByClassName("removeButton");
        botones = btnRemove;
        $(botones[0]).click();
    }
}

// CAMBIA PROGRESS BAR DE FILEUPLOAD
function setProgress(porcentaje) {
    $('.progress-bar').css('width', porcentaje + '%');
}

// OCULTA O MUESTRA EL LOADER
function hideShowLoader(show) {
    if (show)
        $('#divLoader').css('display', 'block');
    else
        $('#divLoader').css('display', 'none');
}

// MUESTRA NOTIFICACION
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

// VALIDA FORMULARIO
function validaFormulario() {
    
    var formValid = true;

    var nombre = $('#txtNombre').val();
    var primerApellido = $('#txtPrimerApellido').val();
    var segundoApellido = $('#txtSegundoApellido').val();
    var calle = $('#txtCalle').val();
    var numero = $('#txtNumero').val();
    var colonia = $('#txtColonia').val();
    var codigoPostal = $('#txtCodigoPostal').val();
    var telefono = $('#txtTelefono').val();
    var rfc = $('#txtRFC').val();

    // Verifica que todos los campos obligatorios estén capturados
    if (nombre == "" || primerApellido == "" || calle == "" || numero == "" || colonia == "" || codigoPostal == "" || telefono == "" || rfc == "") {
        showNotification("Datos sin capturar", "Es necesario capturar todos los campos obligatorios", "warning");
        return false;
    }

    if ($('#grvDocumentos tr').length == 1) {
        showNotification("No se encontraron documentos", "Es necesario subir al menos un documento", "warning");
        return false;
    }

    // Valida contenido de los campos
    if (!(/^[a-zA-Z ]+$/.test(nombre))) { // Nombre
        showNotification("Nombre inválido", "Nombre del prospecto inválido", "warning");
        return false;
    }

    if (!(/^[a-zA-Z ]+$/.test(primerApellido))) { // Primer Apellido
        showNotification("Primer Apellido inválido", "Primer Apellido del prospecto inválido", "warning");
        return false;
    }

    if (segundoApellido != "") {
        if (!(/^[a-zA-Z ]+$/.test(segundoApellido))) { // Segundo Apellido
            showNotification("Segundo Apellido inválido", "Segundo Apellido del prospecto inválido", "warning");
            return false;
        }
    }

    if (!(/^[0-9a-zA-Z. ]+$/.test(calle))) { // Calle
        showNotification("Calle inválida", "Calle del prospecto inválida", "warning");
        return false;
    }

    if (!(/^[0-9a-zA-Z. ]+$/.test(colonia))) { // Colonia
        showNotification("Colonia inválida", "Colonia del prospecto inválida", "warning");
        return false;
    }

    if (!(/^(\(\+?\d{2,3}\)[\*|\s|\-|\.]?(([\d][\*|\s|\-|\.]?){6})(([\d][\s|\-|\.]?){2})?|(\+?[\d][\s|\-|\.]?){8}(([\d][\s|\-|\.]?){2}(([\d][\s|\-|\.]?){2})?)?)$/)
      .test(telefono)) { // Teléfono
        showNotification("Teléfono inválido", "Teléfono del prospecto inválido", "warning");
        return false;
    }

    if (!(/^([A-ZÑ&]{3,4}) ?(?:- ?)?(\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])) ?(?:- ?)?([A-Z\d]{2})([A\d])$/).test(rfc)) { // RFC
        showNotification("RFC inválido", "RFC del prospecto inválido", "warning");
        return false;
    }

    $('#mdlConfirmacion').modal('show');
}

// VALIDA SI EL NOMBRE DEL DOCUMENTO YA EXISTE
function validaNombreDoc() {
    var nombreDocCliente = $('#txtNombreDocumento').val();
    var nombreExistente = false;

    $('#grvDocumentos tr').each(function () {
        var nombreDoc = $(this).closest("tr").find("span").text();

        if (nombreDoc != "") {
            nombreDoc = nombreDoc.split('.').slice(0, -1).join('.');
            if (nombreDocCliente == nombreDoc) {
                nombreExistente = true;
            }
        }
    })

    if (nombreExistente) {
        showNotification("Nombre de documento existente", "El nombre del documento ya se encuentra en el listado", "warning");
        return false;
    }
    else {
        hideShowLoader(true);
        return true;
    }

    
}