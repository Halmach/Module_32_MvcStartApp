using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcStartApp.Models.Db;
using MvcStartApp.Models.Repositories;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MvcStartApp.Middlewares
{
    public class LoggingMiddleware : Controller
    {
        private readonly RequestDelegate _next;
        // ссылка на репозиторий
        private  ILogRepository _repo;

        /// <summary>
        ///  Middleware-компонент должен иметь конструктор, принимающий RequestDelegate
        /// </summary>
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
            
        }

        [HttpGet]
        public async Task<IActionResult> Index(ILogRepository _repo)
        {
            this._repo = _repo;
            var logs = await _repo.GetRequests();
            return View(logs);
        }

        

        /// <summary>
        ///  Необходимо реализовать метод Invoke  или InvokeAsync
        /// </summary>
        public async Task InvokeAsync(HttpContext context, ILogRepository _repo)
        {
            this._repo = _repo;
            // логирование добавлено
            // Для логирования данных о запросе используем свойста объекта HttpContext
            Console.WriteLine($"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}");
            // Строка для публикации в лог
            string logMessage = $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}{Environment.NewLine}";
            // Путь до лога (опять-таки, используем свойства IWebHostEnvironment)
            //  string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "RequestLog.txt");
            // Используем асинхронную запись в файл
            //   await File.AppendAllTextAsync(logFilePath, logMessage);

            var log = new Request()
            {
                Url = $"http://{context.Request.Host.Value + context.Request.Path}"
            };

            await _repo.AddLog(log);
            //Передача запроса далее по конвейеру
            await _next.Invoke(context);
        }
    }
}
