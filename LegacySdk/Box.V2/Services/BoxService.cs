using System.Threading;
// using Nito.AsyncEx;
using System.Threading.Tasks;
using Box.V2.Request;

namespace Box.V2.Services
{
    public class BoxService : IBoxService
    {
        private const int NumberOfThreads = 2;
        private readonly IRequestHandler _handler;

        // Used to limit the number of requests that go out
        private readonly SemaphoreSlim _throttler = new SemaphoreSlim(NumberOfThreads);

        /// <summary>
        /// Instantiates a new BoxService with the provided handler
        /// </summary>
        /// <param name="handler"></param>
        public BoxService(IRequestHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// Executes the request according to the default TaskScheduler
        /// This will allow for concurrent requests and is managed by the thread pool
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IBoxResponse<T>> ToResponseAsyncWithoutRetry<T>(IBoxRequest request)
            where T : class
        {
            return await _handler.ExecuteAsyncWithoutRetry<T>(request).ConfigureAwait(false);
        }

        /// <summary>
        /// Executes the request according to the default TaskScheduler
        /// This will allow for concurrent requests and is managed by the thread pool
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IBoxResponse<T>> ToResponseAsync<T>(IBoxRequest request)
            where T : class
        {
            return await _handler.ExecuteAsync<T>(request).ConfigureAwait(false);
        }

        /// <summary>
        /// Executes the request but limits the number of threads that can be used 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IBoxResponse<T>> EnqueueAsync<T>(IBoxRequest request)
            where T : class
        {
            await _throttler.WaitAsync().ConfigureAwait(false);

            try
            {
                return await _handler.ExecuteAsync<T>(request).ConfigureAwait(false);
            }
            finally
            {
                _throttler.Release();
            }
        }
    }
}
