using DataLogic.Commands;
using DataLogic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Data
    {

        private readonly IDocumentRepository _documentRepository;

        public Data(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository; 
        }

        public void UploadFileOnDatabase(IFormFile file)
        {
            if (file != null)
            {
                if (file.Length > 0)
                {
                    //Getting FileName
                    var fileName = Path.GetFileName(file.FileName);
                    //Getting file Extension
                    // var fileExtension = Path.GetExtension(fileName);
                    // concatenating  FileName + FileExtension
                    //var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);

                    var objfiles = new DocumentOnDatabaseModel()
                    {

                        Name =fileName,
                        FileType = file.ContentType,
                        SubmissionDate = DateTime.Now
                    };

                    using (var target = new MemoryStream())
                    {
                        file.CopyTo(target);
                        objfiles.Content = target.ToArray();
                    }
                    _documentRepository.UploadDocumentToDatabase(objfiles);
                }
            }
        }
        public async Task<IActionResult> UploadFileOnAzureBlob(IFormFile file,string connection)
        {
            if (file != null)
            {


                //Getting FileName
                var fileName = Path.GetFileName(file.FileName);


                var obj = new DocumentInformation()
                {

                    Name = fileName,
                    FileType = file.ContentType,
                    SubmissionDate = DateTime.Now
                };

                DocumentInformation doc = _documentRepository.UploadDocument(obj);

                string blobstorageconnection = connection;

                byte[] dataFiles;

                // Retrieve storage account from connection string.
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);


                // Create the blob client.
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

                // Retrieve a reference to a container.
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("filescontainers");

                BlobContainerPermissions permissions = new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                };

                string systemFileName =string.Format(file.FileName, doc.DocumentId);
                await cloudBlobContainer.SetPermissionsAsync(permissions);
                await using (var target = new MemoryStream())
                {
                    file.CopyTo(target);
                    dataFiles = target.ToArray();
                }
                // This also does not make a service call; it only creates a local object.
                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(systemFileName);
                await cloudBlockBlob.UploadFromByteArrayAsync(dataFiles, 0, dataFiles.Length);
            }
            return new OkResult();
        }
        public void UpdateStatusOfDocument(int documentId)
        {
            var obj = new DocumentStatus()
            {

                Approval = true,
                ApprovalDate = DateTime.Now,
                DocumentId = documentId
            };
            _documentRepository.UpdateDocumentStatus(obj);
        }
        
        public async Task<IActionResult> UploadDocumentToFileSystem(IFormFile file)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\Files\\");
            bool basePathExists = System.IO.Directory.Exists(basePath);
            if (!basePathExists) Directory.CreateDirectory(basePath);
            var fileName = Path.GetFileNameWithoutExtension(file.FileName);
            var filePath = Path.Combine(basePath, file.FileName);
            // var extension = Path.GetExtension(file.FileName);

            if (!System.IO.File.Exists(filePath))
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                var fileModel = new DocumentOnFileSystemModel
                {
                    Name = fileName,
                    FileType = file.ContentType,
                    SubmissionDate = DateTime.UtcNow,
                    FilePath = filePath
                };
                _documentRepository.UploadDocumentToFileSystem(fileModel);
            }
            return new OkResult();
        }
    }
}
