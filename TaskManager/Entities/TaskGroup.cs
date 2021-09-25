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

        public void AddToGroup(Task task) {
            _tasks.Add(task);
        }

        public Task FindTask(string taskName) {
            return _tasks
                .Find(task => task.Name == taskName);
        }
        
        public Task GetTask(TaskId taskId) {
            Task task = _tasks
                .Find(task => task.Id == taskId);

            return task ?? throw new NotImplementedException($"Task with ID {taskId} does not exist.");
        }

        /// <returns>Deleted task.</returns>
        public Task DeleteFromGroup(TaskId taskId) {
            Task task = GetTask(taskId);
            _tasks.Remove(task);
            return task;
        }

        public List<Task> CompletedTasks() {
            return _tasks
                .FindAll(task => task.IsCompleted);
        }
    }
}