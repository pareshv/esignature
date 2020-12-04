using DataLogic.DbContexts;
using DataLogic.Models;

namespace DataLogic.Oueries
{
    public class DocumentQueries : IDocumentQueries
    {
        private readonly DocumentContext _dbContext;

        public DocumentQueries(DocumentContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DocumentStatus ViewDocumentStatus(int id)
        {

            return _dbContext.DocumentStatuses.Find(id);

        }

        public DocumentHistory ViewDocumentHistory(int id)
        {
            return _dbContext.DocumentHistories.Find(id);
        }

        public DocumentOnDatabaseModel ViewDocumentOnDatabaseModel(int id)
        {
            return _dbContext.DocumentOnDatabase.Find(id);
        }

        public DocumentOnFileSystemModel ViewDocumentOnFileSystemModel(int id)
        {
            return _dbContext.DocumentOnFileSystem.Find(id);
        }
    }
}
