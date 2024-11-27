using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Newtonsoft.Json;
using PropertyApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace PropertyApi.Services
{
    public class AzureBlobService
    {
        private readonly string _blobUrl = "https://nmrkpidev.blob.core.windows.net/dev-test/dev-test.json";
        private readonly string _sasToken = "?sp=r&st=2024-10-28T10:35:48Z&se=2025-10-28T18:35:48Z&spr=https&sv=2022-11-02&sr=b&sig=bdeoPWtefikVgUGFCUs4ihsl22ZhQGu4%2B4cAfoMwd4k%3D";

        public async Task<List<Property>> GetPropertiesFromBlobAsync()
        {
            var blobUri = new Uri(_blobUrl + _sasToken);
            var blobClient = new BlobClient(blobUri);

            // Download the JSON data from the blob storage
            BlobDownloadInfo download = await blobClient.DownloadAsync();

            using (var reader = new StreamReader(download.Content))
            {
                var jsonData = await reader.ReadToEndAsync();
                var properties = JsonConvert.DeserializeObject<List<Property>>(jsonData);
                return properties;
            }
        }
    }
}
