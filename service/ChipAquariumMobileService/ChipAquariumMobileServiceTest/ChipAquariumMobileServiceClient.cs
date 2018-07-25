using ChipAquariumMobileServiceTest.DataModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace ChipAquariumMobileServiceTest
{
    public class ChipAquariumMobileServiceClient
    {
        private static readonly Uri uri = new Uri("https://chipaquariummobile.azure-mobile.net/");

        private static string applicationKey = "YhmVlElOaAQsMUoGPijjXYgGipJAiX54";

        public void InsertWaterTemperatures(float temperature)
        {
            var builder = CreateBuilder();
            builder.Path += "tables/WaterTemperatures";

            var waterTemperature = new WaterTemperature()
            {
                AquariumId = -1,
                MeasurementAt = DateTimeOffset.Now,
                Temperature = temperature
            };

            byte[] bytes = Serialize(waterTemperature);

            var request = CreateRequest(builder.Uri);
            request.Method = "POST";
            request.ContentLength = bytes.LongLength;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
            }

            Debug.WriteLine(Encoding.UTF8.GetString(bytes));

            using (var responce = request.GetResponse())
            using (var reader = new StreamReader(responce.GetResponseStream()))
            {
                string buffer = reader.ReadToEnd();
                Console.Write(buffer);
            }
        }

        private static HttpWebRequest CreateRequest(Uri uri)
        {
            var request = HttpWebRequest.CreateHttp(uri);
            request.Accept = "application/json";
            request.ContentType = "application/json";

            request.Headers.Add("X-ZUMO-APPLICATION", applicationKey);

            return request;
        }

        private static UriBuilder CreateBuilder()
        {
            return new UriBuilder(uri);
        }

        public WaterTemperature[] GetWaterTemperatures()
        {
            var builder = CreateBuilder();
            builder.Path += "tables/WaterTemperatures";

            var request = CreateRequest(builder.Uri);
            request.Method = "Get";

            using (var responce = request.GetResponse())
            {
                var serializer = CreateSerializer(typeof(WaterTemperature[]));
                using (var stream = responce.GetResponseStream())
                {
                    return (WaterTemperature[])serializer.ReadObject(stream);
                }
            }
        }

        private static byte[] Serialize(WaterTemperature value)
        {
            var serializer = CreateSerializer(typeof(WaterTemperature));
            using (var strem = new MemoryStream())
            {
                serializer.WriteObject(strem, value);
                return strem.ToArray();
            }
        }

        private static DataContractJsonSerializer CreateSerializer(Type type)
        {
            var settings = new DataContractJsonSerializerSettings
            {
                DateTimeFormat = new DateTimeFormat("yyyy-MM-ddTHH:mm:ss.fffZ")
            };

            return new DataContractJsonSerializer(type, settings);
        }
    }
}
