using Microsoft.AspNetCore.Mvc;
using Persons.API.Database.Entities;
using System.Net;
using System.Reflection;

namespace Persons.API.Controllers
{
    [ApiController]
    [Route("api/persons")]
    public class PersonsController : ControllerBase
    {
        private List<PersonEntity> _persons;
        public PersonsController()
        {
            //_persons = new List<Person>();
            //_persons.Add(new Person { DNI = "0001200100001", FirstName = "Juan", LastName = "Perez", Gender = "M" });    
            //_persons.Add(new Person { DNI = "0001200100002", Gender = "F", FirstName = "Tulipan", LastName = "Bohorquez"});
            //_persons.Add(new Person { DNI = "0001200100003", Gender = "M", FirstName = "Cristhian", LastName = "Guevara"});

            _persons = new List<PersonEntity>
            {
                new PersonEntity{ DNI = "0001200100001", FirstName = "Juan", LastName = "Perez", Gender = "M" },
                new PersonEntity{ DNI = "0001200100002", Gender = "F", FirstName = "Tulipan", LastName = "Bohorquez" },
                new PersonEntity{ DNI = "0001200100003", Gender = "M", FirstName = "Cristhian", LastName = "Guevara" },
            };
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_persons);
        }

        [HttpGet("{DNI}")]

        public IActionResult GetUno(string DNI)
        {
            return Ok(_persons.Where(x => x.DNI == DNI).FirstOrDefault());
        }

        [HttpPost]
        public IActionResult Post([FromBody]PersonEntity person)
        {
            _persons.Add(person);
            return Ok(_persons);
        }

        [HttpPut("{DNI}")]
        public IActionResult Put(string DNI, [FromBody] PersonEntity person)
        {
            var oldPerson = _persons.FirstOrDefault(x => x.DNI == DNI);
            if (oldPerson != null)
            {
                _persons.Remove(oldPerson);
                _persons.Add(person);
            }
            return Ok(_persons);
        }

        [HttpDelete("{DNI}")]

        public IActionResult Delete(string DNI)
        {
            var person = _persons.FirstOrDefault(x =>x.DNI == DNI);
            if(person != null)
            {
                _persons.Remove(person);
            }

            return Ok(_persons);
        }
    }
}
