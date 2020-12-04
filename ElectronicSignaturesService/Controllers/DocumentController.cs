using BusinessLogic;
using DataLogic.Models;
using DataLogic.Oueries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ElectronicSignaturesService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {

        IConfiguration _configuration;
        private readonly Document _obj;

        public DocumentController(IConfiguration configuration, IDocumentQueries documentQueries)
        {
            _configuration = configuration;
            _obj=new Document(documentQueries);
        }

        [HttpGet("{id}", Name = "CheckDocumentStatus")]
        public IActionResult CheckDocumentStatus(int id)
        {
          
            DocumentStatus str=_obj.FindDocumentStatus(id);
           
            return new OkObjectResult(str);
        }

        [HttpGet("{id}", Name = "CheckDocumentOnDataModel")]
        public IActionResult CheckDocumentOnDataModel(int id)
        {
            DocumentOnDatabaseModel str=_obj.FindDocumentOnDatabaseModel(id);
            
            return new OkObjectResult(str);
        }

        [HttpGet("{id}", Name = "CheckDocumentOnFileSystem")]
        public IActionResult CheckDocumentOnFileSystem(int id)
        {
            DocumentOnFileSystemModel str=_obj.FindDocumentOnFileSystemModel(id);
           
            return new OkObjectResult(str);
        }

        [HttpGet("{id}", Name = "CheckDocumentHistory")]
        public IActionResult CheckDocumentHistory(int id)
        {
            DocumentHistory str=_obj.FindDocumentHistory(id);
          
            return new OkObjectResult(str);
        }

       /*public async Task<IActionResult> ShowAllBlobs()
        {
            string blobstorageconnection = _configuration.GetValue<string>("BlobStorageConnectionStrings");


            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);


            // Create the blob client.
            CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();


            CloudBlobContainer container = blobClient.GetContainerReference("filescontainers");


            CloudBlobDirectory dirb = container.GetDirectoryReference("filescontainers");


            BlobResultSegment resultSegment = await container.ListBlobsSegmentedAsync(string.Empty,true, BlobListingDetails.Metadata, 100, null, null, null);
            List<DocumentInformation> fileList = new List<DocumentInformation>();

            foreach (var blobItem in resultSegment.Results)
            {
                // A flat listing operation returns only blobs, not virtual directories.
                var blob = (CloudBlob)blobItem;
                fileList.Add(new DocumentInformation()
                {
                    Name = blob.Name,
                  //  FileSize = Math.Round((blob.Properties.Length / 1024f) / 1024f, 2).ToString(),
                    SubmissionDate = Convert.ToDateTime(DateTime.Parse(blob.Properties.LastModified.ToString()).ToLocalTime().ToString())
                }) ;
            }

            return new OkObjectResult(fileList);
        }*/
    }
}
