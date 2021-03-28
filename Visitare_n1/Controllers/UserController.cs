using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Visitare_n1.Models;


namespace Visitare_n1.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [ResponseType(typeof(Test1))]
            [Route("api/User_/ChangeAccountDetails")]
            public async Task<IHttpActionResult> PutTest12(string FirstName, string LastName)
            {

                string id = RequestContext.Principal.Identity.GetUserId();
                Test1 test1 = await db.Test1.FindAsync(id);
                

                test1.FirstName = FirstName;
                test1.LastName = LastName;


               

                db.Entry(test1).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Test1Exists(test1.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return Ok(test1);
            }

        [ResponseType(typeof(UserReturn))]
        [Route("api/User_/GetUserInfo")]
        public async Task<IHttpActionResult> GetUserInfo(string userName = null)
        {
            using (var context = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUserModel>(context);
                var userManager = new UserManager<ApplicationUserModel>(userStore);
                var roles = context.Roles.ToList();
                UserReturn userReturn = null;
                string id = null;
                Test1 user_ = null;
                if (userName == null)
                {
                    id = RequestContext.Principal.Identity.GetUserId();
                    user_ = await db.Test1.FindAsync(id);
                }
                else
                {
                    user_ =  db.Test1.Where(q => q.Nickname == userName).FirstOrDefault();

                    if (user_ == null)
                    {

                        return StatusCode(HttpStatusCode.NotFound);
                    }
                    id = user_.Id;
                    var userCurrent = await userManager.FindByIdAsync(RequestContext.Principal.Identity.GetUserId());
                    string roleId = roles.Where(q => q.Name == "Admin").FirstOrDefault().Id;
                    var userIsInRole = userCurrent.Roles.Where(q => q.RoleId == roleId && q.UserId == userCurrent.Id).FirstOrDefault();




                    if (userIsInRole == null)
                    {
                        return StatusCode(HttpStatusCode.Unauthorized);
                    }
                    //if (user_ == null)
                    //{
                    //    return null;
                    //}
                }

                var user = await userManager.FindByIdAsync(id);



                if (user_ == null)
                {

                    return StatusCode(HttpStatusCode.NotFound);
                }
                else
                {
                    userReturn = new UserReturn(user_);
                    foreach (var r in user.Roles)
                    {

                        userReturn.Roles.Add(roles.Where(q => q.Id == r.RoleId).FirstOrDefault().Name);


                    }


                }
                return Ok(userReturn); ;
            }



        }
        
        [Route("api/User_/GetAllUserInfo")]
        public async Task<List<UserReturn>> GetAllUserInfo()
        {
            List<Test1> users = db.Test1.ToList();
            List<UserReturn> output = new List<UserReturn>(); 
            using (var context = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUserModel>(context);
                var userManager = new UserManager<ApplicationUserModel>(userStore);
                var roles = context.Roles.ToList();
                foreach(var userLocal in users)
                {
                    UserReturn userReturn;
                    var user = await userManager.FindByIdAsync(userLocal.Id);
                    userReturn = new UserReturn(userLocal);
                    if (user != null)
                    {
                        
                        foreach (var r in user.Roles)
                        {

                            userReturn.Roles.Add(roles.Where(q => q.Id == r.RoleId).FirstOrDefault().Name);


                        }
                    }
                    
                    output.Add(userReturn);

                }
                
                
                

                



                
                
                
                return output;
            }



        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("api/User_/AddToRole")]
        public async Task<IHttpActionResult> AddToARole(string userName, string roleName)
        {
            using (var context = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUserModel>(context);
                var userManager = new UserManager<ApplicationUserModel>(userStore);
                var user = await userManager.FindByNameAsync(userName);
                var roles = context.Roles.ToList();
                if (user == null)
                {
                    return StatusCode(HttpStatusCode.NotFound);
                }
                //if (userManager.FindById(userId) == null)
                //{
                //    var r = userManager.IsInRole(user.Id, roleName);

                //}
                //bool roleExist = roles.Where(q => q.Name == roleName).FirstOrDefault().Name != roleName;
                var roleCurrent = roles.Where(q => q.Name == roleName).FirstOrDefault();
                if (roleCurrent == null)
                {
                    return StatusCode(HttpStatusCode.NotFound);
                }
                string roleId = roleCurrent.Id;
                var userIsInRole = user.Roles.Where(q => q.RoleId == roleId && q.UserId == user.Id).FirstOrDefault();
                if (roleId == null)
                {
                    return StatusCode(HttpStatusCode.NotFound);
                }
                if (userIsInRole != null)
                {
                    return StatusCode(HttpStatusCode.Conflict);
                }



                await userManager.AddToRoleAsync(user.Id, roleName);
                return StatusCode(HttpStatusCode.Created);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("api/User_/RemoveFromRole")]
        public async Task<IHttpActionResult> RemoveFromRole(string userName, string roleName)
        {
            using (var context = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUserModel>(context);
                var userManager = new UserManager<ApplicationUserModel>(userStore);
                var user = await userManager.FindByNameAsync(userName);
                var roles = context.Roles.ToList();
                if (user == null)
                {
                    return StatusCode(HttpStatusCode.NotFound);
                }
                //if (userManager.FindById(userId) == null)
                //{
                //    var r = userManager.IsInRole(user.Id, roleName);

                //}
                //bool roleExist = roles.Where(q => q.Name == roleName).FirstOrDefault().Name != roleName;
                var roleCurrent = roles.Where(q => q.Name == roleName).FirstOrDefault();
                if (roleCurrent == null)
                {
                    return StatusCode(HttpStatusCode.NotFound);
                }
                string roleId = roleCurrent.Id;
                var userIsInRole = user.Roles.Where(q => q.RoleId == roleId && q.UserId == user.Id).FirstOrDefault();
                if (user == null || roleId == null || userIsInRole == null)
                {
                    return StatusCode(HttpStatusCode.NotFound);
                }




                await userManager.RemoveFromRoleAsync(user.Id, roleName);
                return StatusCode(HttpStatusCode.OK);
            }
        }
        private bool Test1Exists(string id)
        {
            return db.Test1.Count(e => e.Id == id) > 0;
        }
        //[HttpPost]
        //[Authorize(Roles = "Admin")]
        //[Route("api/User/Admin/AddToRole")]
        //public void AddToARole(string userId, string roleName)
        //{
        //    using (var context = new ApplicationDbContext())
        //    {
        //        var userStore = new UserStore<ApplicationUserModel>(context);
        //        var userManager = new UserManager<ApplicationUserModel>(userStore);
        //        if (userManager.FindById(userId) == null)
        //        {
        //            return;
        //        }



        //        userManager.AddToRole(userId, roleName);
        //    }
        //}
        //  [Authorize(Roles = "Admin")]
        //[HttpPost]
        //[Route("api/User/Admin/RemoveToRole")]
        //public void RemoveToARole(string userId, string roleName)
        //{
        //    using (var context = new ApplicationDbContext())
        //    {
        //        var userStore = new UserStore<ApplicationUserModel>(context);
        //        var userManager = new UserManager<ApplicationUserModel>(userStore);
        //        if (userManager.FindById(userId) == null)
        //        {
        //            return;
        //        }

        //        userManager.RemoveFromRole(userId, roleName);
        //    }
        //}
    }
}