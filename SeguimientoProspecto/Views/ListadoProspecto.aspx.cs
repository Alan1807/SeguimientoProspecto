using SeguimientoProspecto.Clases;
using SeguimientoProspecto.Clases.Utils;
using SeguimientoProspecto.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeguimientoProspecto.Views
{
    public partial class ListadoProspecto : System.Web.UI.Page
    {
        Result resultado = new Result();
        clsProspecto clsprospecto = new clsProspecto();
        Utils utils = new Utils();

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                llenarListadoEstatus();
                buscarProspectos();
            }
        }
        #endregion

        #region Metodos
        public void llenarListadoEstatus()
        {
            DataSet dsEstatus = new DataSet();

            try
            {
                resultado = clsprospecto.obtenerEstatus();

                if (!resultado.Error)
                {
                    dsEstatus = (DataSet)resultado.Datos;
                    if (dsEstatus != null)
                    {
                        if (dsEstatus.Tables[0].Rows.Count > 0)
                        {
                            ddlEstatus.DataSource = dsEstatus;
                            ddlEstatus.DataTextField = "Nombre";
                            ddlEstatus.DataValueField = "idEstatus";
                            ddlEstatus.DataBind();
                            ddlEstatus.Items.Insert(0, new ListItem("- Seleccione -", "0"));
                        }
                    }
                }
                else
                {
                    utils.showNotification(this, "Error", "Error al listar los estatus", Utils.notiTypes.danger);
                }
            }
            catch (Exception ex)
            {
                utils.showNotification(this, "Error", "Error al listar los estatus: " + ex.Message, Utils.notiTypes.danger);
            }
        }

        public void buscarProspectos()
        {
            Prospecto prospecto = new Prospecto();

            try
            {
                prospecto.Nombre = txtNombre.Text;
                prospecto.PrimerApellido = txtPrimerApellido.Text;
                prospecto.SegundoApellido = txtSegundoApellido.Text;
                prospecto.Calle = txtCalle.Text;
                prospecto.Numero = txtNumero.Text == "" ? 0 : Convert.ToInt32(txtNumero.Text);
                prospecto.Colonia = txtColonia.Text;
                prospecto.CodigoPostal = txtCodigoPostal.Text == "" ? 0 : Convert.ToInt32(txtCodigoPostal.Text);
                prospecto.RFC = txtRFC.Text;
                prospecto.idEstatus = Convert.ToInt32(ddlEstatus.SelectedValue);

                resultado = clsprospecto.obtenerProspecto(prospecto);

                if (resultado.Error)
                {
                    utils.showNotification(this, "Error", "Error al listar los prospectos: " + resultado.MensajeError, Utils.notiTypes.danger);
                    return;
                }

                Session["dtListadoProspecto"] = resultado.Datos.Tables[0];
                gvListadoProspecto.DataSource = resultado.Datos.Tables[0];
                gvListadoProspecto.DataBind();

                divEgProspectos.Visible = gvListadoProspecto.Rows.Count == 0;
            }
            catch(Exception ex)
            {
                utils.showNotification(this, "Error", "Error al listar los prospectos: " + ex.Message, Utils.notiTypes.danger);
            }
            finally
            {
                utils.ocultaLoader(this);
            }
        }

        private void buscarProspecto(int idProspecto)
        {
            DataRow rowProspecto;
            int idEstatus = 0;
            string estatusClass = string.Empty;

            try
            {
                // CARGA LA INFORMACION DEL PROSPECTO
                resultado = clsprospecto.obtenerProspecto(idProspecto);

                if (resultado.Error)
                {
                    utils.showNotification(this, "Error", "Error al mostrar al prospecto: " + resultado.MensajeError, Utils.notiTypes.danger);
                    return;
                }

                if (resultado.Datos.Tables[0].Rows.Count == 0)
                {
                    utils.showNotification(this, "No encontrado", "No se encontró al prospecto", Utils.notiTypes.warning);
                    return;
                }

                rowProspecto = resultado.Datos.Tables[0].Rows[0];
                lblNombre.Text = rowProspecto["Nombre"].ToString();
                lblPrimerApellido.Text = rowProspecto["PrimerApellido"].ToString();
                lblSegundoApellido.Text = rowProspecto["SegundoApellido"].ToString();
                lblCalle.Text = rowProspecto["Calle"].ToString();
                lblNumero.Text = rowProspecto["Numero"].ToString();
                lblColonia.Text = rowProspecto["Colonia"].ToString();
                lblCodigoPostal.Text = rowProspecto["CodigoPostal"].ToString();
                lblTelefono.Text = rowProspecto["Telefono"].ToString();
                lblRfc.Text = rowProspecto["RFC"].ToString();
                lblEstatus.Text = rowProspecto["NombreEstatus"].ToString();
                idEstatus = Convert.ToInt32(rowProspecto["idEstatus"]);
                lblObservacionesRechazo.Text = idEstatus == 3 ? rowProspecto["ObservacionRechazo"].ToString() : string.Empty;

                if (idEstatus == 1) // Enviado
                {
                    estatusClass = "text-primary";
                    divObservacionesRechazo.Visible = false;
                }
                else if (idEstatus == 2) // Autorizado
                {
                    estatusClass = "text-success";
                    divObservacionesRechazo.Visible = false;
                }
                else if (idEstatus == 3) // Rechazado
                {
                    estatusClass = "text-danger";
                    divObservacionesRechazo.Visible = true;
                }

                lblEstatus.CssClass += " " + estatusClass;

                // CARGA LOS DOCUMENTOS DEL PROSPECTO
                idProspecto = Convert.ToInt32(rowProspecto["idProspecto"]);
                resultado = clsprospecto.obtenerDocumentos(idProspecto);

                if (resultado.Error)
                {
                    utils.showNotification(this, "Error", "Error al mostrar los documentos: " + resultado.MensajeError, Utils.notiTypes.danger);
                    return;
                }

                if (resultado.Datos.Tables[0].Rows.Count == 0)
                {
                    utils.showNotification(this, "No encontrado", "No se encontraron documentos", Utils.notiTypes.warning);
                    return;
                }

                Session["dtDocumentos"] = resultado.Datos.Tables[0];
                grvDocumentos.DataSource = resultado.Datos.Tables[0];
                grvDocumentos.DataBind();
                divEgDocumentos.Visible = grvDocumentos.Rows.Count == 0;

                utils.hideShowModal(this, "mdlProspectoInfo", true);
            }
            catch (Exception ex)
            {
                utils.showNotification(this, "Error", "Error al mostrar al prospecto: " + ex.Message, Utils.notiTypes.danger);
            }
        }
        #endregion

        #region Eventos
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            buscarProspectos();
        }

        protected void gvListadoProspecto_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int idEstatus = 0;

            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblNombreEstatus = (Label)e.Row.FindControl("lblNombreEstatus");
                    idEstatus = Convert.ToInt32(gvListadoProspecto.DataKeys[e.Row.RowIndex]["idEstatus"]);

                    lblNombreEstatus.CssClass = lblNombreEstatus.CssClass.Replace("text-primary", string.Empty);
                    lblNombreEstatus.CssClass = lblNombreEstatus.CssClass.Replace("text-success", string.Empty);
                    lblNombreEstatus.CssClass = lblNombreEstatus.CssClass.Replace("text-danger", string.Empty);
                    if (idEstatus == 1)
                        lblNombreEstatus.CssClass += " text-primary";
                    if (idEstatus == 2)
                        lblNombreEstatus.CssClass += " text-success";
                    if (idEstatus == 3)
                        lblNombreEstatus.CssClass += " text-danger";
                }
            }
            catch (Exception ex)
            {
                utils.showNotification(this, "Error", "Error al listar los prospectos: " + ex.Message, Utils.notiTypes.danger);
            }
        }

        protected void gvListadoProspecto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = 0, idProspecto = 0;

            try
            {
                index = Convert.ToInt32(e.CommandArgument.ToString());
                idProspecto = Convert.ToInt32(gvListadoProspecto.DataKeys[index]["idProspecto"]);

                if (e.CommandName == "Ver")
                {
                    buscarProspecto(idProspecto);
                }
                if (e.CommandName == "Evaluar")
                {
                    Response.Redirect(string.Format("~/Views/DetalleProspecto.aspx?idProspecto={0}", idProspecto));
                }
            }
            catch (Exception ex)
            {
                utils.showNotification(this, "Error", "Error al ejecutar la acción del listado: " + ex.Message, Utils.notiTypes.danger);
            }
            finally
            {
                //OCULTA EL LOADER
                utils.ocultaLoader(this);
            }
        }

        protected void gvListadoProspecto_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable dtListadoProspecto = (DataTable)Session["dtListadoProspecto"];
                gvListadoProspecto.PageIndex = e.NewPageIndex;
                gvListadoProspecto.DataSource = dtListadoProspecto;
                gvListadoProspecto.DataBind();
            }
            catch (Exception ex)
            {
                utils.showNotification(this, "Error", "Error al cambiar de página: " + ex.Message, Utils.notiTypes.danger);
            }
        }

        protected void btnNuevoProspecto_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Views/CapturaProspecto.aspx");
            }
            catch (Exception ex)
            {
                utils.showNotification(this, "Error", "Error al redireccionar a la captura: " + ex.Message, Utils.notiTypes.danger);
            }
        }
        #endregion

        protected void grvDocumentos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = 0, idDocumento = 0, idProspecto = 0;
            DataRow[] rowsResult;
            DataTable dtDocumentos;
            MemoryStream msArchivo;
            string nombreDocumento = string.Empty, rutaDocumento = string.Empty, docExtension = string.Empty;
            clsFTP Ftp = new clsFTP();

            try
            {
                if (e.CommandName == "Descargar")
                {
                    dtDocumentos = (DataTable)Session["dtDocumentos"];
                    index = Convert.ToInt32(e.CommandArgument.ToString());
                    idDocumento = Convert.ToInt32(grvDocumentos.DataKeys[index]["idDocumento"]);
                    idProspecto = Convert.ToInt32(grvDocumentos.DataKeys[index]["idProspecto"]);

                    rowsResult = dtDocumentos.Select("idDocumento = '" + idDocumento + "'");

                    if (rowsResult.Count() == 0)
                    {
                        utils.showNotification(this, "No encontrado", "No se ha encontrado el documento", Utils.notiTypes.warning);
                        return;
                    }

                    msArchivo = new MemoryStream();
                    nombreDocumento = rowsResult[0]["Nombre"].ToString();
                    docExtension = Path.GetExtension(nombreDocumento);
                    docExtension = docExtension.Replace(".", string.Empty);
                    Ftp.ftpLogin();
                    rutaDocumento = "ftp://" + Ftp.hostFTP + Ftp.directorioFTP + "/" + idProspecto + "/" + nombreDocumento;


                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(rutaDocumento);
                    request.Method = WebRequestMethods.Ftp.DownloadFile;

                    //Enter FTP Server credentials.
                    request.Credentials = new NetworkCredential(Ftp.usuarioFTP, Ftp.passwordFTP);
                    request.UsePassive = true;
                    request.UseBinary = true;
                    request.EnableSsl = false;

                    //Fetch the Response and read it into a MemoryStream object.
                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                    //Download the File.
                    response.GetResponseStream().CopyTo(msArchivo);

                    using (MemoryStream fs = msArchivo)
                    {
                        // Response.ContentType = "application/pdf";
                        Response.ContentType = "application/" + docExtension;
                        Response.AddHeader("content-disposition", "attachment;filename=" + nombreDocumento.Trim());
                        Response.BufferOutput = true;
                        Response.AddHeader("Content-Length", fs.Length.ToString());
                        Response.BinaryWrite(fs.ToArray());
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                utils.showNotification(this, "Error", "Error al ejecutar la acción del listado: " + ex.Message, Utils.notiTypes.danger);
            }
        }
    }
}