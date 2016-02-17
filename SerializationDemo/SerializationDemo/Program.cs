using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SerializationDemo
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime Day { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DataContract();
            JsonSerialization();
        }

        private static async void JsonSerialization()
        {
            string json = JsonConvert.SerializeObject(GetSamplePerson());
            Console.WriteLine(json);

            using (HttpClient client = new HttpClient())
            {

                var response = await client.PostAsync("http://server", new StringContent(json));


                response.EnsureSuccessStatusCode();
            }

            Person p = JsonConvert.DeserializeObject<Person>(json);
            Console.WriteLine(p.FirstName);
        }

        private static async void DataContract()
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(Person));
            Stream stream = new MemoryStream();
            GZipStream zipStream = new GZipStream(stream, CompressionMode.Compress);



            serializer.WriteObject(stream, GetSamplePerson());
            stream.Seek(0, SeekOrigin.Begin);
            HttpClient client = new HttpClient();
            await client.PostAsync("server", new StreamContent(zipStream));
        }

        public static Person GetSamplePerson()
        {
            return new Person
            {
                FirstName = "Donald",
                LastName = "Duck",
                Day = DateTime.Today
            };
        }
    }
}
