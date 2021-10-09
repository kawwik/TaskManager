using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TaskManager.Services;
using TaskManager.Tools;

namespace TaskManager.Entities {
    public class Task {
        private readonly List<Subtask> _subtasks = new List<Subtask>();
        private readonly IdGenerator _idGenerator = new IdGenerator(1);

        public Task(string taskName, Deadline deadline, Id taskId) {
            Name = taskName;
            Id = taskId;
            Deadline = deadline;
        }

        public string Name { get; }
        public Id Id { get; }

        public Deadline Deadline { get; }
        public bool IsCompleted { get; private set; } = false;
        public ReadOnlyCollection<Subtask> Subtasks => _subtasks.AsReadOnly();

        public void Complete() {
            if (IsCompleted)
                throw new TaskManagerException("Task is already completed.");
            IsCompleted = true;
        }

        public void AddSubtask(string subtaskName) {
            _subtasks.Add(new Subtask(subtaskName, _idGenerator.NewId()));
        }

        private Subtask GetSubtask(Id subtaskId) {
            Subtask subtask = _subtasks
                .FindAll(subtask => subtask.Id.GetIntId() == subtaskId.GetIntId())
                .SingleOrDefault();
            
            if (subtask == null)
                throw new TaskManagerException("Task doesn't exist.");

            return subtask;
        }

        public void CompleteSubtask(Id subtaskId) {
            GetSubtask(subtaskId).Complete();
        }

        public int CompletedSubtasks() {
            return _subtasks.Count(subtask => subtask.IsCompleted);
        }
    }
}