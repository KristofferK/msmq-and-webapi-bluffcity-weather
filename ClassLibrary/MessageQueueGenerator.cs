using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public static class MessageQueueGenerator
    {
        public static string AirTrafficControlCenter_To_Weather { get; private set; } = @".\Private$\Weather_ATCC_To_Weather_Channel";
        public static string AirportInformationCenter_To_Weather { get; private set; } = @".\Private$\Weather_AIC_To_Weather_Channel";
        public static string Airline_SAS_To_Weather { get; private set; } = @".\Private$\Weather_Airline_SAS_To_Weather_Channel";
        public static string Airline_SWA_To_Weather { get; private set; } = @".\Private$\Weather_Airline_SWA_To_Weather_Channel";
        public static string Airline_KLM_To_Weather { get; private set; } = @".\Private$\Weather_Airline_KLM_To_Weather_Channel";
        public static string Airline_BritishAirways_To_Weather { get; private set; } = @".\Private$\Weather_Airline_BritishAirways_To_Weather_Channel";

        public static string Weather_To_AirTrafficControlCenter { get; private set; } = @".\Private$\Weather_Weather_To_ATCC_Channel";
        public static string Weather_To_AirportInformationCenter { get; private set; } = @".\Private$\Weather_Weather_To_AIC_Channel";
        public static string Weather_To_Airline_SAS { get; private set; } = @".\Private$\Weather_Weather_To_Airline_SAS_Channel";
        public static string Weather_To_Airline_SWA { get; private set; } = @".\Private$\Weather_Weather_To_Airline_SWA_Channel";
        public static string Weather_To_Airline_KLM { get; private set; } = @".\Private$\Weather_Weather_To_Airline_KLM_Channel";
        public static string Weather_To_Airline_BritishAirways { get; private set; } = @".\Private$\Weather_Weather_To_Airline_BritishAirways_Channel";

        public static MessageQueue GenerateMessageQueue(string messageQueueName)
        {
            if (!MessageQueue.Exists(messageQueueName))
            {
                MessageQueue.Create(messageQueueName);
            }
            return new MessageQueue(messageQueueName)
            {
                Label = "I'm located at " + messageQueueName
            };
        }
    }
}