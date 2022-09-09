using Microsoft.AspNetCore.Mvc;
using System;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using LiitBankAdminSystem.Services;
using LiitBankAdminSystem.Areas.Identity.Data;

namespace LiitBankAdminSystem.Controllers
{
    [Route("Bank")]
    public class BankServiceController : Controller
    {
        private IBankService _bankService;
        private readonly ILogger<CashierController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;


        public BankServiceController(ILogger<CashierController> logger, UserManager<IdentityUser> userManager, ApplicationDbContext context, IBankService bankService)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
            _bankService = bankService;
        }


      
    }
}
