using Newtonsoft.Json;
using PasswordManager.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PasswordManager.Core
{
    public  static class ResponseHandler
    {
        private class AuthData {
            public string login { get;set; }
            public string password { get;set; }
        }

        public static ResponseModel SendAuth(string login,string password)
        {
            
            AuthData data = new AuthData() { login=login, password = password };
            var client = new RestClient("https://localhost:32776/");
            var request = new RestRequest("/api/Auth/Auth");
            request.Method = Method.Post;
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(JsonConvert.SerializeObject(data));
            var response = client.ExecutePost(request);
            return JsonConvert.DeserializeObject<ResponseModel>(response.Content);
            

        }
    }
}
