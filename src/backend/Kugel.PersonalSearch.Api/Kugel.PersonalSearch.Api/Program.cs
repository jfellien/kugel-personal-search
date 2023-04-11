using Kugel.PersonalSearch.Api;

KugelPersonalSearchApiApplication
        .Configure(args)
        .SetForwardHeader()
        .SetCors()
        .SetAuthorization()
        .SetApplicationLogging()
        .SetApplicationTelemetryLogging()
        .SetApplicationServices()
        .SetDatabase()
        .SetHttpClients()
        .SetControllers()
        .SetOpenApi()
    .ApplyConfigurations()
        .EnableForwardHeaders()
        .EnableExceptionHandler()
        .EnableCors()
        .EnableAuthentication()
        .EnableAuthorization()
        .EnableController()
        .EnableOpenApi()
        .EnableDatabase()
    .RunApplication();