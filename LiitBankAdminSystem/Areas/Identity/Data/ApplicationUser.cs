using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LiitBankAdminSystem.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    //identityuser application object, authinasierar hela applikationen.

    public string FirstName { get; set; }
    public string LastName { get; set; }

}

