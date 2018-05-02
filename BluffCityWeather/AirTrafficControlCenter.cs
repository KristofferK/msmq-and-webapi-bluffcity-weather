using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BluffCityWeather
{
    class AirTrafficControlCenter
    {
        private static MessageQueue inputChannel;
        private static MessageQueue outputChannel;

        static void Main(string[] args)
        {
            Console.Title = "Air Traffic Control Center";
            inputChannel = MessageQueueGenerator.GenerateMessageQueue(MessageQueueGenerator.Weather_To_AirTrafficControlCenter);
            outputChannel = MessageQueueGenerator.GenerateMessageQueue(MessageQueueGenerator.AirTrafficControlCenter_To_Weather);

            ReceiveResponse();

            while (true)
            {
                var query = Console.ReadLine();
                outputChannel.Send(query, query);
            }

        }

        private static void ReceiveResponse()
        {
            inputChannel.Formatter = new XmlMessageFormatter(new Type[] { typeof(ForecastAirTrafficControlCenter) });
            inputChannel.ReceiveCompleted += new ReceiveCompletedEventHandler(HandleResponse);
            inputChannel.BeginReceive();
        }
        private static void HandleResponse(object source, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue messageQueue = (MessageQueue)source;
            var message = messageQueue.EndReceive(asyncResult.AsyncResult);
            var body = (ForecastAirTrafficControlCenter)message.Body;

            Console.WriteLine("Received answer from query: " + message.Label);
            Console.WriteLine(body + "\n\n");

            messageQueue.BeginReceive();
        }
    }
}
