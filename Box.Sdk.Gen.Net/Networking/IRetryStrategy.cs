namespace Box.Sdk.Gen
{
    /// <summary>
    /// Retry strategy used when retrying HTTP request.
    /// </summary>
    public interface IRetryStrategy
    {
        /// <summary>
        /// Returns the time interval after which to retry the request.
        /// </summary>
        /// <param name="attemptNumber">Attempt number</param>
        /// <returns>Retry interval in seconds</returns>
        int GetRetryTimeout(int attemptNumber);
    }
}
