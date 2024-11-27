using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PropertyApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PropertyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Property>>> GetProperties()
        {
            string blobUrl = "https://nmrkpidev.blob.core.windows.net/dev-test/dev-test.json";
            string sasToken = "?sp=r&st=2024-10-28T10:35:48Z&se=2025-10-28T18:35:48Z&spr=https&sv=2022-11-02&sr=b&sig=bdeoPWtefikVgUGFCUs4ihsl22ZhQGu4%2B4cAfoMwd4k%3D";
            BlobServiceClient blobServiceClient = new BlobServiceClient(new Uri(blobUrl + sasToken));
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("dev-test");
            BlobClient blobClient = containerClient.GetBlobClient("dev-test.json");

            try
            {
                BlobDownloadInfo download = await blobClient.DownloadAsync();
                using (var streamReader = new StreamReader(download.Content))
                {
                    string jsonData = await streamReader.ReadToEndAsync();
                    List<Property> properties = JsonConvert.DeserializeObject<List<Property>>(jsonData);
                    return Ok(properties);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }
    }
}
