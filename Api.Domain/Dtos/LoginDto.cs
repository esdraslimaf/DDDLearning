using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email é obrigatório para login!")]
        [EmailAddress(ErrorMessage = "O email está em um formato inválido!")]
        public string Email { get; set; }
    }
}
