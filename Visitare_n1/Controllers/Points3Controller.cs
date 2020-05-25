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


    public class Punkty2
    {
        public List<int> UpdateList;
        public int RouteId;
        public string Name;
        public string Description;
        public string ImageUrl;
    }
    public class Punkty3
    {
        public string Name = "Trasa";
        public string Description = "Brak";
        public List<Points3> PointList;
        public string ImageUrl;
        public string id;



    }
    public class Punkty4
    {
        public List<Points3> Points3s;
        public Route route;
    }
    public class Punkty5
    {

        public int id;
        public List<Points3> PointList;
    }
    public class AsnwerQAndQuestion3
    {
        public string Question1 { get; set; }
        public List<string> AnswersList;

    }
    public class Punkty6
    {

        public string ImageUrl { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public int id { get; set; }
        public string Description { get; set; }
        public List<string> points;
        public List<AsnwerQAndQuestion3> AsnwerQAndQuestion;



    }
    // [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize]
    public class Points3Controller : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Points3
        public IQueryable<Points3> GetPoints3()
        {
            return db.Points3;
        }

        // GET: api/Points3/5
        [ResponseType(typeof(Points3))]
        public async Task<IHttpActionResult> GetPoints3(int id)
        {
            Points3 points3 = await db.Points3.FindAsync(id);
            if (points3 == null)
            {
                return NotFound();
            }

            return Ok(points3);
        }
        [Authorize(Roles = "Creator")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPoints3(int id, Points3 points3)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != points3.Id)
            {
                return BadRequest();
            }

            db.Entry(points3).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Points3Exists(id))
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
        [AllowAnonymous]
        [ResponseType(typeof(Punkty3))]
        [Route("api/Points3/GetAllObjects")]
        public async Task<List<Punkty6>> GetPoints322()
        {
            List<Route> routes2 = new List<Route>(db.Routes);
            List<Punkty6> output = new List<Punkty6>();
            foreach (Route route2 in routes2)
            {
                Punkty6 punkty6 = new Punkty6();
                punkty6.Name = route2.Name;
                punkty6.Description = route2.Description;
                punkty6.ImageUrl = route2.ImageUrl;
                punkty6.UserName = route2.UserName;
                punkty6.id = route2.Id;
                punkty6.points = new List<string>();
                List<Points3> points2 = new List<Points3>(db.Points3.Where(q => q.RouteId == route2.Id));
                foreach (Points3 point2 in points2)
                {
                    punkty6.points.Add(point2.Name);
                }
                List<AnswerAndQuestion2> answerAndQuestion2s =
                    new List<AnswerAndQuestion2>(db.AnswerAndQuestion2.Where(q => q.RouteId == route2.Id));
                punkty6.AsnwerQAndQuestion = new List<AsnwerQAndQuestion3>();

                foreach (AnswerAndQuestion2 answer in answerAndQuestion2s)
                {
                    AsnwerQAndQuestion3 asnwerAnd = new AsnwerQAndQuestion3();
                    asnwerAnd.Question1 = answer.Question1;
                    asnwerAnd.AnswersList = new List<string>(answer.Answers);


                    punkty6.AsnwerQAndQuestion.Add(asnwerAnd);
                }
                //punkty6.
                output.Add(punkty6);
            }
            return output;


        }
        [AllowAnonymous]
        [ResponseType(typeof(Punkty3))]
        [Route("api/Points3/GetAllObjects/{id}")]
        public async Task<Punkty6> GetPoints322(int id)
        {

            Punkty6 output = new Punkty6();
            Route route2 = await db.Routes.FindAsync(id);
            if (route2 == null)
            {
                return null;
            }

            Punkty6 punkty6 = new Punkty6();
            punkty6.Name = route2.Name;
            punkty6.Description = route2.Description;
            punkty6.ImageUrl = route2.ImageUrl;
            punkty6.UserName = route2.UserName;
            punkty6.id = route2.Id;
            punkty6.points = new List<string>();
            List<Points3> points2 = new List<Points3>(db.Points3.Where(q => q.RouteId == route2.Id));
            foreach (Points3 point2 in points2)
            {
                punkty6.points.Add(point2.Name);
            }
            List<AnswerAndQuestion2> answerAndQuestion2s =
                new List<AnswerAndQuestion2>(db.AnswerAndQuestion2.Where(q => q.RouteId == route2.Id));
            punkty6.AsnwerQAndQuestion = new List<AsnwerQAndQuestion3>();

            foreach (AnswerAndQuestion2 answer in answerAndQuestion2s)
            {
                AsnwerQAndQuestion3 asnwerAnd = new AsnwerQAndQuestion3();
                asnwerAnd.Question1 = answer.Question1;
                asnwerAnd.AnswersList = new List<string>(answer.Answers);


                punkty6.AsnwerQAndQuestion.Add(asnwerAnd);
            }
            //punkty6.
            output = punkty6;

            return output;


        }
        // PUT: api/Points3/5
        [Authorize(Roles = "Creator")]
        [ResponseType(typeof(void))]
        [Route("api/Points3/Change")]
        // [FromBody]
        public async Task<Punkty4> PostPoints32(Punkty2 punkty2)
        {
            if (punkty2 == null)
            {
                return null;
            }
            if (punkty2.UpdateList == null)
            {
                return null;
            }
            Route route = new Route
            {
                Name = punkty2.Name,
                Description = punkty2.Description,
                UserId = RequestContext.Principal.Identity.GetUserId(),
                UserName = RequestContext.Principal.Identity.GetUserName(),
                ImageUrl = punkty2.ImageUrl,

            };
            db.Routes.Add(route);
            await db.SaveChangesAsync();
            Punkty4 output = new Punkty4();
            output.route = route;
            output.Points3s = new List<Points3>();
            // List<int> lista = list2.To
            foreach (int n in punkty2.UpdateList)

            {
                int id = n;


                Points3 points3 = await db.Points3.FindAsync(id);
                if (points3 == null)
                {

                }
                else
                {
                    points3.RouteId = route.Id;
                    db.Entry(points3).State = EntityState.Modified;
                    points3.UserId = RequestContext.Principal.Identity.GetUserId();
                    points3.UserName = RequestContext.Principal.Identity.GetUserName();

                    output.Points3s.Add(points3);
                    try
                    {
                        await db.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!Points3Exists(id))
                        {
                            //  return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }
            return output;




            //if (id != points3.Id)
            //{
            //    return BadRequest();
            //}


            //  return StatusCode(HttpStatusCode.NoContent);
        }
        [Route("api/Points3/RouteOnNumber")]
        public IQueryable<Points3> GetPoints32(int id)
        {

            return db.Points3.Where(q => q.RouteId == id);


        }
        [Authorize(Roles = "Creator")]
        [ResponseType(typeof(Punkty3))]
        [Route("api/Points3/Add")]
        public async Task<Punkty4> PostPoints32(Punkty3 punkty3)
        {
            if (punkty3 == null)
            {
                return null;
            }
            if (punkty3.PointList == null)
            {
                //return BadRequest(ModelState);
                return null;
            }
            Route route = new Route
            {
                Name = punkty3.Name,
                Description = punkty3.Description,
                UserId = RequestContext.Principal.Identity.GetUserId(),
                UserName = RequestContext.Principal.Identity.GetUserName(),
                ImageUrl = punkty3.ImageUrl,


            };
            db.Routes.Add(route);
            await db.SaveChangesAsync();
            Punkty4 output = new Punkty4();
            output.route = route;
            output.Points3s = new List<Points3>();

            foreach (Points3 points3 in punkty3.PointList)
            {

                points3.RouteId = route.Id;
                points3.UserId = RequestContext.Principal.Identity.GetUserId();
                points3.UserName = RequestContext.Principal.Identity.GetUserName();
                db.Points3.Add(points3);
                await db.SaveChangesAsync();
                output.Points3s.Add(points3);

            }


            // return CreatedAtRoute("DefaultApi", new { id = route.Id }, punkty3);
            //   return CreatedAtRoute()
            return output;
        }
        [Authorize(Roles = "Creator")]
        [ResponseType(typeof(Punkty3))]
        [Route("api/Points3/AddToExistingRoute")]
        public async Task<Punkty4> PostPoints32(Punkty5 punkty5)
        {
            if (punkty5 == null)
            {
                return null;
            }
            if (punkty5.PointList == null)
            {
                //return BadRequest(ModelState);
                return null;
            }
            Route route = await db.Routes.FindAsync(punkty5.id);
            if (route == null)
            {
                return null;
            }
            Punkty4 output = new Punkty4();
            output.route = route;

            output.Points3s = new List<Points3>();

            foreach (Points3 points3 in punkty5.PointList)
            {

                points3.RouteId = route.Id;
                points3.UserId = RequestContext.Principal.Identity.GetUserId();
                points3.UserName = RequestContext.Principal.Identity.GetUserName();
                db.Points3.Add(points3);
                await db.SaveChangesAsync();
                output.Points3s.Add(points3);

            }


            // return CreatedAtRoute("DefaultApi", new { id = route.Id }, punkty3);
            //   return CreatedAtRoute()
            return output;
        }
        // POST: api/Points3
        [Authorize(Roles = "Creator")]
        [ResponseType(typeof(Points3))]
        public async Task<IHttpActionResult> PostPoints3(Points3 points3)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            points3.UserId = RequestContext.Principal.Identity.GetUserId();
            points3.UserName = RequestContext.Principal.Identity.GetUserName();
            Route route = await db.Routes.FindAsync(points3.RouteId);
            if(route == null)
            {
                return NotFound();
            }

            db.Points3.Add(points3);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = points3.Id }, points3);
        }

        // DELETE: api/Points3/5
        [Authorize(Roles = "Creator")]
        [ResponseType(typeof(Points3))]
        public async Task<IHttpActionResult> DeletePoints3(int id)
        {
            string idU = RequestContext.Principal.Identity.GetUserId();
            Points3 points3 = await db.Points3.FindAsync(id);
            if (points3 == null)
            {
                return NotFound();
            }
            using (var context = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUserModel>(context);
                var userManager = new UserManager<ApplicationUserModel>(userStore);


                Task<bool> checkUser = userManager.IsInRoleAsync(RequestContext.Principal.Identity.GetUserId(), "Admin");


                if (points3.UserId != idU || checkUser.Equals(false) || id == 1)
                {
                    return Unauthorized();
                }
            }
            
            db.Points3.Remove(points3);
            await db.SaveChangesAsync();

            return Ok(points3);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Points3Exists(int id)
        {
            return db.Points3.Count(e => e.Id == id) > 0;
        }
    }
}