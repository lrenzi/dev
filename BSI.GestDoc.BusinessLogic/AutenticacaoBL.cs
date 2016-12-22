using BSI.GestDoc.Entity;
using BSI.GestDoc.Repository.DAL;
using System;
using System.Threading.Tasks;

namespace BSI.GestDoc.BusinessLogic
{
    public class AutenticacaoBL
    {
        public AutenticacaoBL()
        {
        }

        /// <summary>
        /// Retorna usuário
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<Usuario> EfetuarLogin(string usuarioId, string password)
        {
            AutenticacaoDal Dal = new AutenticacaoDal();

            Usuario user = await Dal.Efetuarlogin(usuarioId, GetMd5Hash(password));

            return user;
        }

        /// <summary>
        /// Efetua criptografia da senha
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        private static string GetMd5Hash(string texto)
        {
            try
            {
                System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(texto);
                byte[] hash = md5.ComputeHash(inputBytes);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                return sb.ToString(); // Retorna senha criptografada 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
