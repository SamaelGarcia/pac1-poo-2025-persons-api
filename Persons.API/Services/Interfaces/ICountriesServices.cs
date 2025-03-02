﻿using Persons.API.Dtos.Common;
using Persons.API.Dtos.Countries;

namespace Persons.API.Services.Interfaces
{
    public interface ICountriesServices
    {
        Task<ResponseDto<CountryActionResponseDto>> CreateAsync(CountryCreateDto dto);
        Task<ResponseDto<CountryActionResponseDto>> DeleteAsync(Guid id);
        Task<ResponseDto<CountryActionResponseDto>> EditAsync(CountryEditDto dto, Guid id);
        Task<ResponseDto<List<CountryDto>>> GetListAsync();
        Task<ResponseDto<CountryDto>> GetOneByIdAsync(Guid id);
    }
}
