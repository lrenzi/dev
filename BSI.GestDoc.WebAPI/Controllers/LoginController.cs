using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.GestDoc.Entity;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.GestDoc.WebAPI.Controllers
{
    [System.Web.Http.RoutePrefix("api/Login")]
    public class LoginController : ApiController
    {
        [System.Web.Http.Route("EfetuarLogin")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult EfetuarLogin()
        {
            var DocumentosClienteTipo = new BusinessLogic.UploadFiles().RetornarDocumentoClienteTipo(1);
            
            return Ok(DocumentosClienteTipo);
        }
    }
    //public class FakeController : ControllerBase { protected override void ExecuteCore() { } }
}
