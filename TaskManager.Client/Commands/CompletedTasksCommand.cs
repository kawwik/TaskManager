using System;
using System.Collections.Generic;
using Spectre.Console;
using Spectre.Console.Cli;
using TaskManager.Entities;
using TaskManager.Services;

namespace TaskManager.Client.Commands {
    public class CompletedTasksCommand : Command<CompletedTasksCommand.Settings> {
        public class Settings : CommandSettings {
            [CommandOption("-g | --group")] 
            public string GroupName { get; init; }
        }

        private readonly ITaskManager _taskManager;
        private readonly IUserInterface _userInterface;
        private readonly ITaskPrinter _taskPrinter;

        public CompletedTasksCommand(ITaskManager taskManager, IUserInterface userInterface, ITaskPrinter taskPrinter) {
            _taskManager = taskManager;
            _userInterface = userInterface;
            _taskPrinter = taskPrinter;
        }

        public override int Execute(CommandContext context, Settings settings) {
            Tree taskTree = new Tree("Completed tasks");
            List<Task> completedTasks = 
                String.IsNullOrEmpty(settings.GroupName)
                ? _taskManager.CompletedTasks()
                : _taskManager.GetGroup(settings.GroupName).CompletedTasks();
            
            foreach (Task task in completedTasks) {
                taskTree.AddNode(_taskPrinter.MakeTaskNode(task));
            }
            _userInterface.ShowTree(taskTree);
            return 0;
        }
    }
}