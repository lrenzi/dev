using BSI.GestDoc.WebAPI.Entities;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BSI.GestDoc.Repository.DAL;
using BSI.GestDoc.Entity;

namespace BSI.GestDoc.WebAPI.Providers
{


    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {

            string clientId = string.Empty;
            string clientSecret = string.Empty;
            Client client = null;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                //Remove the comments from the below line context.SetError, and invalidate context 
                //if you want to force sending clientId/secrects once obtain access tokens. 
                context.Validated();
                //context.SetError("invalid_clientId", "ClientId should be sent.");
                return Task.FromResult<object>(null);
            }

            using (AuthRepository _repo = new AuthRepository())
            {
                client = _repo.FindClient(context.ClientId);
            }

            if (client == null)
            {
                context.SetError("invalid_clientId", string.Format("Client '{0}' is not registered in the system.", context.ClientId));
                return Task.FromResult<object>(null);
            }

            if (client.ApplicationType == Models.ApplicationTypes.NativeConfidential)
            {
                if (string.IsNullOrWhiteSpace(clientSecret))
                {
                    context.SetError("invalid_clientId", "Client secret should be sent.");
                    return Task.FromResult<object>(null);
                }
                else
                {
                    if (client.Secret != Helper.GetHash(clientSecret))
                    {
                        context.SetError("invalid_clientId", "Client secret is invalid.");
                        return Task.FromResult<object>(null);
                    }
                }
            }

            if (!client.Active)
            {
                context.SetError("invalid_clientId", "Client is inactive.");
                return Task.FromResult<object>(null);
            }

            context.OwinContext.Set<string>("as:clientAllowedOrigin", client.AllowedOrigin);
            context.OwinContext.Set<string>("as:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString());

            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            AutenticacaoDal Dal = new AutenticacaoDal();


            try
            {


                var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");

                if (allowedOrigin == null) allowedOrigin = "*";

                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

                //using (AuthRepository _repo = new AuthRepository())
                //{
                //    IdentityUser user = await _repo.FindUser(context.UserName, context.Password);

                //    if (user == null)
                //    {
                //        context.SetError("invalid_grant", "The user name or password is incorrect.");
                //        return;
                //    }
                //}

                Usuario user = await Dal.Efetuarlogin(context.UserName, GetMd5Hash(context.Password));

                if (user.StatusProcessamento > 0)
                {
                    context.SetError("invalid_grant", user.MensagemProcessamento);
                    return;

                }

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
                identity.AddClaim(new Claim("sub", context.UserName));

                var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId

                    },
                    {
                        "userName", context.UserName
                    },

                    {
                        "as:nomeUsuario", (user.UsuarioNome == null) ? string.Empty : user.UsuarioNome

                    },
                    {
                        "nomeUsuario", user.UsuarioNome
                    },

                    {
                        "as:perfilUsuario", (user.UsuarioPerfil.UsuPerfilNome == null) ? string.Empty : user.UsuarioPerfil.UsuPerfilNome

                    },
                    {
                        "perfilUsuario", user.UsuarioPerfil.UsuPerfilNome
                    },

                    {
                        "as:usuarioId", (user.UsuarioId == 0) ? string.Empty : ""

                    },
                    {
                        "usuarioId", user.UsuarioId.ToString()
                    },

                    {
                        "as:loginUsuario", (user.UsuarioLogin == null) ? string.Empty : user.UsuarioLogin

                    },
                    {
                        "loginUsuario", user.UsuarioLogin
                    },

                    {
                        "as:clienteId", (user.Cliente.ClienteId == 0) ? string.Empty : ""

                    },
                    {
                        "clienteId", user.Cliente.ClienteId.ToString()
                    },

                    {
                        "as:pathDocumentosCliente", (user.Cliente.ClientePastaDocumentos == null) ? string.Empty : user.Cliente.ClientePastaDocumentos

                    },
                    {
                        "pathDocumentosCliente", user.Cliente.ClientePastaDocumentos
                    }
                });

                var ticket = new AuthenticationTicket(identity, props);
                context.Validated(ticket);
            }
            catch (Exception e)
            {
                context.SetError("invalid_grant", e.Message);
            }

        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
                return Task.FromResult<object>(null);
            }

            // Change auth ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);

            var newClaim = newIdentity.Claims.Where(c => c.Type == "newClaim").FirstOrDefault();
            if (newClaim != null)
            {
                newIdentity.RemoveClaim(newClaim);
            }
            newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        static string GetMd5Hash(string texto)
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
            catch (Exception)
            {
                return null; // Caso encontre erro retorna nulo
            }
        }


    }
}