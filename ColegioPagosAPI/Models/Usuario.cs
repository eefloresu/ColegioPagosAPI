using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
/*
* Clase Usuario 
* Representa a un usuario del sistema.
* Contiene propiedades para el ID, nombre de usuario, clave y rol.
* Utilizada para almacenar y gestionar la información de los usuarios en la base de datos.
* Esta clase se mapea a la tabla "Usuarios" en la base de datos.
*/
namespace ColegioPagosAPI.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NombreUsuario { get; set; }

        [Required]
        public string Clave { get; set; }

        [Required]
        public string role { get; set; }
    }
}