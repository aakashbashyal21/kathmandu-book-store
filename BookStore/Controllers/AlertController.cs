using BookStoreUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreUI.Controllers
{
    public class AlertController : Controller
    {
        // GET: Alert
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Returns Alert Notification Partial View.
        /// </summary>
        /// <returns></returns>
        public ActionResult AlertPV()
        {
            var alert = new AlertViewModel();
            if (TempData["Alert"] != null)
                alert = (AlertViewModel)TempData["Alert"];

            return PartialView("_Alert", alert);
        }
    }
}
