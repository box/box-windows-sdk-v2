using Box.V2.Exceptions;
using Box.V2.Models;
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
        private IResponseParser _parser;
        private IRequestHandler _handler;
        private static object _lock = new object();

        LimitedConcurrencyLevelTaskScheduler _scheduler;
        TaskFactory _factory;

        public BoxService(IResponseParser parser, IRequestHandler handler)
        {
            _parser = parser;
            _handler = handler;

            // This ensures that only one task is executed at a time 
            _scheduler = new LimitedConcurrencyLevelTaskScheduler(1);
            _factory = new TaskFactory(_scheduler);
        
        }

        public async Task<IBoxResponse<T>> ToResponseAsync<T>(IBoxRequest request) 
        {
            IBoxResponse<T> response = await _handler.ExecuteAsync<T>(request);

            switch (response.Status)
            {
                case ResponseStatus.Success:
                    if (!string.IsNullOrWhiteSpace(response.ContentString))
                        response.ResponseObject = _parser.Parse<T>(response.ContentString);
                    break;
                case ResponseStatus.Error:
                    response.Error = _parser.Parse<BoxError>(response.ContentString);
                    throw new BoxException(string.Format("{0}: {1}", response.Error.Name, response.Error.Description));
            }

            return response;
        }

        public async Task<IBoxResponse<T>> EnqueueAsync<T>(IBoxRequest request)
        {
            Task<IBoxResponse<T>> t = _factory.StartNew(async () => await ToResponseAsync<T>(request)).Unwrap();
            return await t;
        }

    }
}
