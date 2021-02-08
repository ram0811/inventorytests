using inventory.tests.DataModel;
using inventory.tests.Utils;
using Microsoft.Rest.Serialization;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace inventory.tests.RestClient
{
    public class BaseClient
    {
        private static string apiID;
        private static string apiKey;
        private JsonSerializerSettings deserializerSettings;
        private JsonSerializerSettings serializerSettings;
        private static string commonEndpt;

        public BaseClient()
        {
            deserializerSettings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Local,
                NullValueHandling = NullValueHandling.Include,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                ContractResolver = new ReadOnlyJsonContractResolver(),
                Converters = new List<JsonConverter>
                {
                    new Iso8601TimeSpanConverter()
                }
            };

            serializerSettings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        public static void SetApiIDKey(string id, string key, string commonurl)
        {
            apiID = id;
            apiKey = key;
            commonEndpt = commonurl;
        }

        private string GenerateSignature(string pkey, string args = null)
        {
            args = args ?? string.Empty;
            var encoding = new System.Text.UTF8Encoding();
            byte[] key = encoding.GetBytes(apiKey);
            var myhmacsha256 = new HMACSHA256(key);
            byte[] hashValue = myhmacsha256.ComputeHash(encoding.GetBytes(args));
            string hmac64 = Convert.ToBase64String(hashValue);
            myhmacsha256.Clear();
            return hmac64;
        }

        private Dictionary<string,string> GetNewHeaders(string urlquery = null)
        {
            var newheaders = new Dictionary<string, string>
            {
                {HeaderKey.ACCEPT, HeaderValue.APPJSON},
                {HeaderKey.AUTHID, apiID},
                {HeaderKey.SIGN, GenerateSignature(apiKey, urlquery)},
                {HeaderKey.CLIENT, HeaderValue.XYZCOMPANY}
            };

            return newheaders;
        }

        public T Put<T>(RequestModel model) where T : class
        {
            model.RequestMethod = Method.PUT;
            return GetResponseObject<T>(model);
        }

        public T Post<T>(RequestModel model) where T : class
        {
            model.RequestMethod = Method.POST;
            return GetResponseObject<T>(model);
        }

        public T Get<T>(RequestModel model) where T : class
        {
            model.RequestMethod = Method.GET;
            return GetResponseObject<T>(model);
        }

        private T GetResponseObject<T>(RequestModel model) where T : class
        {
            var content = GetResponseJson(model);
            var deserializedResponse = SafeJsonConvert.DeserializeObject<T>(content, deserializerSettings);
            return deserializedResponse;
        }

        private string GetResponseJson(RequestModel model)
        {
            var client = new RestSharp.RestClient(commonEndpt + model.Endpoint);
            var queryparts = model.Endpoint.Split('?');
            var query = queryparts.Length > 1 ? queryparts[1] : null;
            var request = CreateRequest(model.RequestMethod, GetNewHeaders(query), model.Body, model.IgnoreNullField);
            var response = client.Execute(request);
            var content = string.IsNullOrEmpty(response.Content) ? "{}" : response.Content;

            try
            {
                if (content.StartsWith("["))
                {
                    content = "{ \"statusDescription\" : \"" + response.StatusDescription + "\", " +
                        "\"statusCode\" : \"" + response.StatusCode + "\", " +
                        "\"contents\" : " + content + " }";
                }
                else
                {
                    dynamic baseResponse = SafeJsonConvert.DeserializeObject<object>(content);
                    baseResponse.statusDescription = response.StatusDescription;
                    baseResponse.statusCode = response.StatusCode;
                    content = SafeJsonConvert.SerializeObject(baseResponse);
                }
            }
            catch (JsonSerializationException e)
            {
                Console.WriteLine(content);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("General exception caught");
                Console.WriteLine(content);
                throw;
            }

            return content;
        }

        private IRestRequest CreateRequest(Method requestMethod, Dictionary<string,string> headers, object body = null, bool ignoreNull = false)
        {
            var request = new RestRequest(requestMethod);
            request.AddHeaders(headers);

            if (body != null)
            {
                var jsonbody = ignoreNull ?
                    SafeJsonConvert.SerializeObject(body, serializerSettings) :
                    SafeJsonConvert.SerializeObject(body);
                request.AddParameter(HeaderValue.APPJSON, jsonbody, ParameterType.RequestBody);
            }

            return request;
        }
    }
}
