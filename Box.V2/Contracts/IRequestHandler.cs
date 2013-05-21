using Box.V2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Services
{
    public interface IRequestHandler
    {
        Task<IBoxResponse<T>> ExecuteAsync<T>(IBoxRequest request);
    }
}
