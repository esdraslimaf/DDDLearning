using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Repository;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
         private IUserRepository _repo;

        public LoginService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<object> FindByLogin(LoginDto user)
        {
           if(user != null && !string.IsNullOrWhiteSpace(user.Email)){
            return await _repo.FindByLogin(user.Email);
           }
           else{
            return null;
           }
        }
    }
}
