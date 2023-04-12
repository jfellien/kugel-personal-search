namespace Kugel.StaffSearch.Api.FluentWebAppBuilder
{
    public interface IConfigureWebApplication :
        IConfigureForwardHeader,
        IConfigureCors,
        IConfigureAuthorization,
        IConfigureApplicationLogging,
        IConfigureApplicationTelemetryLogging,
        IConfigureApplicationServices,
        IConfigureDatabase,
        IConfigureHttpClients,
        IConfigureControllers,
        IConfigureOpenApi,
        IApplyWebApplicationConfiguration
    {

    }
}
