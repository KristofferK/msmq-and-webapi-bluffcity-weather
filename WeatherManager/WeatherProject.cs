using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;

namespace WeatherManager
{
    class Program
    {
        private static MessageQueue airTrafficControlCenterInput;
        private static MessageQueue airTrafficControlCenterOutput;
        private static MessageQueue airportInformationCenterInput;
        private static MessageQueue airportInformationCenterOutput;

        private static WeatherManager weatherManager = new WeatherManager();
        static void Main(string[] args)
        {
            airTrafficControlCenterInput = MessageQueueGenerator.GenerateMessageQueue(MessageQueueGenerator.AirTrafficControlCenter_To_Weather);
            airTrafficControlCenterOutput = MessageQueueGenerator.GenerateMessageQueue(MessageQueueGenerator.Weather_To_AirTrafficControlCenter);

            airportInformationCenterInput = MessageQueueGenerator.GenerateMessageQueue(MessageQueueGenerator.AirportInformationCenter_To_Weather);
            airportInformationCenterOutput = MessageQueueGenerator.GenerateMessageQueue(MessageQueueGenerator.Weather_To_AirportInformationCenter);

            Console.Title = "Weather project. Receives queries and returns corresponding datatype.";

            ReceiveInputFromAirTrafficControlCenter(airTrafficControlCenterInput, airTrafficControlCenterOutput);
            ReceiveInputFromAirportInformationCenter(airportInformationCenterInput, airportInformationCenterOutput);

            while (Console.ReadLine() != "exit")
            {
                Console.WriteLine("Type 'exit' to exit");
            }
        }

        private static void ReceiveInputFromAirTrafficControlCenter(MessageQueue inputChannel, MessageQueue outputQueue)
        {
            inputChannel.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            inputChannel.ReceiveCompleted += ((object source, ReceiveCompletedEventArgs asyncResult) =>
            {
                MessageQueue messageQueue = (MessageQueue)source;
                var message = messageQueue.EndReceive(asyncResult.AsyncResult);
                var location = (string)message.Body;
                Console.WriteLine("Received query from Air Traffic Control Center: " + location);

                var reply = Forecast.GenerateForecastAirTrafficControlCenter(weatherManager.GetForecast(location));
                Console.WriteLine("Responding with object:");
                Console.WriteLine(reply);

                airTrafficControlCenterOutput.Send(reply, location);

                messageQueue.BeginReceive();
            });
            inputChannel.BeginReceive();
        }

        private static void ReceiveInputFromAirportInformationCenter(MessageQueue inputChannel, MessageQueue outputQueue)
        {
            inputChannel.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            inputChannel.ReceiveCompleted += ((object source, ReceiveCompletedEventArgs asyncResult) =>
            {
                MessageQueue messageQueue = (MessageQueue)source;
                var message = messageQueue.EndReceive(asyncResult.AsyncResult);
                var location = (string)message.Body;
                Console.WriteLine("Received query from Airport Information Center: " + location);

                var reply = Forecast.GenerateForecastAirportInformationCenter(weatherManager.GetForecast(location));
                Console.WriteLine("Responding with object:");
                Console.WriteLine(reply);

                outputQueue.Send(reply, location);

                messageQueue.BeginReceive();
            });
            inputChannel.BeginReceive();
        }
    }
}
