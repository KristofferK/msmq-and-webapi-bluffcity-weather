using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AirlineSWA
{
    class AirlineSWA
    {
        private static MessageQueue inputChannel;
        private static MessageQueue outputChannel;

        static void Main(string[] args)
        {
            Console.Title = "Airline SWA. Receives XML document.";
            inputChannel = MessageQueueGenerator.GenerateMessageQueue(MessageQueueGenerator.Weather_To_Airline_SWA);
            outputChannel = MessageQueueGenerator.GenerateMessageQueue(MessageQueueGenerator.Airline_SWA_To_Weather);

            ReceiveResponse();

            while (true)
            {
                var query = Console.ReadLine();
                outputChannel.Send(query, query);
            }
        }

        private static void ReceiveResponse()
        {
            inputChannel.Formatter = new XmlMessageFormatter(new Type[] { typeof(XmlDocument) });
            inputChannel.ReceiveCompleted += new ReceiveCompletedEventHandler(HandleResponse);
            inputChannel.BeginReceive();
        }
        private static void HandleResponse(object source, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue messageQueue = (MessageQueue)source;
            var message = messageQueue.EndReceive(asyncResult.AsyncResult);
            var body = (XmlDocument)message.Body;

            Console.WriteLine("Received XmlDocument from query: " + message.Label);
            Console.WriteLine(body + " with the content:");
            Console.WriteLine(body.OuterXml + "\n\n");

            messageQueue.BeginReceive();
        }
    }
}
