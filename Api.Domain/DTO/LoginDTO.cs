using System.ComponentModel.DataAnnotations;

namespace Api.Domain.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email é um campo obrigatório para o login")]
        [EmailAddress(ErrorMessage = "Email em formato inválido.")]
        [StringLength(255, ErrorMessage = "Email deve ter no maximo {1} caracteres.")]
        public string Email { get; set; }
    }
}
