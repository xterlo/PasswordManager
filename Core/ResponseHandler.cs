using Newtonsoft.Json;
using PasswordManager.Models;
using RestSharp;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static PasswordManager.Core.ResponseHandler;

namespace PasswordManager.Core
{
    public  static class ResponseHandler
    {
        readonly static string url = "https://localhost:32780/";

        public class Response
        {
            public bool status { get; set; }
            public string text { get; set; }
        }
        private class AuthData {
            public string login { get;set; }
            public string password { get;set; }
        }

        private class ValidateData {
            public string token { get;set; }
        }


        public static async Task<Response> SendPost(string route,object data)
        {
            try
            {
                
                var client = new RestClient(url);
                var request = new RestRequest(route);
                request.AddHeader("Content-Type", "application/json");
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(JsonConvert.SerializeObject(data));
                RestResponse response = await client.ExecutePostAsync(request);
                response.ThrowIfError();
                return new Response() { status = true, text = response.Content };
            }
            catch(Exception ex)
            {
                return new Response() { status = false, text = ex.Message };
            }
        }

        public static async Task<Response> SendGet(string route, Dictionary<string, string> parameters)
        {
            try
            {

                var client = new RestClient(url);
                var request = new RestRequest(route);
                request.Method = Method.Get;
                foreach(string key in parameters.Keys)
                {
                    request.AddParameter(key, parameters[key]);
                }
                RestResponse response = await client.ExecuteGetAsync(request);
                response.ThrowIfError();
                return new Response() { status = true, text = response.Content };
            }
            catch (Exception ex)
            {
                return new Response() { status = false, text = ex.Message };
            }
        }

        

        public static async Task<ResponseModel<Models.AuthData>> SendAuth(string login, string password)
        {
            AuthData data = new AuthData() { login = login, password = password };
            string route = "/api/Auth/Auth";
            var response = await SendPost(route, data);
            if (!response.status) return new ResponseModel<Models.AuthData>() { status = false, message = response.text };
            return JsonConvert.DeserializeObject<ResponseModel<Models.AuthData>>(response.text);
        }

        public static async Task<ResponseModel<Models.ValidateData>> SendValidate(string token)
        {
            ValidateData data = new ValidateData() { token = token};
            Dictionary<string, string> pairs = DictFromClass<ValidateData>.GetDictFromClass(data);
            string route = "/api/UserControl/Me";
            var response = await SendGet(route, pairs);
            if (!response.status) return new ResponseModel<Models.ValidateData>() { status = false, message = response.text };
            return JsonConvert.DeserializeObject<ResponseModel<Models.ValidateData>>(response.text);
        }
    }
}
