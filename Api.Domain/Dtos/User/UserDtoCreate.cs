using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Dtos.User
{
   //Dto para criar o objeto na tabela Users
    public class UserDtoCreate //Só precisamos do Nome e Email para esse UserDto baseado na UserEntity:BaseEntity
    {
        [Required(ErrorMessage = "Obrigatório inserir um nome!")]
        [StringLength(65, ErrorMessage = "Tamanho máximo permitido é de {1} caracteres.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "E-mail é um campo obrigatório!")]
        [EmailAddress(ErrorMessage = "E-mail está em formato inválido!")]
        [StringLength(90,ErrorMessage = "O máximo de caractéres permitidos é {1}")]
        public string Email { get; set; }
    }
}
