using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ResearchesUFU.API.Context;
using ResearchesUFU.API.Models;
using ResearchesUFU.API.Services.Interfaces;
using ResearchesUFU.API.Utils;

namespace ResearchesUFU.API.Services
{
    public class FieldService : IFieldService
    {
        private readonly ResearchesUFUContext _dbContext;
        public readonly IMapper _mapper;
        private readonly DbSet<Field> _fieldRepository;

        public FieldService(ResearchesUFUContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

            _fieldRepository = _dbContext.Fields;
        }

        public async Task<HttpResponseBase<Field>> GetAsync(int id)
        {
            try
            {
                var queryResult = await _fieldRepository.FindAsync(id);

                var response = HttpUtils<Field>.GenerateHttpResponse(queryResult);

                return response;
            }
            catch (Exception ex)
            {
                return HttpUtils<Field>.GenerateHttpErrorResponse();
            }
        }

        public async Task<HttpResponseBase<IQueryable<Field>>> GetAllAsync()
        {
            try
            {
                var result = await _fieldRepository.ToListAsync();
                var queryableResult = result.AsQueryable();
                var response = HttpUtils<IQueryable<Field>>.GenerateHttpResponse(queryableResult);

                return response;
            }
            catch (Exception ex)
            {
                return HttpUtils<IQueryable<Field>>.GenerateHttpErrorResponse();
            }
        }
    }
}