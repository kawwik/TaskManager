using System;
using Spectre.Console.Cli;
using TaskManager.Services;
using TaskManager.Tools;

namespace TaskManager.Client.Commands {
    public class CompleteTaskCommand : Command<CompleteTaskCommand.Settings> {
        public class Settings : CommandSettings {
            [CommandArgument(0, "[taskId]")]
            public string TaskId { get; init; }
            
            [CommandOption("-s | --sub")]
            public bool IsSubtask { get; init; }
        }

        private readonly ITaskManager _taskManager;
        private readonly IUserInterface _userInterface;

        public CompleteTaskCommand(ITaskManager taskManager, IUserInterface userInterface) {
            _taskManager = taskManager;
            _userInterface = userInterface;
        }

        public void CompleteSubtask(string subtaskId) {
            if (!subtaskId.Contains('.') 
                || !int.TryParse(subtaskId.Split('.')[0], out int taskId)
                || !int.TryParse(subtaskId.Split('.')[1], out int subId))
                throw new TaskManagerException("Incorrect subtask ID.");

            _taskManager.GetTask(new Id(taskId)).CompleteSubtask(new Id(subId));
        }

        public void CompleteTask(string id) {
            if (!id.Contains('.') 
                || !int.TryParse(id, out int taskId))
                throw new TaskManagerException("Incorrect task ID.");
            
            _taskManager.GetTask(new Id(taskId)).Complete();
        }

        public override int Execute(CommandContext context, Settings settings) {
            if (settings.IsSubtask)
                CompleteSubtask(settings.TaskId);
            else 
                CompleteTask(settings.TaskId);

            return 0;
        }
    }
}