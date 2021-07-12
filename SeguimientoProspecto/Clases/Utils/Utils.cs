using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace SeguimientoProspecto.Clases.Utils
{
    public class Utils
    {
        public enum notiTypes { success = 1, warning = 2, danger = 3 }

        /// <summary>
        /// Oculta loader
        /// </summary>
        /// <param name="control">Control</param>
        public void ocultaLoader(Control control)
        {
            ScriptManager.RegisterStartupScript(control, typeof(Page), "OcultaLoader" + Guid.NewGuid().ToString(), "hideShowLoader(false);", true);
        }

        /// <summary>
        /// Muestra notificación
        /// </summary>
        /// <param name="control">Control</param>
        /// <param name="Titulo">Título del mensaje</param>
        /// <param name="Descripcion">Descripción del mensaje</param>
        /// <param name="Tipo">Tipo de mensaje</param>
        public void showNotification(Control control, string Titulo, string Descripcion, notiTypes Tipo)
        {
            string script = "showNotification('" + Titulo + "', '" + Descripcion + "', '" + Tipo.ToString() + "');";
            ScriptManager.RegisterStartupScript(control, typeof(Page), "showNotification" + Guid.NewGuid().ToString(), script, true);
        }

        /// <summary>
        /// Muestra u oculta modal
        /// </summary>
        /// <param name="control">Control</param>
        /// <param name="idModal">Id del modal</param>
        /// <param name="Show">true: muestra | false: oculta</param>
        public void hideShowModal(Control control, string idModal, bool Show)
        {
            string script = "$('#" + idModal + "').modal('" + (Show ? "show" : "hide") + "');";
            ScriptManager.RegisterStartupScript(control, typeof(Page), "ModalShow" + idModal + Guid.NewGuid().ToString(), script, true);
        }
    }
}