using ChipAquariumMobileService.Config;
using System;
using System.Globalization;

namespace ChipAquariumMobileServiceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please specify the temperature. Please press the Q if you are finished.");

            string input = string.Empty;
            while (!(input = Console.ReadLine()).Equals("Q", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    var temperature = float.Parse(input, CultureInfo.InvariantCulture);
                    WaterTemperatureSettings.Get();
                    var client = new ChipAquariumMobileServiceClient();
                    client.InsertWaterTemperatures(temperature);

                    Console.WriteLine($"It sent the water temperature {temperature}C.");
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
    }
}
