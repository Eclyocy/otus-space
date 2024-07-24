using SpaceShip.Service.Contracts;

namespace SpaceShip.Service.Interfaces
{
    public interface IResourceService
    {
        /// <summary>
        /// Create a Resource.
        /// </summary>
        ResourceDTO CreateResource(ResourceDTO resourceDTO);

        /// <summary>
        /// Retrieve the Resource by <paramref name="resourceId"/>.
        /// </summary>
        ResourceDTO GetResource(Guid resourceId);

        /// <summary>
        /// Update the Resource with <paramref name="resourceId"/>.
        /// </summary>
        ResourceDTO UpdateResource(Guid resourceId, ResourceDTO resourceDTO);

        /// <summary>
        /// Delete the Resource with <paramref name="resourceId"/>.
        /// </summary>
        bool DeleteResource(Guid resourceId);
    }
}
