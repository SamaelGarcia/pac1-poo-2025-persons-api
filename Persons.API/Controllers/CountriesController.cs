using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persons.API.Constants;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Countries;
using Persons.API.Services;
using Persons.API.Services.Interfaces;

namespace Persons.API.Controllers
{
    [Route("api/countries")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesServices _countriesServices;

        public CountriesController(ICountriesServices countriesServices)
        {
            _countriesServices = countriesServices;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<CountryDto>>>> 
            GetList(string searchTerm = "", int page = 1, int pageSize = 0)
        {
            //var response = await _countriesServices.GetListAsync();
            var response = await _countriesServices.GetListAsync(searchTerm, page, pageSize);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<List<CountryDto>>>> GetOne(Guid id)
        {
            var response = await _countriesServices.GetOneByIdAsync(id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });


        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<List<CountryDto>>>> Create([FromBody] CountryCreateDto dto)
        {
            var response = await _countriesServices.CreateAsync(dto);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<List<CountryActionResponseDto>>>> Edit([FromBody] CountryEditDto dto, Guid id)
        {
            var response = await _countriesServices.EditAsync(dto, id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<List<CountryActionResponseDto>>>> Delete(Guid id)
        {
            var response = await _countriesServices.DeleteAsync(id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }
    }
}
