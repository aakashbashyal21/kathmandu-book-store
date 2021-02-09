using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.ViewModel
{
    public class AlertViewModel
    {
        public AlertViewModel()
        {

        }
        public AlertViewModel(string message, AlertType type)
        {
            Message = message;
            Type = type;
        }
        public string Message { get; set; }
        public AlertType Type { get; set; }
    }

    public enum AlertType
    {
        Information = 0,
        Warning = 1,
        Error = 2
    }
}
