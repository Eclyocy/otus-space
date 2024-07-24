using SpaceShip.Service.Contracts;

namespace SpaceShip.Service.Interfaces
{
    public interface IResourceTypeService
    {
        /// <summary>
        /// Create a ResourceType.
        /// </summary>
        ResourceTypeDTO CreateResourceType(ResourceTypeDTO resourceTypeDTO);

        /// <summary>
        /// Retrieve the ResourceType by <paramref name="resourceTypeId"/>.
        /// </summary>
        ResourceTypeDTO GetResourceType(Guid resourceTypeId);

        /// <summary>
        /// Update the ResourceType with <paramref name="resourceTypeId"/>.
        /// </summary>
        ResourceTypeDTO UpdateResourceType(Guid resourceTypeId, ResourceTypeDTO resourceTypeDTO);

        /// <summary>
        /// Delete the ResourceType with <paramref name="ResourceTypeId"/>.
        /// </summary>
        bool DeleteResourceType(Guid resourceTypeId);
    }
}
