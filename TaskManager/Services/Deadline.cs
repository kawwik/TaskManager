using System;

namespace TaskManager.Services {
    public class Deadline {
        public Deadline(DateTime date) {
            Date = date;
        }

        public DateTime Date { get; }

        public bool ExpiresToday() {
            return Date.Day == DateTime.Today.Day;
        }
    }
}