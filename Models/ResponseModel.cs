using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Models
{
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
}
