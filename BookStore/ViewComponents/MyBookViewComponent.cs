using BookStore.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreUI.ViewComponents
{
    public class MyBookViewComponent : ViewComponent
    {
        private readonly ICurrentUserService _currentUserService;

        private readonly IBookRequestService _bookRequestService;

        public MyBookViewComponent(IBookRequestService bookRequestService,
            ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
            _bookRequestService = bookRequestService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int currentPage = 1)
        {
            int pageSize = 10;

            var result = await _bookRequestService.GetAllBookForUser(currentPage, pageSize);

            return View(result);
        }
    }
}
