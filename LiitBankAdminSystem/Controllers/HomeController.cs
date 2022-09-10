using LiitBankAdminSystem.Areas.Identity.Data;
using LiitBankAdminSystem.Models;
using LiitBankAdminSystem.Services;
using LiitBankAdminSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LiitBankAdminSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBankService _bankService;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IBankService bankService)
        {
            _logger = logger;
            _context = context;
            _bankService = bankService;
            _context = context;
        }
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any)]
        public IActionResult Index()
        {
            var viewModel = new HomeIndexViewModel
            {
                SumCustomerCount = _context.Customers.Count(),
                SumAccountCount = _context.Accounts.Count(),
                SumAccountBalance = _context.Accounts.Sum(b => b.Balance)
            };


            return View(viewModel);
        }

        public IActionResult Privacy()
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