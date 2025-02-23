using Persons.API.Constants;
using Persons.API.Database;
using Persons.API.Database.Entities;
using Persons.API.Dtos.Common;
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
        public async Task<ResponseDto<PersonEntity>> CreateAsync(PersonEntity person)
        {
            _context.Add(person);
            await _context.SaveChangesAsync();

            return new ResponseDto<PersonEntity>
            {
                StatusCode = HttpStatusCode.CREATED,
                Status = true,
                Message = "Registro creado correctamente",
                Data = person
            };
        }
    }
}
