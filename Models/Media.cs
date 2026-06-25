using System.ComponentModel.DataAnnotations;

namespace Mediacrit_Review.Models
{
    public class Media
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Se requiere el título.")]
        [StringLength(150, ErrorMessage = "El título no puede exceder los 150 caracteres.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Se requiere el tipo de medio.")]
        public string Type { get; set; } = string.Empty;

        [Required(ErrorMessage = "Se requiere el creador (Autor/Director).")]
        [StringLength(100, ErrorMessage = "El nombre del creador no puede exceder los 100 caracteres.")]
        public string Creator { get; set; } = string.Empty;

        [Required(ErrorMessage = "Se requiere el año de lanzamiento.")]
        [Range(1800, 2100, ErrorMessage = "Por favor, ingrese un año válido entre 1800 y 2100.")]
        public int ReleaseYear { get; set; }
    }
}
