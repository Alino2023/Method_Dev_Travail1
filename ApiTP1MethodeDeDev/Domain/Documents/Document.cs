using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Domain.Borrowers;

namespace Domain.Documents
{
    public enum DocumentType
    {
        EmploymentProof,
        AssetProof,
        CreditProof
    }

    public class ProofDocument
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string FileName { get; set; }

        [Required]
        public string FilePath { get; set; } // Chemin dans le stockage ou blob

        [Required]
        public DocumentType Type { get; set; }

        [Required]
        public string FileExtension { get; set; } // .pdf ou .docx

        [Required]
        public string BorrowerSin { get; set; }

        public Borrowers.Borrower Borrower { get; set; }
    }
}

