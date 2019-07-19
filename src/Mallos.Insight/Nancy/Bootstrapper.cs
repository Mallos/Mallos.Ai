namespace Mallos.Insight.Nancy
{
    using System.Collections.Generic;
    using System.Linq;
    using global::Nancy;
    using global::Nancy.TinyIoc;
    using global::Nancy.Bootstrapper;

    class Bootstrapper : DefaultNancyBootstrapper
    {
        private readonly IAppConfiguration appConfig;

        public Bootstrapper()
        {
        }
        
        public Bootstrapper(IAppConfiguration appConfig)
        {
            this.appConfig = appConfig;
        }

        protected override IEnumerable<ModuleRegistration> Modules
        {
            get
            {
                return base.Modules.Concat(new[]
                {
                    new ModuleRegistration(typeof(Modules.HomeModule))
                });
            }
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register<IAppConfiguration>(appConfig);
        }
    }   
}