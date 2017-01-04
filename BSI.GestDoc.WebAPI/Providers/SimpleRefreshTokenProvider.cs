using BSI.GestDoc.Entity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BSI.GestDoc.WebAPI.Providers
{
    public class SimpleRefreshTokenProvider : IAuthenticationTokenProvider
    {

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var usuarioId = context.Ticket.Properties.Dictionary["usuarioId"];

            if (string.IsNullOrEmpty(usuarioId))
            {
                return;
            }

            var refreshTokenId = Guid.NewGuid().ToString("n");

            BusinessLogic.AutenticacaoBL _auth = new BusinessLogic.AutenticacaoBL();

            var refreshTokenLifeTime = System.Configuration.ConfigurationManager.AppSettings["clientRefreshTokenLifeTime"];

            var token = new Token()
            {
                Id = Helper.GetHash(refreshTokenId),
                UsuarioId = int.Parse(usuarioId),
                IdentityName = context.Ticket.Identity.Name,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
            };

            context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
            context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

            token.ProtectedTicket = context.SerializeTicket();

            var result = await _auth.AddRefreshToken(token);

            if (result)
            {
                context.SetToken(refreshTokenId);
            }


        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {

            var allowedOrigin = context.OwinContext.Get<string>("clientAllowedOrigin");
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            string hashedTokenId = Helper.GetHash(context.Token);

            BusinessLogic.AutenticacaoBL _auth = new BusinessLogic.AutenticacaoBL();
            var existingToken = _auth.GetAllRefreshTokens().ToList().FindAll(r => r.Id == hashedTokenId).SingleOrDefault();

            if (existingToken != null)
            {
                //Get protectedTicket from refreshToken class
                context.DeserializeTicket(existingToken.ProtectedTicket);
                var result = await _auth.RemoveRefreshToken(existingToken.TokenId);
            }

        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }
    }
}