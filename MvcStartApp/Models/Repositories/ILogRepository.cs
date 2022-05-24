using MvcStartApp.Models.Db;
using System.Threading.Tasks;

namespace MvcStartApp.Models.Repositories
{
    public interface ILogRepository
    {
        Task AddLog(Request request);
        Task<Request[]> GetRequests();
    }
}
