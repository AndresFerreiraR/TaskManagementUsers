using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagement.Users.Commons.Errors
{
    public class BaseError
    {
        public string? PropertyMassage { get; set; }
        public string? ErrorMessage { get; set; }
    }
}