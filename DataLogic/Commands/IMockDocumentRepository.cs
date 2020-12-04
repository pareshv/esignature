using DataLogic.Models;

namespace DataLogic.Commands
{
    public interface IMockDocumentRepository
    {
        public bool UploadDocumentToDatabase(DocumentOnDatabaseModel doc);
        public bool UploadDocumentToFileSystem(DocumentOnFileSystemModel doc);

        void Save();

        public bool UpdateDocumentStatus(DocumentStatus doc);
    }
}
