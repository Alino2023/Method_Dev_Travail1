using Microsoft.AspNetCore.Mvc;
using Domain.Documents;
using Microsoft.AspNetCore.Http;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        /// <summary>
        /// Téléverse un document pour un client spécifique.
        /// </summary>
        /// <param name="borrowerSin">Le NAS de l’emprunteur.</param>
        /// <param name="documentType">Le type de document (PreuveEmploi, PreuveActif, PreuveCredit).</param>
        /// <param name="file">Le fichier à téléverser (.pdf ou .docx).</param>
        /// <returns>Un message de succès ou d’erreur.</returns>
        [HttpPost("upload")]
        public async Task<IActionResult> UploadDocument(
            [FromForm] string borrowerSin,
            [FromForm] string documentType,
            [FromForm] IFormFile file)
        {
            var result = await _documentService.UploadAsync(file, borrowerSin, documentType);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message });
        }
    }
}
