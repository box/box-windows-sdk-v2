using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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


        private static bool _isTaskRunning = false;

        public BoxService(IResponseParser parser, IRequestHandler handler)
        {
            TaskQueue = new ObservableCollection<IBoxRequest>();

            _parser = parser;
            _handler = handler;

            // This ensures that only one task is performed at a time 
            _scheduler = new LimitedConcurrencyLevelTaskScheduler(1);
            _factory = new TaskFactory(_scheduler);
        
        }

        public ObservableCollection<IBoxRequest> TaskQueue { get; set; }

        public async Task<IBoxResponse<T>> ToResponse<T>(IBoxRequest request)
        {
            IBoxResponse<T> response = await _handler.Execute<T>(request);

            switch (response.Status)
            {
                case ResponseStatus.Success:
                    response.BoxModel = _parser.Parse<T>(response.ContentString);
                    break;
                case ResponseStatus.Error:
                    response.Error = _parser.Parse<BoxError>(response.ContentString);
                    break;
            }

            return response;
        }

        public async Task<IBoxResponse<T>> Enqueue<T>(IBoxRequest request)
        {
            Task<IBoxResponse<T>> t = _factory.StartNew(async () => await ToResponse<T>(request)).Unwrap();
            return await t;
        }

    }
}
