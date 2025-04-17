using Domain.Documents;

namespace Infrastructure.Services
{
    internal class DocumentEntity
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DocumentType Type { get; set; }
        public string BorrowerSin { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}