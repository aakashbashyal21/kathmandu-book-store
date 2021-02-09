using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.Common.Interfaces
{
    public interface IDateTime
    {
        DateTime Now { get; }
    }
}
