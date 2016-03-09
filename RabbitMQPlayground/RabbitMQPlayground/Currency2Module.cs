using Nancy;

namespace RabbitMQPlayground
{
    public class Currency2Module : NancyModule
    {
        public Currency2Module()
        {
            Get["/currency2"] = parameters =>
                {
                    var param1 = Request.Query["param1"];
                    var param2 = Request.Query["param2"];
                    var param3 = Request.Query["param3"];

                    return "Currency2"
                           + ", param1: " + param1
                           + ", param2: " + param2
                           + ", param3: " + param3;
                };
        }
    }
}