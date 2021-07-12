using SeguimientoProspecto.Clases;
using SeguimientoProspecto.Clases.Utils;
using SeguimientoProspecto.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeguimientoProspecto.Views
{
    public partial class CapturaProspecto : System.Web.UI.Page
    {
        Result resultado = new Result();
        clsProspecto clsProspecto = new clsProspecto();
        Utils utils = new Utils();
        enum notiTypes { success = 1, warning = 2, danger = 3 }

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initDTDocumentos();
                grvDocumentos.DataSource = new string[] { };
                grvDocumentos.DataBind();
                divEgDocumentos.Visible = true;
            }
        }
        #endregion

        #region Metodos

        private void initDTDocumentos()
        {
            DataTable dtDocumentos = new DataTable();

            try
            {
                dtDocumentos.Columns.Add("idDocumento", typeof(int));
                dtDocumentos.Columns.Add("idProspecto", typeof(int));
                dtDocumentos.Columns.Add("Nombre", typeof(string));
                dtDocumentos.Columns.Add("rutaFTP", typeof(string));
                dtDocumentos.Columns.Add("arrDocumento", typeof(byte[]));

                Session["dtDocumentos"] = dtDocumentos;
            }
            catch (Exception ex)
            {
                utils.showNotification(this, "Error", "Error al inicializar el listado de documentos: " + ex.Message, Utils.notiTypes.danger);
            }
        }

        protected void cambiaEstatusArchivo(int Estatus)
        {
            DataTable dtDocumentos = (DataTable)Session["dtDocumentos"];
            string classIcono = "", classEstatus = "", nombreArchivo = "", uploadedFiles = "";

            try
            {
                if (Estatus == 0)
                {
                    if (dtDocumentos.Rows.Count > 0)
                    {
                        dtDocumentos.Clear();
                        Session["dtDocumentos"] = dtDocumentos;
                    }

                    hdnNumeroArchivos.Value = "";
                    hdnNombreArchivo.Value = "";

                    classIcono = fileIcon.Attributes["class"];
                    classIcono = classIcono.Replace("text-success", string.Empty);
                    classIcono = classIcono.Replace("text-danger", string.Empty);
                    classIcono += " text-muted";
                    fileIcon.Attributes["class"] = classIcono;

                    classEstatus = pEstatusArchivo.Attributes["class"];
                    classEstatus = classEstatus.Replace("text-success", string.Empty);
                    classEstatus = classEstatus.Replace("text-danger", string.Empty);
                    classEstatus += " text-muted";
                    pEstatusArchivo.Attributes["class"] = classEstatus;

                    pEstatusArchivo.InnerHtml = "<span class='fa fa-upload'></span> Cargar archivo " +
                                                "<span class='badge badge-danger' id='bgeFilesToUpload' onclick='hideShowListFiles(true);'></span>";

                    pNombreArchivo.InnerText = "Seleccionar archivo...";
                }

                if (Estatus == 1) // Archivos leídos
                {
                    uploadedFiles = hdnNumeroArchivos.Value;

                    classIcono = fileIcon.Attributes["class"];
                    classIcono = classIcono.Replace("text-muted", string.Empty);
                    classIcono = classIcono.Replace("text-danger", string.Empty);
                    classIcono += " text-success";
                    fileIcon.Attributes["class"] = classIcono;

                    classEstatus = pEstatusArchivo.Attributes["class"];
                    classEstatus = classEstatus.Replace("text-muted", string.Empty);
                    classEstatus = classEstatus.Replace("text-danger", string.Empty);
                    classEstatus += " text-success";
                    pEstatusArchivo.Attributes["class"] = classEstatus;
                    pEstatusArchivo.InnerHtml = "<span class='fa fa-check'></span> Archivo leído con éxito <span class='badge badge-danger'>" + uploadedFiles + "</span>";

                    nombreArchivo = hdnNombreArchivo.Value;
                    pNombreArchivo.InnerText = nombreArchivo;
                }
            }
            catch (Exception ex)
            {
                utils.showNotification(this, "Error", "Error al cambiar de estatus de documento: " + ex.Message, Utils.notiTypes.danger);
            }
        }
        #endregion

        #region Eventos

        protected void filupDocumento_UploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
        {
            DataTable dtDocumentos;
            DataRow rowArchivo;
            string nombreFTP = string.Empty;

            try
            {
                if (Session["dtDocumentos"] == null)
                    initDTDocumentos();

                dtDocumentos = (DataTable)Session["dtDocumentos"];

                using (var stream = e.GetStreamContents())
                {
                    nombreFTP = e.FileName.Substring(0, e.FileName.Length - e.ContentType.Length);
                    nombreFTP += "-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    nombreFTP += e.ContentType;

                    rowArchivo = dtDocumentos.NewRow();
                    rowArchivo["idDocumento"] = 0;
                    rowArchivo["idDocumento"] = 0;
                    rowArchivo["Nombre"] = nombreFTP;
                    rowArchivo["rutaFTP"] = "/";
                    rowArchivo["arrDocumento"] = e.GetContents();

                    dtDocumentos.Rows.Add(rowArchivo);
                }

                Session["dtDocumentos"] = dtDocumentos;
                Session["nombreDocumento"] = nombreFTP;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ActFileUpload", "filupDocumento_OnClientUploadCompleteAll();", true);
            }
            catch (Exception ex)
            {
                utils.showNotification(this, "Error", "Error al leer el documento: " + ex.Message, Utils.notiTypes.danger);
            }
        }

        protected void btnSubirArchivos_Click(object sender, EventArgs e)
        {
            string nombreDocumento = string.Empty, nombreDocUsuario = string.Empty, docExtension = string.Empty;
            DataRow[] rowsResult;

            try
            {
                DataTable dtDocumentos = (DataTable)Session["dtDocumentos"];
                nombreDocumento = Session["nombreDocumento"].ToString();
                nombreDocUsuario = txtNombreDocumento.Text;

                if (dtDocumentos != null)
                {
                    // Busca el documento para renombrarlo
                    rowsResult = dtDocumentos.Select("Nombre = '" + nombreDocumento + "'");
                    
                    if (rowsResult != null)
                    {
                        if (rowsResult.Count() > 0)
                        {
                            docExtension = Path.GetExtension(nombreDocumento);
                            rowsResult[0]["Nombre"] = nombreDocUsuario + docExtension;
                        }
                    }

                    grvDocumentos.DataSource = dtDocumentos;
                }
                else
                    grvDocumentos.DataSource = new string[] { };

                grvDocumentos.DataBind();
                divEgDocumentos.Visible = grvDocumentos.Rows.Count == 0;
                txtNombreDocumento.Text = "";
            }
            catch (Exception ex)
            {
                utils.showNotification(this, "Error", "Error al listar el documento: " + ex.Message, Utils.notiTypes.danger);
            }
            finally
            {
                //OCULTA EL LOADER
                utils.ocultaLoader(this);
            }
        }

        protected void btnLimpiarArchivos_Click(object sender, EventArgs e)
        {
            cambiaEstatusArchivo(0);
        }

        protected void btnConfirmacion_Click(object sender, EventArgs e)
        {
            Prospecto prospecto = new Prospecto();
            DataTable dtDocumentos;
            clsFTP Ftp = new clsFTP();
            MemoryStream msDocumento;
            byte[] arrDocumento;
            int idProspecto = 0;

            try
            {
                if (Session["dtDocumentos"] == null)
                    return;

                dtDocumentos = (DataTable)Session["dtDocumentos"];

                prospecto.Nombre = txtNombre.Text;
                prospecto.PrimerApellido = txtPrimerApellido.Text;
                prospecto.SegundoApellido = txtSegundoApellido.Text;
                prospecto.Calle = txtCalle.Text;
                prospecto.Numero = txtNumero.Text == "" ? 0 : Convert.ToInt32(txtNumero.Text);
                prospecto.Colonia = txtColonia.Text;
                prospecto.CodigoPostal = txtCodigoPostal.Text == "" ? 0 : Convert.ToInt32(txtCodigoPostal.Text);
                prospecto.Telefono = txtTelefono.Text;
                prospecto.RFC = txtRFC.Text;
                prospecto.dtDocumentos = dtDocumentos;

                resultado = clsProspecto.enviarProspecto(prospecto);

                if (resultado.Error)
                {
                    utils.showNotification(this, "Error", "Error al enviar: " + resultado.MensajeError, Utils.notiTypes.danger);
                }
                else
                {
                    idProspecto = Convert.ToInt32(resultado.Datos.Tables[0].Rows[0][0]);

                    if (dtDocumentos.Rows.Count > 0)
                    {
                        // Inicia sesión en el servidor ftp
                        Ftp.ftpLogin();
                        foreach (DataRow doc in dtDocumentos.Rows)
                        {
                            arrDocumento = (byte[])doc["arrDocumento"];
                            msDocumento = new MemoryStream(arrDocumento);

                            if (msDocumento != null)
                                Ftp.CargaDocumento(msDocumento, doc["Nombre"].ToString(), idProspecto.ToString());
                        }
                    }

                    btnEnviar.Visible = false;

                    utils.hideShowModal(this, "mdlConfirmacion", false);
                    utils.showNotification(this, "Prospecto enviado", "El prospecto <strong>" + prospecto.Nombre + "</strong> ha sido enviado correctamente", Utils.notiTypes.success);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect", "setTimeout(function(){ window.location ='ListadoProspecto.aspx'; }, 3000);", true);
                }
            }
            catch (Exception ex)
            {
                utils.showNotification(this, "Error", "Error al enviar: " + ex.Message, Utils.notiTypes.danger);
            }
            finally
            {
                //OCULTA EL LOADER
                utils.ocultaLoader(this);
            }
        }

        protected void grvDocumentos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = 0;
            string nombreDoc = string.Empty;
            DataTable dtDocumentos;
            DataRow[] rowsResult;

            try
            {
                if (e.CommandName == "Quitar")
                {
                    index = Convert.ToInt32(e.CommandArgument.ToString());
                    dtDocumentos = (DataTable)Session["dtDocumentos"];

                    nombreDoc = grvDocumentos.DataKeys[index]["Nombre"].ToString();
                    rowsResult = dtDocumentos.Select("Nombre = '" + nombreDoc + "'");

                    if (rowsResult.Count() == 0)
                    {
                        utils.showNotification(this, "No se ha encontrado el documento", "El documento que intenta quitar no se encuentra en el listado", Utils.notiTypes.danger);
                        return;
                    }

                    dtDocumentos.Rows.Remove(rowsResult[0]);

                    Session["dtDocumentos"] = dtDocumentos;
                    grvDocumentos.DataSource = dtDocumentos;
                    grvDocumentos.DataBind();
                    divEgDocumentos.Visible = grvDocumentos.Rows.Count == 0;
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

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Views/ListadoProspecto.aspx");
            }
            catch (Exception ex)
            {
                utils.showNotification(this, "Error", "Error al redireccionar al listado de prospectos: " + ex.Message, Utils.notiTypes.danger);
            }
        }
        #endregion
    }
}