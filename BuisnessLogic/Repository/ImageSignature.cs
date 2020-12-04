using Aspose.Pdf;
using DataLogic.Commands;
using DataLogic.DbContexts;
using DataLogic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace BusinessLogic.Repository
{
    public class ImageSignature : IImageSignature
    {
        IDocumentRepository _documentRepository;
        public ImageSignature()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DocumentContext>();
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["documentDB"].ToString());
            DocumentContext dbContext = new DocumentContext(optionsBuilder.Options);
            _documentRepository = new DocumentRepository(dbContext);
        }

        public bool AddDigitalSignature(Pages PageObj,List<ImageSignParameters>  ImgParamObj)
        {
                
                var pdfDocument = new Aspose.Pdf.Document(PageObj.FilePath);
                foreach (ImageSignParameters obj in ImgParamObj)
                {        
                         //add image signature parameters to database 
                        _documentRepository.AddSignatureToDB(obj);


                        Stream stream = new MemoryStream(obj.ImageStream);
                        ImageStamp imageStamp = new ImageStamp(stream);
                        imageStamp.Background = true;
                        // imageStamp.VerticalAlignment = VerticalAlignment.Top;
                        imageStamp.XIndent = obj.XIndent;
                        imageStamp.YIndent = obj.YIndent;
                        imageStamp.Height = obj.Height;
                        imageStamp.Width = obj.Width;
                        imageStamp.Opacity = obj.Opacity;
                        pdfDocument.Pages[obj.PageNumber].AddStamp(imageStamp);

                }
                pdfDocument.Save(string.Format("D:\\ElectronicSignaturesService\\BusinessLogic\\Files\\Sign1{0}.pdf", DateTime.Now.Ticks));
          
            return true;
        }
    }
}
