using BookStore.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
