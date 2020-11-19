using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[Action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }


        /*
         * https://github.com/davidfowl/AspNetCoreDiagnosticScenarios/blob/master/AspNetCoreGuidance.md
         * https://github.com/davidfowl/AspNetCoreDiagnosticScenarios/blob/master/AsyncGuidance.md
             服务器实现Stream具有同步和异步过载的接口。应该首选异步线程，以避免阻塞线程池线程
             优先使用HttpRequest.ReadAsFormAsync（）而不是HttpRequest.Form
             不要在字段中存储IHttpContextAccessor.HttpContext
             不要从多个线程并行访问HttpContext。它不是线程安全的。
             请求完成后不要使用HttpContext
             不要在后台线程中捕获HttpContext
             不要捕获在后台线程中注入控制器的服务
             避免在HttpResponse启动后添加标题

             避免将Task.Run用于阻塞线程的长时间运行的工作
             如果您阻塞线程，则线程池会增加，但是这样做是不好的做法。
             避免使用Task.Result和Task.Wait
             
         */
        private static List<Person> _db =new List<Person>(){ new Person() };
        private static ConcurrentDictionary<int, Task<Person>> _cache = new ConcurrentDictionary<int, Task<Person>>();
        //private static ConcurrentDictionary<int, AsyncLazy<Person>> _cache = new ConcurrentDictionary<int, AsyncLazy<Person>>();


        [HttpGet]
        public IActionResult GetTime()
        {
            string ip = HttpContext.Connection.RemoteIpAddress.ToString();

            DateTime dateTime = DateTime.Now;
            DateTimeOffset dateTimeOffset =DateTimeOffset.Now;
            _ = LogA(ip).ConfigureAwait(false);
            JsonResult jsonResult = new JsonResult(new { DateTime = dateTime, DateTimeOffset = dateTimeOffset });
            jsonResult.StatusCode = 200;
            return jsonResult;
        }
        private static async Task LogA(string ip)
        {
            //这个地方的await不会影响调用这个方法的阻塞
            await LogB(ip);
            await Task.Delay(1000);
            Console.WriteLine("异步A:"+ip);
        }

        private static async Task LogB(string ip)
        {
            await Task.Delay(10000);
            Console.WriteLine("异步B:"+ip);
        }
        private static void LogC(string ip)
        {
            Thread.Sleep(10000);
            Console.WriteLine("同步:"+ip);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            //var person = await _cache.GetOrAdd();
            //var person = await _cache.GetOrAdd(id, (key) => new AsyncLazy<Person>(() => _db.People.FindAsync(key))).Value;
            return Ok("");
        }
        private class AsyncLazy<T> : Lazy<Task<T>>
        {
            public AsyncLazy(Func<Task<T>> valueFactory) : base(valueFactory)
            {
            }
        }
        private class Person
        {
        }
    }
}
