using BookStore.Application.Common.Interfaces;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookService _bookService;
        public HomeController(ILogger<HomeController> logger, IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }

        public async Task<IActionResult> Index(int currentPage = 1)
        {
            int pageSize = 3;

            var allBooks = await _bookService.GetAllBooks(currentPage, pageSize);

            return View(allBooks);

        }
        [Authorize, HttpGet("send-test-email")]

        public async Task<IActionResult> Privacy()
        {
            MailMessage mail = new MailMessage
            {
                Subject = "Hi",
                Body = "This is the email body",
                From = new MailAddress("devawsapp@gmail.com", "BookStoreTeam"),
                IsBodyHtml = true
            };

            mail.To.Add("aakashbashyal");


            NetworkCredential networkCredential = new NetworkCredential("devawsapp@gmail.com", "Admin@123");

            SmtpClient smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = networkCredential
            };

            mail.BodyEncoding = Encoding.Default;

            await smtpClient.SendMailAsync(mail);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
