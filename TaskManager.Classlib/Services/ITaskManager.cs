using System.Collections.Generic;
using System.Collections.ObjectModel;
using TaskManager.Entities;

namespace TaskManager.Services {
    public interface ITaskManager {
        ReadOnlyCollection<Task> Tasks { get; }

        void AddTask(Task task);
        Task FindTask(string taskName);
        Task GetTask(TaskId taskId);
        void CreateGroup(string groupName);
        void DeleteGroup(string groupName);
        void AddToGroup(TaskId taskId, string groupName);
        void DeleteFromGroup(TaskId taskId, string groupName);
        List<Task> CompletedTasks();
        List<Task> ExpiringTodayTasks();
    }
}