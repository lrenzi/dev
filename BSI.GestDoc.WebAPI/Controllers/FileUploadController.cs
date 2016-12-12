﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.GestDoc.Entity;
using System.Web.Http;
using System.Net.Http;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.GestDoc.WebAPI.Controllers
{
    

    [RoutePrefix("api/FileUpload")]
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

        [Route("RetornarDocumentoClienteTipo")]
        [HttpPost]
        public IHttpActionResult RetornarDocumentoClienteTipo([FromBody]DocumentoClienteTipo documentoClienteTipo)
        {
            var retorno = new BusinessLogic.UploadFiles().RetornarDocumentoClienteTipo(documentoClienteTipo.ClienteId);
            return Ok(retorno);
        }
    }
}
