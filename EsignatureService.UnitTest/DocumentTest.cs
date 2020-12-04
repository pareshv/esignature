using BusinessLogic.Repository;
using DataLogic.Commands;
using DataLogic.DbContexts;
using DataLogic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace EsignatureService.UnitTest
{
    public class DocumentTest
    {

        IMockDocumentRepository _documentRepository;
        IImageSignature _imageSignature;

        [SetUp]
        public void Setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DocumentContext>();
            optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=documentDB; uid=sa; password=aloha@123;");
            DocumentContext dbContext = new DocumentContext(optionsBuilder.Options);

            _documentRepository = new MockDocumentRepository(dbContext);

            _imageSignature = new ImageSignature();

        }

        [Test]
        public void UploadFileInDatabase()
        {
            string content = "hello world";

            string fileName = "xyz.txt";

            byte[] s_Bytes = Encoding.UTF8.GetBytes(content);

            var objfiles = new DocumentOnDatabaseModel()
            {

                Name = fileName,
                FileType = "byte",
                SubmissionDate = DateTime.Now,
                Content = s_Bytes
            };
            Assert.IsTrue(_documentRepository.UploadDocumentToDatabase(objfiles), "File Uploaded To Database");
        }


        [Test]
        public void UploadFileInFileSystem()
        {
            var fileMock = new Mock<IFormFile>();
            //Setup mock file using a memory stream
            var content = "Hello World from a Fake File";
            var fileName = "test.pdf";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            var file = fileMock.Object;

            var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\Files\\");
            bool basePathExists = System.IO.Directory.Exists(basePath);
            if (!basePathExists) Directory.CreateDirectory(basePath);
            var fileName1 = Path.GetFileNameWithoutExtension(file.FileName);
            var filePath = Path.Combine(basePath, file.FileName);
            // var extension = Path.GetExtension(file.FileName);

            if (!System.IO.File.Exists(filePath))
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyToAsync(stream);
                }
                var fileModel = new DocumentOnFileSystemModel
                {
                    Name = fileName,
                    FileType = file.ContentType,
                    SubmissionDate = DateTime.UtcNow,
                    FilePath = filePath
                };
                Assert.IsTrue(_documentRepository.UploadDocumentToFileSystem(fileModel), "File Uploaded To Database");
            }
        }

        [Test]
        public void AddSignature()
        {

            Image image = Image.FromFile(@"D:\ElectronicSignaturesService\BusinessLogic\Files\fake.png");
            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            byte[] arr = ms.ToArray();


            Pages PageObj = new Pages()
            {
                FilePath = @"D:\ElectronicSignaturesService\BusinessLogic\Files\sample.pdf"
            };

            List<ImageSignParameters> ob = new List<ImageSignParameters>();

            ImageSignParameters objParam1 = new ImageSignParameters()
            {
                XIndent = 100,
                YIndent = 100,
                Height = 50,
                Width = 50,
                Opacity = 0.5,
                AttachmentPath = @"D:\ElectronicSignaturesService\BusinessLogic\Files\fake.png",
                ImageStream = arr,
                PageNumber = 1
            };
            ImageSignParameters objParam2 = new ImageSignParameters()
            {
                XIndent = 300,
                YIndent = 600,
                Height = 50,
                Width = 50,
                Opacity = 0.5,
                AttachmentPath = @"D:\ElectronicSignaturesService\BusinessLogic\Files\fake.png",
                ImageStream = arr,
                PageNumber = 2
            };
            ob.Add(objParam1);
            ob.Add(objParam2);
            Assert.IsTrue(_imageSignature.AddDigitalSignature(PageObj, ob), "Added Signature");
        }


        [Test]
        public void UpdateDocumentStatus()
        {

            var obj = new DocumentStatus()
            {

                Approval = true,
                ApprovalDate = DateTime.Now,
                DocumentId = 1
            };
            Assert.IsTrue(_documentRepository.UpdateDocumentStatus(obj), "Document Updated" + obj.DocumentId);
            Assert.IsFalse(_documentRepository.UpdateDocumentStatus(obj), "Document Not Updated" + obj.DocumentId);
        }

        [Test]
        public void UpdateDocumentStatusUisngWrongDocumentId()
        {

            var obj = new DocumentStatus()
            {

                Approval = true,
                ApprovalDate = DateTime.Now,
                DocumentId = -1
            };
            Assert.IsFalse(_documentRepository.UpdateDocumentStatus(obj), "Document Not Updated" + obj.DocumentId);
        }

    }
}