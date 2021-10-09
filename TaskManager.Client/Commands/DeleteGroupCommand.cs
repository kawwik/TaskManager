using System;
using Spectre.Console.Cli;
using TaskManager.Entities;
using TaskManager.Services;

namespace TaskManager.Client.Commands {
    public class DeleteGroupCommand : Command<DeleteGroupCommand.Settings> {
        public class Settings : CommandSettings {
            [CommandArgument(0, "[groupName]")]
            public string GroupName { get; init; }
        }

        private readonly ITaskManager _taskManager;
        private readonly IUserInterface _userInterface;

        public DeleteGroupCommand(ITaskManager taskManager, IUserInterface userInterface) {
            _taskManager = taskManager;
            _userInterface = userInterface;
        }

        public override int Execute(CommandContext context, Settings settings) {
            _taskManager.DeleteGroup(settings.GroupName);
            _userInterface.ShowMessage("Group deleted.");
            return 0;
        }
    }
}