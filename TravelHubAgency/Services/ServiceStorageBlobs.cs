using Azure.Storage.Blobs;
using System.Configuration;

namespace TravelHubAgency.Services
{
    public class ServiceStorageBlobs
    {
        private BlobServiceClient client;
        private string UrlImages;

        public ServiceStorageBlobs(BlobServiceClient client)
        {
            this.client = client;
          // this.UrlImages = config.GetValue<string>("UrlBlobs:UrlContainer");
        }

        //METODO PARA SUBIR UN BLOB A UN CONTAINER 
        public async Task UploadBlobAsync(IFormFile file, string imageName)
        {
            BlobContainerClient containerClient = this.client.GetBlobContainerClient("imagestravelhub");
            BlobClient blobClient = containerClient.GetBlobClient(imageName);
            using (Stream stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, overwrite: true);
            }
        }

        //METODO PARA ELIMINAR UN BLOB DE UN CONTAINER 
        public async Task DeleteBlobAsync(string blobName)
        {
            BlobContainerClient containerClient =
                this.client.GetBlobContainerClient("imagestravelhub");
            await containerClient.DeleteBlobAsync(blobName);
        }

        public async Task<BlobClient> FindBlobAsync(string blobName)
        {
            BlobContainerClient containerClient = this.client.GetBlobContainerClient("imagestravelhub");
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            if (await blobClient.ExistsAsync())
            {
                return blobClient;
            }
            else
            {
                return null;
            }
        }

        //METODO PARA ELIMINAR UN CONTENEDOR 
        public async Task DeleteContainerAsync(string containerName)
        {
            await this.client.DeleteBlobContainerAsync(containerName);
        }

        // ASIGNAMOS LA URL 
        //public async Task<string> GetUrlImageBlob(string imagen)
        //{
        //    string url = this.UrlImages + "/" + imagen;
        //    return url;
        //}
    }
}
