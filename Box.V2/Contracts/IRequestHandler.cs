using Box.V2.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Services
{
    public interface IRequestHandler
    {
        Task<string> Execute(IBoxRequest request);
    }
}
