using System;
using System.Data;

namespace SeguimientoProspecto.Clases.Utils
{
    public class Result
    {
        private bool error;
        private string mensajeError;
        private DataSet datos;
        private Exception excepcion;

        public bool Error
        {
            get { return error; }
            set { error = value; }
        }
        public string MensajeError
        {
            get { return mensajeError; }
            set { mensajeError = value; }
        }
        public DataSet Datos
        {
            get { return datos; }
            set { datos = value; }
        }
        public Exception Excepcion
        {
            get { return excepcion; }
            set { excepcion = value; }
        }
    }
}