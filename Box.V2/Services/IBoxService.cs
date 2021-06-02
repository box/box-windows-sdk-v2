using System.Threading.Tasks;

namespace Box.V2.Services
{
    public interface IBoxService
    {
        /// <summary>
        /// Executes the provided BoxRequest and returns a BoxResponse immediately on the thread pool, without retrying failed requests.
        /// </summary>
        /// <typeparam name="T">The return type of the response</typeparam>
        /// <param name="request">The Box Request to execute</param>
        /// <returns></returns>
        Task<IBoxResponse<T>> ToResponseAsyncWithoutRetry<T>(IBoxRequest request)
            where T : class;

        /// <summary>
        /// Executes the provided BoxRequest and returns a BoxResponse immedeately on the thread pool
        /// </summary>
        /// <typeparam name="T">The return type of the response</typeparam>
        /// <param name="request">The Box Request to execute</param>
        /// <returns></returns>
        Task<IBoxResponse<T>> ToResponseAsync<T>(IBoxRequest request)
            where T : class;

        /// <summary>
        /// Queues the BoxRequest and executes it as threads become available, returning a BoxResponse
        /// </summary>
        /// <typeparam name="T">The return type of the response</typeparam>
        /// <param name="request">The Box Request to execute</param>
        /// <returns></returns>
        Task<IBoxResponse<T>> EnqueueAsync<T>(IBoxRequest request)
            where T : class;
    }
}
