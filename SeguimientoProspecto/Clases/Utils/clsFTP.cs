using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace SeguimientoProspecto.Clases.Utils
{
    public class clsFTP
    {
        clsProspecto prospecto = new clsProspecto();
        private string _hostFTP;
        private string _puertoFTP;
        private string _directorioFTP;
        private string _usuarioFTP;
        private string _passwordFTP;

        public string hostFTP
        {
            get { return _hostFTP; }
            set { _hostFTP = value; }
        }
        public string puertoFTP
        {
            get { return _puertoFTP; }
            set { _puertoFTP = value; }
        }
        public string directorioFTP
        {
            get { return _directorioFTP; }
            set { _directorioFTP = value; }
        }
        public string usuarioFTP
        {
            get { return _usuarioFTP; }
            set { _usuarioFTP = value; }
        }
        public string passwordFTP
        {
            get { return _passwordFTP; }
            set { _passwordFTP = value; }
        }

        public void ftpLogin()
        {
            try
            {
                Result result = new Result();
                result = prospecto.obtenerConfFTP();
                DataRow rowResult;

                if (result.Error)
                    throw result.Excepcion;

                if (result.Datos.Tables[0].Rows.Count > 0)
                {
                    rowResult = result.Datos.Tables[0].Rows[0];
                    hostFTP = rowResult["hostFTP"].ToString();
                    puertoFTP = rowResult["puertoFTP"].ToString();
                    directorioFTP = rowResult["directorioFTP"].ToString();
                    usuarioFTP = rowResult["usuarioFTP"].ToString();
                    passwordFTP = rowResult["passwordFTP"].ToString();
                }
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void CargaDocumento(MemoryStream msDocumento, string nombreDocumento, string Carpeta)
        {
            try
            {
                // Arma las rutas
                string ftpUrl = @"ftp://" + _hostFTP + "/" + _directorioFTP + "/" + Carpeta + "/" + nombreDocumento;
                string carpetaURL = @"ftp://" + _hostFTP + "/" + _directorioFTP + "/" + Carpeta + "/";
                string dir = @"ftp://" + _hostFTP + "/" + _directorioFTP + "/";

                // Escribe el archivo en la ruta del FTP
                FtpWebRequest request = FtpWebRequest.Create(ftpUrl) as FtpWebRequest;

                request.KeepAlive = true;
                request.UseBinary = true;
                request.Credentials = new NetworkCredential(_usuarioFTP, _passwordFTP);

                byte[] buffer = new byte[msDocumento.Length];
                msDocumento.Read(buffer, 0, buffer.Length);

                CreaCarpetaFTP(dir, carpetaURL, Carpeta);

                request.Method = WebRequestMethods.Ftp.UploadFile;

                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(buffer, 0, buffer.Length);
                }

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private void CreaCarpetaFTP(string URL, string carpeta, string nombreCarpeta)
        {
            List<string> lista = new List<string>();
            string line = string.Empty;

            try
            {

                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(URL);
                request.Credentials = new NetworkCredential(_usuarioFTP, _passwordFTP);
                request.Method = WebRequestMethods.Ftp.ListDirectory;

                StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream());

                line = reader.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    lista.Add(line);
                    line = reader.ReadLine();
                }
                reader.Close();

                if (!lista.Contains(nombreCarpeta))
                {
                    request = (FtpWebRequest)FtpWebRequest.Create(carpeta);
                    request.Credentials = new NetworkCredential(_usuarioFTP, _passwordFTP);
                    request.Method = WebRequestMethods.Ftp.MakeDirectory;
                    request.GetResponse();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}