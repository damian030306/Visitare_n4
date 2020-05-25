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
using System.Web.Security;
using Visitare_n1.Models;

namespace Visitare_n1.Controllers
{
    [Authorize]
    public class RoutesController : ApiController
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Routes

        public IQueryable<Route> GetRoutes()
        {
            return db.Routes;
        }
        [Route("api/Routes/Search")]
        public IQueryable<Route> GetPoints(string word)
        {

            return db.Routes.Where(q => q.Name == word);


        }
        [Route("api/Routes/GetMine")]
        public IQueryable<Route> GetPoints2()
        {
            string id = RequestContext.Principal.Identity.GetUserId(); ;
            return db.Routes.Where(q => q.UserId == id);


        }
        // GET: api/Routes/5

        [ResponseType(typeof(Route))]
        public async Task<IHttpActionResult> GetRoute(int id)
        {
            Route route = await db.Routes.FindAsync(id);
            if (route == null)
            {
                return NotFound();
            }

            return Ok(route);
        }
        [Route("api/Routes/Name/{name}")]
        public IQueryable<Route> GetRoutes(string name)
        {
            IQueryable<Route> routes;

            routes = db.Routes.Where(q => q.Name == name);


            return routes;

        }

        [Route("api/Routes/GetMine/Search")]
        public IQueryable<Route> GetPoints2(string word)
        {
            string id = RequestContext.Principal.Identity.GetUserId(); ;
            return db.Routes.Where(q => q.UserId == id && q.Name == word);


        }
        // PUT: api/Routes/5
        [Authorize(Roles = "Creator")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRoute(int id, Route route)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != route.Id)
            {
                return BadRequest();
            }

            route.UserId = RequestContext.Principal.Identity.GetUserId();
            route.UserName = RequestContext.Principal.Identity.GetUserName();
            db.Entry(route).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RouteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        [Authorize(Roles = "Creator")]
        [Route("api/Routes/AddImageUrl/{id}")]
        [ResponseType(typeof(Route))]
        public async Task<IHttpActionResult> PutRoute(int id, string ImageUrl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Route route = await db.Routes.FindAsync(id);
            if (route == null)
            {
                return NotFound();
            }
            if (id != route.Id)
            {
                return BadRequest();
            }
            route.ImageUrl = ImageUrl;

            route.UserId = RequestContext.Principal.Identity.GetUserId();
            route.UserName = RequestContext.Principal.Identity.GetUserName();
            db.Entry(route).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RouteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(route);
        }
        // POST: api/Routes
        [Authorize(Roles = "Creator")]
        [ResponseType(typeof(Route))]
        public async Task<IHttpActionResult> PostRoute(Route route)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            route.UserId = RequestContext.Principal.Identity.GetUserId();
            route.UserName = RequestContext.Principal.Identity.GetUserName();
            db.Routes.Add(route);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = route.Id }, route);
        }

        // DELETE: api/Routes/5
        [AllowAnonymous]
        [Authorize(Roles = "Creator")]
        [ResponseType(typeof(Route))]
        public async Task<IHttpActionResult> DeleteRoute(int id)
        {
            string idU = RequestContext.Principal.Identity.GetUserId();
            Route route = await db.Routes.FindAsync(id);
            if (route == null )
            {
                return NotFound();
            }
            using (var context = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUserModel>(context);
                var userManager = new UserManager<ApplicationUserModel>(userStore);


               Task<bool> checkUser =   userManager.IsInRoleAsync(RequestContext.Principal.Identity.GetUserId(), "Admin");


                if ( id == 1)
                //if (route.UserId != idU || checkUser.Equals(false)  || id == 1)
                {
                return Unauthorized();
            }
            }
            db.Routes.Remove(route);
            await db.SaveChangesAsync();

            return Ok(route);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RouteExists(int id)
        {
            return db.Routes.Count(e => e.Id == id) > 0;
        }
    }
}