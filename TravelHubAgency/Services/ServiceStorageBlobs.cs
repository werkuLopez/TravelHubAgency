using Azure.Storage.Blobs;
using System.Configuration;

namespace TravelHubAgency.Services
{
    public class ServiceStorageBlobs
    {
        private BlobServiceClient client;
        private string UrlImages;

        public ServiceStorageBlobs(BlobServiceClient client, IConfiguration config)
        {
            this.client = client;
            UrlImages = config.GetValue<string>("UrlBlobs:UrlContainer");
        }

        //METODO PARA SUBIR UN BLOB A UN CONTAINER 
        public async Task UploadBlobAsync(string containerName, string blobName, Stream stream)
        {
            BlobContainerClient containerClient = this.client.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);
            //await containerClient.UploadBlobAsync(blobName, stream);
            await blobClient.UploadAsync(stream, overwrite: true);
        }

        //METODO PARA ELIMINAR UN BLOB DE UN CONTAINER 
        public async Task DeleteBlobAsync(string containerName, string blobName)
        {
            BlobContainerClient containerClient =
                this.client.GetBlobContainerClient(containerName);

            await containerClient.DeleteBlobAsync(blobName);
        }

        public async Task<BlobClient> FindBlobAsync(string containerName, string blobName)
        {
            BlobContainerClient containerClient = this.client.GetBlobContainerClient(containerName);
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

        public async Task<string> GetUrlImageBlob(string imagen)
        {
            string url = this.UrlImages + imagen;
            return url;
        }
    }
}
