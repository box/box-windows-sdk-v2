using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Services
{
    public interface IBoxService
    {
        Task<IBoxResponse<T>> ToResponse<T>(IBoxRequest request);
    }
}
