using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace RabbitMQPlayground
{
    public class AsyncHelloModule : NancyModule
    {
        public AsyncHelloModule()
        {
            Get["/hello", true] = async (parameters, ct) => "Hello World!";
        }
    }
}