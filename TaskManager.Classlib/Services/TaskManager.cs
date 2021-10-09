using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using TaskManager.Entities;
using TaskManager.Tools;

namespace TaskManager.Services {
    public class TaskManager : ITaskManager {
        private readonly List<Task> _tasks = new List<Task>();
        private readonly List<TaskGroup> _groups = new List<TaskGroup>();

        public TaskManager() { }

        public ReadOnlyCollection<Task> Tasks => _tasks.AsReadOnly();
        public ReadOnlyCollection<TaskGroup> Groups => _groups.AsReadOnly();
        
        public void AddTask(Task task) {
            if (FindTask(task.Name) != null)
                throw new TaskManagerException("Task already exists.");
            if (_tasks.Exists(addedTask => addedTask.Id.GetIntId() == task.Id.GetIntId()))
                throw new TaskManagerException("This ID is already taken.");

            _tasks.Add(task);
        }

        public void DeleteTask(Id id) {
            GetTask(id);
            _tasks.RemoveAll(task => task.Id.GetIntId() == id.GetIntId());
            foreach (TaskGroup taskGroup in _groups) {
                taskGroup.DeleteFromGroup(id);
            }
        }

        /// <summary>
        /// Returns tasks from all group and also tasks without a group in one list.
        /// </summary>
        private List<Task> AllTasks() {
            return _groups.SelectMany(taskGroup => taskGroup.Tasks)
                .Concat(_tasks)
                .ToList();
        }

        public Task FindTask(string taskName) {
            return AllTasks()
                .Find(task => task.Name == taskName);
        }

        public Task GetTask(Id id) {
            Task task = AllTasks()
                .Find(task => task.Id.GetIntId() == id.GetIntId());

            return task ?? throw new TaskManagerException("Incorrect task ID.");
        }

        private TaskGroup FindGroup(string groupName) {
            return _groups
                .Find(taskGroup => taskGroup.Name == groupName);
        }

        public TaskGroup GetGroup(string groupName) {
            TaskGroup taskGroup = _groups
                .Find(taskGroup => taskGroup.Name == groupName);

            return taskGroup ?? throw new TaskManagerException("Group doesn't exist");
        }

        public List<Task> CurrentTasks() {
            return _tasks
                .FindAll(task => !task.IsCompleted);
        }

        public void CreateGroup(string groupName) {
            if (FindGroup(groupName) != null)
                throw new TaskManagerException("Group already exists.");

            _groups.Add(new TaskGroup(groupName));
        }

        public void DeleteGroup(string groupName) {
            TaskGroup taskGroup = GetGroup(groupName);
            _tasks.AddRange(taskGroup.Tasks);
            _groups.Remove(taskGroup);
        }

        public void AddToGroup(Id id, string groupName) {
            TaskGroup taskGroup = GetGroup(groupName);
            Task task = GetTask(id);
            taskGroup.AddToGroup(task);
            _tasks.Remove(task);
        }

        public void DeleteFromGroup(Id id, string groupName) {
            TaskGroup taskGroup = GetGroup(groupName);
            Task task = taskGroup.GetTask(id);
            taskGroup.DeleteFromGroup(id);
            _tasks.Add(task);
        }

        public List<Task> CompletedTasks() {
            return _tasks
                .FindAll(task => task.IsCompleted);
        }

        public List<Task> ExpiringTodayTasks() {
            return _tasks
                .FindAll(task => task.Deadline.ExpiresToday());
        }
    }
}