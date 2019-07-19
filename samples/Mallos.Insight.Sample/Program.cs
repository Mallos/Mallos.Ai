namespace Mallos.Insight.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new Mallos.Insight.Server();
            server.Start(address: "localhost", port: 5001);

            // var monster = new Monster();
            // server.Register(monster);
        }
    }
}
