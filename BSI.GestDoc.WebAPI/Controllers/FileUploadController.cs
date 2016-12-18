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
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.GestDoc.WebAPI.Controllers
{
    [System.Web.Http.RoutePrefix("api/FileUpload")]
    public class FileUploadController : ApiController
    {
        //public HttpResponseMessage Get()
        //{
        //    var body = RenderViewToString("FileUpload", "~/Views/FileUpload/Index.cshtml", new object());
        //    return Request.CreateResponse(HttpStatusCode.OK, new { content = body });
        //}

        //public static string RenderViewToString(string controllerName, string viewName, object viewData)
        //{
        //    using (var writer = new StringWriter())
        //    {
        //        var routeData = new RouteData();
        //        routeData.Values.Add("controller", controllerName);
        //        var fakeControllerContext = new ControllerContext(new HttpContextWrapper(new HttpContext(new HttpRequest(null, "http://google.com", null), new HttpResponse(null))), routeData, new FakeController());
        //        var razorViewEngine = new RazorViewEngine();
        //        var razorViewResult = razorViewEngine.FindView(fakeControllerContext, viewName, "", false);

        //        var viewContext = new ViewContext(fakeControllerContext, razorViewResult.View, new ViewDataDictionary(viewData), new TempDataDictionary(), writer);
        //        razorViewResult.View.Render(viewContext, writer);
        //        return writer.ToString();
        //    }
        //}

        //[System.Web.Http.Route("EnviarArquivos")]
        //[System.Web.Http.HttpPost]
        //[ValidateMimeMultipartContentFilter]
        //public async Task<FileResult> EnviarArquivos(dynamic parametro)
        //{
        //    UploadFile upload = new WebAPI.UploadFile();
        //    return await new UploadFile().GetFile(Request);
        //}

        private readonly string workingFolder = HttpRuntime.AppDomainAppPath + @"\Uploads";

        [System.Web.Http.Route("EnviarArquivos")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult EnviarArquivos(int idCliente)
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                return BadRequest("Unsupported media type");
            }
            try
            {
                var streamProvider = new MultipartFormDataStreamProvider(workingFolder);
                Request.Content.ReadAsMultipartAsync(streamProvider);

                var fileResult_ = new FileResult
                {
                    FileNames = streamProvider.FileData.Select(entry => entry.LocalFileName),
                    Names = streamProvider.FileData.Select(entry => entry.Headers.ContentDisposition.FileName),
                    ContentTypes = streamProvider.FileData.Select(entry => entry.Headers.ContentType.MediaType),
                    Description = streamProvider.FormData["description"],
                    CreatedTimestamp = DateTime.UtcNow,
                    UpdatedTimestamp = DateTime.UtcNow,
                    DownloadLink = "TODO, will implement when file is persisited"
                };

                //var retorno = new { "posicao": pos, "status": "ok" };

                return Ok(fileResult_);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        // Extracts Request FormatData as a strongly typed model
        private object GetFormData<T>(MultipartFormDataStreamProvider result)
        {
            if (result.FormData.HasKeys())
            {
                var unescapedFormData = Uri.UnescapeDataString(result.FormData
                    .GetValues(0).FirstOrDefault() ?? String.Empty);
                if (!String.IsNullOrEmpty(unescapedFormData))
                    return JsonConvert.DeserializeObject<T>(unescapedFormData);
            }

            return null;
        }

        private string GetDeserializedFileName(MultipartFileData fileData)
        {
            var fileName = GetFileName(fileData);
            return JsonConvert.DeserializeObject(fileName).ToString();
        }

        public string GetFileName(MultipartFileData fileData)
        {
            return fileData.Headers.ContentDisposition.FileName;
        }

        [System.Web.Http.Route("EnviarArquivos2")]
        [System.Web.Http.HttpPost]
        [ValidateMimeMultipartContentFilter]
        public async Task<FileResult> EnviarArquivos2()
        {
            UploadFile upload = new WebAPI.UploadFile();
            return await new UploadFile().GetFile(Request);
        }

        [System.Web.Http.Route("RetornarDocumentoClienteTipo")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult RetornarDocumentoClienteTipo([FromBody]DocumentoClienteTipo documentoClienteTipo)
        {
            var DocumentosClienteTipo = new BusinessLogic.UploadFiles().RetornarDocumentoClienteTipo(documentoClienteTipo.ClienteId);
            return Ok(DocumentosClienteTipo);
        }


    }
    //public class FakeController : ControllerBase { protected override void ExecuteCore() { } }
}
