using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Models
{
    public class ResponseModel<T>
    {
        public bool status { get; set; }
        public string message { get; set; }
        public T data { get; set; }
    }
    public class ResponseModel
    {
        public bool status { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }

    public class AuthData
    {
        public string session { get; set; }
        public string name { get; set; }
    }

    public class ValidateData
    {
        public string name { get; set; }
        public string role { get; set; }
    }
}
