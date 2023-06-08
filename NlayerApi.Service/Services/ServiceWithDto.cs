using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NlayerApi.Core.DTOs;
using NlayerApi.Core.IRepositories;
using NlayerApi.Core.IServices;
using NlayerApi.Core.Models;
using NlayerApi.Core.UnitOfWork;
using System.Linq.Expressions;

namespace NlayerApi.Service.Services
{
    public class ServiceWithDto<Entity, Dto> : IServiceWithDto<Entity, Dto> where Entity : BaseModel where Dto : class
    {
        private readonly IGenericRepository<Entity> _genericRepository;
        protected readonly IMapper _mapper;
        protected readonly IUnitOfWork _unitOfWork;

        public ServiceWithDto(IGenericRepository<Entity> genericRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResponseDto<Dto>> AddAsync(Dto Dto)
        {
            var entity = _mapper.Map<Entity>(Dto);
            await _genericRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            var data = _mapper.Map<Dto>(entity);
            return CustomResponseDto<Dto>.Success(data, StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDto<IEnumerable<Dto>>> AdRangeAsync(IEnumerable<Dto> Dtos)
        {
            var entities = _mapper.Map<IEnumerable<Entity>>(Dtos);
            await _genericRepository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            var datas = _mapper.Map<IEnumerable<Dto>>(entities);
            return CustomResponseDto<IEnumerable<Dto>>.Success(datas, StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDto<bool>> AnyAsync(Expression<Func<Entity, bool>> expression)
        {
            var data = await _genericRepository.AnyAsync(expression);
            return CustomResponseDto<bool>.Success(data,StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDto<IEnumerable<Dto>>> GetAllAsync()
        {
            var data = await _genericRepository.GetAll().ToListAsync();
            var dataMap = _mapper.Map<List<Dto>>(data);
            return CustomResponseDto<IEnumerable<Dto>>.Success(dataMap, StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDto<Dto>> GetByIdAsync(int id)
        {
            var data = await _genericRepository.GetByIdAsync(id);
            var dataMap = _mapper.Map<Dto>(data);
            return CustomResponseDto<Dto>.Success(dataMap, StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id)
        {
            var data = await _genericRepository.GetByIdAsync(id);
             _genericRepository.Remove(data);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveRangeAsync(IEnumerable<int> ids)
        {
            var entities =  _genericRepository.Where(x=> ids.Contains(x.Id)).ToList();
            _genericRepository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(Dto Dto)
        {
            var entity = _mapper.Map<Entity>(Dto);
            _genericRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDto<IEnumerable<Dto>>> WhereAsync(Expression<Func<Entity, bool>> expression)
        {
            var entities = await _genericRepository.Where(expression).ToListAsync();
            var datas = _mapper.Map<IEnumerable<Dto>>(entities);
            return CustomResponseDto<IEnumerable<Dto>>.Success(datas, StatusCodes.Status200OK);
        }
    }
}
