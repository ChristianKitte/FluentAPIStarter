using Autofac;

namespace FluentAPI.Configuration
{
    /// <summary>
    /// Hilfsklasse für die Konfiguration des DI Containers
    /// </summary>
    public static class DIContainerConfiguration
    {
        /// <summary>
        /// Konfiguriert den DI Container und liefert einen IContainer zurück
        /// </summary>
        /// <returns></returns>
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<FluentAPI.StartUp>();
            builder.RegisterType<FluentAPI.Configuration.AppConfiguration>()
                .As<FluentAPI.Configuration.IConfiguration>();
            builder.RegisterType<FluentAPI.FileLineWriter>()
                .As<FluentAPI.IFileLineWriter>();

            return builder.Build();
        }
    }
}