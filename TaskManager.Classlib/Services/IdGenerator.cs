namespace TaskManager.Services {
    public class IdGenerator {
        private int _currentId;

        public IdGenerator(int startId) {
            _currentId = startId;
        }

        public Id NewId() {
            return new Id(_currentId++);
        }
    }
}