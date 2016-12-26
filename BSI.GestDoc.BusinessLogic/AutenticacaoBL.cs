using BSI.GestDoc.Entity;
using BSI.GestDoc.Repository.DAL;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Dispose()
        {

        }

        /*public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);

            return new IdentityUser() { UserName = "Wesley", Email = "email@dominio.com" };
        }

        public Client FindClient(string clientId)
        {
            var client = _ctx.Clients.Find(clientId);

            return client;
        }*/

        public async Task<bool> AddRefreshToken(Token token)
        {

            AutenticacaoDal autDal = new AutenticacaoDal();
            foreach (var existingToken in autDal.GetAtllToken().ToList().FindAll(r => r.IdentityName == token.IdentityName && r.ClienteId == token.ClienteId))
            {

                if (existingToken != null)
                {
                    var result = await RemoveRefreshToken(existingToken.TokenId);
                }
            }
            return autDal.InsertToken(token);

        }

        public async Task<bool> RemoveRefreshToken(long TokenId)
        {
            return new AutenticacaoDal().RemoveToken(TokenId);
        }



        public List<Token> GetAllRefreshTokens()
        {
            return new AutenticacaoDal().GetAtllToken().ToList();
        }
        /*
        public async Task<IdentityUser> FindAsync(UserLoginInfo loginInfo)
        {
            IdentityUser user = await _userManager.FindAsync(loginInfo);

            return user;
        }

        public async Task<IdentityResult> CreateAsync(IdentityUser user)
        {
            var result = await _userManager.CreateAsync(user);

            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await _userManager.AddLoginAsync(userId, login);

            return result;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }*/
    }
}
