﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdeToFoodGit.Controllers
{
    public class CuisineController : Controller
    {
        //
        // GET: /Cuisine/

        public ActionResult Search(string name = "French")
        {
            throw new Exception("Something terrible has happened");

            var message = Server.HtmlEncode(name);

            return Content(message);
        }

    }
}
