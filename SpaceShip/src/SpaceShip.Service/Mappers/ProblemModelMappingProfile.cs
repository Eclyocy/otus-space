using AutoMapper;
using SpaceShip.Domain.Model;
using SpaceShip.Service.Contracts;

namespace SpaceShip.Service.Mappers
{
    /// <summary>
    /// Профиль автомаппера модели проблем корабля из уровня БД.
    /// </summary>
    public class ProblemModelMappingProfile : Profile
    {
        /// <summary>
        /// Конструктор, при создании описываем настройки маппинга.
        /// </summary>
        public ProblemModelMappingProfile()
        {
            CreateMap<Problem, ProblemDTO>();
        }
    }
}
