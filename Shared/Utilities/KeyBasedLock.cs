using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace Shared.Utilities
{
    /// <summary>
    /// Class supplying locking the specified key via a <see cref="SemaphoreSlim"/>,
    /// allowing only one object per a key to be processed at a time.
    /// </summary>
    /// <typeparam name="TKey">Type of the key to lock.</typeparam>
    /// <remarks>Remember to use as a static field.</remarks>
    public class KeyBasedLock<TKey>() : IDisposable
        where TKey : notnull
    {
        #region private fields

        /// <summary>
        /// Semaphores for processing only one object per a key at a time.
        /// </summary>
        private readonly ConcurrentDictionary<TKey, SemaphoreSlim> _semaphores = new();

        #endregion

        #region public methods

        public async Task<IDisposable> LockAsync(TKey key, ILogger logger, CancellationToken cancellationToken)
        {
            logger.LogInformation("Semaphore requested for key: {key}.", key);
            LogCurrentlyLockedKeys(logger);

            SemaphoreSlim semaphore = _semaphores.GetOrAdd(key, x => new SemaphoreSlim(1, 1));

            await semaphore.WaitAsync(cancellationToken);

            logger.LogInformation("Semaphore acquired for key: {key}.", key);
            LogCurrentlyLockedKeys(logger);

            return new SemaphoreReleaser(semaphore, key, logger);
        }

        public void Dispose()
        {
            foreach (SemaphoreSlim semaphore in _semaphores.Values)
            {
                semaphore.Dispose();
            }

            _semaphores.Clear();
        }

        #endregion

        #region private methods

        /// <summary>
        /// Log information about currently locked objects.
        /// </summary>
        private void LogCurrentlyLockedKeys(ILogger logger)
        {
            IEnumerable<TKey> currentlyLockedKeys = _semaphores
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
