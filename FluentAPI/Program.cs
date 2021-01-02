using Autofac;

namespace FluentAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = FluentAPI.Configuration.DIContainerConfiguration.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<StartUp>();
                app.Run();
            }
        }
    }
}