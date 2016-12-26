using BSI.GestDoc.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BSI.GestDoc.WebAPI.Controllers
{
    [RoutePrefix("api/RefreshTokens")]
    public class RefreshTokensController : ApiController
    {
        private AutenticacaoBL _auth = null;

        public RefreshTokensController()
        {
            _auth = new AutenticacaoBL();
        }

        [Authorize(Users = "Bradven1")]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(_auth.GetAllRefreshTokens());
        }

        //[Authorize(Users = "Admin")]
        [AllowAnonymous]
        [Route("")]
        public async Task<IHttpActionResult> Delete(string tokenId)
        {
            var existingToken = _auth.GetAllRefreshTokens().ToList().FindAll(r => r.Id == tokenId).SingleOrDefault();

            var result = await _auth.RemoveRefreshToken(existingToken.TokenId);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Token Id does not exist");

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _auth.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
