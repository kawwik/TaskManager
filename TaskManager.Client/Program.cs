using System;

namespace TaskManager.Client {
    class Program {
        static void Main(string[] args) {
            new Client().Configure().Run();
        }
    }
}