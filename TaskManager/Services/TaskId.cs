namespace TaskManager.Services {
    public class TaskId {
        private static int _currentId = 1;
        private readonly int _id;

        public TaskId(int taskId) {
            _id = taskId;
        }

        internal static TaskId NewId() {
            return new TaskId(_currentId++);
        }

        public static bool operator ==(TaskId id1, TaskId id2) {
            return id1 == id2;
        }
        
        public static bool operator !=(TaskId id1, TaskId id2) {
            return id1 != id2;
        }

        public int GetIntId() {
            return _id;
        }
    }
}