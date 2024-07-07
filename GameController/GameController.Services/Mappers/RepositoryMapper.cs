﻿using AutoMapper;
using GameController.Database.Models;
using GameController.Services.Models.Session;
using GameController.Services.Models.User;

namespace GameController.Services.Mappers
{
    /// <summary>
    /// Mappings for RepositoryModels.
    /// </summary>
    public class RepositoryMapper : Profile
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public RepositoryMapper()
        {
            CreateMap<CreateUserDto, User>()
                .ForMember(x => x.NameUser, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.HashPass, opt => opt.MapFrom(x => x.PasswordHash));

            CreateMap<UpdateUserDto, User>()
                .ForMember(x => x.NameUser, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.HashPass, opt => opt.MapFrom(x => x.PasswordHash));

            CreateMap<User, UserDto>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.NameUser));

            CreateMap<Session, SessionDto>()
                .ForMember(x => x.SessionId, opt => opt.MapFrom(x => x.Id));
        }
    }
}
