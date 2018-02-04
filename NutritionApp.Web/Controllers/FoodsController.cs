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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace NutritionApp.Web.Controllers
{
    public class FoodsController : Controller
    {
        private FoodsDb db = new FoodsDb();

        // GET: Foods
        public async Task<ActionResult> Index()
        {
            return View(await db.Food.ToListAsync());
        }

        // GET: Foods/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = await db.Food.FindAsync(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // GET: Foods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Foods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title")] Food food)
        {
            if (ModelState.IsValid)
            {
                var client = new WebClient();
                var content = client.DownloadString($"https://api.nal.usda.gov/ndb/search/?format=json&q={food.Title}&sort=n&max=25&offset=0&api_key=hyMAaC37dIT57p36cBZ1Sn6tK5XYfnOLP4IaNSs7");
                var JSONContent = JsonConvert.DeserializeObject<JObject>(content);
                var i = 0;
                bool moreItems = true;
                while (moreItems)
                {
                    try
                    {
                        db.Food.Add(new Food() { Title = "" + JSONContent["list"]["item"][i]["name"] });
                    }
                    catch
                    {
                        moreItems = false;
                    }
                    i += 1;
                }
                db.Food.Add(food);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(food);
        }

        // GET: Foods/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = await db.Food.FindAsync(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title")] Food food)
        {
            if (ModelState.IsValid)
            {
                db.Entry(food).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(food);
        }

        // GET: Foods/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = await db.Food.FindAsync(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Food food = await db.Food.FindAsync(id);
            db.Food.Remove(food);
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
