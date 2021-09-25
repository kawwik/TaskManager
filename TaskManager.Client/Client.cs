using Spectre.Console.Cli;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Services;
using TaskManager.Entities;

namespace TaskManager.Client {
    public class Client {
        public void Run() {
            var services = new ServiceCollection();
            services.AddSingleton(typeof(ITaskManager), new Services.TaskManager());
            services.AddScoped(typeof(IUserInterface), typeof(AnsiConsoleUI));
            ServiceProvider provider = services.BuildServiceProvider();
            
            var app = new CommandApp(new TypeRegistrar(services));
            IUserInterface userInterface = provider.GetService<IUserInterface>();
            while (true) {
                app.Run(userInterface.ReadLine().Split());
            }
        }
    }
}