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
        // Method returns a http response message back to the client
        [ResponseType(typeof(void))]
        public HttpResponseMessage PutVideo(int id, Video video)
        {
            if (!ModelState.IsValid || id != video.Id)
                return Request.CreateResponse(HttpStatusCode.BadRequest);        

            try
            {
                // EF - mark entity as modified, changes applied on top
                //db.entry - retrieves a given entry on a entity
                //We then mark the state as modified, so entity framework 
                //can automatically update the entity.
                db.Entry(video).State = EntityState.Modified;
                db.SaveChanges(); //Then save these changes
            }
            catch (DbUpdateConcurrencyException) // Exception when no rows in the db were updated.
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, video);
        }

        // POST: api/Videos
        [ResponseType(typeof(Video))]
        public HttpResponseMessage PostVideo(Video video)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            db.Videos.Add(video);
            db.SaveChanges();

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, video);
            response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = video.Id }));
            return response;
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