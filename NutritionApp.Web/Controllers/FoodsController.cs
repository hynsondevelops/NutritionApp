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
        private NutrientsDb nutrientsDb = new NutrientsDb();
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
                while (moreItems) //loops over items in search
                {
                    try
                    {
                        var currentFood = new Food() { Title = "" + JSONContent["list"]["item"][i]["name"] };
                        db.Food.Add(currentFood); //New food
                        await db.SaveChangesAsync();
                        //setting up nutrient array
                        bool moreNutrients = true;
                        var ndbno = JSONContent["list"]["item"][i]["ndbno"];
                        var nutritionContent = client.DownloadString($"https://api.nal.usda.gov/ndb/reports/?ndbno={ndbno}&type=f&format=json&api_key=hyMAaC37dIT57p36cBZ1Sn6tK5XYfnOLP4IaNSs7");
                        var JSONNutritionContent = JsonConvert.DeserializeObject<JObject>(nutritionContent);
                        var j = 0;
                        while (moreNutrients) //nutrients
                        {
                            try
                            {
                               
                                nutrientsDb.Nutrient.Add(new Nutrient()
                                {
                                    Name = "" + JSONNutritionContent["report"]["food"]["nutrients"][j]["name"],
                                    Quantity = Decimal.Parse("" + JSONNutritionContent["report"]["food"]["nutrients"][j]["value"]),
                                    Unit = "" + JSONNutritionContent["report"]["food"]["nutrients"][j]["unit"],
                                    FoodId = currentFood.Id
                                });
                            }
                            catch
                            {
                                moreNutrients = false;
                            }
                            j += 1;

                        }
                    }
                    catch
                    {
                        moreItems = false;
                    }
                    i += 1;
                }
                db.Food.Add(food);
                await db.SaveChangesAsync();
                await nutrientsDb.SaveChangesAsync();
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
