using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace BSI.GestDoc.WebAPI
{
    public class UploadFile
    {

        private readonly string workingFolder = HttpRuntime.AppDomainAppPath + ConfigurationManager.AppSettings["DiretorioUpload"];

        //private static readonly string ServerUploadFolder = "C:\\Temp"; //Path.GetTempPath();

       /* public async Task<FileResult> GetFile(HttpRequestMessage Request)
        {
            var streamProvider = new MultipartFormDataStreamProvider(workingFolder);
            await Request.Content.ReadAsMultipartAsync(streamProvider);

            return new FileResult
            {
                FileNames = streamProvider.FileData.Select(entry => entry.LocalFileName),
                Names = streamProvider.FileData.Select(entry => entry.Headers.ContentDisposition.FileName),
                ContentTypes = streamProvider.FileData.Select(entry => entry.Headers.ContentType.MediaType),
                Description = streamProvider.FormData["description"],
                CreatedTimestamp = DateTime.UtcNow,
                UpdatedTimestamp = DateTime.UtcNow,
                DownloadLink = "TODO, will implement when file is persisited"
            };
        }*/
    }
}
