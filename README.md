# msmq-and-webapi-bluffcity-weather
Multiple departments and sections at Bluff City Airport are interested in the weather conditions, which we must fetch from an external web API.

At the airport the following departments are interested in information about the weather
* Air Traffic Control Center
* Airport Information Center
* Airline Companies

Information needed by Air Traffic Control Center is
* name of City
* coordinates
* country
* temperature
* humidity
* pressure
* wind
* clouds
* visibility

Information needed by Airport Information Center is
* name of City
* country
* sunrise
* sunset
* temperature

Information needed by Airline Companies is
* name of City
* country
* temperature
* clouds

There are four Airline Companies operating at Bluff City Airport
* Scandinavian Airline Service (SAS)
* South West Airline
* KLM
* British Airways

KLM needs to receive information as a String
SAS needs to receive the information as a Class
South West and British Airways accept the information as an XML document. 
