using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;

namespace Lab09_LINQ
{
    class Program
    {
        public static string path = "../../../data.json";
        public static string readFile = File.ReadAllText(path);
        public static JObject json = JObject.Parse(readFile);
        public static Base listOfPlaces = JsonConvert.DeserializeObject<Base>(readFile);

        static void Main(string[] args)
        {
            Console.WriteLine("All Neighborhoods:");
            GetAllNeighborhoods();
            Console.WriteLine("All Named Neighborhoods:");
            FilterNamelessNeighborhoods(listOfPlaces);
            Console.WriteLine("All Distinct Neighborhoods:");
            FilterDuplicateNeighborhoods(listOfPlaces);
            Console.WriteLine("All queries in one:");
            ChainedQuery(listOfPlaces);
            Console.WriteLine("Query using lambda:");
            QueryWithMethod(listOfPlaces);
        }

        static void GetAllNeighborhoods()
        {
            int neighborhoodCount = 1;
            foreach(Feature place in listOfPlaces.features)
            {
                Console.WriteLine($"{neighborhoodCount}. {place.properties.neighborhood}");
                neighborhoodCount++;
            }
        }

        static void FilterNamelessNeighborhoods(Base places)
        {
            int neighborhoodCount = 1;
            var query = from feature in places.features
                        select feature.properties.neighborhood;
            foreach(var feature in query)
            {
                if(feature != null)
                {
                    Console.WriteLine($"{neighborhoodCount}. {feature}");
                    neighborhoodCount++;
                }
            }
        }

        static void FilterDuplicateNeighborhoods(Base places)
        {
            var query = from feature in places.features
                        where feature.properties.neighborhood != ""
                        select feature.properties.neighborhood;
            var noDuplicates = query.Distinct();

            int neighborhoodCount = 1;
            foreach(var feature in noDuplicates)
            {
                Console.WriteLine($"{neighborhoodCount}. {feature}");
                neighborhoodCount++;
            }
        }

        static void ChainedQuery(Base places)
        {
            var query = from feature in places.features
                        where feature.properties.neighborhood != ""
                        select feature.properties.neighborhood.Distinct();
            int neighborhoodCount = 1;
            foreach(var feature in query)
            {
                Console.WriteLine($"{neighborhoodCount}. {feature}");
                neighborhoodCount++;
            }
        }

        static void QueryWithMethod(Base places)
        {
            int neighborhoodCount = 1;
            var query = places.features.Select(feature => feature.properties.neighborhood);

            foreach(var feature in query)
            {
                Console.WriteLine($"{neighborhoodCount}. {feature}");
            }
        }
    }
}
