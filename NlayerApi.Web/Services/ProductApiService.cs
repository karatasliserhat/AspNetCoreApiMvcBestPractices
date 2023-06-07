using NlayerApi.Core.DTOs;

namespace NlayerApi.Web.Services
{
    public class ProductApiService
    {
        private readonly HttpClient _client;

        public ProductApiService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<ProductWithCategoryDto>> GetProductWithCategoryAsync()
        {
            var response = await _client.GetFromJsonAsync<CustomResponseDto<List<ProductWithCategoryDto>>>("products/GetProductWithCategory");

            return response.Data;
        }

        public async Task<ProductCreateDto> SaveAsync(ProductCreateDto productCreateDto)
        {
            var respone = await _client.PostAsJsonAsync("products", productCreateDto);

            if (!respone.IsSuccessStatusCode) return null;
            var responseBody = await respone.Content.ReadFromJsonAsync<CustomResponseDto<ProductCreateDto>>();

            return responseBody.Data;
        }

        public async Task<bool> UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            var response = await _client.PutAsJsonAsync("products", productUpdateDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var response = await _client.DeleteAsync($"products/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<ProductDto> ProductGetByIdAsync(int id)
        {
            var response = await _client.GetFromJsonAsync<CustomResponseDto<ProductDto>>($"products/{id}");

            return response.Data;
        }
    }
}
