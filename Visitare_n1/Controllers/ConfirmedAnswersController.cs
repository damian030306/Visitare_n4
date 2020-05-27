using Microsoft.AspNet.Identity;
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
    public class ConfirmedAnswersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ConfirmedAnswers
        public IQueryable<ConfirmedAnswer> GetConfirmedAnswers()
        {
            return db.ConfirmedAnswers;
        }

        // GET: api/ConfirmedAnswers/5
        [Route("api/ConfirmedAnswers/Check/{answerid}")]
        [ResponseType(typeof(bool))]
        public async Task<bool> GetConfirmedAnswer(int answerid)
        {
            string id = RequestContext.Principal.Identity.GetUserId();
            ConfirmedAnswer confirmedAnswer = await db.ConfirmedAnswers.FindAsync(id, answerid);
           
            if (confirmedAnswer == null)
            {
                return false;
            }

            return true;
        }

        //// PUT: api/ConfirmedAnswers/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutConfirmedAnswer(string id, ConfirmedAnswer confirmedAnswer)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != confirmedAnswer.UserId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(confirmedAnswer).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ConfirmedAnswerExists(id))
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

        [Route("api/ConfirmedAnswers/Add/{answerid}")]
        // POST: api/ConfirmedAnswers
        [ResponseType(typeof(VisitedPoint))]
        public async Task<IHttpActionResult> PostConfirmedAnswer(int answerid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AnswerAndQuestion2 answerAndQuestion2 = await db.AnswerAndQuestion2.FindAsync(answerid);
            if(answerAndQuestion2 == null)
            {
                return NotFound();
            }
            ConfirmedAnswer confirmedAnswer = new ConfirmedAnswer()
            {
                UserId = RequestContext.Principal.Identity.GetUserId(),
                AnswerAndQuestion2Id = answerid,
                answerAndQuestion2 = answerAndQuestion2,
            };

            
            
            db.ConfirmedAnswers.Add(confirmedAnswer);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ConfirmedAnswerExists(confirmedAnswer.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(confirmedAnswer);
        }

        // DELETE: api/ConfirmedAnswers/5
        [Route("api/ConfirmedAnswers/Remove/{answerid}")]
        [ResponseType(typeof(ConfirmedAnswer))]
        public async Task<IHttpActionResult> DeleteConfirmedAnswer(int answerid)
        {
            string id = RequestContext.Principal.Identity.GetUserId(); ;
            ConfirmedAnswer confirmedAnswer = await db.ConfirmedAnswers.FindAsync(id, answerid);
            if (confirmedAnswer == null)
            {
                return NotFound();
            }

            db.ConfirmedAnswers.Remove(confirmedAnswer);
            await db.SaveChangesAsync();

            return Ok(confirmedAnswer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ConfirmedAnswerExists(string id)
        {
            return db.ConfirmedAnswers.Count(e => e.UserId == id) > 0;
        }
    }
}