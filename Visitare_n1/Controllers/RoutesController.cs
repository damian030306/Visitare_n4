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
    /// <summary>
    /// fvsdfsdf
    /// </summary>
    [Authorize]
    public class RoutesController : ApiController
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Routes

        //public IQueryable<Route> GetRoutes()
        //{
        //    return db.Routes;
        //}
        //[Route("api/Routes/Search")]
        //public IQueryable<Route> GetPoints(string word)
        //{

        //    return db.Routes.Where(q => q.Name == word);


        //}
        /// <summary>
        /// metoda GET służąca do zwracania listy tras stworzonych przez obecnego użytkownika
        /// </summary>
        /// <returns></returns>
        [Route("api/Routes/GetMine")]
        public IQueryable<Route> GetPoints2()
        {
            string id = RequestContext.Principal.Identity.GetUserId(); ;
            return db.Routes.Where(q => q.UserId == id);


        }
        //// GET: api/Routes/5

        //[ResponseType(typeof(Route))]
        //public async Task<IHttpActionResult> GetRoute(int id)
        //{
        //    Route route = await db.Routes.FindAsync(id);
        //    if (route == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(route);
        //}
        /// <summary>
        /// metoda GET wyszukuje trasę o podanej nazwie
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Route("api/Routes/Name/{name}")]
        public IQueryable<Route> GetRoutes(string name)
        {
            IQueryable<Route> routes;

            routes = db.Routes.Where(q => q.Name == name);


            return routes;

        }
        /// <summary>
        /// metoda GET służąca do wyszukiwania tras stworzonych przez obecnego użytkownika
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        [Route("api/Routes/GetMine/Search")]
        public IQueryable<Route> GetPoints2(string word)
        {
            string id = RequestContext.Principal.Identity.GetUserId(); ;
            return db.Routes.Where(q => q.UserId == id && q.Name == word);


        }
        //// PUT: api/Routes/5
        //[Authorize(Roles = "Creator")]
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutRoute(int id, Route route)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != route.Id)
        //    {
        //        return BadRequest();
        //    }

        //    route.UserId = RequestContext.Principal.Identity.GetUserId();
        //    route.UserName = RequestContext.Principal.Identity.GetUserName();
        //    db.Entry(route).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!RouteExists(id))
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ImageUrl"></param>
        /// <returns></returns>
        // [Authorize(Roles = "Creator, Admin")]
        //[Route("api/Routes/AddImageUrl/{id}")]
        //[ResponseType(typeof(Route))]
        //public async Task<IHttpActionResult> PutRoute(int id, string ImageUrl)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    string Uid = RequestContext.Principal.Identity.GetUserId(); 
        //    Route route = await db.Routes.FindAsync(id);
        //    if (route == null)
        //    {
        //        return NotFound();
        //    }
        //    if (id != route.Id)
        //    {
        //        return BadRequest();
        //    }
        //    if(Uid != route.UserId)
        //    {
        //        return Unauthorized();
        //    }
        //    route.ImageUrl = ImageUrl;

        //    route.UserId = RequestContext.Principal.Identity.GetUserId();
        //    route.UserName = RequestContext.Principal.Identity.GetUserName();
        //    db.Entry(route).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!RouteExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Ok(route);
        //}
        //// POST: api/Routes
        //[Authorize(Roles = "Creator, Admin")]
        //[ResponseType(typeof(Route))]
        //public async Task<IHttpActionResult> PostRoute(Route route)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    route.UserId = RequestContext.Principal.Identity.GetUserId();
        //    route.UserName = RequestContext.Principal.Identity.GetUserName();
        //    db.Routes.Add(route);
        //    await db.SaveChangesAsync();

        //    return CreatedAtRoute("DefaultApi", new { id = route.Id }, route);
        //}

        // DELETE: api/Routes/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Creator, Admin")]
        [ResponseType(typeof(Route))]
        public async Task<IHttpActionResult> DeleteRoute(int id)
        {
            string idU = RequestContext.Principal.Identity.GetUserId();
            Route route = await db.Routes.FindAsync(id);
            if (route == null )
            {
                return NotFound();
            }
          
               // if ( id == 1)
                if (route.UserId != idU  || id == 1)
                {
                return Unauthorized();
                }
          
            db.Routes.Remove(route);
            await db.SaveChangesAsync();

            return Ok(route);
        }
        /// <summary>
        /// metoda DELETE służy do usuwania dowolnej trasy
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/User/Admin/RemoveRoute/{id}")]
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(Route))]
        public async Task<IHttpActionResult> DeleteRoute2(int id)
        {
            string idU = RequestContext.Principal.Identity.GetUserId();
            Route route = await db.Routes.FindAsync(id);
            if (route == null)
            {
                return NotFound();
            }

            // if ( id == 1)
            if (id == 1)
            {
                return Unauthorized();
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