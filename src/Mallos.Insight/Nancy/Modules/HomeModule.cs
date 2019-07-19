namespace Mallos.Insight.Nancy.Modules
{
    using Mallos.Insight.Nancy.Models;
    using global::Nancy;

    class HomeModule : NancyModule
    {
        public HomeModule(IAppConfiguration appConfig)
        {
            Get("/", args => "Hello from Nancy running on CoreCLR");

            Get("/conneg/{name}", args => new Person() { Name = args.name });

            Get("/smtp", args =>
            {
                return new
                {
                    appConfig.Smtp.Server,
                    appConfig.Smtp.User,
                    appConfig.Smtp.Pass,
                    appConfig.Smtp.Port
                };
            });
        }
    }
}
