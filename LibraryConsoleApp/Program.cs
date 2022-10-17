using System;
using System.Reflection.Metadata;
using BusinessLogicClassLibrary;

namespace LibraryConsoleApp
{
    internal class Program
    {
        public static BLLUser newUser = new BLLUser();

        static void Main(string[] args)
        {
            int userID = 1;
            int id;
            bool showMenu = true;
            bool success = true;
            int loginUserID;
            string loginUserName = "Guest";
            Roles loginRole = Roles.guest;
            
            while (showMenu)
            {
                Console.WriteLine("Hello " + loginUserName + "!");
                Console.WriteLine("Your Role is " + loginRole);

                Console.WriteLine("Choose an opton:");
                Console.WriteLine("1) Register");
                Console.WriteLine("2) Login");
                Console.WriteLine("3) Print Users");
                Console.WriteLine("4) Print User");
                Console.WriteLine("5) Update User");
                Console.WriteLine("6) Remove User");
                Console.WriteLine("7) Logout");
                Console.WriteLine("8) Exit");

                switch (Console.ReadLine())
                {
                    case "1":
                        registerUser(userID,newUser);
                        userID++;
                        break;
                    case "2":
                        Console.WriteLine("Enter UserName");
                        string userName = Console.ReadLine();
                        Console.WriteLine("Enter Password");
                        string password = Console.ReadLine();
                        success = login(newUser,userName,password);
                        if(success)
                        {
                            loginUserName = userName;
                            loginRole = newUser.getUserRole(userName,password);
                        }
                        break;

                    case "3":
                        if (loginRole == Roles.Administrator || loginRole == Roles.Librarian)
                        {
                            printAllUsers(newUser);
                        }
                        else
                        {
                            Console.WriteLine("Only Administrator and Librarians can use this feature");
                        }
                        break;

                    case "4":
                        if (loginRole == Roles.Administrator || loginRole == Roles.Librarian || loginRole == Roles.Patron)
                        {

                            Console.WriteLine("Enter Id");
                            success = int.TryParse(Console.ReadLine(), out id);
                            if (success)
                            {
                                printSingleUser(id, newUser);
                            }
                            else
                            {
                                Console.WriteLine("Invalid entry");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Guest can not use this feature");
                        }
                        break;
                    case "5":
                        if (loginRole == Roles.Administrator || loginRole == Roles.Librarian)
                        {
                            updateUser(newUser);
                        }
                        else
                        {
                            Console.WriteLine("Only Administrator and Librarians can use this feature");

                        }
                        break;
                    case "6":
                        if (loginRole == Roles.Administrator || loginRole == Roles.Librarian)
                        {
                            Console.WriteLine("Enter Id");
                            success = int.TryParse(Console.ReadLine(), out id);
                            if (success)
                            {
                                removeUser(id, newUser);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Only Administrator and Librarians can use this feature");

                        }
                        break;
                    case "7":
                        loginUserName = "Guest";
                        loginRole = Roles.guest;
                        break;
                    case "8":
                        showMenu = false;
                        break;
                    default:
                        showMenu = false;
                        break;

                }
            }
        }
       public static void registerUser(int userID,BLLUser newUser)
        {
            Console.WriteLine("Enter UserName");
            string userName = Console.ReadLine();
            Console.WriteLine("Enter user's password");
            string password = Console.ReadLine();
            Console.WriteLine("What is the user's role?");
            string userRole = Console.ReadLine();
            newUser.addUser(userID,userName,userRole,password);
            newUser.printAllUsers();
        }
        public static bool login(BLLUser user,string userName,string password)
        {
            
            return user.Login(userName,password);

        }
        public static void printAllUsers(BLLUser user)
        {
            user.printAllUsers();
        }
        public static void removeUser(int id, BLLUser user)
        {
            user.removeUser(id);
        }
        public static void printSingleUser(int id,BLLUser user)
        {
            user.printSingleUser(id);
        }
        

        public static void updateUser(BLLUser user)
        {
            int userId;
            Console.WriteLine("Enter User Id");
            bool success = int.TryParse(Console.ReadLine(), out userId);
            if (success)
            {
                if (user.userExist(userId))
                {


                    Console.WriteLine("1)Update Username");
                    Console.WriteLine("2)Update Password");
                    Console.WriteLine("3)Update Role");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            Console.WriteLine("Enter new UserName");
                            string newUserName = Console.ReadLine();
                            user.updateUserName(userId, newUserName);
                            break;
                        case "2":
                            Console.WriteLine("Enter new User password");
                            string newPassword = Console.ReadLine();
                            user.updatePassword(userId, newPassword);
                            break;
                        case "3":
                            Console.WriteLine("Enter new Role");
                            string newRole = Console.ReadLine();
                            user.updateRole(userId, newRole);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("User does not exist");
                }
            }
            else
            {
                Console.WriteLine("Invalid entry");

                }
            }
        }
    }



