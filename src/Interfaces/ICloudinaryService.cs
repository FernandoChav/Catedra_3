using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace catedra3.src.Interfaces
{
    public interface ICloudinaryService
    {
        Task<string> UploadImageAsync(IFormFile file);
    }
}