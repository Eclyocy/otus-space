using Shared.Enums;
using SpaceShip.Domain.Entities;

namespace SpaceShip.Service.Interfaces
{
    /// <summary>
    /// Interface for working with space ship resources.
    /// </summary>
    public interface IResourceService
    {
        /// <summary>
        /// Get resource type required for lifesupport of <paramref name="resource"/>.
        /// </summary>
        ResourceType? GetRequiredResourceType(Resource resource);

        /// <summary>
        /// Get amount of required for lifesupport of <paramref name="resource"/> resource.
        /// </summary>
        int GetRequiredResourceAmount(Resource resource);

        /// <summary>
        /// Get resource (type and amount) to repair <paramref name="resource"/>.
        /// </summary>
        (ResourceType? ResourceType, int Amount) GetSpareResourceRequirement(Resource resource);

        /// <summary>
        /// Set resource <paramref name="resource"/> state to <paramref name="resourceState"/> with criticality <paramref name="criticality"/>.
        /// </summary>
        void UpdateResourceState(Resource resource, ResourceState resourceState, EventLevel? criticality = null);

        /// <summary>
        /// Set resource <paramref name="resource"/> amount to <paramref name="amount"/>.
        /// </summary>
        void UpdateResourceAmount(Resource resource, int amount);
    }
}
