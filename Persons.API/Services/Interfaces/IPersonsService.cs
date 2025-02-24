using Persons.API.Database.Entities;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Persons;

namespace Persons.API.Services.Interfaces
{
    public interface IPersonsService
    {
        Task<ResponseDto<PersonActionResponseDto>> CreateAsync(PersonCreateDto person);
        Task<ResponseDto<PersonDto>> GetOneByIdAsync(Guid id);
    }
}
