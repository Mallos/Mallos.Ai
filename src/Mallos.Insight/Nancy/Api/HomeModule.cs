namespace Mallos.Insight.Nancy.Api
{
    using global::Nancy;

    class HomeModule : NancyModule
    {
        public HomeModule(IAppConfiguration appConfig)
        {
            Get("/api", args => "Hello from Mallos.Insight");
        }
    }
}
