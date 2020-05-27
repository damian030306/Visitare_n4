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
    [Authorize]
    public class AnswerAndQuestion2Controller : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/AnswerAndQuestion2
        public IQueryable<AnswerAndQuestion2> GetAnswerAndQuestion2()
        {
            return db.AnswerAndQuestion2;
        }

        // GET: api/AnswerAndQuestion2/5
        [ResponseType(typeof(AnswerAndQuestion2))]
        public async Task<IHttpActionResult> GetAnswerAndQuestion2(int id)
        {
            AnswerAndQuestion2 answerAndQuestion2 = await db.AnswerAndQuestion2.FindAsync(id);
            if (answerAndQuestion2 == null)
            {
                return NotFound();
            }

            return Ok(answerAndQuestion2);
        }

        //// PUT: api/AnswerAndQuestion2/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutAnswerAndQuestion2(int id, AnswerAndQuestion2 answerAndQuestion2)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != answerAndQuestion2.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(answerAndQuestion2).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AnswerAndQuestion2Exists(id))
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

        // POST: api/AnswerAndQuestion2
        [Authorize(Roles = "Creator, Admin")]
        [ResponseType(typeof(AnswerAndQuestion2))]
        public async Task<IHttpActionResult> PostAnswerAndQuestion2(AnswerAndQuestion2 answerAndQuestion2)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Route route = await db.Routes.FindAsync(answerAndQuestion2.RouteId);
            answerAndQuestion2.UserId = RequestContext.Principal.Identity.GetUserId();
            answerAndQuestion2.UserName = RequestContext.Principal.Identity.GetUserName();
            if (route == null || answerAndQuestion2.Answers == null )
            {
                return NotFound();
            }

            db.AnswerAndQuestion2.Add(answerAndQuestion2);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = answerAndQuestion2.Id }, answerAndQuestion2);
        }

        // DELETE: api/AnswerAndQuestion2/5
        [Authorize(Roles = "Creator, Admin")]
        [ResponseType(typeof(AnswerAndQuestion2))]
        public async Task<IHttpActionResult> DeleteAnswerAndQuestion2(int id)
        {
            string idU = RequestContext.Principal.Identity.GetUserId();
            AnswerAndQuestion2 answerAndQuestion2 = await db.AnswerAndQuestion2.FindAsync(id);
            if (answerAndQuestion2 == null )
            {
                return NotFound();
                
            }
            using (var context = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUserModel>(context);
                var userManager = new UserManager<ApplicationUserModel>(userStore);


                Task<bool> checkUser = userManager.IsInRoleAsync(RequestContext.Principal.Identity.GetUserId(), "Admin");


                if (answerAndQuestion2.UserId != idU || checkUser.Equals(false) )
                {
                    return Unauthorized();
                }
            }
           

            db.AnswerAndQuestion2.Remove(answerAndQuestion2);
            await db.SaveChangesAsync();

            return Ok(answerAndQuestion2);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AnswerAndQuestion2Exists(int id)
        {
            return db.AnswerAndQuestion2.Count(e => e.Id == id) > 0;
        }
    }
}