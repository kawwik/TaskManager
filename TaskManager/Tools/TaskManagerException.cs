using System;
using System.Runtime.Serialization;

namespace TaskManager.Tools {
    [Serializable]
    public class TaskManagerException : Exception {
        public TaskManagerException() { }
        public TaskManagerException(string message) : base(message) { }
        public TaskManagerException(string message, Exception inner) : base(message, inner) { }

        protected TaskManagerException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}