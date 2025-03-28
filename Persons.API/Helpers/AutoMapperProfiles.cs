﻿using AutoMapper;
using Persons.API.Database.Entities;
using Persons.API.Dtos.Countries;
using Persons.API.Dtos.Persons;

namespace Persons.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<PersonEntity, PersonDto>();
            CreateMap<PersonCreateDto, PersonEntity>();
            CreateMap<PersonEntity, PersonActionResponseDto>();
            CreateMap<PersonEditDto, PersonEntity>();

            CreateMap<CountryEntity, CountryDto>();
            CreateMap<CountryEntity, CountryActionResponseDto>();
            CreateMap<CountryCreateDto, CountryEntity>();
            CreateMap<CountryEditDto, CountryEntity>();
        }
    }
}
