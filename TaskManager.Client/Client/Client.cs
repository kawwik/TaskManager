using Spectre.Console.Cli;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Client.Commands;
using TaskManager.Services;
using TaskManager.Entities;

namespace TaskManager.Client {
    public class Client {
        private ServiceCollection _services = new ServiceCollection();
        private CommandApp _app;
        
        public Client Configure() {
            _services.AddSingleton(typeof(ITaskManager), new Services.TaskManager());
            _services.AddScoped(typeof(ITaskPrinter), typeof(TaskPrinter));
            _services.AddScoped(typeof(IUserInterface), typeof(AnsiConsoleUI));

            _app = new CommandApp(new TypeRegistrar(_services));
            _app.Configure(config => {
                config.AddCommand<AddTaskCommand>("/add");
                config.AddCommand<AllTasksCommand>("/all");
                config.AddCommand<DeleteTaskCommand>("/delete");
                config.AddCommand<CompleteTaskCommand>("/complete");
                config.AddCommand<CompletedTasksCommand>("/completed");
                config.AddCommand<TodayCommand>("/today");
                config.AddCommand<CreateGroupCommand>("/create-group");
                config.AddCommand<DeleteGroupCommand>("/delete-group");
                config.AddCommand<AddToGroupCommand>("/add-to-group");
                config.AddCommand<DeleteFromGroupCommand>("/delete-from-group");
                config.AddCommand<AddSubtaskCommand>("/add-subtask");
            });

            return this;
        }
        
        public void Run() {
            IUserInterface userInterface = _services.BuildServiceProvider().GetService<IUserInterface>();
            while (true) {
                _app.Run(userInterface.ReadLine().Split());
            }
        }
    }
}