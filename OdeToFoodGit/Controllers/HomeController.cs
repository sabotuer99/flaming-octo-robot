using OdeToFoodGit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Web.UI;
using System.Configuration;

namespace OdeToFoodGit.Controllers
{
    public class HomeController : Controller
    {
        IOdeToFoodDb _db;

        public HomeController()
        {
            _db = new OdeToFoodDb();
        }

        public HomeController(IOdeToFoodDb db)
        {
            _db = db;
        }

        public ActionResult Autocomplete(string term)
        {
            var model =
                _db.Query<Restaurant>()
                .Where(r => r.Name.StartsWith(term))
                .Take(10)
                .Select(r => new 
                {
                    label = r.Name,
                });

            return Json(model, JsonRequestBehavior.AllowGet);
        }


        [OutputCache(CacheProfile="Long", VaryByHeader="X-Requested-With;Accept-Language", Location = OutputCacheLocation.Server)]
        public ActionResult Index(string searchTerm = null, int page = 1)
        {

            var greeting = OdeToFoodGit.Views.Home.Resources.Greeting;

            var model =
                _db.Query<Restaurant>()
                .OrderByDescending(r => r.Reviews.Average(review => review.Rating))
                .Where(r => searchTerm == null || r.Name.StartsWith(searchTerm))
                .Select(r => new RestaurantListViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    City = r.City,
                    Country = r.Country,
                    CountOfReviews = r.Reviews.Count()
                }).ToPagedList(page, 10);

            ViewBag.MailServer = ConfigurationManager.AppSettings["MailServer"];

            if(Request.IsAjaxRequest())
            {
                return PartialView("_Restaurants", model);
            }


            return View(model);
        }

        public ActionResult About()
        {
            var model = new AboutModel();
            model.Name = "Troy W.";
            model.Location = "Wyoming, USA";

            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (_db != null)
            {
                _db.Dispose();
            }
            
            base.Dispose(disposing);
        }
    }
}
