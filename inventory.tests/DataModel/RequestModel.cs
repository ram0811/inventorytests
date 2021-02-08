using System;
using RestSharp;

namespace inventory.tests.DataModel
{
    public class RequestModel
    {
        public string Endpoint { get; set; }
        public object Body { get; set; }
        public Method RequestMethod { get; set; }
        public bool IgnoreNullField { get; set; }

        public RequestModel(string endpt, object body = null)
        {
            RequestMethod = Method.GET;
            Body = body;
            Endpoint = endpt;
            IgnoreNullField = false;
        }
    }
}
