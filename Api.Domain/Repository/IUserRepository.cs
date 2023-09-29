using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces;

namespace Api.Domain.Repository
{
    /* A interface IUserRepository está estendendo a interface IRepository<UserEntity> */
    public interface IUserRepository:IRepository<UserEntity>
    {
        Task<UserEntity> FindByLogin(string email);
    }
}
