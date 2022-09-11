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
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
           
            _context = context;
        }
        //[ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any)]
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