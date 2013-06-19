using System.Threading.Tasks;

namespace Box.V2.Request
{
    public interface IRequestHandler
    {
        Task<IBoxResponse<T>> ExecuteAsync<T>(IBoxRequest request)
            where T : class;
    }
}
