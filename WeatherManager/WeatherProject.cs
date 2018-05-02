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
        private static WeatherManager weatherManager = new WeatherManager();
        static void Main(string[] args)
        {
            Console.Title = "Weather project. Receives queries and returns corresponding datatype.";

            var airTrafficControlCenterInput = MessageQueueGenerator.GenerateMessageQueue(MessageQueueGenerator.AirTrafficControlCenter_To_Weather);
            var airTrafficControlCenterOutput = MessageQueueGenerator.GenerateMessageQueue(MessageQueueGenerator.Weather_To_AirTrafficControlCenter);

            var airportInformationCenterInput = MessageQueueGenerator.GenerateMessageQueue(MessageQueueGenerator.AirportInformationCenter_To_Weather);
            var airportInformationCenterOutput = MessageQueueGenerator.GenerateMessageQueue(MessageQueueGenerator.Weather_To_AirportInformationCenter);

            var airlineSASInput = MessageQueueGenerator.GenerateMessageQueue(MessageQueueGenerator.Airline_SAS_To_Weather);
            var airlineSASOutput = MessageQueueGenerator.GenerateMessageQueue(MessageQueueGenerator.Weather_To_Airline_SAS);

            var airlineKLMInput = MessageQueueGenerator.GenerateMessageQueue(MessageQueueGenerator.Airline_KLM_To_Weather);
            var airlineKLMOutput = MessageQueueGenerator.GenerateMessageQueue(MessageQueueGenerator.Weather_To_Airline_KLM);

            ReceiveInputFromAirTrafficControlCenter(airTrafficControlCenterInput, airTrafficControlCenterOutput);
            ReceiveInputFromAirportInformationCenter(airportInformationCenterInput, airportInformationCenterOutput);
            ReceiveInputFromAirlineSAS(airlineSASInput, airlineSASOutput);
            ReceiveInputFromAirlineKLM(airlineKLMInput, airlineKLMOutput);

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

                outputQueue.Send(reply, location);

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

        private static void ReceiveInputFromAirlineSAS(MessageQueue inputChannel, MessageQueue outputQueue)
        {
            inputChannel.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            inputChannel.ReceiveCompleted += ((object source, ReceiveCompletedEventArgs asyncResult) =>
            {
                MessageQueue messageQueue = (MessageQueue)source;
                var message = messageQueue.EndReceive(asyncResult.AsyncResult);
                var location = (string)message.Body;
                Console.WriteLine("Received query from Airline SAS: " + location);

                var reply = Forecast.GenerateForecastAirlineCompany(weatherManager.GetForecast(location));
                Console.WriteLine("Responding with object:");
                Console.WriteLine(reply);

                outputQueue.Send(reply, location);

                messageQueue.BeginReceive();
            });
            inputChannel.BeginReceive();
        }

        private static void ReceiveInputFromAirlineKLM(MessageQueue inputChannel, MessageQueue outputQueue)
        {
            inputChannel.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            inputChannel.ReceiveCompleted += ((object source, ReceiveCompletedEventArgs asyncResult) =>
            {
                MessageQueue messageQueue = (MessageQueue)source;
                var message = messageQueue.EndReceive(asyncResult.AsyncResult);
                var location = (string)message.Body;
                Console.WriteLine("Received query from Airline KLM: " + location);

                var reply = Forecast.GenerateForecastAirlineCompany(weatherManager.GetForecast(location)).ToString();
                Console.WriteLine("Responding with string:");
                Console.WriteLine(reply);

                outputQueue.Send(reply, location);

                messageQueue.BeginReceive();
            });
            inputChannel.BeginReceive();
        }
    }
}
