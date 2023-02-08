using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementSystem
{
    public class UserConsoleUI
    {
       public async Task userSelection() 
        {

            Console.WriteLine("Press [1] to 'Create a new user' \nPress [2] to 'Update' information about an existing user\n" +
                "Press [3] to 'Get User By ID'\nPress [4] to 'Get all user Data' \nPress [5] to 'Delete' an existing user \n\n\n");
        
           User userVar = new User();
            var userID = userVar.Id;
            var userSelect = new UserRepository();
            var selection = Console.ReadKey(intercept: true);
            switch (selection.KeyChar)
            {
                case '1':
                    Thread.Sleep(1000);
                    Console.Write("\n\nLoading......................................................\n\n");
                    Thread.Sleep(500);
                    userSelect.CreateUser(userVar);
                    break;
                case '2':
                    Thread.Sleep(1000);
                    Console.Write("\n\nLoading......................................................\n\n");
                    Thread.Sleep(500);
                    userSelect.UpdateUser(userVar);
                    break;
                case '3':
                    Thread.Sleep(1000);
                    Console.Write("\n\nLoading......................................................\n\n");
                    Thread.Sleep(500);
                    await userSelect.GetUserByID(userID);
                    break;
                case '4':
                    Thread.Sleep(1000);
                    Console.Write("\n\nLoading......................................................\n\n");
                    Thread.Sleep(500);
                    await userSelect.GetAllUsers(userVar);
                    break;
                case '5':
                    Thread.Sleep(1000);
                    Console.Write("\n\nLoading......................................................\n\n");
                    Thread.Sleep(500);
                    await userSelect.DeleteUser(userVar);
                    break;

                default:
                    Console.WriteLine("\n\n\nPlease enter a valid selection");
                    return;
                    


            }
        }
    }
}
