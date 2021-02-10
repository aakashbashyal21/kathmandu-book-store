using BookStore.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreUI.Controllers
{
    [Authorize(Roles = "Reader")]
    public class MyBookController : Controller
    {
        private readonly IBookRequestService _bookRequestService;
        private readonly ICurrentUserService _currentUserService;

        public MyBookController(IBookRequestService bookRequestService, ICurrentUserService currentUserService)
        {
            _bookRequestService = bookRequestService;
            _currentUserService = currentUserService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyAllBook(int currentPage = 1)
        {
            return ViewComponent("MyBook", new { currentPage = currentPage });

        }
        public IActionResult GetApproveBook(int currentPage = 1)
        {
            return View();
        }

        public IActionResult ReservedBook()
        {
            return PartialView("_ReservedBook");
        }

    }
}
