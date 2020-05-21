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

        // PUT: api/AnswerAndQuestion2/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAnswerAndQuestion2(int id, AnswerAndQuestion2 answerAndQuestion2)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != answerAndQuestion2.Id)
            {
                return BadRequest();
            }

            db.Entry(answerAndQuestion2).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnswerAndQuestion2Exists(id))
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

        // POST: api/AnswerAndQuestion2
        [ResponseType(typeof(AnswerAndQuestion2))]
        public async Task<IHttpActionResult> PostAnswerAndQuestion2(AnswerAndQuestion2 answerAndQuestion2)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AnswerAndQuestion2.Add(answerAndQuestion2);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = answerAndQuestion2.Id }, answerAndQuestion2);
        }

        // DELETE: api/AnswerAndQuestion2/5
        [ResponseType(typeof(AnswerAndQuestion2))]
        public async Task<IHttpActionResult> DeleteAnswerAndQuestion2(int id)
        {
            AnswerAndQuestion2 answerAndQuestion2 = await db.AnswerAndQuestion2.FindAsync(id);
            if (answerAndQuestion2 == null)
            {
                return NotFound();
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