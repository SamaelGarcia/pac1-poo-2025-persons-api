using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Persons.API.Dtos.Persons
{
    public class PersonCreateDto
    {
        [Display(Name = "Nombres")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "El campo {0} debe tener un minimo de {2} y un maximo de {1} caracteres")]
        public string FirstName { get; set; }

        [Display(Name = "Apelllidos")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "El campo {0} debe tener un minimo de {2} y un maximo de {1} caracteres")]
        public string LastName { get; set; }

        [Display(Name = "Documento Nacional de Identidad")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "El campo {0} debe tener un minimo de {2} y un maximo de {1} caracteres")]
        public string DNI { get; set; }

        public string Gender { get; set; }
    }
}
