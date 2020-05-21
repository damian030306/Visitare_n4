using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

using Visitare_n1.Models;


namespace Visitare_n1.Controllers
{
    public class UserController : ApiController
    {
        [HttpPost]
        [Route("api/User/Admin/AddToRole")]
        public void AddToARole(string userId, string roleName)
        {
            using (var context = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUserModel>(context);
                var userManager = new UserManager<ApplicationUserModel>(userStore);
                if (userManager.FindById(userId) == null)
                {
                    return;
                }



                userManager.AddToRole(userId, roleName);
            }
        }
        //  [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("api/User/Admin/RemoveToRole")]
        public void RemoveToARole(string userId, string roleName)
        {
            using (var context = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUserModel>(context);
                var userManager = new UserManager<ApplicationUserModel>(userStore);
                if (userManager.FindById(userId) == null)
                {
                    return;
                }

                userManager.RemoveFromRole(userId, roleName);
            }
        }
    }
}