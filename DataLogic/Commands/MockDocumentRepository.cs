using DataLogic.DbContexts;
using DataLogic.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataLogic.Commands
{
    public class MockDocumentRepository:IMockDocumentRepository
    {
       
        private readonly DocumentContext _dbContext;

        public MockDocumentRepository(DocumentContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool UploadDocumentToDatabase(DocumentOnDatabaseModel info)
        {
            try
            {
                _dbContext.DocumentOnDatabase.Add(info);
                Save();
                return true;
            }
            catch (Exception)
            {
                return false;
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
            catch (Exception)
            {
                return false;
            }
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
        public static bool CheckUrl(string uriName)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(uriName, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            return true;
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        } 
    }
}
