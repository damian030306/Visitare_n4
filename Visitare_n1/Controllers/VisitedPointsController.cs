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
    public class VisitedPointsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //// GET: api/VisitedPoints
        //public IQueryable<VisitedPoint> GetVisitedPoints()
        //{
        //    return db.VisitedPoints;
        //}

        /// <summary>
        /// metoda GET służąca do sprawdzania czy podane miejsce zostało odwiedzone przez zalogowanego użytkownika
        /// </summary>
        /// <param name="pointId"></param>
        /// <returns></returns>
        [Route("api/VisitedPoints/Check/{pointid}")]
        [ResponseType(typeof(bool))]
        /// <summary>
        /// This class does something.
        /// </summary>
        public async Task<bool> GetVisitedPoint2(int pointId)
        {
            string id = RequestContext.Principal.Identity.GetUserId(); 
            VisitedPoint visitedPoint = await db.VisitedPoints.FindAsync(id, pointId);

            if (visitedPoint == null)
            {
                return false;
            }



            return true;
        }

        //// PUT: api/VisitedPoints/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutVisitedPoint(string id, VisitedPoint visitedPoint)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != visitedPoint.UserId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(visitedPoint).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!VisitedPointExists(id))
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

        // POST: api/VisitedPoints
        /// <summary>
        /// metoda POST służąca do dodawania odwiedzin miejsca o podanym identyfikatorze przez zalogowanego użytkownika
        /// </summary>
        /// <param name="pointId"></param>
        /// <returns></returns>
        [Route("api/VisitedPoints/Add/{pointid}")]
        [ResponseType(typeof(VisitedPoint))]
        public async Task<IHttpActionResult> PostVisitedPoint(int pointId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Points3 points3 = await db.Points3.FindAsync(pointId);
            if (points3 == null)
            {
                return NotFound();
            }

            VisitedPoint visitedPoint = new VisitedPoint();
            visitedPoint.Points3 = points3;
            visitedPoint.UserId = RequestContext.Principal.Identity.GetUserId();
            visitedPoint.Points3Id = pointId;
            db.VisitedPoints.Add(visitedPoint);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VisitedPointExists(visitedPoint.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(visitedPoint);
        }
        /// <summary>
        /// metoda DELETE służąca do usuwania odwiedzin miejsca przez użytkownika, 
        /// </summary>
        /// <param name="pointId"></param>
        /// <returns></returns>
        [Route("api/VisitedPoints/Remove/{pointid}")]
        // DELETE: api/VisitedPoints/5
        [ResponseType(typeof(VisitedPoint))]
        public async Task<IHttpActionResult> DeleteVisitedPoint(int pointId)
        {
            string id = RequestContext.Principal.Identity.GetUserId(); ;
            VisitedPoint visitedPoint = await db.VisitedPoints.FindAsync(id, pointId);

            if (visitedPoint == null)
            {
                return NotFound();
            }

            db.VisitedPoints.Remove(visitedPoint);
            await db.SaveChangesAsync();

            return Ok(visitedPoint);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VisitedPointExists(string id)
        {
            return db.VisitedPoints.Count(e => e.UserId == id) > 0;
        }
    }
}