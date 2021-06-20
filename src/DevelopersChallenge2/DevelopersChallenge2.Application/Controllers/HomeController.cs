using DevelopersChallenge2.Application.Domain.Interfaces;
using DevelopersChallenge2.Application.Models;
using DevelopersChallenge2.Application.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DevelopersChallenge2.Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOfxService _ofxService;
        private readonly IConfiguration _config;
        private readonly ITransactionRepository _transactionRepository;

        public HomeController(ILogger<HomeController> logger, IOfxService service, 
            IConfiguration configuration, ITransactionRepository transactionRepository)
        {
            _logger = logger;
            _ofxService = service;
            _config = configuration;
            _transactionRepository = transactionRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles(List<IFormFile> FormFiles)
        {
            //TODO Validate files
            await _ofxService.ProcessOfxFiles(FormFiles);

            return RedirectToAction("Transactions", "Home");
        }

        public async Task<IActionResult> TransactionsAsync()
        {
            var transactions = await _transactionRepository.GetTransactionsWithoutDuplicates();
            return View(transactions);
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
