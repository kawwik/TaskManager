using System;
using TaskManager.Tools;

namespace TaskManager.Entities {
    public class Subtask {
        public Subtask(string subtaskName) {
            Name = subtaskName;
        }

        public string Name { get; }
        public bool IsCompleted { get; private set; } = false;

        public void Complete() {
            if (IsCompleted)
                throw new TaskManagerException("Subtask is already completed.");
            IsCompleted = true;
        }
    }
}