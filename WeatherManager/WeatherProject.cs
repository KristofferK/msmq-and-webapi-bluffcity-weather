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

        private static WeatherManager weatherManager = new WeatherManager();
        static void Main(string[] args)
        {
            airTrafficControlCenterInput = MessageQueueGenerator.GenerateMessageQueue(MessageQueueGenerator.AirTrafficControlCenter_To_Weather);
            airTrafficControlCenterOutput = MessageQueueGenerator.GenerateMessageQueue(MessageQueueGenerator.Weather_To_AirTrafficControlCenter);

            Console.Title = "Weather project";

            ReceiveInputFromAirTrafficControlCenter();

            Console.ReadLine();
        }

        private static void ReceiveInputFromAirTrafficControlCenter()
        {
            airTrafficControlCenterInput.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            airTrafficControlCenterInput.ReceiveCompleted += new ReceiveCompletedEventHandler(HandleInputFromAirTrafficControlCenter);
            airTrafficControlCenterInput.BeginReceive();
        }

        private static void HandleInputFromAirTrafficControlCenter(object source, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue messageQueue = (MessageQueue)source;
            var message = messageQueue.EndReceive(asyncResult.AsyncResult);
            var location = (string)message.Body;
            Console.WriteLine("Received query from Air Traffic Control Center: " + location);

            var reply = Forecast.GenerateForecastAirTrafficControlCenter(weatherManager.GetForecast(location));
            Console.WriteLine("Answering with:");
            Console.WriteLine(reply);

            airTrafficControlCenterOutput.Send(reply, location);

            messageQueue.BeginReceive();
        }
    }
}
