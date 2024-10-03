using SpaceShip.Service.Contracts;

namespace SpaceShip.Service.Interfaces
{
    /// <summary>
    /// Resources service interface.
    /// </summary>
    public interface IResourceService
    {
        /// <summary>
        /// Decrease resource amount.
        /// </summary>
        ResourceDTO DecreaseResourceAmount(Guid resourceId);

        /// <summary>
        /// Set resource state to Dead.
        /// </summary>
        ResourceDTO SetResourceStateToDead(Guid resourceId);
    }
}
