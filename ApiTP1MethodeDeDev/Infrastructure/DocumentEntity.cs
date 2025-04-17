using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure
{
    public enum DocumentType
    {
        EmploymentProof,
        AssetProof,
        CreditProof
    }

    public class DocumentEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string FileName { get; set; }

        [Required]
        public string FilePath { get; set; }

        [Required]
        public DocumentType Type { get; set; }

        [Required]
        public string BorrowerSin { get; set; } // Foreign key

        [ForeignKey("BorrowerSin")]
        public Domain.Borrowers.Borrower Borrower { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
