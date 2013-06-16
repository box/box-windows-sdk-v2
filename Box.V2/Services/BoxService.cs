using Box.V2.Auth;
using Box.V2.Exceptions;
using Box.V2.Models;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Services
{
    public class BoxService : IBoxService
    {
        private IRequestHandler _handler;

        AsyncSemaphore _throttler = new AsyncSemaphore(2);

        LimitedConcurrencyLevelTaskScheduler _scheduler;

        TaskFactory _factory;

        public BoxService(IRequestHandler handler)
        {
            _handler = handler;

            // This ensures that only one task is executed at a time 
            _scheduler = new LimitedConcurrencyLevelTaskScheduler(1);
            _factory = new TaskFactory(_scheduler);

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
            return await _handler.ExecuteAsync<T>(request);
        }

        /// <summary>
        /// Executes the request using the LimitedConcurrencyLevelTaskScheduler
        /// This will force the tasks in the queue to be executed sequentially
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        //public async Task<IBoxResponse<T>> EnqueueAsync<T>(IBoxRequest request)
        //    where T : class
        //{
        //    Task<IBoxResponse<T>> t = _factory.StartNew(async () => await ToResponseAsync<T>(request)).Unwrap();
        //    return await t;
        //}


        public async Task<IBoxResponse<T>> EnqueueAsync<T>(IBoxRequest request)
            where T : class
        {
            await _throttler.WaitAsync();

            try
            {
                return await _handler.ExecuteAsync<T>(request);
            }
            finally
            {
                _throttler.Release();
            }
        }
    }
}
