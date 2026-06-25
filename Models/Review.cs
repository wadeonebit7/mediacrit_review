
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mediacrit_Review.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int MediaId { get; set; }


        [Required(ErrorMessage = "Se requiere una calificación.")]
        [Range(1, 5, ErrorMessage = "La calificación debe estar entre 1 y 5 estrellas.")]
        public int Rating { get; set; }

        [StringLength(1000, ErrorMessage = "El comentario no puede exceder los 1000 caracteres.")]
        public string? Comment { get; set; }

        [Required(ErrorMessage = "Se requiere indicar si se recomienda.")]
        public bool IsRecommended { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [ForeignKey("MediaId")]
        public Media? Media { get; set; }
    }
}
