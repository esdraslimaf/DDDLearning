using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services.User
{
    public interface IUserService
    {
        //No método estou utilizando nomeclaturas que lembrem aos verbos HTTP para ficar mais didático
        Task<UserEntity> Get(Guid id); //Buscará o usuário
        Task<IEnumerable<UserEntity>> GetAll(); //Buscará todos os usuários
        Task<UserEntity> Post(UserEntity user); //Irá adicionar o usuário
        Task<UserEntity> Put(UserEntity user); //Irá atualizar o usuário
        Task<bool> Delete(Guid id); //Deleta por id
    }
}
