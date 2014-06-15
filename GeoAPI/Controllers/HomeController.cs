using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeoLib.Resources;

namespace RichGeobase.WebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Home()
        {
            return PartialView();
        }

        public PartialViewResult About()
        {
            return PartialView();
        }
        public PartialViewResult Features()
        {
            return PartialView();
        }

        public PartialViewResult Portfolio()
        {
            return PartialView();
        }

        public PartialViewResult Team()
        {
            return PartialView();
        }

        public PartialViewResult Contacts()
        {
            return PartialView();
        }

    }
}
