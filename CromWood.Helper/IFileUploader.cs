using Microsoft.AspNetCore.Http;

namespace CromWood.Helper
{
    public interface IFileUploader
    {
        public Task<string> Upload(IFormFile file, string container = "default");
        public Task<bool> Delete(string url, string container = "default");
        public Task<ExportFile> Download(string url, string container = "default");
    }
}
