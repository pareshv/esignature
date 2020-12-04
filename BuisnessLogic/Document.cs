using DataLogic.Models;
using DataLogic.Oueries;

namespace BusinessLogic
{
    public class Document
    {
        private readonly IDocumentQueries _documentQueries;

        public Document(IDocumentQueries documentQueries)
        {
            _documentQueries = documentQueries;
        }

        public DocumentStatus FindDocumentStatus(int id) {
            var str = _documentQueries.ViewDocumentStatus(id);
            return str; 
        }

        public DocumentOnDatabaseModel FindDocumentOnDatabaseModel(int id)
        {
            var str = _documentQueries.ViewDocumentOnDatabaseModel(id);
            return str;
        }

        public DocumentOnFileSystemModel FindDocumentOnFileSystemModel(int id)
        {
            var str = _documentQueries.ViewDocumentOnFileSystemModel(id);
            return str;
        }
        public DocumentHistory FindDocumentHistory(int id)
        {
            var str = _documentQueries.ViewDocumentHistory(id);
            return str;
        }

    }
}
