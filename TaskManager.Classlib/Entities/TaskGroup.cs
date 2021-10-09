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

        public Task GetTask(Id id) {
            Task task = _tasks
                .Find(task => task.Id.GetIntId() == id.GetIntId());

            return task ?? throw new TaskManagerException($"Task with ID {id} does not exist.");
        }

        /// <returns>Deleted task.</returns>
        internal bool DeleteFromGroup(Id id) {
            Task task = GetTask(id);
            return _tasks.Remove(task);
        }

        public List<Task> CompletedTasks() {
            return _tasks
                .FindAll(task => task.IsCompleted);
        }
    }
}