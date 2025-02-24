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

        public PersonsServices(PersonsDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto<List<PersonDto>>> GetListAsync()
        {
            var personEntity = await _context.Persons.ToListAsync();

            var personsDto = new List<PersonDto>();
            foreach (var person in personEntity)
            {
                personsDto.Add(new PersonDto
                {
                    Id = person.Id,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    DNI = person.DNI,
                    Gender = person.Gender,
                });
            }

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
                Data = new PersonDto
                {
                    Id = personEntity.Id,
                    FirstName = personEntity.FirstName,
                    LastName = personEntity.LastName,
                    DNI = personEntity.DNI,
                    Gender = personEntity.Gender,
                }
            };
        }
        public async Task<ResponseDto<PersonActionResponseDto>> CreateAsync(PersonCreateDto dto)
        {
            var personEntity = new PersonEntity
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DNI = dto.DNI,
                Gender = dto.Gender
            };
            _context.Persons.Add(personEntity);
            await _context.SaveChangesAsync();

            var response = new PersonActionResponseDto
            {
                Id = personEntity.Id,
                FirstName = personEntity.FirstName,
                LastName = personEntity.LastName,
            };
            return new ResponseDto<PersonActionResponseDto>
            {
                StatusCode = HttpStatusCode.CREATED,
                Status = true,
                Message = "Registro creado correctamente",
                Data = response
            };
        }
    }
}
