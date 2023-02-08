using UserManagementSystem;

Console.WriteLine("...........................Welcome..................................\n\n\n");
Thread.Sleep(2000);

var userInterface = new UserConsoleUI();
await userInterface.userSelection();