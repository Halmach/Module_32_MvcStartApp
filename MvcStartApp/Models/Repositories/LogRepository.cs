using Microsoft.EntityFrameworkCore;
using MvcStartApp.Models.Db;
using System;
using System.Threading.Tasks;

namespace MvcStartApp.Models.Repositories
{
    public class LogRepository : ILogRepository
    {
        // ссылка на контекст
        private readonly BlogContext _context;

        public LogRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task AddLog(Request request)
        {
           request.Id = Guid.NewGuid();
           request.Date = DateTime.Now;

            // Добавление пользователя
            var entry = _context.Entry(request);
            if (entry.State == EntityState.Detached)
                await _context.Requests.AddAsync(request);

            // Сохранение изенений
            await _context.SaveChangesAsync();

        }

        public Task<Request[]> GetRequests()
        {
            throw new System.NotImplementedException();
        }
    }
}
