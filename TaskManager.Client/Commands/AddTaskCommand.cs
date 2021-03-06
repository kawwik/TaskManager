using System;
using System.ComponentModel;
using Spectre.Console.Cli;
using TaskManager.Entities;
using TaskManager.Services;

namespace TaskManager.Client.Commands {
    public class AddTaskCommand : Command<AddTaskCommand.Settings> {
        public class Settings : CommandSettings {
            [CommandArgument(0, "[taskName]")]
            public string TaskName { get; init; }
            
            [CommandArgument(1, "[deadLine]")]
            public DateTime Deadline { get; init; }
        }

        private readonly ITaskManager _taskManager;
        private readonly IUserInterface _userInterface;
        private readonly IdGenerator _idGenerator;

        public AddTaskCommand(ITaskManager taskManager, IUserInterface userInterface, IdGenerator idGenerator) {
            _taskManager = taskManager;
            _userInterface = userInterface;
            _idGenerator = idGenerator;
        }

        public override int Execute(CommandContext context, Settings settings) {
            _taskManager.AddTask(new Task(settings.TaskName, new Deadline(settings.Deadline), _idGenerator.NewId()));
            _userInterface.ShowMessage("Task successfully added.");
            return 0;
        }
    }
}