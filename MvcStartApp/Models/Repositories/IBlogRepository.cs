using MvcStartApp.Models.Db;
using System.Threading.Tasks;

namespace MvcStartApp.Models.Repositories
{
    public interface IBlogRepository
    {
        Task AddUser(User user);
    }
}
