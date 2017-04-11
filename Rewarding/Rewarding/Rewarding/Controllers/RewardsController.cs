using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Rewarding.Models;
using System.Web;

namespace Rewarding.Controllers
{
    public class RewardsController : Controller
    {
        private PersonContext db = new PersonContext();

        // GET: Rewards
        public ActionResult Index()
        {
            return View(db.Rewards.Include(i=>i.Image).ToList());
        }

        // GET: Rewards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reward reward = db.Rewards.Find(id);
            if (reward == null)
            {
                return HttpNotFound();
            }
            return View(reward);
        }

        // GET: Rewards/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rewards/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description")] Reward reward,  HttpPostedFileBase uploaded)
        {
            if (ModelState.IsValid)
            {
                if (uploaded != null && uploaded.ContentLength > 0)
                {
                    reward.Image = new Image();
                    reward.Image.ImageName = uploaded.FileName;
                    reward.Image.ContentType = uploaded.ContentType;
                    reward.Image.Content = new byte[uploaded.ContentLength];
                    uploaded.InputStream.Read(reward.Image.Content, 0, uploaded.ContentLength);
                }

                db.Rewards.Add(reward);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reward);
        }

        // GET: Rewards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reward reward = db.Rewards.Find(id);
            if (reward == null)
            {
                return HttpNotFound();
            }
            return View(reward);
        }

        // POST: Rewards/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description")] Reward reward,  HttpPostedFileBase uploaded)
        {
            Reward newReward = db.Rewards.Find(reward.Id);
            newReward.Description = reward.Description;
            newReward.Title = reward.Title;
            
            if (uploaded != null && uploaded.ContentLength > 0)
            {
                newReward.Image = new Image();
                newReward.Image.ImageName = uploaded.FileName;
                newReward.Image.ContentType = uploaded.ContentType;
                newReward.Image.Content = new byte[uploaded.ContentLength];
                uploaded.InputStream.Read(newReward.Image.Content, 0, uploaded.ContentLength);
            }

            db.Entry(newReward).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Rewards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Reward reward = db.Rewards.Find(id);
            if (reward.Persons.Any())
            {
                ViewBag.Message = "You can't delete the reward. It's used in another table";
                return View("DeleteFailure");
            }
            else
            {
                if (reward == null)
                {
                    return HttpNotFound();
                }
                return View(reward);
            }
        }

        // POST: Rewards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reward reward = db.Rewards.Find(id);
            Image image = db.Pictures.SingleOrDefault(d => d.ImageId == reward.ImageId);
            db.Rewards.Remove(reward);
            db.Pictures.Remove(image);
            db.SaveChanges();
            return RedirectToAction("Index");
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
