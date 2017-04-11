using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Rewarding.Models;

namespace Rewarding.Controllers
{
    public class ImagesController : Controller
    {
        private PersonContext db = new PersonContext();

        public FileContentResult GetImage(int imageId)
        {
            Image image = db.Pictures
                .FirstOrDefault(g => g.ImageId == imageId);

            if (image != null)
            {
                return File(image.Content, image.ContentType);
            }
            else
            {
                return null;
            }
        }
        public FileContentResult Delete(int id)
        {
            Image image = db.Pictures.Find(id);
            db.Pictures.Remove(image);
            db.SaveChanges();
            return null;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
