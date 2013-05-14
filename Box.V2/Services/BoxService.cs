using Box.V2.Web;
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
        private static bool _isTaskRunning = false;

        public BoxService(IResponseParser parser, IRequestHandler handler)
        {
            TaskQueue = new ObservableCollection<IBoxRequest>();
            
            _parser = parser;
            _handler = handler;

            //Observable.FromEventPattern<NotifyCollectionChangedEventArgs>(TaskQueue, "CollectionChanged")
            //    .Select(c => c.EventArgs).Subscribe(ea => {
            //        switch (ea.Action)
            //        {
            //            case NotifyCollectionChangedAction.Add:
            //            case NotifyCollectionChangedAction.Remove:
            //                lock (_lock)
            //                {
            //                    if (!_isTaskRunning && TaskQueue.Count > 0)
            //                    {
            //                        ToResponse(TaskQueue[0]);
            //                    }
            //                }
            //                break;
            //            default:
            //                // Not interested in other actions
            //                break;
            //        }
            //    });
        }


        //LimitedConcurrencyLevelTaskScheduler scheduler = new LimitedConcurrencyLevelTaskScheduler(5);
        //TaskFactory factory = new TaskFactory(scheduler);

        public static ObservableCollection<IBoxRequest> TaskQueue { get; set; }

        public async Task<T> ToResponse<T>(IBoxRequest request)
        {
            string response = await _handler.Execute(request);
            return _parser.Parse<T>(response);
        }

        //public async Task<T> EnqueueTask<T>(IBoxRequest request)
        //{
        //    lock (_lock)
        //    {
        //        TaskQueue.Add(request);
        //    }
        //}
    }
}
