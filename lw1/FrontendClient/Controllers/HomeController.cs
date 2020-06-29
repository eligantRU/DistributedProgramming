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
            using var channel = GrpcChannel.ForAddress("http://localhost:5000");
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
