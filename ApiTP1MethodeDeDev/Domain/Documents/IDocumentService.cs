using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Domain.Documents
{
    public interface IDocumentService
    {
        Task<(bool IsSuccess, string Message)> UploadAsync(IFormFile file, string borrowerSin, string documentType);
    }
}
