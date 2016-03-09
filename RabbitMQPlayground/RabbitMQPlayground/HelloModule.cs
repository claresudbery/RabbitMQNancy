using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace RabbitMQPlayground
{
    public class HelloModule : NancyModule
    {
        public HelloModule()
        {
            Get["/hello"] = parameters => "Hello World";
        }
    }
}