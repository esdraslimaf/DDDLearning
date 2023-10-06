using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Dtos.User
{
    //Dto para atualizar
    public class UserDtoUpdate
    {
        [Required(ErrorMessage ="O campo Id é obrigatório!")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Obrigatório inserir um nome!")]
        [StringLength(65, ErrorMessage = "Tamanho máximo permitido é de {1} caracteres.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "E-mail é um campo obrigatório!")]
        [EmailAddress(ErrorMessage = "E-mail está em formato inválido!")]
        [StringLength(90,ErrorMessage = "O máximo de caractéres permitidos é {1}")]
        public string Email { get; set; }
    }
}
