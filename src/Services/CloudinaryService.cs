using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using catedra3.src.DTOs;
using catedra3.src.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace catedra3.src.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

    public CloudinaryService(IOptions<CloudinarySettings> config)
    {
        var cloudinaryAccount = new Account(
            config.Value.CloudName,
            config.Value.ApiKey,
            config.Value.ApiSecret
        );
        _cloudinary = new Cloudinary(cloudinaryAccount);
    }

    public async Task<string> UploadImageAsync(IFormFile file)
    {
        if (file.Length > 5 * 1024 * 1024) // Tamaño máximo: 5MB
            throw new ArgumentException("El tamaño del archivo no debe exceder los 5MB.");

        var validFormats = new[] { "image/jpeg", "image/png" };
        if (!Array.Exists(validFormats, format => format == file.ContentType))
            throw new ArgumentException("Formato de archivo no permitido. Solo JPG y PNG son aceptados.");

        using var stream = file.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            Transformation = new Transformation().Quality(80).FetchFormat("auto")
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        if (uploadResult.Error != null)
            throw new Exception(uploadResult.Error.Message);

        return uploadResult.SecureUrl.AbsoluteUri;
    }
}

}
  