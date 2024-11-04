using Microsoft.Extensions.Logging;
using Shared.Enums;
using SpaceShip.Domain.Entities;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Service.Interfaces;

namespace SpaceShip.Service.Services
{
    /// <inheritdoc/>
    public class ResourceService : IResourceService
    {
        #region private fields

        private readonly IResourceRepository _resourceRepository;

        private readonly ILogger<ResourceService> _logger;

        #endregion

        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ResourceService(
            IResourceRepository resourceRepository,
            ILoggerFactory loggerFactory)
        {
            _resourceRepository = resourceRepository;

            _logger = loggerFactory.CreateLogger<ResourceService>();
        }

        #endregion

        #region public methods

        /// <inheritdoc/>
        public ResourceType? GetRequiredResourceType(Resource resource)
        {
            if (resource.State != ResourceState.OK)
            {
                _logger.LogInformation(
                    "Resource {resourceName} of type {resourceType} is in {resourceState} state.",
                    resource.Name,
                    resource.ResourceType,
                    resource.State);

                return null;
            }

            if (resource.RequiredResourceType == null)
            {
                _logger.LogInformation(
                    "Resource {resourceName} of type {resourceType} is self-sufficient.",
                    resource.Name,
                    resource.ResourceType);

                return null;
            }

            return resource.RequiredResourceType;
        }

        /// <inheritdoc/>
        public int GetRequiredResourceAmount(Resource resource)
        {
            int requiredAmount = resource.Amount;

            _logger.LogInformation(
                "Resource {resourceName} of type {resourceType} requires {requiredAmount} resource(s) for lifesupport.",
                resource.Name,
                resource.ResourceType,
                requiredAmount);

            return requiredAmount;
        }

        /// <inheritdoc/>
        public void UpdateResourceState(Resource resource, ResourceState resourceState)
        {
            _logger.LogInformation(
                "Update resource {resourceName} of type {resourceType} state to {resourceState}.",
                resource.Name,
                resource.ResourceType,
                resourceState);

            resource.State = resourceState;
            _resourceRepository.Update(resource);
        }

        /// <inheritdoc/>
        public void UpdateResourceAmount(Resource resource, int amount)
        {
            _logger.LogInformation(
                "Update resource {resourceName} of type {resourceType} amount to {resourceAmount}.",
                resource.Name,
                resource.ResourceType,
                amount);

            resource.Amount = amount;
            _resourceRepository.Update(resource);
        }

        #endregion
    }
}
