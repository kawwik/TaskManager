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

        public int GetIntId() {
            return _id;
        }
    }
}