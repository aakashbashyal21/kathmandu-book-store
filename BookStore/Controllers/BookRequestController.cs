using BookStore.Application.Common;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Model;
using BookStore.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreUI.Controllers
{

    public class BookRequestController : Controller
    {
        readonly IEmailService _emailService;
        IIdentityService _identityService;
        private readonly IBookRequestService _bookRequestService;

        public BookRequestController(IBookRequestService bookRequestService,
            IEmailService emailService,
            IIdentityService identityService)
        {
            _emailService = emailService;
            _identityService = identityService;
            _bookRequestService = bookRequestService;
        }

        public async Task<IActionResult> Index(int currentpage = 1)
        {

            if (TempData["Alert"] != null)
                ViewBag.AlertMessage = TempData["Alert"];


            int pageSize = 10;
            var result = await _bookRequestService.GetRequestedBookList(currentpage, pageSize);

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveBook(BookApproveViewModel model)
        {
            try
            {
                await _bookRequestService.ApproveBook(model);


                await _bookRequestService.SendEmailForApprove(model);

                TempData["Alert"] = JsonConvert.SerializeObject(new AlertViewModel(AlertMessage.ApproveBookRequest, AlertType.Information));
            }
            catch
            {
                TempData["Alert"] = JsonConvert.SerializeObject(new AlertViewModel(AlertMessage.Error, AlertType.Error));
            }

            return RedirectToAction("Index");
        }
    }
}
