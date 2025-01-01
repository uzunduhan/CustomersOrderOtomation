using Azure.Storage.Blobs;
using CustomersOrderOtomation.Service.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace CustomersOrderOtomation.Service.Concrete
{
    public class StorageService : IStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public StorageService(IConfiguration configuration)
        {
            var connectionString = configuration["Azure:Storage:ConnectionString"];
            _containerName = configuration["Azure:Storage:ContainerName"];
            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            await containerClient.CreateIfNotExistsAsync();

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var blobClient = containerClient.GetBlobClient(fileName);

            using var stream = file.OpenReadStream();
            var result = await blobClient.UploadAsync(stream, true);

            if(result.GetRawResponse().Status == (int)System.Net.HttpStatusCode.Created)
            {
                return blobClient.Uri.ToString();
            }

            return "";
       
        }

        public async Task DeleteFileAsync(string blobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync();
        }
    }
}
