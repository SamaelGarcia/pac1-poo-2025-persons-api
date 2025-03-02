using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Persons.API.Dtos.Countries
{
    public class CountryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AlphaCode3 { get; set; }
    }
}
