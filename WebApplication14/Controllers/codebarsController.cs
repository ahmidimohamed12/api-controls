using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication14;

namespace WebApplication14.Controllers
{
    public class codebarsController : ApiController
    {
        private controlsEntities db = new controlsEntities();

        // GET: api/codebars
        public List<codebar> Getcodebars()
        {
            var listOfUsers = (from a in db.codebars
                              select a).ToList();

            return listOfUsers;
        //    return db.codebars;
        }

        // GET: api/codebars/5
        [ResponseType(typeof(codebar))]
        public IHttpActionResult Getcodebar(int id)
        {
            codebar codebar = db.codebars.Find(id);
            if (codebar == null)
            {
                return NotFound();
            }

            return Ok(codebar);
        }

        // PUT: api/codebars/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcodebar(int id, codebar codebar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != codebar.id)
            {
                return BadRequest();
            }

            db.Entry(codebar).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!codebarExists(id))
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

        // POST: api/codebars
        [ResponseType(typeof(codebar))]
        public IHttpActionResult Postcodebar(codebar codebar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.codebars.Add(codebar);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = codebar.id }, codebar);
        }

        // DELETE: api/codebars/5
        [ResponseType(typeof(codebar))]
        public IHttpActionResult Deletecodebar(int id)
        {
            codebar codebar = db.codebars.Find(id);
            if (codebar == null)
            {
                return NotFound();
            }

            db.codebars.Remove(codebar);
            db.SaveChanges();

            return Ok(codebar);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool codebarExists(int id)
        {
            return db.codebars.Count(e => e.id == id) > 0;
        }
    }
}