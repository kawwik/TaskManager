using Spectre.Console;
using Spectre.Console.Cli;
using TaskManager.Entities;
using TaskManager.Services;

namespace TaskManager.Client.Commands {
    public class TodayCommand : Command<TodayCommand.Settings> {
        public class Settings : CommandSettings { }

        private readonly ITaskManager _taskManager;
        private readonly IUserInterface _userInterface;
        private readonly ITaskPrinter _taskPrinter;

        public TodayCommand(ITaskManager taskManager, IUserInterface userInterface, ITaskPrinter taskPrinter) {
            _taskManager = taskManager;
            _userInterface = userInterface;
            _taskPrinter = taskPrinter;
        }

        public override int Execute(CommandContext context, Settings settings) {
            var taskTree = new Tree("Expiring today");
            foreach (Task task in _taskManager.ExpiringTodayTasks()) {
                taskTree.AddNode(_taskPrinter.MakeTaskNode(task));
            }
            return 0;
        }
    }
}