using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.User;

namespace Api.Service.Services
{
    public class UserService : IUserService
    {

        private IRepository<UserEntity> _repository; //Logicamente depois vamos dizer que para IRepository<UserEntity> deverá ser chamado por exemplo o BaseRepository<UserEntity>

        public UserService(IRepository<UserEntity> repository) //Aqui o UserEntity está substituindo o <T> generics do IRepository<T>
        {
            _repository = repository;
        }

 // Aqui na Service poderíamos implementar também as regras de negócios, validações, etc. Exemplo: Supondo que para buscar um UserEntity ele tem que ter Id>=100, 
 // podemos colocar essa regra dentro do método aqui na service para verificar se o user que chegou aqui possui tal requisito /\.

        public async Task<bool> Delete(Guid id)
        {    
            return await _repository.DeleteAsync(id); //Await pra aguardar o método do repositório assíncrono ser concluído e receber o bool
        }

        public async Task<UserEntity> Get(Guid id)
        {
            return await _repository.SelectAsync(id); //Await pra aguardar o método do repositório assíncrono ser concluído e receber o UserEntity
        }

        public async Task<IEnumerable<UserEntity>> GetAll()
        {
            return await _repository.SelectAsync(); //Await pra aguardar o método do repositório assíncrono ser concluído e receber o IEnumerable<UserEntity>
        }

        public async Task<UserEntity> Post(UserEntity user)
        {
            return await _repository.InsertAsync(user); //Await pra aguardar o método do repositório assíncrono ser concluído e receber o UserEntity
        }

        public async Task<UserEntity> Put(UserEntity user)
        {
            return await _repository.UpdateAsync(user); //Await pra aguardar o método do repositório assíncrono ser concluído e receber o UserEntity
        }
    }
}
