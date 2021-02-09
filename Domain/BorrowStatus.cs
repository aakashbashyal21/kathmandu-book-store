using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Domain
{
    public enum BorrowStatus
    {
        
        Approved = 0,
        Requested = 1,
        Verified = 2,
        Rejected = 3
    }
}
