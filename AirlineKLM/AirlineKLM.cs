using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace AirlineKLM
{
    class AirlineKLM
    {
        private static MessageQueue inputChannel;
        private static MessageQueue outputChannel;

        static void Main(string[] args)
        {
            Console.Title = "Airline KLM. Receives text-string.";
            inputChannel = MessageQueueGenerator.GenerateMessageQueue(MessageQueueGenerator.Weather_To_Airline_KLM);
            outputChannel = MessageQueueGenerator.GenerateMessageQueue(MessageQueueGenerator.Airline_KLM_To_Weather);

            ReceiveResponse();

            while (true)
            {
                var query = Console.ReadLine();
                outputChannel.Send(query, query);
            }
        }

        private static void ReceiveResponse()
        {
            inputChannel.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            inputChannel.ReceiveCompleted += new ReceiveCompletedEventHandler(HandleResponse);
            inputChannel.BeginReceive();
        }
        private static void HandleResponse(object source, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue messageQueue = (MessageQueue)source;
            var message = messageQueue.EndReceive(asyncResult.AsyncResult);
            var body = (string)message.Body;

            Console.WriteLine("Received STRING from query: " + message.Label);
            Console.WriteLine(body + "\n\n");

            messageQueue.BeginReceive();
        }
    }
}
