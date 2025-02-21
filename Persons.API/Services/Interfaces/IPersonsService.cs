using Persons.API.Database.Entities;
using Persons.API.Dtos.Common;

namespace Persons.API.Services.Interfaces
{
    public interface IPersonsService
    {
        Task<ResponseDto<PersonEntity>> CreateAsync(PersonEntity person);
    }
}
