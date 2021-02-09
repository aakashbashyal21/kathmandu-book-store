using BookStore.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreUI.Controllers
{
    public class ManageController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IBookService _bookService;

        public ManageController(IBookService bookService,
            RoleManager<IdentityRole> roleManager)
        {
            _bookService = bookService;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateRole()
        {
            return View();
        }
    }

}
