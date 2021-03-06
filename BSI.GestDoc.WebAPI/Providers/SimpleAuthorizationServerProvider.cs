﻿using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BSI.GestDoc.Entity;
using BSI.GestDoc.BusinessLogic;
using System.Web;

namespace BSI.GestDoc.WebAPI.Providers
{


    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
            /*
            //Deixei comentado, para possivel implementação de login com clientid
            string clientId = string.Empty;
            string clientSecret = string.Empty;
            Usuario usuario = null;

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

            usuario = new UsuarioBL().ConsultarUsuarioById(context.ClientId);

            if (usuario == null)
            {
                context.SetError("invalid_clientId", string.Format("Client '{0}' is not registered in the system.", context.ClientId));
                return Task.FromResult<object>(null);
            }

            if (usuario.ApplicationType == Entity.Enum.ApplicationTypes.NativeConfidential)
            {
                /*if (string.IsNullOrWhiteSpace(clientSecret))
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
                }*/
            /*}

            if (!usuario.UsuarioAtivo)
            {
                context.SetError("invalid_clientId", "Client is inactive.");
                return Task.FromResult<object>(null);
            }

            context.OwinContext.Set<string>("clientAllowedOrigin", usuario.AllowedOrigin);
            context.OwinContext.Set<string>("clientRefreshTokenLifeTime", System.Configuration.ConfigurationManager.AppSettings["clientRefreshTokenLifeTime"].ToString());

            context.Validated();
            return Task.FromResult<object>(null);*/
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            AutenticacaoBL Dal = new AutenticacaoBL();

            try
            {

                var allowedOrigin = context.OwinContext.Get<string>("clientAllowedOrigin");

                if (allowedOrigin == null) allowedOrigin = "*";

                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

                //recupera usuario da base de dados
                Usuario user = await Dal.EfetuarLogin(context.UserName, context.Password);

                if (user.StatusProcessamento > 0)
                {
                    context.SetError("invalid_grant", user.MensagemProcessamento);
                    return;

                }

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
                identity.AddClaim(new Claim("sub", context.UserName));

                #region mapeamento de atributos do usuario logado
                var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                   
                    {
                        "userName", context.UserName
                    },

                    {
                        "nomeUsuario", user.UsuarioNome
                    },

                    {
                        "perfilUsuario", user.UsuarioPerfil.UsuPerfilNome
                    },

                    {
                        "usuarioId", user.UsuarioId.ToString()
                    },

                    {
                        "loginUsuario", user.UsuarioLogin
                    },

                    {
                        "clienteId", user.Cliente.ClienteId.ToString()
                    }
                });
                #endregion

                //var session = HttpContext.Current.Session;
                //session.Add("AuthenticationProperties", props.Dictionary);

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
            var originalClient = context.Ticket.Properties.Dictionary["clientId"];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError("invalid_ClienteId", "Refresh token is issued to a different clientId.");
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

       


    }
}