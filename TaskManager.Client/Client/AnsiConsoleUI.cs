using System;
using Spectre.Console;

namespace TaskManager.Client {
    public class AnsiConsoleUI : IUserInterface {
        public void ShowMessage(params object[] args) {
            foreach (var arg in args) {
                AnsiConsole.WriteLine(arg.ToString());
            }
        }

        public void ShowError(params object[] args) {
            foreach (var arg in args) {
                AnsiConsole.Markup($"[red]{arg}[/]");
            }
        }

        public string ReadLine() {
            return Console.ReadLine();
        }

        public void ShowTable(Table table) {
            AnsiConsole.Render(table);
        }

        public void ShowTree(Tree tree) {
            AnsiConsole.Render(tree);
        }
    }
}