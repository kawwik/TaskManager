using System;
using System.Data;
using System.Linq;
using NUnit.Framework;
using TaskManager.Entities;
using TaskManager.Services;
using TaskManager.Tools;

namespace TaskManager.Tests {
    public class Tests {
        private ITaskManager _taskManager;
        
        [SetUp]
        public void Setup() {
            _taskManager = new TaskManager.Services.TaskManager();
        }

        [Test]
        public void AddTask_ManagerContainsTask() {
            const string taskName = "taskName";
            var taskId = new Id(1);
            var deadline = new Deadline(new DateTime(2010, 10, 10));
            
            _taskManager.AddTask(new Task(taskName, deadline, taskId));
            Assert.IsNotNull(_taskManager.FindTask(taskName));
        }

        [Test]
        public void TryAddTaskTwice_ExceptionThrown() {
            const string taskName = "taskName";
            var taskId1 = new Id(1);
            var taskId2 = new Id(2);
            var deadline = new Deadline(new DateTime(2010, 10, 10));
            
            _taskManager.AddTask(new Task(taskName, deadline, taskId1));
            Assert.Catch<TaskManagerException>(() => {
                _taskManager.AddTask(new Task(taskName, deadline, taskId2));
            });
        }

        [Test]
        public void AddTaskToGroup_GroupContainsTask() {
            const string taskName = "taskName";
            const string groupName = "groupName";
            var taskId = new Id(1);
            var deadline = new Deadline(new DateTime(2010, 10, 10));
            
            _taskManager.AddTask(new Task(taskName, deadline, taskId));
            _taskManager.CreateGroup(groupName);
            _taskManager.AddToGroup(taskId, groupName);
            Assert.IsNotNull(_taskManager.GetGroup(groupName).FindTask(taskName));
        }

        [Test]
        public void DeleteTaskFromGroup_GroupDoesntContainTaskAndManagerContainsTask() {
            const string taskName = "taskName";
            const string groupName = "groupName";
            var taskId = new Id(1);
            var deadline = new Deadline(new DateTime(2010, 10, 10));
            
            _taskManager.AddTask(new Task(taskName, deadline, taskId));
            _taskManager.CreateGroup(groupName);
            _taskManager.AddToGroup(taskId, groupName);
            
            _taskManager.DeleteFromGroup(taskId, groupName);
            Assert.IsNull(_taskManager.GetGroup(groupName).FindTask(taskName));
            Assert.IsNotNull(_taskManager.FindTask(taskName));
        }

        [Test]
        public void DeleteGroup_GroupDoesntExistAndTaskAreStillInManager() {
            const string taskName = "taskName";
            const string groupName = "groupName";
            var taskId = new Id(1);
            var deadline = new Deadline(new DateTime(2010, 10, 10));
            
            _taskManager.AddTask(new Task(taskName, deadline, taskId));
            _taskManager.CreateGroup(groupName);
            _taskManager.AddToGroup(taskId, groupName);
            
            _taskManager.DeleteGroup(groupName);
            
            //If we catch an exception, group doesnt exist.
            Assert.Catch<TaskManagerException>(() => {
                _taskManager.GetGroup(groupName);
            });
            Assert.IsNotNull(_taskManager.FindTask(taskName));
        }

        [Test]
        public void AddSubtask_SubtaskIsInTask() {
            const string taskName = "taskName";
            const string subTaskName = "subTaskName";
            var taskId = new Id(1);
            var deadline = new Deadline(new DateTime(2010, 10, 10));

            var task = new Task(taskName, deadline, taskId);
            task.AddSubtask(subTaskName);
            
            Assert.IsNotNull(task.Subtasks.SingleOrDefault(subtask => subtask.Name == subTaskName));
        }
        
    }
}