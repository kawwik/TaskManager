using System.Collections.Generic;
using System.Collections.ObjectModel;
using TaskManager.Entities;

namespace TaskManager.Services {
    public interface ITaskManager {
        ReadOnlyCollection<Task> Tasks { get; }
        ReadOnlyCollection<TaskGroup> Groups { get; }

        void AddTask(Task task);
        void DeleteTask(TaskId taskId);
        Task FindTask(string taskName);
        Task GetTask(TaskId taskId);
        void CreateGroup(string groupName);
        void DeleteGroup(string groupName);
        void AddToGroup(TaskId taskId, string groupName);
        void DeleteFromGroup(TaskId taskId, string groupName);
        TaskGroup GetGroup(string groupName);
        List<Task> CurrentTasks();
        List<Task> CompletedTasks();
        List<Task> ExpiringTodayTasks();
    }
}