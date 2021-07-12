<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CapturaProspecto.aspx.cs" Inherits="SeguimientoProspecto.Views.CapturaProspecto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Captura de Prospectos</title>

    <link href="../Content/Principal.css" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/font-awesome.min.css" rel="stylesheet" />

    <script src="../Scripts/jquery-3.0.0.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/Views/CapturaProspecto.js"></script>
</head>
<body>
    <form id="frmCapturaProspecto" runat="server">
        <asp:ScriptManager runat="server" ID="smgPrincipal" EnableScriptGlobalization="true" EnableScriptLocalization="true"></asp:ScriptManager>

        <div class="container-fluid p-5">
            <asp:UpdatePanel runat="server" ID="udpEnviar">
                <ContentTemplate>
                    <div class="row mb-3">
                        <div class="col">
                            <asp:LinkButton runat="server" ID="btnSalir" CssClass="text-danger float-left font-weight-bold" Font-Underline="false"
                                OnClientClick="hideShowLoader(true);" OnClick="btnSalir_Click">
                                <span class="fa fa-chevron-left"></span> Salir
                            </asp:LinkButton>
                        </div>
                        <div class="col col-sm-4 col-md-3 col-lg-2">
                            <asp:LinkButton runat="server" ID="btnEnviar" CssClass="btn btn-success btn-block float-right"
                                OnClientClick="validaFormulario()">
                                <span class="fa fa-send"></span> Enviar
                            </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            

            <div class="row bg-white card-shadow pt-4 pb-4">
                <div class="col-12 col-md-4 col-xl-3">
                    <asp:Label runat="server" CssClass="text-muted font-weight-bold" AssociatedControlID="txtNombre">Nombre</asp:Label>
                    <span class="text-danger font-weight-bold">*</span>
                    <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control nombres" autocomplete="off" placeholder="Nombre Prospecto" 
                        MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                </div>
                <div class="col-12 col-md-4 col-xl-3 mb-3">
                    <asp:Label runat="server" CssClass="text-muted font-weight-bold" AssociatedControlID="txtPrimerApellido">Primer Apellido</asp:Label>
                    <span class="text-danger font-weight-bold">*</span>
                    <asp:TextBox runat="server" ID="txtPrimerApellido" CssClass="form-control nombres" autocomplete="off" 
                        placeholder="Primer Apellido" MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                </div>
                <div class="col-12 col-md-4 col-xl-3 mb-3">
                    <asp:Label runat="server" CssClass="text-muted font-weight-bold" AssociatedControlID="txtSegundoApellido">Segundo Apellido</asp:Label>
                    <asp:TextBox runat="server" ID="txtSegundoApellido" CssClass="form-control nombres" autocomplete="off" placeholder="Segundo Apellido" 
                        MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                </div>
                <div class="col-12 col-md-4 col-xl-3 mb-3">
                    <asp:Label runat="server" CssClass="text-muted font-weight-bold" AssociatedControlID="txtCalle">Calle</asp:Label>
                    <span class="text-danger font-weight-bold">*</span>
                    <asp:TextBox runat="server" ID="txtCalle" CssClass="form-control" autocomplete="off" placeholder="Calle" MaxLength="100" ClientIDMode="Static"></asp:TextBox>
                </div>
                <div class="col-12 col-md-4 col-xl-3 mb-3">
                    <asp:Label runat="server" CssClass="text-muted font-weight-bold" AssociatedControlID="txtNumero">Número</asp:Label>
                    <span class="text-danger font-weight-bold">*</span>
                    <asp:TextBox runat="server" ID="txtNumero" CssClass="form-control numeros" autocomplete="off" placeholder="Número" ClientIDMode="Static"
                        MaxLength="5"></asp:TextBox>
                </div>
                <div class="col-12 col-md-4 col-xl-3 mb-3">
                    <asp:Label runat="server" CssClass="text-muted font-weight-bold" AssociatedControlID="txtColonia">Colonia</asp:Label>
                    <span class="text-danger font-weight-bold">*</span>
                    <asp:TextBox runat="server" ID="txtColonia" CssClass="form-control" autocomplete="off" placeholder="Colonia" MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                </div>
                <div class="col-12 col-md-4 col-xl-3 mb-3">
                    <asp:Label runat="server" CssClass="text-muted font-weight-bold" AssociatedControlID="txtCodigoPostal">Código Postal</asp:Label>
                    <span class="text-danger font-weight-bold">*</span>
                    <asp:TextBox runat="server" ID="txtCodigoPostal" CssClass="form-control numeros" autocomplete="off" placeholder="Código Postal" 
                        ClientIDMode="Static" MaxLength="5"></asp:TextBox>
                </div>
                <div class="col-12 col-md-4 col-xl-3 mb-3">
                    <asp:Label runat="server" CssClass="text-muted font-weight-bold" AssociatedControlID="txtTelefono">Teléfono</asp:Label>
                    <span class="text-danger font-weight-bold">*</span>
                    <asp:TextBox runat="server" ID="txtTelefono" CssClass="form-control telefono" autocomplete="off" placeholder="Teléfono" MaxLength="15" ClientIDMode="Static"></asp:TextBox>
                </div>
                <div class="col-12 col-md-4 col-xl-3 mb-3">
                    <asp:Label runat="server" CssClass="text-muted font-weight-bold" AssociatedControlID="txtRFC">RFC</asp:Label>
                    <span class="text-danger font-weight-bold">*</span>
                    <asp:TextBox runat="server" ID="txtRFC" CssClass="form-control rfc" autocomplete="off" placeholder="RFC" MaxLength="20" ClientIDMode="Static"></asp:TextBox>
                </div>
            </div>
            <div class="row mt-3">
                <div class="col">
                    <h5 class="text-muted font-weight-bold">Documentos</h5>
                </div>
            </div>
            
            <asp:UpdatePanel runat="server" ID="udpDocumentos">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-12 col-sm-6 col-md-6 col-lg-4 pr-0 align-self-center align-self-lg-start mt-lg-4">
                            <asp:Label runat="server" CssClass="text-muted font-weight-bold" AssociatedControlID="txtNombreDocumento">Nombre del Documento</asp:Label>
                            <asp:TextBox runat="server" ID="txtNombreDocumento" CssClass="form-control w-100 filename" 
                                placeholder="Nombre Documento" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-12 col-sm-6 col-md-6 col-lg-3 mb-4">

                            <div class="container-fluid d-none">
                                <div class="row">
                                    <div class="col-12 filupCont">
                                        <asp:HiddenField runat="server" ID="hdnNumeroArchivos" ClientIDMode="Static" />
                                        <ajaxToolkit:AjaxFileUpload runat="server" ID="filupDocumento" ClientIDMode="Static" AutoStartUpload="true"
                                            OnUploadComplete="filupDocumento_UploadComplete" OnClientUploadCompleteAll="filupDocumento_OnClientUploadCompleteAll"
                                            OnClientUploadStart="filupDocumento_OnClientUploadStart" ClearFileListAfterUpload="true"
                                            OnClientUploadError="filupDocumento_OnClientUploadError" onchange="validaExtension();" MaximumNumberOfFiles="1" />
                                        
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid mt-3 p-0">
                                <div class="row">

                                    <div class="col-12 p-0">
                                        <div class="container-fluid">
                                            <div class="row mt-3">
                                                <asp:HiddenField runat="server" ID="hdnNombreArchivo" ClientIDMode="Static" />
                                                <div class="col-3 text-right">
                                                    <a runat="server" id="fileIcon" class="fa fa-file text-muted file-icon" onclick="btnSeleccionaArchivo_OnClick()"></a>
                                                </div>
                                                <div class="col text-left form-group pl-0 mb-0 cut-text pl-1">
                                                    <a runat="server" id="pNombreArchivo" onclick="btnSeleccionaArchivo_OnClick()"
                                                        class="font-weight-bold text-muted nombre-archivo">Seleccionar archivo...</a>
                                                    <p runat="server" id="pEstatusArchivo" class="estatus-archivo">
                                                        <span class="fa fa-upload"></span> Cargar archivo
                                                    </p>
                                                    <span>
                                                        <span class="estatus-archivo"><strong>Formatos: </strong>pdf, jpg, png, doc</span>
                                                    </span>
                                                    <br />
                                                    <asp:LinkButton runat="server" ID="btnSubirArchivos" CssClass="badge badge-success text-white" 
                                                        OnClick="btnSubirArchivos_Click" OnClientClick="return validaNombreDoc();">
                                                        <span class="fa fa-plus"></span> Agregar
                                                    </asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="btnLimpiarArchivos" CssClass="badge badge-primary text-white" 
                                                        OnClick="btnLimpiarArchivos_Click" OnClientClick="btnLimpiarArchivos_OnClick()">
                                                        <span class="fa fa-eraser"></span> Limpiar
                                                    </asp:LinkButton>
                                                    
                                                </div>
                                            </div>
                                            <div class="row mt-2">
                                                <div class="col-12">
                                                    <div class="progress d-none">
                                                        <div class="progress-bar progress-bar-striped bg-info" role="progressbar" style="width: 0%" aria-valuenow="0"
                                                            aria-valuemin="0" aria-valuemax="100">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="col">
                            <asp:GridView runat="server" ID="grvDocumentos" AutoGenerateColumns="false" CssClass="table bg-white mb-0 card-shadow"
                                GridLines="None" ShowHeaderWhenEmpty="true" OnRowCommand="grvDocumentos_RowCommand" DataKeyNames="Nombre" ClientIDMode="Static">
                                <Columns>
                                    <asp:TemplateField HeaderText="Documento">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblNombreDoc" Text='<%# Eval("Nombre") %>' CssClass="data" ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="50">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" CssClass="text-danger" CommandName="Quitar" 
                                                CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" OnClientClick="hideShowLoader(true);">
                                                <span class="fa fa-times"></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <!--EMPTY STATE GRID DOCUMENTOS-->
                            <div runat="server" id="divEgDocumentos" class="text-center mt-0 pt-4 pb-3 bg-white card-shadow" visible="true">
                                <span class="fa fa-archive text-muted mb-2" style="font-size:8em!important;"></span>
                                <p class="font-weight-bold font-size-x1_5 text-muted">Sin información para mostrar</p>
                                <p class="text-muted">No se han encontrado documentos para listar.                       
                            </div>
                    
                        </div>
                    </div>
                </div>
                </ContentTemplate>
        </asp:UpdatePanel>


        

        <!--====================== SECCION DE MODALES ===================================-->
        <!--LOADER-->
        <div id="divLoader" class="loader">
            <img src="../Images/loading.gif" class="text-center" />
        </div>

        <!-- NOTIFICACION -->
        <div class="notification alert d-none" role="alert">
          <h4 id="hTituloNoti" class="alert-heading"></h4>
          <p id="pDescNoti"></p>
        </div>

        <!-- MODAL DE CONFIRMACIÓN DE ENVIAR -->
        <div class="modal fade" data-keyboard="false" data-backdrop="static" id="mdlConfirmacion">
            <div class="modal-dialog">
                <div class="modal-content">
                    <asp:UpdatePanel ID="udpMdlConfirmacion" runat="server">
                        <ContentTemplate>
                            <div class="modal-header">
                                <h4 class="modal-title text-center">
                                    <span class="fa fa-send"></span>
                                    Confirmación de enviado
                                </h4>
                            </div>
                            <div class="modal-body modal-body-color container">
                                <div class="row">
                                    <div class="col">
                                        <h6>
                                            Está por enviar el prospecto
                                            <label runat="server" id="lblNombre_confirma"></label>
                                        </h6>
                                        <h6>
                                            ¿Desea continuar?
                                        </h6>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button class="btn btn-danger" data-dismiss="modal">
                                    <span class="fa fa-times"></span> Cancelar
                                </button>
                                <asp:LinkButton runat="server" ID="btnConfirmacion" CssClass="btn btn-success" 
                                    OnClientClick="hideShowLoader(true);" OnClick="btnConfirmacion_Click">
                                    <span class="fa fa-check"></span> Aceptar
                                </asp:LinkButton>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
