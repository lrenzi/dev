using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace BSI.GestDoc.WebAPI.UploadFile
{
    public class CustomMultipartStreamProvider : MultipartStreamProvider
    {
        private readonly string _containerName;
        private readonly string _fileName;

        public CustomMultipartStreamProvider(string containerName, string fileName)
        {
            _containerName = containerName;
            _fileName = fileName;
        }

        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            Stream stream = null;

            if (!String.IsNullOrWhiteSpace(_fileName))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["BlobStorage"].ConnectionString;
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer blobContainer = blobClient.GetContainerReference(_containerName);
                CloudBlockBlob blob = blobContainer.GetBlockBlobReference(_fileName);
                stream = blob.OpenWrite();
            }
            return stream;
        }
    }
}