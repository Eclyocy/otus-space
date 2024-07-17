using AutoMapper;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Service.Contracts;
using SpaceShip.Service.Interfaces;

namespace SpaceShip.Service.Services
{
    public class ResourceTypeService : IResourceTypeService
    {
        private readonly IResourceTypeRepository _resourceTypeRepository;
        private readonly IMapper _mapper;

        public ResourceTypeService(IResourceTypeRepository resourceTypeRepository, IMapper mapper)
        {
            _resourceTypeRepository = resourceTypeRepository;
            _mapper = mapper;
        }

        public ResourceTypeDTO Create(ResourceTypeDTO resourceTypeDTO)
        {
            return _mapper.Map<ResourceTypeDTO>(_resourceTypeRepository.Create(resourceTypeDTO.Name));
        }
    }
}
