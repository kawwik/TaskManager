using Spectre.Console;

namespace TaskManager.Client {
    public interface IUserInterface {
        void ShowMessage(params object[] args);
        string ReadLine();
        void ShowTable(Table table);
        void ShowTree(Tree tree);
    }
}