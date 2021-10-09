using System;
using Spectre.Console.Cli;
using TaskManager.Services;

namespace TaskManager.Client.Commands {
    public class CompleteTaskCommand : Command<CompleteTaskCommand.Settings> {
        public class Settings : CommandSettings {
            [CommandArgument(0, "[taskId]")]
            public int TaskId { get; init; }
        }

        private readonly ITaskManager _taskManager;
        private readonly IUserInterface _userInterface;

        public CompleteTaskCommand(ITaskManager taskManager, IUserInterface userInterface) {
            _taskManager = taskManager;
            _userInterface = userInterface;
        }

        public override int Execute(CommandContext context, Settings settings) {
            _taskManager.GetTask(new Id(settings.TaskId)).Complete();
            _userInterface.ShowMessage("Task completed.");
            return 0;
        }
    }
}