using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Videos.Models;

namespace Videos.Controllers
{
    public class VideosController : ApiController
    {
        private VideoConext db;

        public VideosController()
        {
            db = new VideoConext();
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Videos
        public IQueryable<Video> GetVideos()
        {
            return db.Videos;
        }

        // GET: api/Videos/5
        [ResponseType(typeof(Video))]
        public IHttpActionResult GetVideo(int id)
        {
            Video video = db.Videos.Find(id);
            if (video == null)
            {
                return NotFound();
            }

            return Ok(video);
        }

        // PUT: api/Videos/5
        [ResponseType(typeof(void))]
        public HttpResponseMessage PutVideo(int id, Video video)
        {
            if (!ModelState.IsValid || id != video.Id)
                return Request.CreateResponse(HttpStatusCode.BadRequest);        

            try
            {
                // EF - mark entity as modified, changes applied on top
                db.Entry(video).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, video);
        }

        // POST: api/Videos
        [ResponseType(typeof(Video))]
        public IHttpActionResult PostVideo(Video video)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Videos.Add(video);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = video.Id }, video);
        }

        // DELETE: api/Videos/5
        [ResponseType(typeof(Video))]
        public IHttpActionResult DeleteVideo(int id)
        {
            Video video = db.Videos.Find(id);
            if (video == null)
            {
                return NotFound();
            }

            db.Videos.Remove(video);
            db.SaveChanges();

            return Ok(video);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VideoExists(int id)
        {
            return db.Videos.Count(e => e.Id == id) > 0;
        }
    }
}