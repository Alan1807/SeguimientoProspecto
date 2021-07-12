<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetalleProspecto.aspx.cs" Inherits="SeguimientoProspecto.Views.DetalleProspecto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Detalle de Prospecto</title>

    <link href="../Content/Principal.css" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/font-awesome.min.css" rel="stylesheet" />

    <script src="../Scripts/jquery-3.0.0.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/Views/ListadoProspecto.js"></script>
</head>
<body>
    <form id="frmDetalleProspecto" runat="server">
        <asp:ScriptManager runat="server" ID="smgPrincipal" EnableScriptGlobalization="true" ></asp:ScriptManager>
        <div class="container-fluid p-5">
            <asp:UpdatePanel runat="server" ID="udpBotones">
                <ContentTemplate>
                    <div class="row mb-3">
                        <div class="col">
                            <asp:LinkButton runat="server" ID="btnSalir" CssClass="text-danger font-weight-bold" OnClientClick="hideShowLoader(true);" 
                                OnClick="btnSalir_Click" Font-Underline="false">
                                <span class="fa fa-chevron-left"></span> Salir
                            </asp:LinkButton>
                        </div>
                        <div class="col-4 col-md-3 col-lg-2">
                            <asp:LinkButton runat="server" ID="btnRechazar" CssClass="btn btn-danger btn-block" OnClientClick="hideShowLoader(true);"
                                OnClick="btnRechazar_Click">
                                <span class="fa fa-ban"></span> Rechazar
                            </asp:LinkButton>
                        </div>
                        <div class="col-4 col-md-3 col-lg-2">
                            <asp:LinkButton runat="server" ID="btnAutorizar" CssClass="btn btn-success btn-block" OnClientClick="hideShowLoader(true);"
                                OnClick="btnAutorizar_Click">
                                <span class="fa fa-check"></span> Autorizar
                            </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

             <div class="row">
                <div class="col-12 col-lg-6">
                    <asp:UpdatePanel runat="server" ID="udpCampos">
                        <ContentTemplate>
                            <div class="row bg-white card-shadow pt-3 pb-3">
                                <div class="col-12 mb-3">
                                    <h4>Datos generales</h4>
                                </div>
                                <div class="col-6 col-sm-6 col-lg-6 mb-3">
                                    <asp:Label runat="server" CssClass="text-muted font-weight-bold">Nombre</asp:Label><br />
                                    <asp:Label runat="server" ID="lblNombre" CssClass="font-weight-bold"></asp:Label>
                                </div>
                                <div class="col-6 col-sm-6 col-lg-6 mb-3">
                                    <asp:Label runat="server" CssClass="text-muted font-weight-bold">Primer Apellido</asp:Label><br />
                                    <asp:Label runat="server" ID="lblPrimerApellido" CssClass="font-weight-bold"></asp:Label>
                                </div>
                                <div class="col-6 col-sm-6 col-lg-6 mb-3">
                                    <asp:Label runat="server" CssClass="text-muted font-weight-bold">Segundo Apellido</asp:Label><br />
                                    <asp:Label runat="server" ID="lblSegundoApellido" CssClass="font-weight-bold"></asp:Label>
                                </div>
                                <div class="col-6 col-sm-6 col-lg-6 mb-3">
                                    <asp:Label runat="server" CssClass="text-muted font-weight-bold">Calle</asp:Label><br />
                                    <asp:Label runat="server" ID="lblCalle" CssClass="font-weight-bold"></asp:Label>
                                </div>
                                <div class="col-6 col-sm-6 col-lg-6 mb-3">
                                    <asp:Label runat="server" CssClass="text-muted font-weight-bold">Número</asp:Label><br />
                                    <asp:Label runat="server" ID="lblNumero" CssClass="font-weight-bold"></asp:Label>
                                </div>
                                <div class="col-6 col-sm-6 col-lg-6 mb-3">
                                    <asp:Label runat="server" CssClass="text-muted font-weight-bold">Colonia</asp:Label><br />
                                    <asp:Label runat="server" ID="lblColonia" CssClass="font-weight-bold"></asp:Label>
                                </div>
                                <div class="col-6 col-sm-6 col-lg-6 mb-3">
                                    <asp:Label runat="server" CssClass="text-muted font-weight-bold">Código Postal</asp:Label><br />
                                    <asp:Label runat="server" ID="lblCodigoPostal" CssClass="font-weight-bold"></asp:Label>
                                </div>
                                <div class="col-6 col-sm-6 col-lg-6 mb-3">
                                    <asp:Label runat="server" CssClass="text-muted font-weight-bold">Teléfono</asp:Label><br />
                                    <asp:Label runat="server" ID="lblTelefono" CssClass="font-weight-bold"></asp:Label>
                                </div>
                                <div class="col-6 col-sm-6 col-lg-6 mb-3">
                                    <asp:Label runat="server" CssClass="text-muted font-weight-bold">RFC</asp:Label><br />
                                    <asp:Label runat="server" ID="lblRfc" CssClass="font-weight-bold"></asp:Label>
                                </div>
                                <div class="col-6 col-sm-6 col-lg-6 mb-3">
                                    <asp:Label runat="server" CssClass="text-muted font-weight-bold">Estatus</asp:Label><br />
                                    <asp:Label runat="server" ID="lblEstatus" CssClass="font-weight-bold"></asp:Label>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-12 col-lg-6">
                    <div class="row mt-3 mt-lg-0 ml-3">
                        <h4>Documentos</h4>
                    </div>
                    <asp:UpdatePanel runat="server" ID="udpDocumentos">
                        <ContentTemplate>

                            <div class="row mt-3 mt-lg-0">
                                <div class="col pl-0 pl-lg-3 pr-0">
                                    <asp:GridView runat="server" ID="grvDocumentos" AutoGenerateColumns="false" CssClass="table bg-white mb-0 card-shadow"
                                        GridLines="None" ShowHeaderWhenEmpty="true" OnRowCommand="grvDocumentos_RowCommand" 
                                        DataKeyNames="idDocumento, Nombre, rutaFTP, idProspecto" 
                                        ClientIDMode="Static">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Documento">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblNombreDoc" Text='<%# Eval("Nombre") %>' CssClass="data" ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="50">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" CssClass="text-primary" CommandName="Descargar" 
                                                        CommandArgument="<%# ((GridViewRow)Container).RowIndex %>">
                                                        <span class="fa fa-download"></span>
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

                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="grvDocumentos" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

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

        <!-- MODAL DE CONFIRMACIÓN AUTORIZA -->
        <div class="modal fade" data-keyboard="false" data-backdrop="static" id="mdlConfAutoriza">
            <div class="modal-dialog">
                <div class="modal-content">
                    <asp:UpdatePanel ID="udpMdlConfAutoriza" runat="server">
                        <ContentTemplate>
                            <div class="modal-header">
                                <h4 class="modal-title text-center">
                                    <span class="fa fa-check"></span>
                                    Confirmación de Autorización
                                </h4>
                            </div>
                            <div class="modal-body modal-body-color container">
                                <div class="row">
                                    <div class="col">
                                        <h6>
                                            Está por autorizar el prospecto
                                            <label runat="server" class="font-weight-bold" id="lblNombre_autoriza"></label>
                                        </h6>
                                        <h6>
                                            ¿Desea continuar?
                                        </h6>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button class="btn btn-danger" data-dismiss="modal">
                                    <span class="fa fa-times"></span> Cerrar
                                </button>
                                <asp:LinkButton runat="server" ID="btnConfirmaAutoriza" CssClass="btn btn-success" 
                                    OnClientClick="hideShowLoader(true);" OnClick="btnConfirmaAutoriza_Click">
                                    <span class="fa fa-check"></span> Autorizar
                                </asp:LinkButton>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        <!-- MODAL DE CONFIRMACIÓN RECHAZO -->
        <div class="modal fade" data-keyboard="false" data-backdrop="static" id="mdlConfRechazo">
            <div class="modal-dialog">
                <div class="modal-content">
                    <asp:UpdatePanel ID="udpMdlConfRechazo" runat="server">
                        <ContentTemplate>
                            <div class="modal-header">
                                <h4 class="modal-title text-center">
                                    <span class="fa fa-ban"></span>
                                    Confirmación de Rechazo
                                </h4>
                            </div>
                            <div class="modal-body modal-body-color container">
                                <div class="row">
                                    <div class="col">
                                        <h6>
                                            Está por rechazar el prospecto
                                            <label runat="server" class="font-weight-bold" id="lblNombre_rechazo"></label>
                                        </h6>
                                        <h6>
                                            ¿Desea continuar?
                                        </h6>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button class="btn btn-danger" data-dismiss="modal">
                                    <span class="fa fa-times"></span> Cerrar
                                </button>
                                <asp:LinkButton runat="server" ID="btnConfirmaRechazo" CssClass="btn btn-success" 
                                    OnClientClick="hideShowLoader(true);" OnClick="btnConfirmaRechazo_Click">
                                    <span class="fa fa-check"></span> Rechazar
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
