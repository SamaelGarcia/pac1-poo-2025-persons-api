using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Persons.API.Constants;
using Persons.API.Database;
using Persons.API.Database.Entities;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Persons;
using Persons.API.Services.Interfaces;

namespace Persons.API.Services
{
    public class PersonsServices : IPersonsService
    {
        private readonly PersonsDbContext _context;
        private readonly IMapper _mapper;

        public PersonsServices(PersonsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<PersonDto>>> GetListAsync()
        {
            var personEntity = await _context.Persons.ToListAsync();

            //var personsDto = new List<PersonDto>();
            //foreach (var person in personEntity)
            //{
            //    personsDto.Add(new PersonDto
            //    {
            //        Id = person.Id,
            //        FirstName = person.FirstName,
            //        LastName = person.LastName,
            //        DNI = person.DNI,
            //        Gender = person.Gender,
            //    });
            //}

            var personsDto = _mapper.Map<List<PersonDto>>(personEntity);

            return new ResponseDto<List<PersonDto>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = personEntity.Count() > 0 ? "Registros Encontrados" : "No se encontraron registros",
                Data = personsDto
            };
        }

        public async Task<ResponseDto<PersonDto>> GetOneByIdAsync(Guid id)
        {
            var personEntity = await _context.Persons.
                FirstOrDefaultAsync(person => person.Id == id);

            if(personEntity == null)
            {
                return new ResponseDto<PersonDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado"
                };
            }

            return new ResponseDto<PersonDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro encontrado",
                //Data = new PersonDto
                //{
                //    Id = personEntity.Id,
                //    FirstName = personEntity.FirstName,
                //    LastName = personEntity.LastName,
                //    DNI = personEntity.DNI,
                //    Gender = personEntity.Gender,
                //}
                Data = _mapper.Map<PersonDto>(personEntity)
            };
        }

        public async Task<ResponseDto<PersonActionResponseDto>> CreateAsync(PersonCreateDto dto)
        {
            //var personEntity = new PersonEntity
            //{
            //    Id = Guid.NewGuid(),
            //    FirstName = dto.FirstName,
            //    LastName = dto.LastName,
            //    DNI = dto.DNI,
            //    Gender = dto.Gender
            //};

            var personEntity = _mapper.Map<PersonEntity>(dto);

            _context.Persons.Add(personEntity);
            await _context.SaveChangesAsync();

            //var response = new PersonActionResponseDto
            //{
            //    Id = personEntity.Id,
            //    FirstName = personEntity.FirstName,
            //    LastName = personEntity.LastName,
            //};

            return new ResponseDto<PersonActionResponseDto>
            {
                StatusCode = HttpStatusCode.CREATED,
                Status = true,
                Message = "Registro creado correctamente",
                //Data = response
                Data = _mapper.Map<PersonActionResponseDto>(personEntity) /* Usando AutoMapper */
            };
        }

        public async Task<ResponseDto<PersonActionResponseDto>> EditAsync(PersonEditDto dto, Guid id)
        {
            var personEntity = await _context.Persons.FirstOrDefaultAsync(x => x.Id == id);

            if (personEntity is null)
            {
                return new ResponseDto<PersonActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado"
                };
            }

            //personEntity.FirstName = dto.FirstName;
            //personEntity.LastName = dto.LastName;
            //personEntity.DNI = dto.DNI;
            //personEntity.Gender = dto.Gender;

            _mapper.Map<PersonEditDto, PersonEntity>(dto, personEntity);

            _context.Persons.Update(personEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<PersonActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro editado correctamente",
                //Data = new PersonActionResponseDto
                //{
                //    Id = personEntity.Id,
                //    FirstName = dto.FirstName,
                //    LastName =  dto.LastName,
                //}
                Data = _mapper.Map<PersonActionResponseDto>(personEntity)
            };
        }

        public async Task<ResponseDto<PersonActionResponseDto>> DeleteAsync(Guid id)
        {
            var personEntity = await _context.Persons.FirstOrDefaultAsync(x => x.Id == id);

            if (personEntity is null)
            {
                return new ResponseDto<PersonActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado",
                };
            }
            _context.Persons.Remove(personEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<PersonActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro eliminado correctamente",
                //Data = new PersonActionResponseDto
                //{
                //    Id = personEntity.Id,
                //    FirstName = personEntity.FirstName,
                //    LastName = personEntity.LastName,
                //}
                Data = _mapper.Map<PersonActionResponseDto>(personEntity)
            };
        }
    }
}
