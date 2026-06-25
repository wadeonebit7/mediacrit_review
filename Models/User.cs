using System.ComponentModel.DataAnnotations;

namespace Mediacrit_Review.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Se requiere un nombre de usuario.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre de usuario debe tener entre 3 y 50 caracteres.")]
        public string Username { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Se requiere una dirección de correo electrónico.")]
        [EmailAddress(ErrorMessage = "Dirección de correo electrónico no válida.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Se requiere una contraseña.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string Password { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
