using BSI.GestDoc.Entity;
using BSI.GestDoc.Repository.CRUD;
using BSI.GestDoc.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BSI.GestDoc.CustomException
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message)
        {
            this.gravarLog(message, null, EnumTipoMensagem.Erro);
        }

        public CustomException(string message, Exception innerException) : base(message, innerException)
        {
            this.gravarLog(message, innerException, EnumTipoMensagem.Erro);
        }

        public CustomException(string message, Exception innerException, EnumTipoMensagem TipoMensagem) : base(message, innerException)
        {
            this.gravarLog(message, innerException, TipoMensagem);
        }

        private void gravarLog(string message, Exception innerException, EnumTipoMensagem TipoMensagem)
        {
            Entity.LogErro logErro = new Entity.LogErro();

            try
            {
                var session = HttpContext.Current.Session;
                //IDictionary authenticationProperties = (IDictionary)session["AuthenticationProperties"];

                logErro.Ambiente = (int)Util.Ambiente.AmbienteExecucao;
                logErro.Data = DateTime.Now;
                logErro.Descricao = message;
                logErro.HostName = HttpContext.Current.Request.UserHostAddress;
                logErro.TipoMensagem = (int)TipoMensagem;
                if (innerException != null)
                    logErro.Trace = innerException.StackTrace;

                logErro.UsuarioId = "1";//authenticationProperties["usuarioId"].ToString();

                new LogErroDal().Insert(logErro);

            }
            catch (Exception ex)
            {
                //Deu erro para gravar no banco, grava em arquivo.
                string directoryLogErro = ConfigurationManager.AppSettings["DirectoryLogErro"];
                string WorkingFolder = HttpRuntime.AppDomainAppPath;
                if (!Directory.Exists(WorkingFolder + directoryLogErro))
                {
                    Directory.CreateDirectory(WorkingFolder + directoryLogErro);
                }

                DateTime DateTime = DateTime.Now;
                string path = WorkingFolder + directoryLogErro + "\\" +
                                              DateTime.ToString("yyyyMMdd") + "_Erro.txt";

                if (!File.Exists(path))
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        writeFile(sw, logErro, ex);
                    }
                }
                using (StreamWriter sw = File.AppendText(path))
                {
                    writeFile(sw, logErro, ex);
                }
            }

           
        }

        private void writeFile(StreamWriter sw, LogErro logErro, Exception ex )
        {
            sw.WriteLine("-------------------------------------------------------");
            sw.WriteLine("Data --> " + logErro.Data);
            sw.WriteLine("Ambiente --> " + Ambiente.AmbienteExecucao.ToString());
            sw.WriteLine("Descricao --> " + logErro.Descricao);
            sw.WriteLine("HostName --> " + logErro.HostName);
            sw.WriteLine("TipoErro --> " + logErro.TipoMensagem);
            sw.WriteLine("Trace --> " + logErro.Trace);
            sw.WriteLine("UsuarioId --> " + logErro.UsuarioId);
            sw.WriteLine("Erro SQL --> " + ex.Message);
            sw.WriteLine("Trace SQL --> " + ex.StackTrace);
        }
    }
}
