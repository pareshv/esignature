using DataLogic.DbContexts;
using DataLogic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DataLogic.Commands
{
    public class DocumentRepository : IDocumentRepository
    {
        
        private readonly DocumentContext _dbContext;


        public DocumentRepository(DocumentContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public DocumentInformation UploadDocument(DocumentInformation info)
        {
            _dbContext.DocumentInformations.Add(info);
            Save();
            var doc = _dbContext.DocumentInformations.Where(a => a.Name == info.Name && a.SubmissionDate == info.SubmissionDate).Single();
            return doc;
        }



        public bool UpdateDocumentStatus(DocumentStatus doc)
        {
            try
            {
                _dbContext.Entry(doc).State = EntityState.Modified;
                Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void AddSignatureToDB(ImageSignParameters obj)
        {
            try
            {
                _dbContext.ImageSignParameter.Add(obj);
                Save();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UploadDocumentToFileSystem(DocumentOnFileSystemModel doc)
        {
            try
            {
                _dbContext.DocumentOnFileSystem.Add(doc);
                Save();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public bool UploadDocumentToDatabase(DocumentOnDatabaseModel doc)
        {
            try
            {
                _dbContext.DocumentOnDatabase.Add(doc);
                Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
