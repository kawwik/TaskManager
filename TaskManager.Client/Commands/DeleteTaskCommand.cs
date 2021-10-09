using System;
using Spectre.Console.Cli;
using TaskManager.Entities;
using TaskManager.Services;

namespace TaskManager.Client.Commands {
    public class DeleteTaskCommand : Command<DeleteTaskCommand.Settings> {
        public class Settings : CommandSettings {
            [CommandArgument(0, "[taskId]")]
            public int TaskId { get; init; }
        }

        private readonly ITaskManager _taskManager;
        private readonly IUserInterface _userInterface;

        public DeleteTaskCommand(ITaskManager taskManager, IUserInterface userInterface) {
            _taskManager = taskManager;
            _userInterface = userInterface;
        }

        public override int Execute(CommandContext context, Settings settings) {
            _taskManager.DeleteTask(new TaskId(settings.TaskId));
            _userInterface.ShowMessage("Task successfully deleted.");
            return 0;
        }
    }
}