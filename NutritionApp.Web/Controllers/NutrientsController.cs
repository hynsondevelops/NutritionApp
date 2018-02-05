using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NutritionApp.Entities;
using NutritionApp.Web.DataContexts;

namespace NutritionApp.Web.Controllers
{
    public class NutrientsController : Controller
    {
        private NutrientsDb db = new NutrientsDb();

        // GET: Nutrients
        public async Task<ActionResult> Index()
        {
            return View(await db.Nutrient.ToListAsync());
        }

        // GET: Nutrients/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nutrient nutrient = await db.Nutrient.FindAsync(id);
            if (nutrient == null)
            {
                return HttpNotFound();
            }
            return View(nutrient);
        }

        // GET: Nutrients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Nutrients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Quantity,Unit,FoodId")] Nutrient nutrient)
        {
            if (ModelState.IsValid)
            {
                db.Nutrient.Add(nutrient);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(nutrient);
        }

        // GET: Nutrients/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nutrient nutrient = await db.Nutrient.FindAsync(id);
            if (nutrient == null)
            {
                return HttpNotFound();
            }
            return View(nutrient);
        }

        // POST: Nutrients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Quantity,Unit,FoodId")] Nutrient nutrient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nutrient).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(nutrient);
        }

        // GET: Nutrients/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nutrient nutrient = await db.Nutrient.FindAsync(id);
            if (nutrient == null)
            {
                return HttpNotFound();
            }
            return View(nutrient);
        }

        // POST: Nutrients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Nutrient nutrient = await db.Nutrient.FindAsync(id);
            db.Nutrient.Remove(nutrient);
            await db.SaveChangesAsync();
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
