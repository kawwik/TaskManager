using Spectre.Console.Rendering;
using TaskManager.Entities;

namespace TaskManager.Client {
    public interface ITaskPrinter {
        IRenderable MakeTaskNode(Task task);
        IRenderable MakeGroupNode(TaskGroup taskGroup);
    }
}