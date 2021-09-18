using System;
using Microsoft.AspNetCore.Http;

namespace Jeto.Basel.Domain.Contracts
{
    [Serializable]
    public class FileContract
    {
        public IFormFile FileData { get; set; }
    }
}