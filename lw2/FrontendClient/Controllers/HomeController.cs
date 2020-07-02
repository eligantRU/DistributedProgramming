using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FrontendClient.Models;
using Grpc.Net.Client;
using BackendApi;
using Grpc.Core;

namespace FrontendClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormSubmit(String description)
        {
	    if (description == null)
	    {
	        return View("Error", new ErrorViewModel());
	    }

            string backendProtocol = Environment.GetEnvironmentVariable("BACKEND_PROTOCOL");
            string backendHost = Environment.GetEnvironmentVariable("BACKEND_HOST");
            string backendPort = Environment.GetEnvironmentVariable("BACKEND_PORT");
            using var channel = GrpcChannel.ForAddress(string.Format("{0}://{1}:{2}", backendProtocol, backendHost, backendPort));
            var client = new Job.JobClient(channel);
            var response = await client.RegisterAsync(new RegisterRequest { Description = description });            
            return View("Task", new TaskViewModel { Id = response.Id });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
