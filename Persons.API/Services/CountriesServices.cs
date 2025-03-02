﻿using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Persons.API.Constants;
using Persons.API.Database;
using Persons.API.Database.Entities;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Countries;
using Persons.API.Services.Interfaces;
using Persons.API.Dtos.Persons;

namespace Persons.API.Services
{
    public class CountriesServices : ICountriesServices
    {
        private readonly PersonsDbContext _context;
        private readonly IMapper _mapper;

        public CountriesServices(PersonsDbContext personsDbContext, IMapper mapper)
        {
            _context = personsDbContext;
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<CountryDto>>> GetListAsync()
        {
            var countries = await _context.Countries.OrderBy(x => x.AlphaCode3).ToListAsync();

            var countriesDtos = _mapper.Map<List<CountryDto>>(countries);

            return new ResponseDto<List<CountryDto>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registros obtenidos correctamente",
                Data = countriesDtos
            };
        }

        public async Task<ResponseDto<CountryDto>> GetOneByIdAsync(Guid id)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);

            if(country == null)
            {
                return new ResponseDto<CountryDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "El registro no se ha encontrado",
                };
            }

            return new ResponseDto<CountryDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro encontrado correctamente",
                Data = _mapper.Map<CountryDto>(country),
            };
        }

        public async Task<ResponseDto<CountryActionResponseDto>> CreateAsync(CountryCreateDto dto)
        {
            var countryEntity = _mapper.Map<CountryEntity>(dto);

            _context.Countries.Add(countryEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<CountryActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro creado correctamente",
                Data = _mapper.Map<CountryActionResponseDto>(countryEntity)
            };
        }

        public async Task<ResponseDto<CountryActionResponseDto>> EditAsync(CountryEditDto dto, Guid id)
        {
            var countryEntity = await _context.Countries.FindAsync(id);

            if (countryEntity is null)
            {
                return new ResponseDto<CountryActionResponseDto> 
                { 
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado",
                };   
            }

            _mapper.Map<CountryEditDto, CountryEntity>(dto, countryEntity);
            _context.Countries.Update(countryEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<CountryActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro editado correctamente",
                Data = _mapper.Map<CountryActionResponseDto>(countryEntity),
            };
        } 

        public async Task<ResponseDto<CountryActionResponseDto>> DeleteAsync(Guid id)
        {
            var countryEntity = await _context.Countries.FindAsync(id);

            if (countryEntity is null)
            {
                return new ResponseDto<CountryActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado",
                };
            }

            var personsInCountry = await _context.Persons
                .CountAsync(p => p.CountryId == countryEntity.Id);

            if(personsInCountry > 0)
            {
                return new ResponseDto<CountryActionResponseDto>
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "El pais tiene datos relacionados."
                };
            }

            _context.Countries.Remove(countryEntity);

            await _context.SaveChangesAsync();

            return new ResponseDto<CountryActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro eliminado correctamente",
                Data = _mapper.Map<CountryActionResponseDto>(countryEntity),
            };
        }
    }
}
