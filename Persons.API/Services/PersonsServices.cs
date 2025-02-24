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
