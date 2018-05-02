using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace AirportInformationCenter
{
    class AirportInformationCenter
    {
        private static MessageQueue inputChannel;
        private static MessageQueue outputChannel;

        static void Main(string[] args)
        {
            Console.Title = "Airport Information Center. Receives object.";
            inputChannel = MessageQueueGenerator.GenerateMessageQueue(MessageQueueGenerator.Weather_To_AirportInformationCenter);
            outputChannel = MessageQueueGenerator.GenerateMessageQueue(MessageQueueGenerator.AirportInformationCenter_To_Weather);

            ReceiveResponse();

            while (true)
            {
                var query = Console.ReadLine();
                outputChannel.Send(query, query);
            }
        }

        private static void ReceiveResponse()
        {
            inputChannel.Formatter = new XmlMessageFormatter(new Type[] { typeof(ForecastAirportInformationCenter) });
            inputChannel.ReceiveCompleted += new ReceiveCompletedEventHandler(HandleResponse);
            inputChannel.BeginReceive();
        }
        private static void HandleResponse(object source, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue messageQueue = (MessageQueue)source;
            var message = messageQueue.EndReceive(asyncResult.AsyncResult);
            var body = (ForecastAirportInformationCenter)message.Body;

            Console.WriteLine("Received OBJECT from query: " + message.Label);
            Console.WriteLine(body + "\n\n");

            messageQueue.BeginReceive();
        }
    }
}
