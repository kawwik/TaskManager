﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TaskManager.Entities;
using TaskManager.Tools;

namespace TaskManager.Services {
    public class TaskManager : ITaskManager {
        private readonly List<Task> _tasks = new List<Task>();
        private readonly List<TaskGroup> _groups = new List<TaskGroup>();

        public TaskManager() { }
        
        public ReadOnlyCollection<Task> Tasks { get; }
        
        public void AddTask(Task task) {
            if (FindTask(task.Name) != null)
                throw new TaskManagerException("Task already exists.");
            _tasks.Add(task);
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

        public Task GetTask(TaskId taskId) {
            Task task = AllTasks()
                .Find(task => task.Id.GetIntId() == taskId.GetIntId());

            return task ?? throw new TaskManagerException("Incorrect task ID.");
        }

        private TaskGroup FindGroup(string groupName) {
            return _groups
                .Find(taskGroup => taskGroup.Name == groupName);
        }

        private TaskGroup GetGroup(string groupName) {
            TaskGroup taskGroup = _groups
                .Find(taskGroup => taskGroup.Name == groupName);

            return taskGroup ?? throw new TaskManagerException("Group doesn't exist");
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

        public void AddToGroup(TaskId taskId, string groupName) {
            TaskGroup taskGroup = GetGroup(groupName);
            Task task = GetTask(taskId);
            taskGroup.AddToGroup(task);
        }

        public void DeleteFromGroup(TaskId taskId, string groupName) {
            TaskGroup taskGroup = GetGroup(groupName);
            _tasks.Add(taskGroup.DeleteFromGroup(taskId));
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