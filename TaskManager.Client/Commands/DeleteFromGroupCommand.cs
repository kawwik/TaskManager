using System;
using Spectre.Console.Cli;
using TaskManager.Entities;
using TaskManager.Services;

namespace TaskManager.Client.Commands {
    public class DeleteFromGroupCommand : Command<DeleteFromGroupCommand.Settings> {
        public class Settings : CommandSettings {
            [CommandArgument(0, "[groupName]")]
            public string GroupName { get; init; }
            
            [CommandArgument(1, "[taskId]")]
            public int TaskId { get; init; }
        }

        private readonly ITaskManager _taskManager;
        private readonly IUserInterface _userInterface;

        public DeleteFromGroupCommand(ITaskManager taskManager, IUserInterface userInterface) {
            _taskManager = taskManager;
            _userInterface = userInterface;
        }

        public override int Execute(CommandContext context, Settings settings) {
            _taskManager.DeleteFromGroup(new TaskId(settings.TaskId), settings.GroupName);
            _userInterface.ShowMessage("Task deleted from the group.");
            return 0;
        }
    }
}