using SeguimientoProspecto.Clases;
using SeguimientoProspecto.Clases.Utils;
using SeguimientoProspecto.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
                if (e.CommandName == "Ver")
                {
                    index = Convert.ToInt32(e.CommandArgument.ToString());
                    idProspecto = Convert.ToInt32(gvListadoProspecto.DataKeys[index]["idProspecto"]);

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
    }
}