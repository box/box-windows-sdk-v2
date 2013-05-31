using Box.V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Services
{
    public interface IBoxService
    {
        Task<IBoxResponse<T>> ToResponseAsync<T>(IBoxRequest request)
            where T : class;

        Task<IBoxResponse<T>> EnqueueAsync<T>(IBoxRequest request)
            where T : class;
    }
}
