using System.Linq;
using Spectre.Console;
using Spectre.Console.Rendering;
using TaskManager.Entities;

namespace TaskManager.Client {
    public class TaskPrinter : ITaskPrinter {
        public IRenderable MakeTaskNode(Task task) {
            Tree taskNode = new Tree($"{task.Id.GetIntId()}. {task.Name} until {task.Deadline.Date.Date}" +
                                     (task.Subtasks.Count == 0 
                                         ? "" : $" {task.CompletedSubtasks()}/{task.Subtasks.Count}") +
                                     (task.IsCompleted ? " (completed)" : ""));
            
            foreach (Subtask subtask in task.Subtasks) {
                taskNode.AddNode($"{subtask.Name}");
            }
            
            return taskNode;
        }

        public IRenderable MakeGroupNode(TaskGroup taskGroup) {
            Tree taskNode = new Tree(taskGroup.Name);
            foreach (Task task in taskGroup.Tasks) {
                taskNode.AddNode(MakeTaskNode(task));
            }
            
            return taskNode;
        }
    }
}