using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RacingApp.UI.Models
{
    public class ExceptionModel
    {
        public string ErrorMessage { get; set; }
        public ExceptionModel(string E)
        {
            ErrorMessage = E;
        }
    }
}
