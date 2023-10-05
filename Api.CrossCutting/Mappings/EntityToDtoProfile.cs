using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            //Esse servirá geralmente para get
            CreateMap<UserDto, UserEntity>().ReverseMap();
            //Esse servirá geralmente para insert
            CreateMap<UserDtoCreateResult, UserEntity>().ReverseMap();
            //Esse servirá geralmente para update
            CreateMap<UserDtoUpdateResult, UserEntity>().ReverseMap();
        }
    }
}
