using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Models;
using AutoMapper;

namespace Api.Service.Services
{
    public class UserService : IUserService
    {

        private IRepository<UserEntity> _repository; //Logicamente depois vamos dizer que para IRepository<UserEntity> deverá ser chamado por exemplo o BaseRepository<UserEntity>
        private readonly IMapper _mapper;
        public UserService(IRepository<UserEntity> repository, IMapper mapper) //Aqui o UserEntity está substituindo o <T> generics do IRepository<T>
        {
            _repository = repository;
            _mapper = mapper;
        }

        // Aqui na Service poderíamos implementar também as regras de negócios, validações, etc. Exemplo: Supondo que para buscar um UserEntity ele tem que ter Id>=100, 
        // podemos colocar essa regra dentro do método aqui na service para verificar se o user que chegou aqui possui tal requisito /\.

        public async Task<bool> Delete(Guid id)
        {    
            return await _repository.DeleteAsync(id); //Await pra aguardar o método do repositório assíncrono ser concluído e receber o bool
        }

        public async Task<UserDto> Get(Guid id)
        {
            //return antes do mapper/dto: return await _repository.SelectAsync(id);

            var entity = await _repository.SelectAsync(id); //Await pra aguardar o método do repositório assíncrono ser concluído e receber o UserEntity
            return _mapper.Map<UserDto>(entity); //Converte UserEntity para UserDto
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
           // return await _repository.SelectAsync(); //Await pra aguardar o método do repositório assíncrono ser concluído e receber o IEnumerable<UserEntity>
            var entities = await _repository.SelectAsync();
            return _mapper.Map<IEnumerable<UserDto>>(entities);
        }

        public async Task<UserDtoCreateResult> Post(UserDto user)
        {           
            // return await _repository.InsertAsync(user); //Await pra aguardar o método do repositório assíncrono ser concluído e receber o UserEntity
            var model = _mapper.Map<UserModel>(user);
             //Acima convertemos o UserDto para UserModel(Inclusive é acima que a CreateAt é atribuido)

            var entity = _mapper.Map<UserEntity>(model);
            //Acima convertemos UserModel para UserEntity

            var result = await _repository.InsertAsync(entity);

            return _mapper.Map<UserDtoCreateResult>(result);
            //Acima convertemos result do repository(UserEntity) para UserDtoCreateResult
            
            
        }

        public async Task<UserDtoUpdateResult> Put(UserDto user)
        {
            //return await _repository.UpdateAsync(user); //Await pra aguardar o método do repositório assíncrono ser concluído e receber o UserEntity
            var model = _mapper.Map<UserModel>(user);
            var entity = _mapper.Map<UserEntity>(model);
            var result = await _repository.UpdateAsync(entity);
            return _mapper.Map<UserDtoUpdateResult>(result);

        
        }
    }
}
