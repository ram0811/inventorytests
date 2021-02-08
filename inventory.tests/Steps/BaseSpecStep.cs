using BoDi;
using System;
using System.Threading.Tasks;
using NUnit.Framework;
using TechTalk.SpecFlow;
using inventory.tests.DataModel;

namespace inventory.tests.Steps
{
    public class BaseSpecStep
    {
        protected ContextObject Context;
        protected ScenarioContext ScenContext;

        public BaseSpecStep(ContextObject context, IObjectContainer ocontainer)
        {
            Context = context;
            ScenContext = ocontainer.Resolve<ScenarioContext>();
        }

        public void WriteInfo(string message)
        {
            Console.WriteLine("--- " + message);
        }

        public string GenerateRandomChars(int n)
        {
            var rand = new Random();
            string output = "";
            for (int i = 0; i < n; i++)
                output += rand.Next(10).ToString();

            return output;
        }

        public void SetScenarioVar(string key, object val)
        {
            if (ScenContext.ContainsKey(key))
                ScenContext[key] = val;
            else
                ScenContext.Add(key, val);

        }

        public void RemoveScenarioVar(string key)
        {
            ScenContext.Remove(key);
        }

        public T GetScenarioVar<T>(string key) where T : class
        {
            T val = null;

            if (ScenContext.ContainsKey(key))
                val = (T)ScenContext[key];

            return val;
        }

        public void AssertHTTPResponse(string code, string desc, BaseResponse actualresponse)
        {
            Assert.IsTrue(code == actualresponse.StatusCode || desc == actualresponse.StatusDescription,
                $"Expected: {code} {desc}; Actual: {actualresponse.StatusCode} {actualresponse.StatusDescription}");
        }
    }
}
