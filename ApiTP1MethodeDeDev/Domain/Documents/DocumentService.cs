
using Domain.Borrowers;
using Domain.Documents;
using Microsoft.AspNetCore.Http;


namespace Infrastructure.Services
{
    public class DocumentService : Domain.Borrowers.IDocumentService
    {
      
        private readonly IWebHostEnvironment _env;

        private readonly string[] AllowedExtensions = new[] { ".pdf", ".docx" };

        public string Add(Borrower borrower)
        {
            throw new NotImplementedException();
        }

        public IList<Borrower> GetAll()
        {
            throw new NotImplementedException();
        }

        public Borrower GetBySin(string sin)
        {
            throw new NotImplementedException();
        }

        public void Update(Borrower borrower)
        {
            throw new NotImplementedException();
        }

        public async Task<(bool IsSuccess, string Message)> UploadAsync(IFormFile file, string borrowerSin, string documentType)
        {
            // 1. Vérifier le fichier
            if (file == null || file.Length == 0)
                return (false, "Fichier vide ou manquant.");

            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!AllowedExtensions.Contains(extension))
                return (false, "Seuls les fichiers PDF et DOCX sont autorisés.");

            // 2. Vérifier le type de document
            if (!Enum.TryParse<DocumentType>(documentType, true, out var parsedType))
                return (false, "Type de document invalide.");

            //// 3. Vérifier l'existence de l'emprunteur
            //var borrower = await _context.Set<Domain.Borrowers.Borrower>()
            //                             .FirstOrDefaultAsync(b => b.Sin == borrowerSin);

            //if (borrower == null)
            //    return (false, "Emprunteur non trouvé.");

            // 4. Créer dossier s’il n'existe pas
            var uploadPath = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            // 5. Sauvegarder le fichier
            var uniqueFileName = Guid.NewGuid() + extension;
            var filePath = Path.Combine(uploadPath, uniqueFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // 6. Enregistrer dans la BD
            var document = new DocumentEntity
            {
                FileName = file.FileName,
                FilePath = Path.Combine("uploads", uniqueFileName),
                Type = parsedType,
                BorrowerSin = borrowerSin,
                UploadedAt = DateTime.UtcNow
            };

          

            return (true, "Fichier téléversé avec succès.");
        }
    }
}
