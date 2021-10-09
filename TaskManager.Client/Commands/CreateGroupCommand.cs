using System;
using Spectre.Console.Cli;
using TaskManager.Entities;
using TaskManager.Services;

namespace TaskManager.Client.Commands {
    public class CreateGroupCommand : Command<CreateGroupCommand.Settings> {
        public class Settings : CommandSettings {
            [CommandArgument(0, "[groupName]")]
            public string GroupName { get; init; }
        }

        private readonly ITaskManager _taskManager;
        private readonly IUserInterface _userInterface;

        public CreateGroupCommand(ITaskManager taskManager, IUserInterface userInterface) {
            _taskManager = taskManager;
            _userInterface = userInterface;
        }

        public override int Execute(CommandContext context, Settings settings) {
            _taskManager.CreateGroup(settings.GroupName);
            _userInterface.ShowMessage("Group created.");
            return 0;
        }
    }
}