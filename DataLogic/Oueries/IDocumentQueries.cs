using DataLogic.Models;

namespace DataLogic.Oueries
{
    public interface IDocumentQueries
    {
        public DocumentStatus ViewDocumentStatus(int id);


        DocumentOnDatabaseModel ViewDocumentOnDatabaseModel(int id);

        DocumentOnFileSystemModel ViewDocumentOnFileSystemModel(int id);


        DocumentHistory ViewDocumentHistory(int id);
    }
}
