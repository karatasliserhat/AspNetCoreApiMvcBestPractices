using AutoMapper;
using NlayerApi.Core.DTOs;
using NlayerApi.Core.IRepositories;
using NlayerApi.Core.IServices;
using NlayerApi.Core.Models;
using NlayerApi.Core.UnitOfWork;

namespace NlayerApi.Service.Services
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(IGenericRepository<Category> repository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<GetByCategoryWithProductDto>> GetGetByCategoryWithProduct(int id)
        {
            var categoryData = await _categoryRepository.GetByIdCategoryWithProduct(id);
            var mapperCategory = _mapper.Map<GetByCategoryWithProductDto>(categoryData);
            return CustomResponseDto<GetByCategoryWithProductDto>.Success(mapperCategory, 200);
        }
    }
}
