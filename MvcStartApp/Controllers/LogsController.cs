using Microsoft.AspNetCore.Mvc;
using MvcStartApp.Models.Repositories;
using System.Threading.Tasks;

namespace MvcStartApp.Controllers
{
    public class LogsController : Controller
    {
        private readonly ILogRepository _repo;


        public LogsController(ILogRepository repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            var logs = await _repo.GetRequests();
            return View(logs);
        }
    }
}
