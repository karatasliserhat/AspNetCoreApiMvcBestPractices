using NlayerApi.Core.DTOs;

namespace NlayerApi.Web.Services
{
    public class CategoryApiService
    {
        private readonly HttpClient _client;

        public CategoryApiService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<CategoryDto>> GetAllCategoryAsync()
        {
            var response = await _client.GetFromJsonAsync<CustomResponseDto<List<CategoryDto>>>("categories");

            return response.Data;
        }
    }
}
