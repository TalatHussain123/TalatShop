using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TalatShop.Models;

namespace TalatShop.Controllers
{
    public class ShopModelsController : Controller
    {
        private TalatShopDBContext db = new TalatShopDBContext();

        // GET: ShopModels
        public ActionResult Index(string searchString)
        {
            var items = from m in db.Items
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.ItemName.Contains(searchString));
            }

            return View(items);
        }


        // GET: ShopModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopModel shopModel = db.Items.Find(id);
            if (shopModel == null)
            {
                return HttpNotFound();
            }
            return View(shopModel);
        }

        // GET: ShopModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShopModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ItemName,PurchaseDate,ItemType,ItemPrice")] ShopModel shopModel)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(shopModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shopModel);
        }

        // GET: ShopModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopModel shopModel = db.Items.Find(id);
            if (shopModel == null)
            {
                return HttpNotFound();
            }
            return View(shopModel);
        }

        // POST: ShopModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ItemName,PurchaseDate,ItemType,ItemPrice")] ShopModel shopModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shopModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shopModel);
        }

        // GET: ShopModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopModel shopModel = db.Items.Find(id);
            if (shopModel == null)
            {
                return HttpNotFound();
            }
            return View(shopModel);
        }

        // POST: ShopModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShopModel shopModel = db.Items.Find(id);
            db.Items.Remove(shopModel);
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
