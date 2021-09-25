using System;
using Spectre.Console;

namespace TaskManager.Client {
    public class AnsiConsoleUI : IUserInterface {
        public void ShowMessage(params object[] args) {
            throw new NotImplementedException();
        }

        public string ReadLine() {
            throw new NotImplementedException();
        }

        public void ShowTable(Table table) {
            throw new NotImplementedException();
        }

        public void ShowTree(Tree tree) {
            throw new NotImplementedException();
        }
    }
}