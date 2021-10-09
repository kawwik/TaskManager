using System.Collections.Generic;
using System.Collections.ObjectModel;
using TaskManager.Entities;

namespace TaskManager.Services {
    public interface ITaskManager {
        ReadOnlyCollection<Task> Tasks { get; }
        ReadOnlyCollection<TaskGroup> Groups { get; }

        void AddTask(Task task);
        void DeleteTask(Id id);
        Task FindTask(string taskName);
        Task GetTask(Id id);
        void CreateGroup(string groupName);
        void DeleteGroup(string groupName);
        void AddToGroup(Id id, string groupName);
        void DeleteFromGroup(Id id, string groupName);
        TaskGroup GetGroup(string groupName);
        List<Task> CurrentTasks();
        List<Task> CompletedTasks();
        List<Task> ExpiringTodayTasks();
    }
}