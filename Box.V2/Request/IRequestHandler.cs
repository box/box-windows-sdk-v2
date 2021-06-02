using System.Threading.Tasks;

namespace Box.V2.Request
{
    public interface IRequestHandler
    {

        /// <summary>
        /// Executes the BoxRequest, without retrying failed requests
        /// </summary>
        /// <typeparam name="T">The return type</typeparam>
        /// <param name="request">The box request to execute</param>
        /// <returns>A BoxResponse</returns>
        Task<IBoxResponse<T>> ExecuteAsyncWithoutRetry<T>(IBoxRequest request)
            where T : class;

        /// <summary>
        /// Executes the BoxRequest
        /// </summary>
        /// <typeparam name="T">The return type</typeparam>
        /// <param name="request">The box request to execute</param>
        /// <returns>A BoxResponse</returns>
        Task<IBoxResponse<T>> ExecuteAsync<T>(IBoxRequest request)
            where T : class;
    }
}
