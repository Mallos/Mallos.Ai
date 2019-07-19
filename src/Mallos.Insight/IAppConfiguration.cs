namespace Mallos.Insight
{
    using Mallos.Insight.Nancy;

    interface IAppConfiguration
    {
        Logging Logging { get; }
        Smtp Smtp { get; }
    }
}
