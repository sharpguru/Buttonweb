using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace UseButton
{
    public static class API
    {
        private const string baseAddress = "https://api.usebutton.com/";

        public static string GetAuthHeader(string api_key)
        {
            //Add a colon to the end of the api key(e.g.apiKey +":")
            string apikeywcolon = api_key + ":";

            //Base64 encode the resulting string
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(apikeywcolon);
            string encodedapikey = System.Convert.ToBase64String(plainTextBytes);

            //The authorization method and a space i.e. “Basic ” is then put before the encoded string. (e.g. "Basic " + base64Encode(apiKey + ":"))
            string result = "Basic " + encodedapikey;

            return result;
        }

        public static string CreateOrder(string Order, string api_key)
        {
            string result = "";

            try
            {
                string path = "v1/order";

                var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress + path));
                http.Accept = "application/json";
                http.ContentType = "application/json";
                http.Method = "POST";
                http.Headers.Add(HttpRequestHeader.Authorization, GetAuthHeader(api_key));

                string parsedContent = Order;
                UTF8Encoding encoding = new UTF8Encoding();
                Byte[] bytes = encoding.GetBytes(parsedContent);

                Stream newStream = http.GetRequestStream();
                newStream.Write(bytes, 0, bytes.Length);
                newStream.Close();

                var response = http.GetResponse();

                var stream = response.GetResponseStream();
                var sr = new StreamReader(stream);
                result = sr.ReadToEnd();

            } catch (Exception ex)
            {
                if (ex.Message == "The remote server returned an error: (409) Conflict.")
                {
                    throw new Exception("Order already exists!", ex);
                }
                else
                {
                    throw ex;
                }
            }

            return result;
        }

        public static string GetOrder(string ButtonOrderID, string api_key)
        {
            // https://api.usebutton.com/v1/order/ord-xxxxx
            string result = "";

            try
            {
                string path = "v1/order/" + ButtonOrderID;

                var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress + path));
                http.Accept = "application/json";
                http.ContentType = "application/json";
                http.Method = "GET";
                http.Headers.Add(HttpRequestHeader.Authorization, GetAuthHeader(api_key));

                var response = http.GetResponse();
                var stream = response.GetResponseStream();
                var sr = new StreamReader(stream);

                result = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return result;
        }

        /// <summary>
        /// Update order
        /// </summary>
        /// <param name="Order">json order</param>
        /// <param name="OrderID">order_id or button_order_id</param>
        /// <param name="api_key"></param>
        /// <returns></returns>
        public static string UpdateOrder(string Order, string OrderID, string api_key)
        {
            string result = "";

            try
            {
                string path = "v1/order/" + OrderID;

                var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress + path));
                http.Accept = "application/json";
                http.ContentType = "application/json";
                http.Method = "POST";
                http.Headers.Add(HttpRequestHeader.Authorization, GetAuthHeader(api_key));

                string parsedContent = Order;
                UTF8Encoding encoding = new UTF8Encoding();
                Byte[] bytes = encoding.GetBytes(parsedContent);

                Stream newStream = http.GetRequestStream();
                newStream.Write(bytes, 0, bytes.Length);
                newStream.Close();

                var response = http.GetResponse();

                var stream = response.GetResponseStream();
                var sr = new StreamReader(stream);
                result = sr.ReadToEnd();

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("403"))
                {
                    throw new Exception("Order is already finalized!", ex);
                }
                else
                {
                    throw ex;
                }
            }

            return result;
        }
        public static string DeleteOrder(string ButtonOrderID, string api_key)
        {
            // https://api.usebutton.com/v1/order/ord-xxxxx
            string result = "";

            try
            {
                string path = "v1/order/" + ButtonOrderID;

                var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress + path));
                http.Accept = "application/json";
                http.ContentType = "application/json";
                http.Method = "DELETE";
                http.Headers.Add(HttpRequestHeader.Authorization, GetAuthHeader(api_key));

                var response = http.GetResponse();
                var stream = response.GetResponseStream();
                var sr = new StreamReader(stream);

                result = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("403"))
                {
                    throw new Exception("Order is already finalized!", ex);
                }
                else
                {
                    throw ex;
                }
            }

            return result;
        }

    }
}
