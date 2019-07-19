namespace Mallos.Insight
{
    using Mallos.Insight.Nancy;
    using Microsoft.AspNetCore.Hosting;
    using System.IO;

    public class Server
    {
        public Server()
        {

        }

        public void Start(string address = "localhost", int port = 5001)
        {
            var uri = $"http://{address}:{port}/";

            var host = new WebHostBuilder()
                .UseUrls(uri)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
