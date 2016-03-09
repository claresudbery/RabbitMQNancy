using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace RabbitMQPlayground
{
    public class CurrencyModule : NancyModule
    {
        public CurrencyModule()
        {
            Get["/currency/{thing}/{thing2}/{value}"] = parameters => "Currency"
                                                                      + ", thing: "  + parameters.thing
                                                                      + ", thing2: " + parameters.thing2
                                                                      + ", value: "  + parameters.value;
        }
    }
}