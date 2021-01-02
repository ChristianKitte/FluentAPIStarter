using Autofac;

namespace FluentAPI
{
    /// <summary>
    /// 
    /// </summary>
    class Program
    {
        /// <summary>
        /// Der Einstiegspunkt der Anwendung
        /// </summary>
        /// <param name="args">Die Übergabeparameter beim Programmstart</param>
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