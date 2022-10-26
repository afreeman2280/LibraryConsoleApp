using System;
using System.Reflection.Metadata;
using BusinessLogicClassLibrary;

namespace LibraryConsoleApp
{
    internal class Program
    {
        public static BLLUser newUser = new BLLUser();
        public static BLLBook newBook = new BLLBook();
        public static BLLRole newRole = new BLLRole();
        private static BLLBookInventory newBookInventory = new BLLBookInventory();
        public static int temp = 0;

        static void Main(string[] args)
        {
            bool showMenu = true;
            bool success = true;
            string loginUserName = "Guest";
            string loginRole = "Guest";
            int loginRoleId = 1;

            while (showMenu)
            {
                Console.WriteLine("Hello " + loginUserName + "!");
                Console.WriteLine("Your Role is " + loginRole +"\n"  );
                
                Console.WriteLine("Choose an opton:");
                Console.WriteLine("1) Register");
                Console.WriteLine("2) Login");
                Console.WriteLine("3) Print Users");
                Console.WriteLine("4) Print User");
                Console.WriteLine("5) Update User");
                Console.WriteLine("6) Remove User");
                Console.WriteLine("7) Logout");
                Console.WriteLine("8) Add Book");
                Console.WriteLine("9) Print Books");
                Console.WriteLine("10) Update Book ");
                Console.WriteLine("11) Remove Book");
                Console.WriteLine("12) CheckIn/CheckOut Book");
                Console.WriteLine("13) Exit");

                switch (Console.ReadLine())
                {
                    case "1":
                        registerUser(newUser);
                        break;
                    case "2":
                        Console.WriteLine("Enter UserName");
                        string userName = Console.ReadLine();
                        Console.WriteLine("Enter Password");
                        string password = Console.ReadLine();
                        success = login(newUser, userName, password);
                        if (success)
                        {
                            loginUserName = userName;
                            loginRoleId = newUser.getUserInfoForLogin(userName, password);
                            loginRole = newRole.getRole(loginRoleId);
                            temp = newUser.getID(userName, password);
                            Console.WriteLine("Login Sucessful");
                        }
                        else
                        {
                            Console.WriteLine("Username or Password is incorrect");
                        }
                        break;

                    case "3":
                        if (loginRole.ToLower() == "administrator" || loginRole.ToLower() == "librarian")
                        {
                            printAllUsers(newUser);
                        }
                        else
                        {
                            Console.WriteLine("Only Administrator and Librarians can use this feature");
                        }
                        break;

                    case "4":
                        if (loginRole.ToLower() == "administrator" || loginRole.ToLower() == "librarian" || loginRole.ToLower() == "patron")
                        {
                            int userID;
                            Console.WriteLine("Enter Id");
                            success = int.TryParse(Console.ReadLine(), out userID);
                            if (success)
                            {
                                printSingleUser(userID, newUser);
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
                        if (loginRole.ToLower() == "administrator" || loginRole.ToLower() == "librarian")
                        {
                            updateUser(newUser);
                        }
                        else
                        {
                            Console.WriteLine("Only Administrator and Librarians can use this feature");

                        }
                        break;
                    case "6":
                        int id;
                        if (loginRole.ToLower() == "administrator" || loginRole.ToLower() == "librarian")
                        {
                            Console.WriteLine("Enter Id");
                            success = int.TryParse(Console.ReadLine(), out id);
                            if (success)
                            {
                                removeUser(id, newUser);
                            }
                            else
                            {
                                Console.WriteLine("Invalid Entry");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Only Administrator and Librarians can use this feature");

                        }
                        break;
                    case "7":
                        loginUserName = "Guest";
                        loginRoleId = 1;
                        loginRole = newRole.getRole(loginRoleId);

                        break;
                    case "8":
                        if (loginRole.ToLower() == "administrator" || loginRole.ToLower() == "librarian")
                        {
                            AddBook(newBook);

                        }
                        else
                        {
                            Console.WriteLine("Only Administrator and Librarians can use this feature");

                        }
                        break;
                    case "9":
                        if (loginRole.ToLower() == "administrator" || loginRole.ToLower() == "librarian")
                        {
                        printAllBooks(newBook);

                        }
                        else
                        {
                            Console.WriteLine("Only Administrator and Librarians can use this feature");

                        }
                        break;
                    case "10":
                        if (loginRole.ToLower() == "administrator" || loginRole.ToLower() == "librarian")
                        {
                            updateBook(newBook);

                        }
                        else
                        {
                            Console.WriteLine("Only Administrator and Librarians can use this feature");

                        }
                        break;
                    case "11":
                        if (loginRole.ToLower() == "administrator" || loginRole.ToLower() == "librarian")
                        {
                            Console.WriteLine("Enter Id");
                            success = int.TryParse(Console.ReadLine(), out id);
                            if (success)
                            {
                                removeBook(id, newBook);
                            }
                            else
                            {
                                Console.WriteLine("Not Valid Entry");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Only Administrator and Librarians can use this feature");

                        }
                        break;
                    case "12":
                        if (loginRole.ToLower() == "administrator" || loginRole.ToLower() == "librarian" || loginRole.ToLower() == "patron")
                        {
                            Console.WriteLine("Enter Id");
                            success = int.TryParse(Console.ReadLine(), out id);
                            if (success)
                            {
                                checkInOrOut();

                            }
                            else
                            {
                                Console.WriteLine("Not Valid Entry");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Only Administrator,Patrons and Librarians can use this feature");

                        }
                        break;
                    case "13":
                        showMenu = false;
                        break;
                    default:
                        showMenu = false;
                        break;

                }
            }
        }

        private static void checkInOrOut()
        {
            Console.WriteLine("Do you want to check in or out");
            Console.WriteLine("Please Enter In or Out");
            string option = Console.ReadLine(); 
            if(option.ToLower() == "in")
            {
                Console.WriteLine("Enter Id of the book you want to check in");
                int bookId = int.Parse(Console.ReadLine());
                if (newBookInventory.ExistInInventory(bookId, temp))
                {
                    newBookInventory.checkedInInventory(bookId, temp);

                }
                else
                {
                    Console.WriteLine("You have not checked out this book");
                }

            }
            else if (option.ToLower() == "out")
            {
                printAllBooks(newBook);
                Console.WriteLine("Enter Book Id");
                int bookId =int.Parse(Console.ReadLine());
                if (!newBookInventory.ExistInInventory(bookId, temp))
                {

                   newBookInventory.addBookToInventory(bookId, temp);
                }
                else
                {
                    Console.WriteLine("Already checked out");
                }

            }
            else
            {
                Console.WriteLine("Invalid entry");
            }
        }

        public static void registerUser( BLLUser newUser)
        {
            Console.WriteLine("Enter UserName");
            string userName = Console.ReadLine();
            Console.WriteLine("Enter user's password");
            string password = Console.ReadLine();
            Console.WriteLine("What is the user's role?");
            int userRole = int.Parse(Console.ReadLine());
            newUser.addUser( userName, userRole, password);
        }
        public static void AddBook(BLLBook newBook)
        {
            Console.WriteLine("Enter Book Name");
            string bookName = Console.ReadLine();
            Console.WriteLine("Enter The Author Name");
            string authorName = Console.ReadLine();
            newBook.addBook(bookName, authorName);
        }
        public static bool login(BLLUser user, string userName, string password)
        {

            return user.Login(userName, password);

        }
        public static void printAllBooks(BLLBook book)
        {
            book.printAllBooks();
        }
        public static void printAllUsers(BLLUser user)
        {
            user.printAllUsers();
        }
        public static void removeUser(int id, BLLUser user)
        {
            user.removeUser(id);
        }
        public static void removeBook(int id, BLLBook book)
        {
            book.removeBook(id);
        }
        public static void printSingleUser(int id, BLLUser user)
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
        public static void updateBook(BLLBook book)
        {
            int BookId;
            Console.WriteLine("Enter Book Id");
            bool success = int.TryParse(Console.ReadLine(), out BookId);
            if (success)
            {
                if (book.bookExist(BookId))
                {


                    Console.WriteLine("1)Update Book Name");
                    Console.WriteLine("2)Update Author");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            Console.WriteLine("Enter new Book Name");
                            string newBookName = Console.ReadLine();
                            book.updateBookName(BookId, newBookName);
                            break;
                        case "2":
                            Console.WriteLine("Enter new Author ");
                            string newAuthor = Console.ReadLine();
                            book.updateAuthor(BookId, newAuthor);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Book does not exist");
                }
            }
            else
            {
                Console.WriteLine("Invalid entry");

            }
        }
        }
    }



