using SeguimientoProspecto.Clases;
using SeguimientoProspecto.Clases.Utils;
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
    public partial class DetalleProspecto : System.Web.UI.Page
    {
        Result resultado = new Result();
        clsProspecto clsprospecto = new clsProspecto();
        Utils utils = new Utils();

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // VERIFICA SI SE ENVIÓ UN ID PROSPECTO POR EL REQUEST
                if (Request.QueryString["idProspecto"] != null)
                {
                    int idProspecto = 0;
                    idProspecto = Convert.ToInt32(Request.QueryString["idProspecto"]);
                    Session["idProspecto"] = idProspecto;

                    // Busca el prospecto
                    buscarProspecto(idProspecto);
                }
                else
                    Session["idProspecto"] = null;
            }
        }
        #endregion

        #region Metodos
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

                if (idEstatus == 1) // Enviado
                {
                    estatusClass = "text-primary";
                    btnRechazar.Visible = true;
                    btnAutorizar.Visible = true;
                }
                else if (idEstatus == 2) // Autorizado
                {
                    estatusClass = "text-success";
                    btnRechazar.Visible = false;
                    btnAutorizar.Visible = false;
                }
                else if (idEstatus == 3) // Rechazado
                {
                    estatusClass = "text-danger";
                    btnRechazar.Visible = false;
                    btnAutorizar.Visible = false;
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
            }
            catch(Exception ex)
            {
                utils.showNotification(this, "Error", "Error al mostrar al prospecto: " + ex.Message, Utils.notiTypes.danger);
            }
        }
        #endregion

        #region Eventos
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

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Views/ListadoProspecto.aspx");
            }
            catch (Exception ex)
            {
                utils.showNotification(this, "Error", "Error al redireccionar al listado: " + ex.Message, Utils.notiTypes.danger);
            }
        }

        protected void btnAutorizar_Click(object sender, EventArgs e)
        {
            try
            {
                lblNombre_autoriza.InnerText = lblNombre.Text;
                utils.hideShowModal(this, "mdlConfAutoriza", true);
            }
            catch(Exception ex)
            {
                utils.showNotification(this, "Error", "Error al mostrar confirmación: " + ex.Message, Utils.notiTypes.danger);
            }
            finally
            {
                utils.ocultaLoader(this);
            }
        }

        protected void btnConfirmaAutoriza_Click(object sender, EventArgs e)
        {
            int idProspecto = 0;

            try
            {
                if (Session["idProspecto"] == null)
                {
                    utils.showNotification(this, "Prospecto no encontrado", "No se ha seleccionado un prospecto", Utils.notiTypes.warning);
                    return;
                }

                idProspecto = Convert.ToInt32(Session["idProspecto"]);
                resultado = clsprospecto.autorizaRechazaProspecto(true, idProspecto);
                
                if (resultado.Error)
                {
                    utils.showNotification(this, "Error", "Error al autorizar: " + resultado.MensajeError, Utils.notiTypes.danger);
                    return;
                }

                utils.showNotification(this, "Autorizado", "Prospecto autorizado correctamente", Utils.notiTypes.success);
                lblEstatus.Text = "Autorizado";
                
                lblEstatus.CssClass = lblEstatus.CssClass.Replace("text-danger", string.Empty);
                lblEstatus.CssClass = lblEstatus.CssClass.Replace("text-primary", string.Empty);
                lblEstatus.CssClass += " text-success";
                btnRechazar.Visible = false;
                btnAutorizar.Visible = false;

                utils.hideShowModal(this, "mdlConfAutoriza", false);
            }
            catch (Exception ex)
            {
                utils.showNotification(this, "Error", "Error al autorizar: " + ex.Message, Utils.notiTypes.danger);
            }
            finally
            {
                utils.ocultaLoader(this);
            }
        }

        protected void btnRechazar_Click(object sender, EventArgs e)
        {
            try
            {
                lblNombre_rechazo.InnerText = lblNombre.Text;
                utils.hideShowModal(this, "mdlConfRechazo", true);
            }
            catch (Exception ex)
            {
                utils.showNotification(this, "Error", "Error al mostrar confirmación: " + ex.Message, Utils.notiTypes.danger);
            }
            finally
            {
                utils.ocultaLoader(this);
            }
        }

        protected void btnConfirmaRechazo_Click(object sender, EventArgs e)
        {
            int idProspecto = 0;

            try
            {
                if (Session["idProspecto"] == null)
                {
                    utils.showNotification(this, "Prospecto no encontrado", "No se ha seleccionado un prospecto", Utils.notiTypes.warning);
                    return;
                }

                idProspecto = Convert.ToInt32(Session["idProspecto"]);
                resultado = clsprospecto.autorizaRechazaProspecto(false, idProspecto);

                if (resultado.Error)
                {
                    utils.showNotification(this, "Error", "Error al rechazar: " + resultado.MensajeError, Utils.notiTypes.danger);
                    return;
                }

                utils.showNotification(this, "Rechazado", "Prospecto rechazado correctamente", Utils.notiTypes.success);
                lblEstatus.Text = "Rechazado";

                lblEstatus.CssClass = lblEstatus.CssClass.Replace("text-success", string.Empty);
                lblEstatus.CssClass = lblEstatus.CssClass.Replace("text-primary", string.Empty);
                lblEstatus.CssClass += " text-danger";
                btnRechazar.Visible = false;
                btnAutorizar.Visible = false;

                utils.hideShowModal(this, "mdlConfRechazo", false);
            }
            catch (Exception ex)
            {
                utils.showNotification(this, "Error", "Error al rechazar: " + ex.Message, Utils.notiTypes.danger);
            }
            finally
            {
                utils.ocultaLoader(this);
            }
        }
        #endregion
    }
}