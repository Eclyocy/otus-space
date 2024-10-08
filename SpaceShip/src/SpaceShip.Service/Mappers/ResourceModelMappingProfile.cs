﻿using AutoMapper;
using SpaceShip.Domain.Model;
using SpaceShip.Service.Contracts;

namespace SpaceShip.Service.Mappers
{
    /// <summary>
    /// Профииль автомаппера модели ресурсов корабля уровня БД.
    /// </summary>
    public class ResourceModelMappingProfile : Profile
    {
        /// <summary>
        /// Конструктор, при создании описываем настройки маппинга.
        /// </summary>
        public ResourceModelMappingProfile()
        {
            // Database models -> Service models // Database models -> Service models
            CreateMap<Resource, ResourceDTO>();
        }
    }
}
