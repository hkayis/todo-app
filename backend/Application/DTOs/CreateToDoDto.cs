using System.ComponentModel.DataAnnotations;

namespace TodoApi.Application.DTOs
{
    public class CreateToDoDto
    {
        [Required(ErrorMessage = "Başlık zorunludur")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "Başlık 3 ile 100 karakter arasında olmalıdır")]
        public string Title { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir")]
        public string? Description { get; set; }

        public bool IsCompleted { get; set; }
    }
}