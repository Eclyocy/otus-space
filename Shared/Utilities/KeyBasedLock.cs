using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace Shared.Utilities
{
    /// <summary>
    /// Class supplying locking the specified key via a <see cref="SemaphoreSlim"/>,
    /// allowing only one object per a key to be processed at a time.
    /// </summary>
    /// <typeparam name="TKey">Type of the key to lock.</typeparam>
    public class KeyBasedLock<TKey>
        where TKey : notnull
    {
        #region private fields

        /// <summary>
        /// Semaphores for processing only one object per a key at a time.
        /// </summary>
        private readonly ConcurrentDictionary<TKey, SemaphoreSlim> _objects = new();

        #endregion

        #region public methods

        public async Task<IDisposable> LockAsync(TKey key, ILogger logger, CancellationToken cancellationToken)
        {
            SemaphoreSlim semaphore = _objects.GetOrAdd(key, x => new SemaphoreSlim(1, 1));

            await semaphore.WaitAsync(cancellationToken);

            logger.LogInformation("Semaphore acquired for key: {key}.", key);

            LogCurrentlyLockedKeys(logger);

            return new SemaphoreReleaser(semaphore, key, logger);
        }

        #endregion

        #region private methods

        /// <summary>
        /// Log information about currently locked objects.
        /// </summary>
        private void LogCurrentlyLockedKeys(ILogger logger)
        {
            IEnumerable<TKey> currentlyLockedKeys = _objects
                .Where(x => x.Value.CurrentCount == 0)
                .Select(x => x.Key);

            string currentlyLockedKeysRepresentation = currentlyLockedKeys.Any()
                ? string.Join(", ", currentlyLockedKeys)
                : "none";

            logger.LogInformation("Currently locked keys: {keys}.", currentlyLockedKeysRepresentation);
        }

        #endregion

        #region nested classes

        /// <summary>
        /// Disposable class allowing disposing of the <see cref="SemaphoreSlim"/>.
        /// </summary>
        private class SemaphoreReleaser(SemaphoreSlim semaphore, TKey key, ILogger logger)
            : IDisposable
        {

            /// <summary>
            /// Release semaphore on dispose.
            /// </summary>
            public void Dispose()
            {
                semaphore.Release();

                logger.LogInformation("Semaphore released for key: {key}.", key);
            }
        }

        #endregion
    }
}
