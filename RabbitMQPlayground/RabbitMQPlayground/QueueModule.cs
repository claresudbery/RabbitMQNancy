using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using MassTransit;
using Nancy;
using System.Net.Http;

namespace RabbitMQPlayground
{
    public class QueueModule : NancyModule
    {
        public QueueModule()
        {
            Get["/queue"] = parameters =>
            //Get["/queue/{value}", true] = async (parameters, ct) =>
            //Get["/queue", true] = async (parameters, ct) =>
            {
                string result;

                try
                {
                    result = DoQueueingStuff();
                }
                catch (Exception ex)
                {
                    result = ex.Message + ": " + ex.InnerException;
                }

                return result;
                //return await DoQueueingStuff();
                //return await DoQueueingStuff(parameters.value);
            };
        }

        //private async Task<string> DoQueueingStuff()
        //private async Task<string> DoQueueingStuff(string queueItem)
        private string DoQueueingStuff()
        {
            string receivedMessage = "nothing received";

            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                sbc.ReceiveEndpoint(host, "my_queue", endpoint =>
                {
                    //endpoint.Handler<MyMessage>(async context =>
                    //{
                    //    await Console.Out.WriteLineAsync($"Received: {context.Message.Value}");
                    //});

                    endpoint.Handler<IMyMessage>( async context =>
                    {
                        receivedMessage = $"Received: {context.Message.Value}";

                        var client = new HttpClient();
                        var url = "http://www.laterooms.com";
                        await client.GetStringAsync(url);

                        //return Task.FromResult(receivedMessage);
                    });
                });
            });

            using (bus.Start())
            {
                //await bus.Publish(new MyMessage { Value = $"Queue item: {queueItem}" });
                //await bus.Publish(new MyMessage { Value = "Hello world 02" });
                bus.Publish(new MyMessage { Value = "Hello world 02" });

                //Console.ReadLine();
            }

            return receivedMessage;
        }
    }

    public interface IMyMessage
    {
        string Value { get; set; }
    }

    public class MyMessage : IMyMessage
    {
        public string Value { get; set; }
    }
}