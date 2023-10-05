using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services.User
{
    public interface IUserService
    {
        //No método estou utilizando nomeclaturas que lembrem aos verbos HTTP para ficar mais didático
        Task<UserDto> Get(Guid id); //Buscará o usuário
        Task<IEnumerable<UserDto>> GetAll(); //Buscará todos os usuários
        Task<UserDtoCreateResult> Post(UserDto user); //Irá adicionar o usuário
        Task<UserDtoUpdateResult> Put(UserDto user); //Irá atualizar o usuário
        Task<bool> Delete(Guid id); //Deleta por id
    }
} 
