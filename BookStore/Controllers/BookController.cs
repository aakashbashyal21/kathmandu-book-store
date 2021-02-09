using BookStore.Application.Common;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace BookStoreUI.Controllers
{
    public class BookController : Controller
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IBookService _bookService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IImageUpload _imageUpload;
        public BookController(IBookService bookService,
            ICurrentUserService currentUserService,
            IImageUpload imageUpload,
            IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _currentUserService = currentUserService;
            _imageUpload = imageUpload;
            _bookService = bookService;
        }

        // GET: BookController
        [Authorize(Roles = "Administrator")]

        public async Task<ActionResult> Index(int currentPage = 1)
        {

            var allBooks = await _bookService.GetAllBooks(currentPage, 10);

            return View(allBooks);
        }
        [Authorize(Roles = "Administrator")]

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [Route("book-details/{id:int:min(1)}", Name = "bookDetailsRoute")]
        public async Task<ViewResult> GetBook(int id)
        {
            var data = await _bookService.GetBookById(id);

            if (TempData["Alert"] != null)
                ViewBag.AlertMessage = TempData["Alert"];


            return View(data);
        }
        [Authorize(Roles = "Administrator")]

        public ActionResult Create(bool isSuccess = false, int bookId = 0)
        {

            var model = new BookCreateViewModel();

            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]

        public async Task<ActionResult> Create(BookCreateViewModel bookCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(bookCreateViewModel);
            }

            if (bookCreateViewModel.CoverPhoto != null)
            {

                string folder = "books/cover/";
                string rootPath = _webHostEnvironment.WebRootPath;

                bookCreateViewModel.CoverImageUrl = await _imageUpload.UploadImage(folder, bookCreateViewModel.CoverPhoto, rootPath);
            }

            int id = await _bookService.AddNewBook(bookCreateViewModel);

            return RedirectToAction(nameof(Index), new { isSuccess = true, bookId = id });

        }

        // GET: BookController/Edit/5
        [Authorize(Roles = "Administrator")]

        public async Task<ActionResult> Edit(int id)
        {
            var data = await _bookService.GetBookById(id);

            return View(data);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]

        public async Task<ActionResult> Edit(int id, BookEditViewModel bookEditViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(bookEditViewModel);
            }
            if (bookEditViewModel.CoverPhoto != null)
            {

                string folder = "books/cover/";
                string rootPath = _webHostEnvironment.WebRootPath;

                bookEditViewModel.ExistingImageUrl = await _imageUpload.UploadImage(folder, bookEditViewModel.CoverPhoto, rootPath);
            }
            int returnBookId = await _bookService.UpdateBook(id, bookEditViewModel);

            return RedirectToAction(nameof(Index), new { isSuccess = true, bookId = id });

        }



        //POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _bookService.DeleteBook(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        [Authorize(Roles = "Reader")]

        public async Task<ActionResult> BorrowRequest(int bookId)
        {
            try
            {
                if (bookId != 0)
                {
                    //var user = User?.Claims;

                    //return View("Claims", user);

                    var userId = _currentUserService.UserId;
                    if (await _bookService.CheckIfBorrowed(bookId, userId))
                    {
                        TempData["Alert"] = JsonConvert.SerializeObject(new AlertViewModel(AlertMessage.DuplicateBookRequest, AlertType.Warning));
                        return RedirectToRoute("bookDetailsRoute", new { id = bookId });
                    }
                    await _bookService.RequestForBorrow(bookId, userId);

                    TempData["Alert"] = JsonConvert.SerializeObject(new AlertViewModel(AlertMessage.BookRequestSuccess, AlertType.Information));
                }
            }
            catch (Exception)
            {
                TempData["Alert"] = JsonConvert.SerializeObject(new AlertViewModel(AlertMessage.DuplicateBookRequest, AlertType.Error)); ;
            }

            return RedirectToRoute("bookDetailsRoute", new { id = bookId });
        }

    }
}
