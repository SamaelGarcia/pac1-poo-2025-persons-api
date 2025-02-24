using Microsoft.AspNetCore.Mvc;
using Persons.API.Database.Entities;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Persons;
using Persons.API.Services.Interfaces;
using System.Net;
using System.Reflection;

namespace Persons.API.Controllers
{
    [ApiController]
    [Route("api/persons")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonsService _personsService;
        private List<PersonEntity> _persons;
        public PersonsController(IPersonsService personsService)
        {
            _personsService = personsService;


            //_persons = new List<Person>();
            //_persons.Add(new Person { DNI = "0001200100001", FirstName = "Juan", LastName = "Perez", Gender = "M" });    
            //_persons.Add(new Person { DNI = "0001200100002", Gender = "F", FirstName = "Tulipan", LastName = "Bohorquez"});
            //_persons.Add(new Person { DNI = "0001200100003", Gender = "M", FirstName = "Cristhian", LastName = "Guevara"});

            //Comentado en Clase 14
            //_persons = new List<PersonEntity> 
            //{
            //    new PersonEntity{ DNI = "0001200100001", FirstName = "Juan", LastName = "Perez", Gender = "M" },
            //    new PersonEntity{ DNI = "0001200100002", Gender = "F", FirstName = "Tulipan", LastName = "Bohorquez" },
            //    new PersonEntity{ DNI = "0001200100003", Gender = "M", FirstName = "Cristhian", LastName = "Guevara" },
            //};
        }
        //Comentado en clase 14
        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    return Ok(_persons);
        //}

        //[HttpGet("{DNI}")]

        //public IActionResult GetUno(string DNI)
        //{
        //    return Ok(_persons.Where(x => x.DNI == DNI).FirstOrDefault());
        //}

        [HttpPost]
        public async Task<ActionResult<ResponseDto<PersonActionResponseDto>>> Post([FromBody]PersonCreateDto dto)
        {
            //_persons.Add(person);
            //return Ok(_persons);
            var response = await _personsService.CreateAsync(dto);
            return StatusCode(response.StatusCode, new 
            {
                response.Status,
                response.Message,
                response.Data,
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<PersonActionResponseDto>>> GetOne(Guid id)
        {
            var response = await _personsService.GetOneByIdAsync(id);

            return StatusCode(response.StatusCode, response);
        }

        //[HttpPut("{DNI}")]
        //public IActionResult Put(string DNI, [FromBody] PersonEntity person)
        //{
        //    var oldPerson = _persons.FirstOrDefault(x => x.DNI == DNI);
        //    if (oldPerson != null)
        //    {
        //        _persons.Remove(oldPerson);
        //        _persons.Add(person);
        //    }
        //    return Ok(_persons);
        //}

        //[HttpDelete("{DNI}")]

        //public IActionResult Delete(string DNI)
        //{
        //    var person = _persons.FirstOrDefault(x =>x.DNI == DNI);
        //    if(person != null)
        //    {
        //        _persons.Remove(person);
        //    }

        //    return Ok(_persons);
        //}
    }
}
