using System;
using Spectre.Console;
using Spectre.Console.Cli;
using TaskManager.Entities;
using TaskManager.Services;

namespace TaskManager.Client.Commands {
    public class AllTasksCommand : Command<AllTasksCommand.Settings> {
        public class Settings : CommandSettings { }

        private readonly ITaskManager _taskManager;
        private readonly IUserInterface _userInterface;
        private readonly ITaskPrinter _taskPrinter;

        public AllTasksCommand(ITaskManager taskManager, IUserInterface userInterface, ITaskPrinter taskPrinter) {
            _taskManager = taskManager;
            _userInterface = userInterface;
            _taskPrinter = taskPrinter;
        }

        public override int Execute(CommandContext context, Settings settings) {
            var taskTree = new Tree("Tasks");

            foreach (Task task in _taskManager.CurrentTasks()) {
                taskTree.AddNode(_taskPrinter.MakeTaskNode(task));
            }

            foreach (TaskGroup taskGroup in _taskManager.Groups) {
                taskTree.AddNode(_taskPrinter.MakeGroupNode(taskGroup));
            }

            foreach (Task task in _taskManager.CompletedTasks()) {
                taskTree.AddNode(_taskPrinter.MakeTaskNode(task));
            }

            _userInterface.ShowTree(taskTree);
            return 0;
        }
    }
}