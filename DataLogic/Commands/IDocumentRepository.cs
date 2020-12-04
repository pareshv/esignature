using DataLogic.Models;

namespace DataLogic.Commands
{
    public interface IDocumentRepository
    {
        public bool UploadDocumentToDatabase(DocumentOnDatabaseModel doc);

        public bool UpdateDocumentStatus(DocumentStatus doc);

        public bool UploadDocumentToFileSystem(DocumentOnFileSystemModel doc);

        void Save();
       
        public DocumentInformation UploadDocument(DocumentInformation info);

        public void AddSignatureToDB(ImageSignParameters obj);
    }
}
