using NUnit.Framework;

namespace TaskManager.Tests {
    public class ShopManagerTests {
        private ITaskManager _taskManager;

        [SetUp]
        public void Setup() {
            _taskManager = new Services.TaskManager();
        }

        [Test]
        public void Test1() {
            Assert.Pass();
        }
    }
}