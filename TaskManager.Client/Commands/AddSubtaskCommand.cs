using System;
using System.ComponentModel;
using Spectre.Console.Cli;
using TaskManager.Entities;
using TaskManager.Services;

namespace TaskManager.Client.Commands {
    public class AddSubtaskCommand : Command<AddSubtaskCommand.Settings> {
        public class Settings : CommandSettings {
            [CommandArgument(0, "[taskId]")]
            public int TaskId { get; init; }
            
            [CommandArgument(1, "[subtaskName]")]
            public string SubtaskName { get; init; }
        }

        private readonly ITaskManager _taskManager;
        private readonly IUserInterface _userInterface;

        public AddSubtaskCommand(ITaskManager taskManager, IUserInterface userInterface) {
            _taskManager = taskManager;
            _userInterface = userInterface;
        }

        public override int Execute(CommandContext context, Settings settings) {
            _taskManager.GetTask(new Id(settings.TaskId)).AddSubtask(settings.SubtaskName);
            _userInterface.ShowMessage("Subtask added.");
            return 0;
        }
    }
}