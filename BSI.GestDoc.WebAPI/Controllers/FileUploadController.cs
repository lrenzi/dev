using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.GestDoc.Entity;
using System.Web.Http;
using System.Net.Http;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.GestDoc.WebAPI.Controllers
{
    

    [Route("api/[controller]")]
    public class FileUploadController : ApiController
    {
        [Route("UploadSingleFile")]
        [HttpPost]
        [ValidateMimeMultipartContentFilter]
        public async Task<FileResult> UploadSingleFile()
        {
            UploadFile upload = new WebAPI.UploadFile();
            return await new UploadFile().GetFile(Request);
        }

        // GET api/values
        [Route("RetornarDocumentoClienteTipo")]
        [HttpGet]
        public List<DocumentoClienteTipo> RetornarDocumentoClienteTipo(int ClienteId)
        {
            return new BSI.GestDoc.BusinessLogic.UploadFiles().RetornarDocumentoClienteTipo(ClienteId);
        }
    }
}
