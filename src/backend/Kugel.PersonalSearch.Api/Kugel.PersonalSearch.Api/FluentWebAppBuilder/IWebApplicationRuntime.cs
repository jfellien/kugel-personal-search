namespace Kugel.PersonalSearch.Api.FluentWebAppBuilder
{
    public interface IWebApplicationRuntime:
        IUseForwardHeaders,
        IUseExceptionHandler,
        IUseCors,
        IUseAuthentication,
        IUseAuthorization,
        IUseController,
        IUseOpenApi,
        IEnableDatabase,
        IRunWebApplication
    {

    }
}
