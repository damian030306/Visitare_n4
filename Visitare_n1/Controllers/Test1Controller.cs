using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Visitare_n1.Models;

namespace Visitare_n1.Controllers
{

    public class UserRolePairModel
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
    }
    //  [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class userB
        {
            public string Email { get; set; }
            public string Id { get; set; }
            public int Points { get; set; }
        }
        public class TestZ
        {
            public string Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Nickname { get; set; }
            public int Punkty { get; set; }
            public List<string> Roles { get; set; }
        }
       [Authorize]
        public class Test1Controller : ApiController
        {
            public void AddARole2(UserRolePairModel pairing)
            {
                using (var context = new ApplicationDbContext())
                {
                    var userStore = new UserStore<ApplicationUserModel>(context);
                    var userManager = new UserManager<ApplicationUserModel>(userStore);

                    userManager.AddToRole(pairing.UserId, pairing.RoleName);
                }
            }
            private ApplicationDbContext db = new ApplicationDbContext();


            // GET: api/Test1
            [Route("api/Rewards")]
            public IQueryable<Test1> GetAll()
            {
                return db.Test1;
            }
            [ResponseType(typeof(Test1))]
            [Route("api/Rewards/GetMine/{id}")]
            public async Task<IHttpActionResult> GetTest1(string id)
            {


                Test1 test1 = await db.Test1.FindAsync(id);
                if (test1 == null)
                {
                    test1 = new Test1();
                    test1.Id = RequestContext.Principal.Identity.GetUserId();
                    test1.Punkty = 10;
                    test1.Nickname = RequestContext.Principal.Identity.GetUserName();

                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }





                    db.Test1.Add(test1);

                    try
                    {
                        await db.SaveChangesAsync();
                    }
                    catch (DbUpdateException)
                    {
                        if (Test1Exists(test1.Id))
                        {
                            return Conflict();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

                return Ok(test1);
            }
            // GET: api/Test1/5
            [ResponseType(typeof(Test1))]
            [Route("api/Rewards/GetMine")]
            public async Task<TestZ> GetTest1()
            {
                using (var context = new ApplicationDbContext())
                {
                    var userStore = new UserStore<ApplicationUserModel>(context);
                    var userManager = new UserManager<ApplicationUserModel>(userStore);



                    TestZ Output = new TestZ();
                    string id = RequestContext.Principal.Identity.GetUserId();
                    Test1 test1 = await db.Test1.FindAsync(id);
                    var user = userManager.FindById(id);
                    var roles = context.Roles.ToList();
                    if (test1 == null)
                    {
                        test1 = new Test1();
                        test1.Id = RequestContext.Principal.Identity.GetUserId();
                        test1.Punkty = 10;
                        test1.Nickname = RequestContext.Principal.Identity.GetUserName();


                        if (!ModelState.IsValid)
                        {
                            return null;
                        }





                        db.Test1.Add(test1);

                        try
                        {
                            await db.SaveChangesAsync();
                        }
                        catch (DbUpdateException)
                        {
                            if (Test1Exists(test1.Id))
                            {
                                return null;
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }
                    Output.LastName = test1.LastName;
                    Output.FirstName = test1.FirstName;
                    Output.Id = test1.Id;
                    Output.Punkty = test1.Punkty;
                    Output.Nickname = test1.Nickname;
                    Output.Roles = new List<string>();
                if(user.Roles == null)
                {

                }
                else
                {
                    foreach (var r in user.Roles)
                    {

                        Output.Roles.Add(roles.Where(x => x.Id == r.RoleId).First().Name);
                    }
                }
                   

                    return Output;
                }
            }
            [ResponseType(typeof(Test1))]
            [Route("api/Rewards/Get10")]
            public async Task<IHttpActionResult> Get10()
            {

                string id = RequestContext.Principal.Identity.GetUserId();
                Test1 test1 = await db.Test1.FindAsync(id);
                if (test1 == null)
                {
                    test1 = new Test1();
                    test1.Id = RequestContext.Principal.Identity.GetUserId();
                    test1.Punkty = 10;
                    test1.Nickname = RequestContext.Principal.Identity.GetUserName();

                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }





                    db.Test1.Add(test1);

                    try
                    {
                        await db.SaveChangesAsync();
                    }
                    catch (DbUpdateException)
                    {
                        if (Test1Exists(test1.Id))
                        {
                            return Conflict();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                Test1 test1Get19 = await db.Test1.FindAsync(id);
                if (test1.Punkty >= 490 && test1.Punkty < 500)
                {
                    UserRolePairModel userRolePairModel = new UserRolePairModel();
                    userRolePairModel.UserId = test1.Id;
                    userRolePairModel.RoleName = "Creator";
                    AddARole2(userRolePairModel);
                }
                test1.Punkty = test1Get19.Punkty + 10;


                test1.Id = RequestContext.Principal.Identity.GetUserId();

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
            [ResponseType(typeof(Test1))]
            [Route("api/Rewards/Get15")]
            public async Task<IHttpActionResult> Get15()
            {



                string id = RequestContext.Principal.Identity.GetUserId();
                Test1 test1 = await db.Test1.FindAsync(id);
                if (test1 == null)
                {
                    test1 = new Test1();
                    test1.Id = RequestContext.Principal.Identity.GetUserId();
                    test1.Punkty = 10;
                    test1.Nickname = RequestContext.Principal.Identity.GetUserName();

                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }





                    db.Test1.Add(test1);

                    try
                    {
                        await db.SaveChangesAsync();
                    }
                    catch (DbUpdateException)
                    {
                        if (Test1Exists(test1.Id))
                        {
                            return Conflict();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                Test1 test1Get19 = await db.Test1.FindAsync(id);
                if (test1.Punkty >= 485 && test1.Punkty < 500)
                {
                    UserRolePairModel userRolePairModel = new UserRolePairModel();
                    userRolePairModel.UserId = test1.Id;
                    userRolePairModel.RoleName = "Creator";
                    AddARole2(userRolePairModel);
                }
                test1.Punkty = test1Get19.Punkty + 15;


                test1.Id = RequestContext.Principal.Identity.GetUserId();

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
            [ResponseType(typeof(Test1))]
            [Route("api/Rewards/Get50")]
            public async Task<IHttpActionResult> Get50()
            {


                string id = RequestContext.Principal.Identity.GetUserId();
                Test1 test1 = await db.Test1.FindAsync(id);
                if (test1 == null)
                {
                    test1 = new Test1();
                    test1.Id = RequestContext.Principal.Identity.GetUserId();
                    test1.Punkty = 10;
                    test1.Nickname = RequestContext.Principal.Identity.GetUserName();

                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }





                    db.Test1.Add(test1);

                    try
                    {
                        await db.SaveChangesAsync();
                    }
                    catch (DbUpdateException)
                    {
                        if (Test1Exists(test1.Id))
                        {
                            return Conflict();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                Test1 test1Get19 = await db.Test1.FindAsync(id);
                if (test1.Punkty >= 440 && test1.Punkty < 500)
                {
                    UserRolePairModel userRolePairModel = new UserRolePairModel();
                    userRolePairModel.UserId = test1.Id;
                    userRolePairModel.RoleName = "Creator";
                    AddARole2(userRolePairModel);
                }
                test1.Punkty = test1Get19.Punkty + 50;


                test1.Id = RequestContext.Principal.Identity.GetUserId();

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
        [AllowAnonymous]
            [HttpGet]
            [Route("api/Rewards/GetAll")]
            public async Task<List<TestZ>> GetAllUsers()
            {
                List<TestZ> output = new List<TestZ>();

                using (var context = new ApplicationDbContext())
                {
                    var userStore = new UserStore<ApplicationUserModel>(context);
                    var userManager = new UserManager<ApplicationUserModel>(userStore);


                    var users = userManager.Users.ToList();
                    var roles = context.Roles.ToList();
                    // var rewards = db.Test1.ToList();

                    foreach (var user in users)
                    {

                        string Id = user.Id;
                        TestZ testZ = new TestZ();
                        testZ.Roles = new List<string>();
                        Test1 test1 = db.Test1.Find(Id);
                        if (test1 == null)
                        {
                            test1 = new Test1();
                            test1.Id = Id;
                            test1.Punkty = 10;
                            test1.Nickname = user.UserName;

                            if (!ModelState.IsValid)
                            {
                                return null;
                            }





                            db.Test1.Add(test1);

                            try
                            {
                                await db.SaveChangesAsync();
                            }
                            catch (DbUpdateException)
                            {
                                if (Test1Exists(test1.Id))
                                {
                                    return null;
                                }
                                else
                                {
                                    throw;
                                }
                            }
                            testZ.Nickname = test1.Nickname;
                            testZ.Id = test1.Id;
                            testZ.LastName = test1.LastName;
                            testZ.Punkty = test1.Punkty;
                            testZ.FirstName = test1.FirstName;

                            foreach (var r in user.Roles)
                            {

                                testZ.Roles.Add(roles.Where(x => x.Id == r.RoleId).First().Name);
                            }
                            // continue;
                        }
                        else
                        {
                            testZ.Nickname = test1.Nickname;
                            testZ.Id = test1.Id;
                            testZ.LastName = test1.LastName;
                            testZ.Punkty = test1.Punkty;
                            testZ.FirstName = test1.FirstName;
                            foreach (var r in user.Roles)
                            {

                                testZ.Roles.Add(roles.Where(x => x.Id == r.RoleId).First().Name);
                            }

                        }

                        // u.Roles.Add(r.Id, roles.Where(x => x.Id == r.RoleId).First().Name);





                        output.Add(testZ);
                    }
                }

                return output;
            }
            //// PUT: api/Test1/5
            //[Route("api/Rewards")]
            //[ResponseType(typeof(void))]
            //public async Task<IHttpActionResult> PutTest1(Test1 test1)
            //{
            //    if (!ModelState.IsValid)
            //    {
            //        return BadRequest(ModelState);
            //    }

            //    test1.Id = RequestContext.Principal.Identity.GetUserId();

            //    db.Entry(test1).State = EntityState.Modified;

            //    try
            //    {
            //        await db.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!Test1Exists(test1.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }

            //    return StatusCode(HttpStatusCode.NoContent);
            //}

            [ResponseType(typeof(Test1))]
            [Route("api/Rewards/ChangeName")]
            public async Task<IHttpActionResult> PutTest12(string FirstName, string LastName)
            {

                string id = RequestContext.Principal.Identity.GetUserId();
                Test1 test1 = await db.Test1.FindAsync(id);
                if (test1 == null)
                {
                    test1 = new Test1();
                    test1.Id = RequestContext.Principal.Identity.GetUserId();
                    test1.Punkty = 10;
                    test1.Nickname = RequestContext.Principal.Identity.GetUserName();
                    test1.FirstName = FirstName;
                    test1.LastName = LastName;

                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }





                    db.Test1.Add(test1);

                    try
                    {
                        await db.SaveChangesAsync();
                    }
                    catch (DbUpdateException)
                    {
                        if (Test1Exists(test1.Id))
                        {
                            return Conflict();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

                test1.FirstName = FirstName;
                test1.LastName = LastName;


                test1.Id = RequestContext.Principal.Identity.GetUserId();

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
        // POST: api/Test1
        // [Route("api/Rewards")]
        //[ResponseType(typeof(Test1))]
        //public async Task<IHttpActionResult> PostTest1(Test1 test1)
        //{
        //    test1 = new Test1();
        //    test1.Id = RequestContext.Principal.Identity.GetUserId();
        //    test1.Punkty = 10;
        //    test1.Nickname = RequestContext.Principal.Identity.Name;

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }





        //    db.Test1.Add(test1);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (Test1Exists(test1.Id))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = test1.Id }, test1);
        //}
        // [Route("api/Rewards")]
        // DELETE: api/Test1
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(Test1))]
            public async Task<IHttpActionResult> DeleteTest1()
            {
                string id = RequestContext.Principal.Identity.GetUserId();
                Test1 test1 = await db.Test1.FindAsync(id);
                if (test1 == null)
                {
                    return NotFound();
                }

                db.Test1.Remove(test1);
                await db.SaveChangesAsync();

                return Ok(test1);
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                base.Dispose(disposing);
            }

            private bool Test1Exists(string id)
            {
                return db.Test1.Count(e => e.Id == id) > 0;
            }
        }
    
}