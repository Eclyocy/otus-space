using AutoMapper;
using SpaceShip.Domain.Model;
using SpaceShip.Service.Contracts;

namespace SpaceShip.Service.Mappers
{
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
