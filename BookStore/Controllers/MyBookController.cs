using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreUI.Controllers
{
    public class MyBookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ApprovedBook(int currentPage = 1)
        {
            return ViewComponent("ApprovedBook", new { currentPage = currentPage });

        }
        public IActionResult ReservedBook()
        {
            return PartialView("_ReservedBook");
        }

    }
}
