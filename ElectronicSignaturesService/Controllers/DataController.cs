using BusinessLogic;
using BusinessLogic.Repository;
using DataLogic.Commands;
using DataLogic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectronicSignaturesService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly Data _obj;
        IConfiguration _configuration;
        IImageSignature _imageSignature;
        private readonly ILogger _logger;


        public DataController(IConfiguration configuration, IDocumentRepository documentRepository, IImageSignature imageSignature, ILoggerFactory logFactory)
        {
            _configuration = configuration;
            _obj = new Data(documentRepository);
            _imageSignature = imageSignature;
            _logger = logFactory.CreateLogger<DataController>();
        }


        [HttpPost]
        public IActionResult AddDigitalSign(Pages PageObj, List<ImageSignParameters> ImgParamObj)
        {
            _logger.LogInformation("Log message in the Add Digital Signature method");

            _imageSignature.AddDigitalSignature(PageObj, ImgParamObj);

            return new OkResult();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            _logger.LogInformation("Log Message in the Upload  File Methid");
            int type = _configuration.GetValue<int>("UploadDocumentType");
            switch (type)
            {
                case (int)DocumentUploadType.FileSystem:
                    await _obj.UploadDocumentToFileSystem(file);
                    return new OkResult();
                case (int)DocumentUploadType.Database:
                    _obj.UploadFileOnDatabase(file);
                    return new OkResult();
                case (int)DocumentUploadType.AzureBlob:
                    string connection = _configuration.GetValue<string>("BlobStorageConnectionStrings");
                    await _obj.UploadFileOnAzureBlob(file, connection);
                    return new OkResult();
                default: return new BadRequestResult();
            }

        }

        [HttpPost]
        public IActionResult UpdateDocumentStatus(int documentId)
        {
            _logger.LogInformation("Log Message in the UpdateDocumentStatus method");

            _obj.UpdateStatusOfDocument(documentId);

            return new OkResult();
        }


    }
}
