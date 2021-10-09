using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TaskManager.Services;
using TaskManager.Tools;

namespace TaskManager.Entities {
    public class TaskGroup {
        private readonly List<Task> _tasks = new List<Task>();

        public TaskGroup(string groupName) {
            Name = groupName;
        }

        public string Name { get; }
        public ReadOnlyCollection<Task> Tasks => _tasks.AsReadOnly();

        internal void AddToGroup(Task task) {
            _tasks.Add(task);
        }

        public Task FindTask(string taskName) {
            return _tasks
                .Find(task => task.Name == taskName);
        }

        public Task GetTask(TaskId taskId) {
            Task task = _tasks
                .Find(task => task.Id.GetIntId() == taskId.GetIntId());

            return task ?? throw new TaskManagerException($"Task with ID {taskId} does not exist.");
        }

        /// <returns>Deleted task.</returns>
        internal bool DeleteFromGroup(TaskId taskId) {
            Task task = GetTask(taskId);
            return _tasks.Remove(task);
        }

        public List<Task> CompletedTasks() {
            return _tasks
                .FindAll(task => task.IsCompleted);
        }
    }
}