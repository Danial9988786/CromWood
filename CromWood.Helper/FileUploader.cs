using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace CromWood.Helper
{
    public class FileUploader : IFileUploader
    {
        private readonly string storageAccount;
        private readonly IConfiguration _config;
        public FileUploader(IConfiguration configuration)
        {
            _config = configuration;
            storageAccount = _config.GetConnectionString("StorageAccount") ?? "";
        }

        public async Task<string> Upload(IFormFile file, string containerName)
        {
            try
            {
                string name = Guid.NewGuid().ToString();
                BlobContainerClient container = new(storageAccount, containerName);
                if (!container.Exists())
                    await container.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);

                string format = Path.GetExtension(file.FileName);
                BlobClient client = container.GetBlobClient($"{name}{format}");

                await using (Stream? data = file.OpenReadStream())
                {
                    await client.UploadAsync(data, true, new CancellationToken());
                    client.SetHttpHeaders(new BlobHttpHeaders() { ContentType = file.ContentType });
                }
                return client.Uri.AbsoluteUri;
            }

            catch
            {
                throw;
            }

        }

        public async Task<bool> Delete(string url, string containerName)
        {
            try
            {
                string blob = url.Split('/')[^1];
                BlobContainerClient container = new(storageAccount, containerName);
                if (container.Exists())
                {
                    return await container.DeleteBlobIfExistsAsync(blob);
                }
                return false;
            }

            catch
            {
                throw;
            }

        }
    }
}
