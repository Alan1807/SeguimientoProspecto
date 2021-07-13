using SeguimientoProspecto.Clases.Utils;
using SeguimientoProspecto.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SeguimientoProspecto.Clases
{
    public class clsProspecto
    {
        /// <summary>
        /// Envía prospecto
        /// </summary>
        /// <param name="prospecto">Prospecto</param>
        /// <returns></returns>
        public Result enviarProspecto(Prospecto prospecto)
        {
            SqlConnection connection = new SqlConnection();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter data = new SqlDataAdapter();
            DataSet dsResult = new DataSet();
            Result result = new Result();
            DataTable dtDocumentos;
            SqlParameter[] sqlParametros = new SqlParameter[11];

            try
            {
                dtDocumentos = prospecto.dtDocumentos.Copy();
                dtDocumentos.Columns.Remove("arrDocumento");

                sqlParametros[0] = new SqlParameter();
                sqlParametros[0].ParameterName = "@Opcion";
                sqlParametros[0].SqlDbType = SqlDbType.Int;
                sqlParametros[0].Direction = ParameterDirection.Input;
                sqlParametros[0].Value = 1;

                sqlParametros[1] = new SqlParameter();
                sqlParametros[1].ParameterName = "@Nombre";
                sqlParametros[1].SqlDbType = SqlDbType.VarChar;
                sqlParametros[1].Size = 50;
                sqlParametros[1].Direction = ParameterDirection.Input;
                sqlParametros[1].Value = prospecto.Nombre;

                sqlParametros[2] = new SqlParameter();
                sqlParametros[2].ParameterName = "@PrimerApellido";
                sqlParametros[2].SqlDbType = SqlDbType.VarChar;
                sqlParametros[2].Size = 50;
                sqlParametros[2].Direction = ParameterDirection.Input;
                sqlParametros[2].Value = prospecto.PrimerApellido;

                sqlParametros[3] = new SqlParameter();
                sqlParametros[3].ParameterName = "@SegundoApellido";
                sqlParametros[3].SqlDbType = SqlDbType.VarChar;
                sqlParametros[3].Size = 50;
                sqlParametros[3].Direction = ParameterDirection.Input;
                sqlParametros[3].Value = prospecto.SegundoApellido;

                sqlParametros[4] = new SqlParameter();
                sqlParametros[4].ParameterName = "@Calle";
                sqlParametros[4].SqlDbType = SqlDbType.VarChar;
                sqlParametros[4].Size = 100;
                sqlParametros[4].Direction = ParameterDirection.Input;
                sqlParametros[4].Value = prospecto.Calle;

                sqlParametros[5] = new SqlParameter();
                sqlParametros[5].ParameterName = "@Numero";
                sqlParametros[5].SqlDbType = SqlDbType.Int;
                sqlParametros[5].Direction = ParameterDirection.Input;
                sqlParametros[5].Value = prospecto.Numero;

                sqlParametros[6] = new SqlParameter();
                sqlParametros[6].ParameterName = "@Colonia";
                sqlParametros[6].SqlDbType = SqlDbType.VarChar;
                sqlParametros[6].Size = 50;
                sqlParametros[6].Direction = ParameterDirection.Input;
                sqlParametros[6].Value = prospecto.Colonia;

                sqlParametros[7] = new SqlParameter();
                sqlParametros[7].ParameterName = "@CodigoPostal";
                sqlParametros[7].SqlDbType = SqlDbType.Int;
                sqlParametros[7].Direction = ParameterDirection.Input;
                sqlParametros[7].Value = prospecto.CodigoPostal;

                sqlParametros[8] = new SqlParameter();
                sqlParametros[8].ParameterName = "@Telefono";
                sqlParametros[8].SqlDbType = SqlDbType.VarChar;
                sqlParametros[8].Size = 15;
                sqlParametros[8].Direction = ParameterDirection.Input;
                sqlParametros[8].Value = prospecto.Telefono;

                sqlParametros[9] = new SqlParameter();
                sqlParametros[9].ParameterName = "@RFC";
                sqlParametros[9].SqlDbType = SqlDbType.VarChar;
                sqlParametros[9].Size = 20;
                sqlParametros[9].Direction = ParameterDirection.Input;
                sqlParametros[9].Value = prospecto.RFC;

                sqlParametros[10] = new SqlParameter();
                sqlParametros[10].ParameterName = "@Documentos";
                sqlParametros[10].SqlDbType = SqlDbType.Structured;
                sqlParametros[10].Direction = ParameterDirection.Input;
                sqlParametros[10].Value = dtDocumentos;

                connection.ConnectionString = ConfigurationManager.ConnectionStrings["ProspectoConnection"].ConnectionString;

                connection.Open();
                command.Connection = connection;

                command.CommandText = "spProspecto";
                command.CommandTimeout = 1200;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(sqlParametros);

                data.SelectCommand = command;
                data.Fill(dsResult, "Prospecto");

                connection.Close();
                connection.Dispose();

                result.Datos = dsResult;
                result.Error = false;
            }
            catch (SqlException ex)
            {
                result.Error = true;
                result.MensajeError = "[ERROR: BaseDeDatos - enviarProspecto] = " + ex.Message;
                result.Excepcion = ex;
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.MensajeError = "[ERROR: Exception - enviarProspecto]= " + ex.Message;
                result.Excepcion = ex;
            }

            return result;
        }

        /// <summary>
        /// Obtiene la configuración del FTP
        /// </summary>
        /// <returns></returns>
        public Result obtenerConfFTP()
        {
            SqlConnection connection = new SqlConnection();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter data = new SqlDataAdapter();
            DataSet dsResult = new DataSet();

            Result result = new Result();
            SqlParameter[] sqlParametros = new SqlParameter[1];

            try
            {
                sqlParametros[0] = new SqlParameter();
                sqlParametros[0].ParameterName = "@Opcion";
                sqlParametros[0].SqlDbType = SqlDbType.Int;
                sqlParametros[0].Direction = ParameterDirection.Input;
                sqlParametros[0].Value = 2;

                connection.ConnectionString = ConfigurationManager.ConnectionStrings["ProspectoConnection"].ConnectionString;

                connection.Open();
                command.Connection = connection;

                command.CommandText = "spProspecto";
                command.CommandTimeout = 1200;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(sqlParametros);

                data.SelectCommand = command;
                data.Fill(dsResult, "FTPConfig");

                connection.Close();
                connection.Dispose();

                result.Datos = dsResult;
                result.Error = false;
            }
            catch (SqlException ex)
            {
                result.Error = true;
                result.MensajeError = "[ERROR: BaseDeDatos - obtenerConfFTP] = " + ex.Message;
                result.Excepcion = ex;
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.MensajeError = "[ERROR: Exception - obtenerConfFTP]= " + ex.Message;
                result.Excepcion = ex;
            }

            return result;
        }

        /// <summary>
        /// Obtiene listado de estatus de prospecto
        /// </summary>
        /// <returns></returns>
        public Result obtenerEstatus()
        {
            SqlConnection connection = new SqlConnection();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter data = new SqlDataAdapter();
            DataSet dsResult = new DataSet();

            Result result = new Result();
            SqlParameter[] sqlParametros = new SqlParameter[1];

            try
            {
                sqlParametros[0] = new SqlParameter();
                sqlParametros[0].ParameterName = "@Opcion";
                sqlParametros[0].SqlDbType = SqlDbType.Int;
                sqlParametros[0].Direction = ParameterDirection.Input;
                sqlParametros[0].Value = 4;

                connection.ConnectionString = ConfigurationManager.ConnectionStrings["ProspectoConnection"].ConnectionString;

                connection.Open();
                command.Connection = connection;

                command.CommandText = "spProspecto";
                command.CommandTimeout = 1200;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(sqlParametros);

                data.SelectCommand = command;
                data.Fill(dsResult, "ProspectoEstatus");

                connection.Close();
                connection.Dispose();

                result.Datos = dsResult;
                result.Error = false;
            }
            catch (SqlException ex)
            {
                result.Error = true;
                result.MensajeError = "[ERROR: BaseDeDatos - obtenerEstatus] = " + ex.Message;
                result.Excepcion = ex;
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.MensajeError = "[ERROR: Exception - obtenerEstatus]= " + ex.Message;
                result.Excepcion = ex;
            }

            return result;
        }

        /// <summary>
        /// Obtiene detalle de prospecto
        /// </summary>
        /// <param name="idProspecto">Id del prospecto</param>
        /// <returns></returns>
        public Result obtenerProspecto(int idProspecto)
        {
            SqlConnection connection = new SqlConnection();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter data = new SqlDataAdapter();
            DataSet dsResult = new DataSet();
            Result result = new Result();
            SqlParameter[] sqlParametros = new SqlParameter[2];

            try
            {
                sqlParametros[0] = new SqlParameter();
                sqlParametros[0].ParameterName = "@Opcion";
                sqlParametros[0].SqlDbType = SqlDbType.Int;
                sqlParametros[0].Direction = ParameterDirection.Input;
                sqlParametros[0].Value = 3;

                sqlParametros[1] = new SqlParameter();
                sqlParametros[1].ParameterName = "@idProspecto";
                sqlParametros[1].SqlDbType = SqlDbType.Int;
                sqlParametros[1].Direction = ParameterDirection.Input;
                sqlParametros[1].Value = idProspecto;

                connection.ConnectionString = ConfigurationManager.ConnectionStrings["ProspectoConnection"].ConnectionString;

                connection.Open();
                command.Connection = connection;

                command.CommandText = "spProspecto";
                command.CommandTimeout = 1200;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(sqlParametros);

                data.SelectCommand = command;
                data.Fill(dsResult, "Prospecto");

                connection.Close();
                connection.Dispose();

                result.Datos = dsResult;
                result.Error = false;
            }
            catch (SqlException ex)
            {
                result.Error = true;
                result.MensajeError = "[ERROR: BaseDeDatos - obtenerProspecto] = " + ex.Message;
                result.Excepcion = ex;
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.MensajeError = "[ERROR: Exception - obtenerProspecto]= " + ex.Message;
                result.Excepcion = ex;
            }

            return result;
        }

        /// <summary>
        /// Obtiene listado de prospectos que coincidan con los filtros
        /// </summary>
        /// <param name="prospecto">Prospecto a filtrar</param>
        /// <returns></returns>
        public Result obtenerProspecto(Prospecto prospecto)
        {
            SqlConnection connection = new SqlConnection();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter data = new SqlDataAdapter();
            DataSet dsResult = new DataSet();
            Result result = new Result();
            SqlParameter[] sqlParametros = new SqlParameter[10];

            try
            {
                sqlParametros[0] = new SqlParameter();
                sqlParametros[0].ParameterName = "@Opcion";
                sqlParametros[0].SqlDbType = SqlDbType.Int;
                sqlParametros[0].Direction = ParameterDirection.Input;
                sqlParametros[0].Value = 3;

                sqlParametros[1] = new SqlParameter();
                sqlParametros[1].ParameterName = "@Nombre";
                sqlParametros[1].SqlDbType = SqlDbType.VarChar;
                sqlParametros[1].Size = 50;
                sqlParametros[1].Direction = ParameterDirection.Input;
                sqlParametros[1].Value = prospecto.Nombre;

                sqlParametros[2] = new SqlParameter();
                sqlParametros[2].ParameterName = "@PrimerApellido";
                sqlParametros[2].SqlDbType = SqlDbType.VarChar;
                sqlParametros[2].Size = 50;
                sqlParametros[2].Direction = ParameterDirection.Input;
                sqlParametros[2].Value = prospecto.PrimerApellido;

                sqlParametros[3] = new SqlParameter();
                sqlParametros[3].ParameterName = "@SegundoApellido";
                sqlParametros[3].SqlDbType = SqlDbType.VarChar;
                sqlParametros[3].Size = 50;
                sqlParametros[3].Direction = ParameterDirection.Input;
                sqlParametros[3].Value = prospecto.SegundoApellido;

                sqlParametros[4] = new SqlParameter();
                sqlParametros[4].ParameterName = "@Calle";
                sqlParametros[4].SqlDbType = SqlDbType.VarChar;
                sqlParametros[4].Size = 100;
                sqlParametros[4].Direction = ParameterDirection.Input;
                sqlParametros[4].Value = prospecto.Calle;

                sqlParametros[5] = new SqlParameter();
                sqlParametros[5].ParameterName = "@Numero";
                sqlParametros[5].SqlDbType = SqlDbType.Int;
                sqlParametros[5].Direction = ParameterDirection.Input;
                sqlParametros[5].Value = prospecto.Numero;

                sqlParametros[6] = new SqlParameter();
                sqlParametros[6].ParameterName = "@Colonia";
                sqlParametros[6].SqlDbType = SqlDbType.VarChar;
                sqlParametros[6].Size = 50;
                sqlParametros[6].Direction = ParameterDirection.Input;
                sqlParametros[6].Value = prospecto.Colonia;

                sqlParametros[7] = new SqlParameter();
                sqlParametros[7].ParameterName = "@CodigoPostal";
                sqlParametros[7].SqlDbType = SqlDbType.Int;
                sqlParametros[7].Direction = ParameterDirection.Input;
                sqlParametros[7].Value = prospecto.CodigoPostal;

                sqlParametros[8] = new SqlParameter();
                sqlParametros[8].ParameterName = "@RFC";
                sqlParametros[8].SqlDbType = SqlDbType.VarChar;
                sqlParametros[8].Size = 20;
                sqlParametros[8].Direction = ParameterDirection.Input;
                sqlParametros[8].Value = prospecto.RFC;

                sqlParametros[9] = new SqlParameter();
                sqlParametros[9].ParameterName = "@idEstatus";
                sqlParametros[9].SqlDbType = SqlDbType.Int;
                sqlParametros[9].Direction = ParameterDirection.Input;
                sqlParametros[9].Value = prospecto.idEstatus;

                connection.ConnectionString = ConfigurationManager.ConnectionStrings["ProspectoConnection"].ConnectionString;

                connection.Open();
                command.Connection = connection;

                command.CommandText = "spProspecto";
                command.CommandTimeout = 1200;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(sqlParametros);

                data.SelectCommand = command;
                data.Fill(dsResult, "Prospecto");

                connection.Close();
                connection.Dispose();

                result.Datos = dsResult;
                result.Error = false;
            }
            catch (SqlException ex)
            {
                result.Error = true;
                result.MensajeError = "[ERROR: BaseDeDatos - obtenerProspecto] = " + ex.Message;
                result.Excepcion = ex;
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.MensajeError = "[ERROR: Exception - obtenerProspecto]= " + ex.Message;
                result.Excepcion = ex;
            }

            return result;
        }

        /// <summary>
        /// Obtiene documentos del prospecto
        /// </summary>
        /// <param name="idProspecto">Id del prospecto</param>
        /// <returns></returns>
        public Result obtenerDocumentos(int idProspecto)
        {
            SqlConnection connection = new SqlConnection();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter data = new SqlDataAdapter();
            DataSet dsResult = new DataSet();
            Result result = new Result();
            SqlParameter[] sqlParametros = new SqlParameter[2];

            try
            {
                sqlParametros[0] = new SqlParameter();
                sqlParametros[0].ParameterName = "@Opcion";
                sqlParametros[0].SqlDbType = SqlDbType.Int;
                sqlParametros[0].Direction = ParameterDirection.Input;
                sqlParametros[0].Value = 5;

                sqlParametros[1] = new SqlParameter();
                sqlParametros[1].ParameterName = "@idProspecto";
                sqlParametros[1].SqlDbType = SqlDbType.Int;
                sqlParametros[1].Direction = ParameterDirection.Input;
                sqlParametros[1].Value = idProspecto;

                connection.ConnectionString = ConfigurationManager.ConnectionStrings["ProspectoConnection"].ConnectionString;

                connection.Open();
                command.Connection = connection;

                command.CommandText = "spProspecto";
                command.CommandTimeout = 1200;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(sqlParametros);

                data.SelectCommand = command;
                data.Fill(dsResult, "Prospecto");

                connection.Close();
                connection.Dispose();

                result.Datos = dsResult;
                result.Error = false;
            }
            catch (SqlException ex)
            {
                result.Error = true;
                result.MensajeError = "[ERROR: BaseDeDatos - obtenerDocumentos] = " + ex.Message;
                result.Excepcion = ex;
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.MensajeError = "[ERROR: Exception - obtenerDocumentos]= " + ex.Message;
                result.Excepcion = ex;
            }

            return result;
        }

        /// <summary>
        /// Autoriza o Rechaza prospecto
        /// </summary>
        /// <param name="Autoriza">True: Autoriza | False: Rechaza</param>
        /// <returns></returns>
        public Result autorizaRechazaProspecto(bool Autoriza, int idProspecto, string observacionesRechazo)
        {
            SqlConnection connection = new SqlConnection();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter data = new SqlDataAdapter();
            DataSet dsResult = new DataSet();
            Result result = new Result();
            SqlParameter[] sqlParametros = new SqlParameter[4];

            try
            {
                sqlParametros[0] = new SqlParameter();
                sqlParametros[0].ParameterName = "@Opcion";
                sqlParametros[0].SqlDbType = SqlDbType.Int;
                sqlParametros[0].Direction = ParameterDirection.Input;
                sqlParametros[0].Value = 6;

                sqlParametros[1] = new SqlParameter();
                sqlParametros[1].ParameterName = "@idProspecto";
                sqlParametros[1].SqlDbType = SqlDbType.Int;
                sqlParametros[1].Direction = ParameterDirection.Input;
                sqlParametros[1].Value = idProspecto;

                sqlParametros[2] = new SqlParameter();
                sqlParametros[2].ParameterName = "@Autoriza";
                sqlParametros[2].SqlDbType = SqlDbType.Bit;
                sqlParametros[2].Direction = ParameterDirection.Input;
                sqlParametros[2].Value = Autoriza;

                sqlParametros[3] = new SqlParameter();
                sqlParametros[3].ParameterName = "@ObservacionRechazo";
                sqlParametros[3].SqlDbType = SqlDbType.VarChar;
                sqlParametros[3].Size = 500;
                sqlParametros[3].Direction = ParameterDirection.Input;
                sqlParametros[3].Value = observacionesRechazo;

                connection.ConnectionString = ConfigurationManager.ConnectionStrings["ProspectoConnection"].ConnectionString;

                connection.Open();
                command.Connection = connection;

                command.CommandText = "spProspecto";
                command.CommandTimeout = 1200;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(sqlParametros);

                data.SelectCommand = command;
                data.Fill(dsResult, "Prospecto");

                connection.Close();
                connection.Dispose();

                result.Datos = dsResult;
                result.Error = false;
            }
            catch (SqlException ex)
            {
                result.Error = true;
                result.MensajeError = "[ERROR: BaseDeDatos - autorizaRechazaProspecto] = " + ex.Message;
                result.Excepcion = ex;
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.MensajeError = "[ERROR: Exception - autorizaRechazaProspecto]= " + ex.Message;
                result.Excepcion = ex;
            }

            return result;
        }
    }
}