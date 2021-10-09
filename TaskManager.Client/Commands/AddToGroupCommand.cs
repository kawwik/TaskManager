using System;
using Spectre.Console.Cli;
using TaskManager.Entities;
using TaskManager.Services;

namespace TaskManager.Client.Commands {
    public class AddToGroupCommand : Command<AddToGroupCommand.Settings> {
        public class Settings : CommandSettings {
            [CommandArgument(0, "[groupName]")]
            public string GroupName { get; init; }
            
            [CommandArgument(1, "[taskId]")]
            public int TaskId { get; init; }
        }

        private readonly ITaskManager _taskManager;
        private readonly IUserInterface _userInterface;

        public AddToGroupCommand(ITaskManager taskManager, IUserInterface userInterface) {
            _taskManager = taskManager;
            _userInterface = userInterface;
        }

        public override int Execute(CommandContext context, Settings settings) {
            _taskManager.AddToGroup(new Id(settings.TaskId), settings.GroupName);
            _userInterface.ShowMessage("Task added to the group.");
            return 0;
        }
    }
}