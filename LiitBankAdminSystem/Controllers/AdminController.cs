using LiitBankAdminSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiitBankAdminSystem.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly ApplicationDbContext _context;

        public AdminController(ILogger<AdminController> logger, ApplicationDbContext context, ApplicationDbContext appContext)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {

            return Content("index");
        }
        public IActionResult SearchUser()
        {

            return Content("searchUser");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AddUser()
        {
            //var claims = User.Claims.ToList();

            //User.IsInRole("Admin");

            return Content("addUser");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteUser()
        {

            return Content("deleteUser");
        }
    }
}
