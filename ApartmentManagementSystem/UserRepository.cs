using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UserManagementSystem
{/// <summary>
/// This UserRepository class inherits from IUser and IUserUpdate Interface.
/// It contains methods such as: 
/// [1]."CreateUser(User user) :- used to create a new user," 
/// [2]."GetAllUsers(User user) :- used to get all the users from the database. 
///     Depending on the user input, this method also allows user to filter their search by getting a specific set of users"
/// [3]."GetUserByID(int id) :- used to fetch user with their IDs"
/// [4]."UpdateUser(User user) :- used to update details of a user from the database"
/// [5]."DeleteUser(User user) :- used to delete the details of a user from the database"
/// It also contains methods which allows user to update specific parameters of a user.
/// </summary>
    public class UserRepository : IUser, IUserUpdate
    {
        //This Method Creates user
        public async void CreateUser(User user)
        {
            //var optionsbuilder = new DbContextOptionsBuilder<ApartmentDBContext>().UseSqlServer().Options;
            //optionsbuilder.UseSqlServer();
            //var options = optionsbuilder.Options;

            using (var dbcontext = new UserManagementDBContext())
            {
                string? FirstName;
                try
                {
                    Console.Write("\n\nFirstName: ");

                    FirstName = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(FirstName) || int.TryParse(FirstName, out int result))
                    {
                        throw new Exception("\nPlease enter a valid name");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }

                //-----------------------------------------------------------------------------------------------------

                string? LastName;
                try
                {
                    Console.Write("\nLastName: ");
                    LastName = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(LastName) || int.TryParse(LastName, out int result))
                    {
                        throw new Exception("\nPlease enter a valid named");

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }

                //-----------------------------------------------------------------------------------------------------
                Console.Write("\nEmail: ");

                string? Email = Console.ReadLine();
                



                //-----------------------------------------------------------------------------------------------------
                string? dob;
                DateTime date;

                try
                {
                    Console.Write("\nDate Of Birth (mm/dd/yyyy): ");
                    dob = Console.ReadLine();
                    if (!DateTime.TryParse(dob, out date))
                    {
                        throw new Exception("\nPlease enter a valid date format");
                    }
                    int age = (DateTime.Now - date).Days / 365;
                    if (age < 18)
                    {
                        throw new Exception("You have to be at least 18yrs");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
                //-----------------------------------------------------------------------------------------------------
                //string? phone;
                //int number;
                //try
                //{
                //    Console.Write("\nPhone Number: ");
                //    phone = Console.ReadLine();
                //    if (int.TryParse(phone, out number))
                //    {
                //        throw new Exception("\nPlease enter a valid Phone Number");
                //    }
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.Message);
                //    return;
                //}

                string phone;
                try
                {
                    Console.Write("\nPhone Number: ");
                    phone = Console.ReadLine();
                    if (!Regex.IsMatch(phone, @"^\+?\d+$"))
                    {
                        throw new Exception("\nPlease enter a valid Phone Number");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
                //-----------------------------------------------------------------------------------------------------
                string? address;
                try
                {
                    Console.Write("\nAddress: ");
                    address = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(address))
                    {
                        throw new Exception("Please enter a valid address");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }


                //-----------------------------------------------------------------------------------------------------
                try
                {
                    User createUser = new()
                    {

                        Name = FirstName + " " + LastName,
                        Email = Email,
                        Address = address,
                        DateOfBirth = date,
                        Phone = phone,


                    };
                    if (user.Name == null)
                    {
                        dbcontext.Users.Add(createUser);
                        dbcontext.SaveChanges();
                        Thread.Sleep(1000);
                        Console.WriteLine("Successfull");
                        Console.WriteLine(string.Format("ID: {0}\nName: {1}\nEmail: {2}\nAddress: {3}\nDate Of Birth: {4}\nPhone: {5}", createUser.Id, createUser.Name, createUser.Email, createUser.Address, createUser.DateOfBirth, createUser.Phone));

                    }
                    
                    else
                    {
                        Thread.Sleep(700);
                        Console.WriteLine("\n\nLoading................................................");
                        Thread.Sleep(600);
                        Console.Write("\n\n This User exists already");
                        var backup = new UserConsoleUI();
                       await backup.userSelection();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }
            };
        }

//---------------------------------------------------------------------------------------------------------
        public async Task<IEnumerable<User>> GetAllUsers(User user)
        {
            try
            {
                using (var db = new UserManagementDBContext())
                {
                    Console.Write("Enter user name to search for: ");
                    var userInput = Console.ReadLine();

                    var searchedUsers = db.Users.Where(u => u.Name.Contains(userInput) || u.Email.Contains(userInput) || u.Id.Equals(userInput));//.DefaultIfEmpty().ToListAsync();

                    if (searchedUsers.Any())
                    {
                        Console.Write("Users matching the search criteria: ");
                        foreach (var search in searchedUsers)
                        {
                            Console.WriteLine("ID: {0}\nName: {1}\nEmail: {2}\nDate Of Birth: {3}\nAddress: {4}", search.Id, search.Name, search.Email, search.Address, search.DateOfBirth);
                           
                        }

                    }
                    else
                    {
                        Console.WriteLine("\n\nNo users matching the search criteria were found.");
                    }

                    //Console.Write("\nAll users in the database: ");
                    //var allUsers = db.Users.ToList();
                    //foreach (var all in allUsers)
                    //{
                    //    Console.WriteLine("ID: {0}\nName: {1}\nEmail: {2}\nAddress: {3}\nDate Of Birth: {4}", all.Id, all.Name, all.Email, all.Address, all.DateOfBirth);
                       
                    //}
                    return searchedUsers.ToList();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured: {0}", ex.Message);
                return await GetAllUsers(user);
            }
            
            //using (var db = new ApartmentDBContext())
            //{
            //    Console.WriteLine("Enter the name of the user: ");
            //    var userNameInput = Console.ReadLine();

            //    return await db.Users.ToListAsync();
            //    //return await db.Users.Where(user => user.Name == userNameInput).ToListAsync();
        }
//----------------------------------------------------------------------------------------------------------
        public async Task<User?> GetUserByID(int id)
        {
            try
            {
                using (var db = new UserManagementDBContext())
                {
                    Console.Write("Enter the ID of the user: ");
                    var userIDInput = int.Parse(Console.ReadLine());

                    var getUser = await db.Users.FirstOrDefaultAsync(getUser => getUser.Id == userIDInput);
                    if (getUser != null)
                    {
                        Console.WriteLine("ID: {0}\nName: {1}\nEmail: {2}\nAddress: {3}\nDate Of Birth: {4}\nPhone Number: {5}", getUser.Id, getUser.Name, getUser.Email, getUser.Address, getUser.DateOfBirth, getUser.Phone);
                       
                    }
                    else
                    {
                        Console.WriteLine(" No ID matches your input");
                        return null;
                    }
                    return getUser;
                }
               
            }
            catch 
            {
                Console.WriteLine("Please input a correct value\n\n");
                Thread.Sleep(1000);
                return await GetUserByID(id);
            }
        }
        //-------------------------------------------------------------------------------------------------------
        public async void UpdateUser(User user)
        {
            Console.Write("\nPress [1] to update user 'Name'" +
                "\nPress [2] to update user 'Date Of Birth' \nPress [3] to update user 'E-mail'\n" +
                "Press [4] to update user 'Address'\nPress [5] to Update user 'Phone Number'\n" +
                "Press [6] to update all user data\n\n");

            var input = Console.ReadKey(intercept: true);
            switch (input.KeyChar)
            {
                case '1':
                    UpdateName(user);
                    break;
                case '2':
                    UpdateDateOfBirth(user);
                    break;
                case '3':
                    UpdateEmail(user);
                    break;
                case '4':
                    UpdateAddress(user);
                    break;
                case '5':
                    UpdatePhoneNumber(user);
                    break;
                case '6':
                    UpdateAll(user);
                         break;
                default:
                    Console.WriteLine("\nInvalid option. Please try again.");
                    break;
            }
            
        }
        //------------------------------------------------------------------------------------------------------
        public async Task<string> DeleteUser(User user)
        {
            using var db = new UserManagementDBContext();
            try
            {
                Console.WriteLine("Enter the ID of the user you want to delete: ");
                var userIDInput = int.Parse(Console.ReadLine());

                var userToDelete = await db.Users.FirstOrDefaultAsync(user => user.Id == userIDInput);
                if (userToDelete != null)
                {
                    db.Users.Remove(userToDelete);
                    await db.SaveChangesAsync();
                    return "User successfully deleted";
                }
                else
                {
                    throw new Exception("User does not exist");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "";
        }
        //---------------------------------------------------------------------------------------------------


        public async void UpdateName(User user)
        {
            string? FirstName;
            string? LastName;
            try
            {
                Console.Write("\n\nFirstName: ");
                Console.WriteLine("\nLastName: ");

                FirstName = Console.ReadLine();
                LastName = Console.ReadLine();



                if (string.IsNullOrWhiteSpace(FirstName)  || int.TryParse(FirstName, out int result))
                {
                    if(string.IsNullOrEmpty(LastName) || int.TryParse(LastName, out result))
                    {

                        throw new Exception("\nPlease enter a valid name");
                    }
                }
                
                Thread.Sleep(1000);
                Console.WriteLine("Successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            using var db = new UserManagementDBContext();
            var updateUser = await db.Users.FirstOrDefaultAsync(update => update.Id == user.Id);
            if (updateUser != null)
            {
                user.Name = FirstName + " " + LastName;
                db.Users.Update(user);
                await db.SaveChangesAsync();
            }
            else
            {
                Console.Write("\nThis User doesn't exist");
            }


        }

        

        public async void UpdatePhoneNumber(User user)
        {
            using var db = new UserManagementDBContext();
            string? phone;
            
            try
            {
                Console.Write("\nPhone Number: ");
                phone = Console.ReadLine();
                if (!Regex.IsMatch(phone, @"^\+?\d+$"))
                {
                    throw new Exception("\nPlease enter a valid Phone Number");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            //try
            //{
            //    Console.Write("\nPhone Number: ");
            //    phone = Console.ReadLine();
            //    if (!int.TryParse(phone, out int number))
            //    {
            //        throw new Exception("\nPlease enter a valid Phone Number");
            //    }
            //    Thread.Sleep(1000);
            //    Console.WriteLine("Successful");
            //}

            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    return;
            //}

            var updateUser = await db.Users.FirstOrDefaultAsync(update => update.Id == user.Id);
            if (updateUser != null)
            {
                user.Phone = phone;
                db.Users.Update(user);
                await db.SaveChangesAsync();
            }
            else
            {
                Console.Write("\nThis User doesn't exist");
            }
        }

        public async void UpdateEmail(User user)
        {
            using var db = new UserManagementDBContext();
            Console.Write("\nEmail: ");
            string? Email = Console.ReadLine();

            
            var updateUser = await db.Users.FirstOrDefaultAsync(update => update.Id == user.Id);
            if (updateUser != null)
            {
                user.Email = Email;
                db.Users.Update(user);
                await db.SaveChangesAsync();
            }
            else
            {
                Console.Write("\nThis User doesn't exist");
            }
        }

        public async void UpdateAddress(User user)
        {
            using var db = new UserManagementDBContext();
            string? address;
            try
            {
                Console.Write("\nAddress: ");
                address = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(address))
                {
                    throw new Exception("\nPlease enter a valid address");
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            
            var updateUser = await db.Users.FirstOrDefaultAsync(update => update.Id == user.Id);
            if (updateUser != null)
            {
                user.Address = address;
                db.Users.Update(user);
                await db.SaveChangesAsync();
            }
            else
            {
                Console.Write("\nThis User doesn't exist");
            }
        }

        public async void UpdateDateOfBirth(User user)
        {
            using var db = new UserManagementDBContext();
            string? dob;
            DateTime date;

            try
            {
                Console.Write("\nDate Of Birth (mm/dd/yyyy): ");
                dob = Console.ReadLine();
                if (!DateTime.TryParse(dob, out date))
                {
                    throw new Exception("\nPlease enter a valid date format");
                }
                int age = (DateTime.Now - date).Days / 365;
                if (age < 18)
                {
                    Thread.Sleep(300);
                    throw new Exception("\nYou have to be at least 18yrs");
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
           
            var updateUser = await db.Users.FirstOrDefaultAsync(update => update.Id == user.Id);
            if (updateUser != null)
            {
                user.DateOfBirth = date;
                db.Users.Update(user);
                await db.SaveChangesAsync();
            }
            else
            {
                Console.Write("\nThis User doesn't exist");
            }

        }

        public async void UpdateAll(User user)
        {
            using (var dbcontext = new UserManagementDBContext())
            {
                string? FirstName;
                try
                {
                    Console.Write("\n\nFirstName: ");

                    FirstName = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(FirstName) || int.TryParse(FirstName, out int result))
                    {
                        throw new Exception("\nPlease enter a valid name");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
                string? LastName;
                try
                {
                    Console.Write("\nLastName: ");
                    LastName = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(LastName) || int.TryParse(LastName, out int result))
                    {
                        throw new Exception("\nPlease enter a valid named");

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }

                //-----------------------------------------------------------------------------------------------------
                Console.WriteLine("\nEmail: ");
                string email = Console.ReadLine();


                //string? email;
                //try
                //{
                //    Console.Write("\nEmail: ");
                //    email = Console.ReadLine();
                //    if (Regex.IsMatch(email, @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$"))
                //    {
                //        Console.WriteLine("\nPlease enter a valid Email");
                //        return;
                //    }
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.Message);
                //}



                //-----------------------------------------------------------------------------------------------------
                string? dob;
                DateTime date;

                try
                {
                    Console.Write("\nDate Of Birth (mm/dd/yyyy): ");
                    dob = Console.ReadLine();
                    if (!DateTime.TryParse(dob, out date))
                    {
                        throw new Exception("\nPlease enter a valid date format");
                    }
                    int age = (DateTime.Now - date).Days / 365;
                    if (age < 18)
                    {
                        throw new Exception("You have to be at least 18yrs");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
                //-----------------------------------------------------------------------------------------------------
                string? phone;
                
                try
                {
                    Console.Write("\nPhone Number: ");
                    phone = Console.ReadLine();
                    if (!Regex.IsMatch(phone, @"^\+?\d+$"))
                    {
                        throw new Exception("\nPlease enter a valid Phone Number");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }



                //int number;
                //try
                //{
                //    Console.Write("\nPhone Number: ");
                //    phone = Console.ReadLine();
                //    if (!int.TryParse(phone, out number))
                //    {
                //        throw new Exception("\nPlease enter a valid Phone Number");
                //    }
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.Message);
                //    return;
                //}
                //-----------------------------------------------------------------------------------------------------
                string? address;
                try
                {
                    Console.Write("\nAddress: ");
                    address = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(address))
                    {
                        throw new Exception("Please enter a valid address");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }

                using var db = new UserManagementDBContext();
                var updateUser = await db.Users.FirstOrDefaultAsync(update => update.Id == user.Id);
                if (updateUser != null)
                {
                    updateUser.Name = FirstName + " " + LastName;
                    updateUser.Address = address;
                    updateUser.Phone = phone;
                    updateUser.Email = email;
                    updateUser.DateOfBirth = date;


                    db.Users.Update(user);
                    await db.SaveChangesAsync();
                    Thread.Sleep(1000);
                    Console.WriteLine("\nSuccessful");
                }
                else
                {
                    Thread.Sleep(1000);
                    Console.Write("\nThis User doesn't exist");
                }

            }
        }
    }
}
