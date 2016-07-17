using System;
using Microsoft.Owin.Hosting;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace apistation.owin
{
    public static class Extensions
    {
        public static string ReadAsString(this Stream stream)
        {
            // convert stream to string
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        public static Task<string> ReadAsStringAsync(this Stream stream)
        {
            // convert stream to string
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEndAsync();
        }

        public static JObject ToJObject(this Stream stream)
        {
            return JObject.Parse(stream.ReadAsString());
        }
    }

    public class Program
    {
        /// <summary>
        /// Main Program Entry Points
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string baseUrl = "http://127.0.0.1:9908";
            using (WebApp.Start<ApiStartup>(baseUrl))
            {
                Console.WriteLine("Ready.");
                Console.ReadKey();
            }
        }
    }
}
