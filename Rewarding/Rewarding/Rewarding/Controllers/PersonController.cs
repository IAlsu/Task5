using System.Linq;
using System.Web.Mvc;
using Rewarding.Models;
using System.Net;
using System.Data.Entity;
using System.Web;
using System;

namespace Rewarding.Controllers
{
    public class PersonController : Controller
    {
        PersonContext db = new PersonContext();

        // GET: Person
        public ActionResult Index()
        {
            var persons = db.Persons.Include(s=>s.Photo).Include(i => i.Rewards);
            return View(persons);
        }
        
        //GET
        public ActionResult Create()
        {
            ViewBag.Rewards = db.Rewards.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Birthdate,Age,Rewards")] Person person, int[] selectedRewards, HttpPostedFileBase uploaded)
        {
            if (selectedRewards != null)
            {
                //получаем выбранные rewards
                foreach (var c in db.Rewards.Where(co => selectedRewards.Contains(co.Id)))
                {
                    person.Rewards.Add(c);
                }
            }

            if (uploaded != null && uploaded.ContentLength > 0)
            {
                person.Photo = new Image();
                person.Photo.ImageName = uploaded.FileName;
                person.Photo.ContentType = uploaded.ContentType;
                person.Photo.Content = new byte[uploaded.ContentLength];
                uploaded.InputStream.Read(person.Photo.Content, 0, uploaded.ContentLength);
            }
            db.Persons.Add(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /Persons/Details/1 
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: /Persons/Edit/1 
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            ViewBag.Rewards = db.Rewards.ToList();
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Birthdate,Age")] Person person, int[] selectedRewards, HttpPostedFileBase uploaded)
        {
            Person newPerson = db.Persons.Find(person.Id);
            newPerson.Name = person.Name;
            newPerson.Birthdate = person.Birthdate;
            newPerson.Age = person.Age;

            newPerson.Rewards.Clear();
            if (selectedRewards != null)
            {
                //получаем выбранные rewards
                foreach (var c in db.Rewards.Where(co => selectedRewards.Contains(co.Id)))
                {
                    newPerson.Rewards.Add(c);
                }
            }

            if (uploaded != null && uploaded.ContentLength > 0)
            {
                newPerson.Photo = new Image();
                newPerson.Photo.ImageName = uploaded.FileName;
                newPerson.Photo.ContentType = uploaded.ContentType;
                newPerson.Photo.Content = new byte[uploaded.ContentLength];
                uploaded.InputStream.Read(newPerson.Photo.Content, 0, uploaded.ContentLength);
            }

            db.Entry(newPerson).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /Persons/Delete/1
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.Persons.Include(s => s.Photo).SingleOrDefault(s => s.Id == id);
            Image photo = db.Pictures.SingleOrDefault(d => d.ImageId == person.PhotoId);
            db.Persons.Remove(person);
            db.Pictures.Remove(photo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public FileResult DownloadListOfPeople()
        {
            var list = db.Persons.Select(c => c.Name ).ToList();
            string people = string.Join(Environment.NewLine, list.ToArray());
            return File(new System.Text.UTF8Encoding().GetBytes(people), "text/plain", "People.txt"); 
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}