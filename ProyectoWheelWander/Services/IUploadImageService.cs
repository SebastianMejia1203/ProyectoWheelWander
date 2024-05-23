using ProyectoWheelWander.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ProyectoWheelWander.Services
{
    public interface IUploadImageService
    {
        Task<string> UploadImageAsync(IFormFile file);
    }

    public class UploadImageService : IUploadImageService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _uploadUrl;

        public UploadImageService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["ImgBB:ApiKey"];
            _uploadUrl = "https://api.imgbb.com/1/upload";
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            using (var content = new MultipartFormDataContent())
            {
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    var fileBytes = ms.ToArray();
                    var byteArrayContent = new ByteArrayContent(fileBytes);
                    byteArrayContent.Headers.ContentType = MediaTypeHeaderValue.Parse(file.ContentType);

                    content.Add(byteArrayContent, "image", file.FileName);

                    var response = await _httpClient.PostAsync($"{_uploadUrl}?key={_apiKey}", content);
                    response.EnsureSuccessStatusCode();

                    var responseString = await response.Content.ReadAsStringAsync();
                    var imgBBResponse = JsonConvert.DeserializeObject<ImgBBResponse>(responseString);

                    return imgBBResponse.data.url;
                }
            }
        }
    }
}
