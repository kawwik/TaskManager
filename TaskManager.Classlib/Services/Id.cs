namespace TaskManager.Services {
    public class Id {
        private readonly int _id;

        public Id(int taskId) {
            _id = taskId;
        }

        public int GetIntId() {
            return _id;
        }
    }
}