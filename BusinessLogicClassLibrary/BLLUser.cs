using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicClassLibrary
{
   public enum Roles
    {
        guest,
        Administrator,
        Librarian,
        Patron
    }
    public class BLLUser
    {
        int ID;
        string UserName { get; set; }
        string password { get; set; }
       public Roles Role { get; set; }
      //  BLLUser User = new BLLUser();
        List<BLLUser> bLLUsers = new List<BLLUser>();
    public BLLUser()
        {
            ID = 0;
            UserName = string.Empty;
            password = string.Empty;
            Role =  Roles.guest;
        }
    public BLLUser(int iD, string userName, Roles role,string password)
        {
           this.ID = iD;
            this.UserName = userName;
            this.password = password;
           this.Role = role;
        }
      public void addUser(BLLUser user)
        {
            bLLUsers.Add(user);
            Console.WriteLine("New user added");
        }
        public void addUser(int id,string userName,string role,string password)
        {
            this.Role = Enum.Parse<Roles>(role);
            bLLUsers.Add(new BLLUser(id,userName,this.Role,password));
        }
        public bool userExist(int id)
        {
           return  bLLUsers.Any(p=> p.ID == id);
        }
        public void removeUser(int id)
        {
            foreach (BLLUser user in bLLUsers)
            {

                if (user.ID == id)
                {
                    bLLUsers.Remove(user);
                    Console.WriteLine("User Removed");
                }
                else
                {
                    Console.WriteLine("User does not exist");
                }
            }

        }
        public bool Login(string userName,string password)
        {
            
           bool sucess = bLLUsers.Any(p => p.UserName == userName && p.password == password);

            return sucess;
        }
        public BLLUser getUserInfoForLogin(string userName, string password)
        {
            BLLUser user = bLLUsers.FirstOrDefault(p => p.UserName == userName && p.password == password);
            return user;

        }
        public Roles getUserRole(string userName, string password)
        {
            
            foreach(BLLUser user in bLLUsers)
                if(user.UserName == userName && user.password == password)
                {
                    return user.Role;
                }
            return Roles.guest;
        }
        public void updateUserName (int id,string newUserName)
        {
            foreach(BLLUser user in bLLUsers)
            {
                if(user.ID == id)
                {
                    user.UserName = newUserName;
                    Console.WriteLine("Username update");
                }
                else
                {
                    Console.WriteLine("User does not exist");
                }
            }
           
        }

        public void updatePassword(int id, string password)
        {
            foreach (BLLUser user in bLLUsers)
            {
                if (user.ID == id)
                {
                    user.password = password;
                    Console.WriteLine("Password Updated");
                }
                else
                {
                    Console.WriteLine("User does not exist");
                }
            }

        }
        public void updateRole(int id, string newRole)
        {
            Roles updatedRole = Enum.Parse<Roles>(newRole);

            foreach (BLLUser user in bLLUsers)
            {
                if (user.ID == id)
                {
                    user.Role = updatedRole;
                    Console.WriteLine("User Role updated");
                }
                else
                {
                    Console.WriteLine("User Does not Exist");
                }
            }

        }
        public BLLUser getUser(int id)
        {
            foreach(BLLUser user in bLLUsers)
            {
                if(user.ID == id)
                {
                    return user;
                }
            }
            return new BLLUser();
        }
        public void ValidateRole(string Role)
        {
            bool success = Enum.IsDefined(typeof(Roles), Role);
            if (success)
            {
                Console.WriteLine("Access Granted");
            }
            else
            {
                Console.WriteLine("Access Denied");
            }
        }
        public void printSingleUser(int id)
        {
            foreach (BLLUser user in bLLUsers)
            {
                if (user.ID == id)
                {
                    Console.WriteLine(user.ID);
                    Console.WriteLine(user.UserName);
                    Console.WriteLine(user.Role);
                }
                else
                {
                    Console.WriteLine("User does not Exist");
                }
            }
        }
        public void printAllUsers()
        {
            foreach(BLLUser user in bLLUsers)
            {
                Console.WriteLine(user.ID);
                Console.WriteLine(user.UserName);
                Console.WriteLine(user.Role);
            }
        }
    }
}
