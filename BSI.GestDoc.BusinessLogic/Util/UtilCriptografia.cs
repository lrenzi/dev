using System;

namespace BSI.GestDoc.BusinessLogic.Util
{
    public static class UtilCriptografia
    {

        /// <summary>
        /// Efetua criptografia da senha
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static string GetMd5Hash(string texto)
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
