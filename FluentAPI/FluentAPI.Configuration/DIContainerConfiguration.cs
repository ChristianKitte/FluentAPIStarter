using Autofac;

namespace FluentAPI.Configuration
{
    public static class DIContainerConfiguration
    {
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