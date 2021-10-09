using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TaskManager.Services;
using TaskManager.Tools;

namespace TaskManager.Entities {
    public class Task {
        private readonly List<Subtask> _subtasks = new List<Subtask>();

        public Task(string taskName, Deadline deadline) {
            Name = taskName;
            Id = TaskId.NewId();
            Deadline = deadline;
        }

        public string Name { get; }
        public TaskId Id { get; }
        public Deadline Deadline { get; }
        public bool IsCompleted { get; private set; } = false;
        public ReadOnlyCollection<Subtask> Subtasks => _subtasks.AsReadOnly();

        public void Complete() {
            if (IsCompleted)
                throw new TaskManagerException("Subtask is already completed.");
            IsCompleted = true;
        }

        public void AddSubtask(string subtaskName) {
            _subtasks.Add(new Subtask(subtaskName));
        }

        public int CompletedSubtasks() {
            return _subtasks.Count(subtask => subtask.IsCompleted);
        }
    }
}