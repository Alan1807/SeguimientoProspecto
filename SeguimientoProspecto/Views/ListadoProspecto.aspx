<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListadoProspecto.aspx.cs" Inherits="SeguimientoProspecto.Views.ListadoProspecto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    
    <title>Listado de Prospectos</title>

    <link href="../Content/Principal.css" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/font-awesome.min.css" rel="stylesheet" />

    <script src="../Scripts/jquery-3.0.0.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/Views/ListadoProspecto.js"></script>

</head>
<body>
    <form id="frmListadoProspecto" runat="server">
        <asp:ScriptManager runat="server" ID="smgPrincipal" EnableScriptGlobalization="true" ></asp:ScriptManager>
        <div class="container-fluid p-5">

            <asp:UpdatePanel runat="server" ID="udpFiltros">
                <ContentTemplate>
                    <div class="row bg-white card-shadow pt-4 pb-4">
                        <div class="col-12 col-sm-6 col-xl-3 mb-3">
                            <asp:Label runat="server" CssClass="text-muted font-weight-bold" AssociatedControlID="txtNombre">Nombre</asp:Label>
                            <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control nombres" autocomplete="off" placeholder="Nombre Prospecto" 
                                MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                        </div>
                        <div class="col-12 col-sm-6 col-xl-3 mb-3">
                            <asp:Label runat="server" CssClass="text-muted font-weight-bold" AssociatedControlID="txtPrimerApellido">Primer Apellido</asp:Label>
                            <asp:TextBox runat="server" ID="txtPrimerApellido" CssClass="form-control nombres" autocomplete="off" 
                                placeholder="Primer Apellido" MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                        </div>
                        <div class="col-12 col-sm-6 col-xl-3 mb-3">
                            <asp:Label runat="server" CssClass="text-muted font-weight-bold" AssociatedControlID="txtSegundoApellido">Segundo Apellido</asp:Label>
                            <asp:TextBox runat="server" ID="txtSegundoApellido" CssClass="form-control nombres" autocomplete="off" placeholder="Segundo Apellido" 
                                MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                        </div>
                        <div class="col-12 col-sm-6 col-xl-3 mb-3">
                            <asp:Label runat="server" CssClass="text-muted font-weight-bold" AssociatedControlID="txtCalle">Calle</asp:Label>
                            <asp:TextBox runat="server" ID="txtCalle" CssClass="form-control" autocomplete="off" placeholder="Calle" MaxLength="100" ClientIDMode="Static"></asp:TextBox>
                        </div>
                        <div class="col-12 col-sm-6 col-xl-3 mb-3">
                            <asp:Label runat="server" CssClass="text-muted font-weight-bold" AssociatedControlID="txtNumero">Número</asp:Label>
                            <asp:TextBox runat="server" ID="txtNumero" CssClass="form-control numeros" autocomplete="off" placeholder="Número" ClientIDMode="Static"
                                MaxLength="5"></asp:TextBox>
                        </div>
                        <div class="col-12 col-sm-6 col-xl-3 mb-3">
                            <asp:Label runat="server" CssClass="text-muted font-weight-bold" AssociatedControlID="txtColonia">Colonia</asp:Label>
                            <asp:TextBox runat="server" ID="txtColonia" CssClass="form-control" autocomplete="off" placeholder="Colonia" MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                        </div>
                        <div class="col-12 col-sm-6 col-xl-3 mb-3">
                            <asp:Label runat="server" CssClass="text-muted font-weight-bold" AssociatedControlID="txtCodigoPostal">Código Postal</asp:Label>
                            <asp:TextBox runat="server" ID="txtCodigoPostal" CssClass="form-control numeros" autocomplete="off" placeholder="Código Postal" 
                                ClientIDMode="Static" MaxLength="5"></asp:TextBox>
                        </div>
                        <div class="col-12 col-sm-6 col-xl-3 mb-3">
                            <asp:Label runat="server" CssClass="text-muted font-weight-bold" AssociatedControlID="txtRFC">RFC</asp:Label>
                            <asp:TextBox runat="server" ID="txtRFC" CssClass="form-control rfc" autocomplete="off" placeholder="RFC" MaxLength="20" ClientIDMode="Static"></asp:TextBox>
                        </div>
                        <div class="col-12 col-sm-6 col-xl-3 mb-3">
                            <asp:Label runat="server" CssClass="text-muted font-weight-bold" AssociatedControlID="ddlEstatus">Estatus</asp:Label>
                            <asp:DropDownList runat="server" ID="ddlEstatus" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-6 col-md-3 col-xl-3 justify-content-center align-self-center mt-3">
                            <asp:LinkButton runat="server" ID="btnBuscar" CssClass="btn btn-primary btn-block" OnClientClick="hideShowLoader(true);"
                                OnClick="btnBuscar_Click">
                                <span class="fa fa-search"></span> Buscar
                            </asp:LinkButton>
                        </div>
                        <div class="col-6 col-md-3 col-xl-3 offset-sm-6 offset-md-0 justify-content-center align-self-center mt-3">
                            <asp:LinkButton runat="server" ID="btnNuevoProspecto" CssClass="btn btn-success btn-block" OnClientClick="hideShowLoader(true);"
                                OnClick="btnNuevoProspecto_Click">
                                <span class="fa fa-plus"></span> Nuevo Prospecto
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="row mt-3">
                        <div class="col pr-0 pl-0">
                            <asp:GridView ID="gvListadoProspecto" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                    CssClass="table bg-white card-shadow mb-0 table-responsive-sm" GridLines="None"
                                    PageSize="20" ShowHeaderWhenEmpty="true" OnRowDataBound="gvListadoProspecto_RowDataBound" DataKeyNames="idEstatus, idProspecto"
                                    OnRowCommand="gvListadoProspecto_RowCommand" OnPageIndexChanging="gvListadoProspecto_PageIndexChanging"
                                >
                                <Columns>
                                    <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre Completo" />
                                    <asp:BoundField DataField="Calle" HeaderText="Calle" />
                                    <asp:BoundField DataField="Colonia" HeaderText="Colonia" />
                                    <asp:BoundField DataField="CodigoPostal" HeaderText="Código Postal" />
                                    <asp:BoundField DataField="RFC" HeaderText="RFC" />
                                    <asp:TemplateField HeaderText="Estatus">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblNombreEstatus" CssClass="font-weight-bold" Text='<%# Eval("NombreEstatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="btnVer" CssClass="text-info" CommandName="Ver"  OnClientClick="hideShowLoader(true);"
                                                CommandArgument="<%# ((GridViewRow)Container).RowIndex %>">
                                                <span class="fa fa-eye"></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="pagination-gv" HorizontalAlign="Center" BorderStyle="None" Width="900" BorderWidth="0px" BorderColor="White" />
                            </asp:GridView>
                            <!--EMPTY STATE GRID LIDERES-->
                            <div runat="server" id="divEgProspectos" class="text-center mt-0 pt-4 pb-3 bg-white" visible="false">
                                <span class="fa fa-archive text-muted mb-2" style="font-size:8em!important;"></span>
                                <p class="font-weight-bold font-size-x1_5 text-muted">Sin información para mostrar</p>
                                <p class="text-muted">No se han encontrado prospectos para listar.<br /> 
                                    Intente realizar una nueva búsqueda</p>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

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
    </form>
</body>
</html>
