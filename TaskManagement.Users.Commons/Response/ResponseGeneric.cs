using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Users.Commons.Errors;

namespace TaskManagement.Users.Commons.Response
{
    public class ResponseGeneric<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public IEnumerable<BaseError> Errors { get; set; }
    }
}